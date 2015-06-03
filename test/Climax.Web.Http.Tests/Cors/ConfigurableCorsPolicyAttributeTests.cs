using System.Net.Http;
using System.Threading;
using Climax.Web.Http.Cors;
using NUnit.Framework;
using Should;

namespace Climax.Web.Http.Tests.Cors
{
    [TestFixture]
    class ConfigurableCorsPolicyAttributeTests
    {
        [Test]
        public void Ctor_CanSetAllAllowSettings_ToStar()
        {
            var elements = new CorsElementCollection();
            var fooElement = new CorsElement
            {
                Name = "foo",
                Headers = "*",
                Methods = "*",
                Origins = "*"
            };
            elements.Add(fooElement);

            var corsSection = new CorsSection
            {
                CorsPolicies = elements
            };

            var attr = new ConfigurableCorsPolicyAttribute("foo", corsSection);

            var policy = attr.GetCorsPolicyAsync(new HttpRequestMessage(), default(CancellationToken)).Result;
            policy.AllowAnyMethod.ShouldEqual(true);
            policy.AllowAnyHeader.ShouldEqual(true);
            policy.AllowAnyOrigin.ShouldEqual(true);
        }

        [Test]
        public void Ctor_CanSetAllAllowSettings_ToRequiredValues()
        {
            var elements = new CorsElementCollection();
            var fooElement = new CorsElement
            {
                Name = "foo",
                Headers = "X-Api-Rate;Foo",
                Methods = "GET;POST;PUT",
                Origins = "http://foo.com;http://www.abc.com"
            };
            elements.Add(fooElement);

            var corsSection = new CorsSection
            {
                CorsPolicies = elements
            };

            var attr = new ConfigurableCorsPolicyAttribute("foo", corsSection);
            var policy = attr.GetCorsPolicyAsync(new HttpRequestMessage(), default(CancellationToken)).Result;

            policy.AllowAnyMethod.ShouldEqual(false);
            policy.Methods.ShouldContain("GET");
            policy.Methods.ShouldContain("POST");
            policy.Methods.ShouldContain("PUT");

            policy.AllowAnyHeader.ShouldEqual(false);
            policy.Headers.ShouldContain("X-Api-Rate");
            policy.Headers.ShouldContain("Foo");

            policy.AllowAnyOrigin.ShouldEqual(false);
            policy.Origins.ShouldContain("http://foo.com");
            policy.Origins.ShouldContain("http://www.abc.com");
        }
    }
}
