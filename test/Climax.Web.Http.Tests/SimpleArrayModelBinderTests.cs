using System.Globalization;
using System.Linq;
using System.Web.Http.Controllers;
using System.Web.Http.Metadata;
using System.Web.Http.Metadata.Providers;
using System.Web.Http.ModelBinding;
using System.Web.Http.ValueProviders;
using Climax.Web.Http.Binders;
using Moq;
using NUnit.Framework;
using Should;

namespace Climax.Web.Http.Tests
{
    [TestFixture]
    public class SimpleArrayModelBinderTests
    {
        [Test]
        public void BindModel_CanBindIntArray([Values(";", ",", "-")] string separator)
        {
            var val = string.Join(separator, new[] { 1, 2, 3 });
            var valueProvider = new Mock<IValueProvider>();
            valueProvider.Setup(x => x.GetValue("foo"))
                .Returns(new ValueProviderResult(val, val, CultureInfo.CurrentCulture));

            var ctx = new ModelBindingContext
            {
                ModelMetadata = new ModelMetadata(new EmptyModelMetadataProvider(), null, null, typeof(int[]), null),
                ModelName = "foo",
                ValueProvider = valueProvider.Object
            };

            var binder = separator == ";"
                ? new SimpleArrayModelBinder<int>()
                : new SimpleArrayModelBinder<int>(separator);
            var result = binder.BindModel(new HttpActionContext(), ctx);

            ctx.Model.ShouldBeType<int[]>();
            ((int[])ctx.Model).Count().ShouldEqual(3);
            ((int[])ctx.Model).First().ShouldEqual(1);
            ((int[])ctx.Model).Last().ShouldEqual(3);
        }
    }
}