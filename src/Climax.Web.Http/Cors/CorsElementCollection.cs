using System.Configuration;
using System.Reflection;

namespace Climax.Web.Http.Cors
{
    [ConfigurationCollection(typeof(CorsElement))]
    public class CorsElementCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new CorsElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((CorsElement)element).Name;
        }

        internal void Add(CorsElement element)
        {
            BaseAdd(element);
        }

        public override bool IsReadOnly()
        {
            if (Assembly.GetCallingAssembly().GetName().Name == "Climax.Web.Http.Tests")
            {
                return false;
            }

            return base.IsReadOnly();
        }
    }
}