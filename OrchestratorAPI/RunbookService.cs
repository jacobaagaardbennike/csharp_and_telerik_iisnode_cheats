using Lib.Runbook.OrchestratorApiService;
using System;
using System.Collections.Generic;
using System.Data.Services.Client;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Text;

namespace Lib.Runbook
{
    public class RunbookService
    {
        private string username;
        private string password;
        private string runbookId;
        private OrchestratorApi orchestratorApi;

        public RunbookService(string username, string password, string orchestratorApiAddress, string runbookId)
        {
            this.username = username;
            this.password = password;
            this.runbookId = runbookId;
            this.orchestratorApi = new OrchestratorApi(new Uri(orchestratorApiAddress));
            ((DataServiceContext)orchestratorApi).Credentials = new NetworkCredential(username, password);

            //This is used here to ignore certificate errors as you might be using test certificates. You can remove this if you are using trusted certificates.
            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(
                delegate
                {
                    return true;
                });
        }

        public bool CreateAndStartRunbookJob(string someParam, out string errorStr)
        {
            OperationParameter operationParameters = CreateRunbookParameters(someParam);

            return CreateRunbookJob(operationParameters, out errorStr);
        }

        private bool CreateRunbookJob(OperationParameter operationParameters, out string errorStr)
        {
            try
            {
                string uri = string.Concat(orchestratorApi.Runbooks, string.Format("(guid'{0}')/{1}", runbookId, "Start"));
                Uri uriSMA = new Uri(uri, UriKind.Absolute);

                var jobIdValue = orchestratorApi.Execute<Guid>(uriSMA, "POST", true, operationParameters) as QueryOperationResponse<Guid>;
                jobIdValue.Single();
                errorStr = "";
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.InnerException.Message);
                Debug.WriteLine(ex.Message);
                errorStr = "Fejl: " + ex.InnerException.Message + " - " + ex.Message;
                return false;


            }
        }

        private OperationParameter CreateRunbookParameters(string someParam)
        {
            var runbookParams = new List<NameValuePair>();

            //Fordi microsoft har "glemt" type casting, caster SMA serveren selv værdierne - følgende er et hack til at komme uden om buggen

            string strHack = "DefaultString:";

            runbookParams.Add(new NameValuePair() { Name = "RunbookParam", Value = someParam });
            runbookParams.Add(new NameValuePair() { Name = "RunbookParam", Value = strHack + someParam });


            return new BodyOperationParameter("parameters", runbookParams);
        }

        private Lib.Runbook.OrchestratorApiService.Runbook GetRunbookByName(string runbookName)
        {
            return orchestratorApi.Runbooks.Where(r => r.RunbookName == runbookName).First();
        }
    }
}
