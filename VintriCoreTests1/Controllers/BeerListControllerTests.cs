using Microsoft.VisualStudio.TestTools.UnitTesting;
using VintriCore.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Abstractions;
using Moq;
using Microsoft.Extensions.Hosting;
using System.Net.Http;
using AutoFixture;

namespace VintriCore.Controllers.Tests
{
    [TestClass()]
    public class BeerListControllerTests
    {
        readonly Mock<IHttpClientFactory> _httpClient;
        readonly Mock<IHostingEnvironment> _hostingEnv;

        readonly Mock<Microsoft.AspNetCore.Hosting.IHostingEnvironment> _hostingEnv1;


        readonly Mock<IFileSystem> _fileSystem;

        readonly Fixture _fixture;


        public BeerListControllerTests()
        {
            _fixture = new Fixture();

            _httpClient = new Mock<IHttpClientFactory>();
            _hostingEnv = new Mock<IHostingEnvironment>();
            _fileSystem = new Mock<IFileSystem>();


            _hostingEnv1 = new Mock<Microsoft.AspNetCore.Hosting.IHostingEnvironment>();


        }


        [TestMethod()]
        public void GetAsyncTest()
        {
            BeerListController controller = new BeerListController(_httpClient.Object, _hostingEnv1.Object, _fileSystem.Object);

            var r  = controller.GetAsync("buzz").Result;

            Assert.Fail();
        }
    }
}