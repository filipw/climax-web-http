using System.Net.Http;
using System.Web.Http.Routing;
using Climax.Web.Http.Constraints;
using Moq;
using NUnit.Framework;
using Should;

namespace Climax.Web.Http.Tests
{
    [TestFixture]
    public class StringCollectionConstraintTests
    {
        [Test]
        public void ShouldMatch_IfParameterValue_InTheList([Values("foo", "notfoo")] string input)
        {
            var constraint = new StringCollectionConstraint("foo,bar");

            var result = constraint.Match(new HttpRequestMessage(), new Mock<IHttpRoute>().Object, "test",
                new HttpRouteValueDictionary
                {
                    {"test", input}
                }, HttpRouteDirection.UriResolution);

            if (input == "foo")
            {
                result.ShouldBeTrue();
            }
            else
            {
                result.ShouldBeFalse();
            }
        }
    }
}