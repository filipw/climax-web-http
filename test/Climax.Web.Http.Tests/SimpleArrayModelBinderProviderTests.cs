using System;
using System.Web.Http;
using Climax.Web.Http.Binders;
using NUnit.Framework;
using Should;

namespace Climax.Web.Http.Tests
{
    [TestFixture]
    public class SimpleArrayModelBinderProviderTests
    {
        [Test]
        public void GetBinder_ShouldThrow_IfTypeIsNull()
        {
            Assert.Throws<ArgumentNullException>(
                () => new SimpleArrayModelBinderProvider().GetBinder(new HttpConfiguration(), null));
        }

        [Test]
        public void GetBinder_ReturnsNull_ForNonArrays()
        {
            var provider = new SimpleArrayModelBinderProvider();
            provider.GetBinder(new HttpConfiguration(), typeof(int)).ShouldBeNull();
        }

        [Test]
        public void GetBinder_ReturnsNull_ForNonPrimitives()
        {
            var provider = new SimpleArrayModelBinderProvider();
            provider.GetBinder(new HttpConfiguration(), typeof(HttpConfiguration)).ShouldBeNull();
        }

        [Test]
        public void GetBinder_ReturnssimpleArrayModelBinder_ForPrimitiveArrys()
        {
            var provider = new SimpleArrayModelBinderProvider();
            provider.GetBinder(new HttpConfiguration(), typeof(int[])).ShouldBeType(typeof(SimpleArrayModelBinder<int>));
            provider.GetBinder(new HttpConfiguration(), typeof(string[])).ShouldBeType(typeof(SimpleArrayModelBinder<string>));
            provider.GetBinder(new HttpConfiguration(), typeof(double[])).ShouldBeType(typeof(SimpleArrayModelBinder<double>));
        }
    }
}