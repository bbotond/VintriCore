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
        readonly Mock<IFileSystem> _fileSystem;

        readonly Fixture _fixture;


        public BeerListControllerTests()
        {
            _fixture = new Fixture();

            _httpClient = new Mock<IHttpClientFactory>();
            _hostingEnv = new Mock<IHostingEnvironment>();
            _fileSystem = new Mock<IFileSystem>();

    //        _httpClient.Setup(s => s.CreateClient().GetAsync(""));
        }


        [TestMethod()]
        public void GetAsyncTest()
        {
            Assert.Fail();
        }
    }
}