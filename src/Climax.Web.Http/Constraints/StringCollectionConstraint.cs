using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http.Routing;

namespace Climax.Web.Http.Constraints
{
    public class StringCollectionConstraint : IHttpRouteConstraint
    {
        public StringCollectionConstraint(string values)
        {
            if (values == null)
            {
                throw new ArgumentNullException("values");
            }

            Values = values.Split(',');
        }

        protected StringCollectionConstraint(string[] values)
        {
            if (values == null)
            {
                throw new ArgumentNullException("values");
            }

            Values = values;
        }

        public IEnumerable<string> Values { get; private set; }

        public bool Match(HttpRequestMessage request, IHttpRoute route, string parameterName,
            IDictionary<string, object> values, HttpRouteDirection routeDirection)
        {
            object value;
            if (values.TryGetValue(parameterName, out value) && value != null)
            {
                return Values.Contains(value.ToString(), StringComparer.OrdinalIgnoreCase);
            }

            return false;
        }
    }
}