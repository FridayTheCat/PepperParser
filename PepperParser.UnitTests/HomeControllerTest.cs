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
    public class HomeControllerTest
    {
        private HomeController _controller;
        private ViewResult? _viewResult;

        [SetUp]
        public void SetUp()
        {
            _controller = new HomeController();
            _viewResult = _controller.Index() as ViewResult;
        }

        [Test]
        public void Test_Index_ReturnsViewName()
        {
            Assert.That(_viewResult?.ViewName, Is.EqualTo(null));
        }

        [Test]
        public void Test_Index_ReturnsViewBagTitle()
        {
            Assert.That(_viewResult?.ViewData["Title"], Is.EqualTo("Pepper Parser"));
        }

        [Test]
        public void Test_Index_ReturnsViewBagText()
        {
            Assert.That(_viewResult?.ViewData["Text"], Is.EqualTo("Главная"));
        }
    }
}
