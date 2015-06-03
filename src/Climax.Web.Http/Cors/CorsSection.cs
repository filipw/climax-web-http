using System.Configuration;

namespace Climax.Web.Http.Cors
{
    public class CorsSection : ConfigurationSection
    {
        [ConfigurationProperty("corsPolicies", IsDefaultCollection = true)]
        public CorsElementCollection CorsPolicies
        {
            get { return (CorsElementCollection)this["corsPolicies"]; }
            set { this["corsPolicies"] = value; }
        }
    }
}