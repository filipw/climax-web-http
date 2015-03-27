using System;
using System.Collections.Concurrent;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using Climax.Web.Http.Extensions;

namespace Climax.Web.Http.Services
{
    public class PerControllerConfigActivator : IHttpControllerActivator
    {
        private readonly IHttpControllerActivator _innerActivator;

        private readonly ConcurrentDictionary<Type, HttpConfiguration> _cache =
            new ConcurrentDictionary<Type, HttpConfiguration>();

        public PerControllerConfigActivator() : this(new DefaultHttpControllerActivator())
        {}

        public PerControllerConfigActivator(IHttpControllerActivator innerActivator)
        {
            if (innerActivator == null)
            {
                throw new ArgumentNullException("innerActivator");
            }

            _innerActivator = innerActivator;
        }

        public IHttpController Create(HttpRequestMessage request, HttpControllerDescriptor controllerDescriptor,
            Type controllerType)
        {
            HttpConfiguration controllerConfig;
            if (_cache.TryGetValue(controllerType, out controllerConfig))
            {
                controllerDescriptor.Configuration = controllerConfig;
            }
            else
            {
                var configMap = request.GetConfiguration().GetControllerConfigurationMap();
                if (configMap != null && configMap.ContainsKey(controllerType))
                {
                    controllerDescriptor.Configuration =
                        controllerDescriptor.Configuration.Copy(configMap[controllerType]);
                    _cache.TryAdd(controllerType, controllerDescriptor.Configuration);
                }
            }

            var result = _innerActivator.Create(request, controllerDescriptor, controllerType);
            return result;
        }
    }
}