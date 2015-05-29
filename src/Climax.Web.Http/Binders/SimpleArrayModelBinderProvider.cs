using System;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace Climax.Web.Http.Binders
{
    public class SimpleArrayModelBinderProvider : ModelBinderProvider
    {
        public override IModelBinder GetBinder(HttpConfiguration configuration, Type modelType)
        {
            if (modelType == null)
            {
                throw new ArgumentNullException();
            }

            if (!modelType.IsArray)
            {
                return null;
            }

            var elementType = modelType.GetElementType();
            if (elementType.IsPrimitive || typeof(string) == elementType)
            {
                return (IModelBinder)Activator.CreateInstance(typeof(SimpleArrayModelBinder<>).MakeGenericType(elementType));
            }

            return null;
        }
    }
}