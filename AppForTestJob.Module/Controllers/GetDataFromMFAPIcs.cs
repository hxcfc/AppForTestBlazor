using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppForTestJob.Module.BusinessObjects;
using System.Security.AccessControl;
using DevExpress.XtraRichEdit.Model;
using Newtonsoft.Json;
using Microsoft.JSInterop;
using Microsoft.CodeAnalysis;
using System.Net;

namespace AppForTestJob.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class GetDataFromMFAPIcs : ViewController
    {
        private SimpleAction getDataFromApi;

        public GetDataFromMFAPIcs()
        {
            this.TargetObjectType = typeof(Entity);
            TargetViewType = ViewType.DetailView;
            InitializeComponent();


            getDataFromApi = new SimpleAction(this, "Get data from API from NIP", PredefinedCategory.View)
            {
                ImageName = "ReorderTabs",
                SelectionDependencyType = SelectionDependencyType.Independent,
            };

            getDataFromApi.Execute += getDataFromApi_execute;

        }


        private async void getDataFromApi_execute(object sender, SimpleActionExecuteEventArgs e)
        {
            try
            {
                Entity objectOnView = (Entity)e.CurrentObject;
                HttpClient httpClient = new HttpClient();

                // string adressHttp = $"https://localhost:44318/api/search/nip/"; 
                // string adressHttp = $"https://wl-test.mf.gov.pl/api/search/nip/";
                string adressHttp = $"https://wl-api.mf.gov.pl/api/search/nip/";
                string niptoUri = objectOnView.Nip;
                string dateToUri = "?date=2022-10-13";
                var jsonFromApi = await httpClient.GetStringAsync(adressHttp + niptoUri + dateToUri);
                var myobjList = JsonConvert.DeserializeObject<ResultApi>(FixApiResponseString(jsonFromApi));

                ResultApi myObj = myobjList;
                Entity FromApi = myObj.Result.Subject;
                if (myObj.Result.Subject is not null)
                {
                    objectOnView.Name = FromApi.Name;
                    objectOnView.StatusVat = FromApi.StatusVat;
                    objectOnView.Regon = FromApi.Regon;
                    objectOnView.Pesel = FromApi.Pesel;
                    objectOnView.Krs = FromApi.Krs;
                    objectOnView.ResidenceAddress = FromApi.ResidenceAddress;
                    objectOnView.WorkingAddress = FromApi.WorkingAddress;
                    objectOnView.RegistrationLegalDate = FromApi.RegistrationLegalDate;
                    objectOnView.RegistrationDenialDate = FromApi.RegistrationDenialDate;
                    objectOnView.RestorationDate = FromApi.RestorationDate;
                    objectOnView.RestorationBasis = FromApi.RestorationBasis;
                    objectOnView.RemovalDate = FromApi.RemovalDate;
                    objectOnView.RemovalBasis = FromApi.RemovalBasis;
                    objectOnView.HasVirtualAccounts = FromApi.HasVirtualAccounts;
                    View.Refresh();
                    Application.ShowViewStrategy.ShowMessage("Found a data!", InformationType.Success, 3000, InformationPosition.Top);
                }
                else
                {
                    Application.ShowViewStrategy.ShowMessage("Can't find company with that nip.", InformationType.Info, 3000, InformationPosition.Top);
                }

            }
            catch (WebException ex) when (ex.Response is HttpWebResponse response)
            {
                Application.ShowViewStrategy.ShowMessage("Can't get data from API. Error code: " + response.StatusCode.ToString(), InformationType.Error, 3000, InformationPosition.Top);

            }
            catch (Exception error)
            {
                Application.ShowViewStrategy.ShowMessage("Can't get data. Error code: " + error.Message.ToString(), InformationType.Error, 3000, InformationPosition.Top);
            }
        }
        private string FixApiResponseString(string input)
        {
            string findRepresentatives = $"representatives";
            string findAuthorizedClerks = $"authorizedClerks";
            string findPartners = $"partners";
            string findNextToPartners = $"registrationLegalDate";
            string findAccountNumbers = $"accountNumbers";
            string findNextToAccountNumbers = $"hasVirtual";

            int startRepresentatives = input.IndexOf(findRepresentatives) + 3;
            int startAuthorizedClerks = input.IndexOf(findAuthorizedClerks) - 3;
            input = input.Remove(startRepresentatives + findRepresentatives.Length, startAuthorizedClerks - (startRepresentatives + findRepresentatives.Length));

            startAuthorizedClerks = input.IndexOf(findAuthorizedClerks) + 3;
            int startPartners = input.IndexOf(findPartners) - 3;
            input = input.Remove(startAuthorizedClerks + findAuthorizedClerks.Length, startPartners - (startAuthorizedClerks + findAuthorizedClerks.Length));

            startPartners = input.IndexOf(findPartners) + 3;
            int startNextPartners = input.IndexOf(findNextToPartners) - 3;
            input = input.Remove(startPartners + findPartners.Length, startNextPartners - (startPartners + findPartners.Length));

            int startAccountNumbers = input.IndexOf(findAccountNumbers) + 3;
            int startNextToAccountNumbers = input.IndexOf(findNextToAccountNumbers) - 3;
            input = input.Remove(startAccountNumbers + findAccountNumbers.Length, startNextToAccountNumbers - (startAccountNumbers + findAccountNumbers.Length));

            input = input.Trim('"');
            return input;
        }
    }
}