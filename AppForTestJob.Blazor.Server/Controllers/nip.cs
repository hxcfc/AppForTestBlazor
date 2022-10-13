using DevExpress.ExpressApp.Core;
using DevExpress.ExpressApp;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DevExpress.Xpo;
using DevExpress.XtraRichEdit.API.Layout;
using System.Net.NetworkInformation;
using DevExpress.Persistent.BaseImpl;
using AppForTestJob.Module.BusinessObjects;
using AppForTestJob.Blazor.Server.DBModels;
using DevExpress.ClipboardSource.SpreadsheetML;
using AccountNumber = AppForTestJob.Blazor.Server.DBModels.AccountNumber;

namespace AppForTestJob.Blazor.Server.Controllers
{
    [Route("api/search/[controller]")]
    [ApiController]
    public class nip : ControllerBase
    {
        IObjectSpaceFactory objectSpaceFactory;
        public nip(IObjectSpaceFactory objectSpaceFactory)
        {
            this.objectSpaceFactory = objectSpaceFactory;
        }
        [HttpGet("{nip}")]
        public ActionResult GetNip([FromRoute] string nip, [FromQuery] DateTime date)
        {
            try
            {
                if (date.ToString("d") == DateTime.Now.ToString("d"))
                {
                    using (var context = new TestDbContext())
                    {
                        var data = context.Entities.Where(d=>d.Nip == nip).ToList();
                        if (data.Count() > 0)
                        {
                            foreach(var item in data)
                            {
                                item.AccountNumbers = context.AccountNumbers.Where(d=>d.EntityId == item.EntityId).ToList();
                                item.AuthorizedClerks = context.AuthorizedClerks.Where(d=>d.EntityId == item.EntityId).ToList();
                                item.Partners = context.Partners.Where(d=>d.EntityId == item.EntityId).ToList();
                                item.Representatives = context.Representatives.Where(d=>d.EntityId == item.EntityId).ToList();
                            }
                            return Ok(data);
                        }
                        else
                        {
                            return BadRequest("No one has that NIP.");
                        }

                    }
                }
                else
                {
                    return BadRequest("Only today's date avaible.");
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}