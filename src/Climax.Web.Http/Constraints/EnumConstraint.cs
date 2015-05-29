using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http.Routing;

namespace Climax.Web.Http.Constraints
{
    public class EnumConstraint : IHttpRouteConstraint
    {
        private static readonly ConcurrentDictionary<string, string[]> EnumDefinitions = new ConcurrentDictionary<string, string[]>();

        public EnumConstraint(string enumName)
        {
            EnumName = enumName;
        }

        public string EnumName { get; private set; }

        public bool Match(HttpRequestMessage request, IHttpRoute route, string parameterName, IDictionary<string, object> values,
            HttpRouteDirection routeDirection)
        {
            object value;
            if (values.TryGetValue(parameterName, out value) && value != null)
            {
                string[] validValues;
                if (!EnumDefinitions.TryGetValue(EnumName, out validValues))
                {
                    var type = Type.GetType(EnumName);
                    validValues = type != null ? Enum.GetNames(type) : new string[0];
                    EnumDefinitions.TryAdd(EnumName, validValues);
                }

                return validValues.Contains(value.ToString(), StringComparer.OrdinalIgnoreCase);
            }
            return false;
        }
    }
}
