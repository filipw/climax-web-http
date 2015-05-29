using System;
using System.Linq;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;

namespace Climax.Web.Http.Binders
{
    public class SimpleArrayModelBinder<T> : IModelBinder
    {
        private readonly string _separator;

        public SimpleArrayModelBinder()
            : this(";")
        {
        }

        public SimpleArrayModelBinder(string separator)
        {
            _separator = separator;
        }

        public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
        {
            var key = bindingContext.ModelName;
            var val = bindingContext.ValueProvider.GetValue(key);
            try
            {
                if (val != null)
                {
                    var s = val.AttemptedValue;
                    if (s != null && s.IndexOf(_separator, StringComparison.Ordinal) > 0)
                    {
                        var stringArray = s.Split(new[] { _separator }, StringSplitOptions.None);
                        bindingContext.Model = stringArray.Select(x => (T)Convert.ChangeType(x, typeof(T))).ToArray();
                    }
                    else
                    {
                        bindingContext.Model = new[] { (T)Convert.ChangeType(s, typeof(T)) };
                    }

                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }

            return false;
        }
    }
}