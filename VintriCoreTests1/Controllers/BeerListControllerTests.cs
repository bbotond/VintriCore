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
using System.Threading.Tasks;
using Moq.Protected;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using System.Net.Http.Formatting;
using System.Net;
using System.IO;

namespace VintriCore.Controllers.Tests
{
    [TestClass()]
    public class BeerListControllerTests
    {
        readonly Mock<IHttpClientFactory> _httpClient;
        readonly Mock<Microsoft.AspNetCore.Hosting.IHostingEnvironment> _hostingEnv;
        readonly Mock<IFileSystem> _fileSystem;

        readonly Fixture _fixture;


        public BeerListControllerTests()
        {
            _fixture = new Fixture();

            _httpClient = new Mock<IHttpClientFactory>();
            _hostingEnv = new Mock<Microsoft.AspNetCore.Hosting.IHostingEnvironment>();
            _fileSystem = new Mock<IFileSystem>();





        }


        [TestMethod()]
        public void GetAsyncTest()
        {

            // create the mock client factory mock
            var httpClientFactoryMock = new Mock<IHttpClientFactory>();

            // setup the method call
            httpClientFactoryMock.Setup(x => x.CreateClient(""))
                                 .Returns(new HttpClient());

            var fileMock = new Mock<IFileSystem>();
            fileMock.Setup(x => x.File.OpenRead(""))
                .Returns(new MemoryStream(Encoding.UTF32.GetBytes("[{\"BeerId\":1,\"Username\":\"brendan@hotmail.com\",\"Rating\":3,\"Comments\":\"dnkjhe kfdj j fklje lfj pkel; j fkjfmk l\"}]")));

            var hostMock = new Mock<Microsoft.AspNetCore.Hosting.IHostingEnvironment>();
            hostMock.Setup(x => x.ContentRootPath)
                .Returns(@"C:\Users\bboto\source\repos\VintriCore\VintriCore\");

            BeerListController controller = new BeerListController(httpClientFactoryMock.Object, _hostingEnv.Object, fileMock.Object);


            var r  = controller.GetAsync("buzz").Result;

            Assert.Fail();
        }
    }
}