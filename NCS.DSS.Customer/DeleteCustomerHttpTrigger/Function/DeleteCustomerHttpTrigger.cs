using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http.Description;
using NCS.DSS.Customer.Annotations;
using NCS.DSS.Customer.Cosmos.Helper;
using System;

namespace NCS.DSS.Customer.DeleteCustomerHttpTrigger
{
    public static class DeleteCustomerHttpTrigger
    {
        [FunctionName("DELETE")]
        [Response(HttpStatusCode = (int)HttpStatusCode.OK, Description = "Customer Deleted", ShowSchema = true)]
        [Response(HttpStatusCode = (int)HttpStatusCode.NoContent, Description = "Resource Does Not Exist", ShowSchema = false)]
        [Response(HttpStatusCode = (int)HttpStatusCode.BadRequest, Description = "Delete request is malformed", ShowSchema = false)]
        [Response(HttpStatusCode = (int)HttpStatusCode.Unauthorized, Description = "API Key unknown or invalid", ShowSchema = false)]
        [Response(HttpStatusCode = (int)HttpStatusCode.Forbidden, Description = "Insufficient Access To This Resource", ShowSchema = false)]
        [ResponseType(typeof(Models.Customer))]
        public static HttpResponseMessage Run([HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "Customers/{customerId}")]HttpRequestMessage req, TraceWriter log, string customerId)
        {
            log.Info("C# HTTP trigger function Delete Customer processed a request.");

            if (!Guid.TryParse(customerId, out var customerGuid))
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(JsonConvert.SerializeObject(customerId),
                        System.Text.Encoding.UTF8, "application/json")
                };
            }

            var resourceHelper = new ResourceHelper();
            var doesCustomerExist = resourceHelper.DoesCustomerExist(customerGuid);

            if (!doesCustomerExist)
            {
                return new HttpResponseMessage(HttpStatusCode.NoContent)
                {
                    Content = new StringContent("Unable to find a customer with Id of : " +
                                                JsonConvert.SerializeObject(customerGuid),
                        System.Text.Encoding.UTF8, "application/json")
                };
            }

            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("Deleted record with Id of : " + customerId)
            };
        }
    }
}