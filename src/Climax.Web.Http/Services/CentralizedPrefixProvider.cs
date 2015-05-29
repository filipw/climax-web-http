using System.Web.Http.Controllers;
using System.Web.Http.Routing;

namespace Climax.Web.Http.Services
{
    public class CentralizedPrefixProvider : DefaultDirectRouteProvider
    {
        private readonly string _centralizedPrefix;

        public CentralizedPrefixProvider(string centralizedPrefix)
        {
            _centralizedPrefix = centralizedPrefix;
        }

        protected override string GetRoutePrefix(HttpControllerDescriptor controllerDescriptor)
        {
            var existingPrefix = base.GetRoutePrefix(controllerDescriptor);
            return existingPrefix ?? _centralizedPrefix;
        }
    }
}
