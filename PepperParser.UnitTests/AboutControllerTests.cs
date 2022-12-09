using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using PepperParser.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PepperParser.UnitTests
{
    [TestFixture]
    public class AboutControllerTests
    {
        private AboutController _controller;
        private ViewResult? _viewResult;

        [SetUp]
        public void SetUp()
        {
            _controller = new AboutController();
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
            Assert.That(_viewResult?.ViewData["Title"], Is.EqualTo("О нас"));
        }

        [Test]
        public void Test_Index_ReturnsViewBagText()
        {
            Assert.That(_viewResult?.ViewData["Text"], Is.EqualTo("Информация"));
        }
    }
}
