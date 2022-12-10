using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using PepperParser.Controllers;

namespace PepperParser.UnitTests
{
    [TestFixture]
    public class AggregatorsControllerTests
    {
        private AggregatorsController _controller;
        private ViewResult? _viewResult;

        [SetUp]
        public void SetUp()
        {
            _controller = new AggregatorsController();
            _viewResult = _controller.Index() as ViewResult;
        }

        [Test]
        public void Test_Index_ReturnsViewResultNotNull()
        {
            Assert.IsNotNull(_viewResult);
        }

        [Test]
        public void Test_Index_ReturnsViewBagTitle()
        {
            Assert.That(_viewResult?.ViewData["Title"], Is.EqualTo("Площадки"));
        }

        [Test]
        public void Test_Index_ReturnsViewBagText()
        {
            Assert.That(_viewResult?.ViewData["Text"], Is.EqualTo("Актуальные промокоды"));
        }
    }
}
