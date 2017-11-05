using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Web.Http;

namespace InvestmentTracker.Api
{
    public abstract class ControllerBase : ApiController
    {
        private static readonly JsonSerializerSettings _serializerSettings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        protected IHttpActionResult JsonResult<T>(T data)
        {
            return Json<T>(data, _serializerSettings);
        }
    }
}