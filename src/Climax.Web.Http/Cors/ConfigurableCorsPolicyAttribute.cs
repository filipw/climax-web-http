using System;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Cors;
using System.Web.Http.Cors;

namespace Climax.Web.Http.Cors
{
    public class ConfigurableCorsPolicyAttribute : Attribute, ICorsPolicyProvider
    {
        private readonly CorsPolicy _policy;

        internal ConfigurableCorsPolicyAttribute(string name, CorsSection corsSection)
        {
            _policy = new CorsPolicy();

            if (corsSection != null)
            {
                var policy = corsSection.CorsPolicies.Cast<CorsElement>().FirstOrDefault(x => x.Name == name);
                if (policy != null)
                {
                    if (policy.Headers == "*")
                    {
                        _policy.AllowAnyHeader = true;
                    }
                    else
                    {
                        policy.Headers.Split(';').ToList().ForEach(x => _policy.Headers.Add(x));
                    }

                    if (policy.Methods == "*")
                    {
                        _policy.AllowAnyMethod = true;
                    }
                    else
                    {
                        policy.Methods.Split(';').ToList().ForEach(x => _policy.Methods.Add(x));
                    }

                    if (policy.Origins == "*")
                    {
                        _policy.AllowAnyOrigin = true;
                    }
                    else
                    {
                        policy.Origins.Split(';').ToList().ForEach(x => _policy.Origins.Add(x));
                    }
                }
            }
        }

        public ConfigurableCorsPolicyAttribute(string name)
            : this(name, ConfigurationManager.GetSection("cors") as CorsSection)
        {

        }

        public Task<CorsPolicy> GetCorsPolicyAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_policy);
        }
    }
}