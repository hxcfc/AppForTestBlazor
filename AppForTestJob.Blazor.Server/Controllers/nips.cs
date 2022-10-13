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
using System.Linq;
using System.Collections.Generic;
using Entity = AppForTestJob.Blazor.Server.DBModels.Entity;

namespace AppForTestJob.Blazor.Server.Controllers
{
    [Route("api/search/[controller]")]
    [ApiController]
    public class nips : ControllerBase
    {
        IObjectSpaceFactory objectSpaceFactory;
        public nips(IObjectSpaceFactory objectSpaceFactory)
        {
            this.objectSpaceFactory = objectSpaceFactory;
        }
        [HttpGet("{nips}")]
        public ActionResult<IEnumerable<Entity>> GetNips([FromRoute] string nips, [FromQuery] DateTime date)
        {
            try
            {
                if (date.ToString("d") == DateTime.Now.ToString("d"))
                {


                    if (!string.IsNullOrEmpty(nips))
                    {
                        using (var context = new TestDbContext())
                        {
                            if (nips.Contains(','))
                            {
                                List<string> result = nips.Split(new char[] { ',' }).ToList();
                                List<Entity> entities = new List<Entity>();
                                foreach (var oneNip in result)
                                {
                                    var data = context.Entities.Where(d => d.Nip == oneNip).FirstOrDefault();
                                    if (data != null)
                                    {
                                        data.AccountNumbers = context.AccountNumbers.Where(d => d.EntityId == data.EntityId).ToList();
                                        data.AuthorizedClerks = context.AuthorizedClerks.Where(d => d.EntityId == data.EntityId).ToList();
                                        data.Partners = context.Partners.Where(d => d.EntityId == data.EntityId).ToList();
                                        data.Representatives = context.Representatives.Where(d => d.EntityId == data.EntityId).ToList();
                                        entities.Add(data);
                                    }

                                }
                                return Ok(entities);

                            }
                            else
                            {
                                return BadRequest("You must use more than one NIP. Use , between nips.");
                            }
                        }
                    }
                    else
                    {
                        return BadRequest("Nip cant be empty.");
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