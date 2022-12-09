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
    public class DonationControllerTests
    {
        private DonationController _controller;
        private ViewResult? _viewResult;

        [SetUp]
        public void SetUp()
        {
            _controller = new DonationController();
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
            Assert.That(_viewResult?.ViewData["Title"], Is.EqualTo("Донат"));
        }

        [Test]
        public void Test_Index_ReturnsViewBagText()
        {
            Assert.That(_viewResult?.ViewData["Text"], Is.EqualTo("Помощь сайту"));
        }

    }
}
