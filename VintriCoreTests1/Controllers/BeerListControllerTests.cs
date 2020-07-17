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

            //            // Mock the handler
            //            var handlerMock = new Mock<HttpMessageHandler>();

            ////            handlerMock.

            //            handlerMock.Protected()
            //                       // Setup the PROTECTED method to mock
            //                       .Setup<Task<HttpResponseMessage>>("GetAsync",
            //                                                         ItExpr.IsAny<String>())
            //                       // prepare the expected response of the mocked http call
            //                       .ReturnsAsync(new HttpResponseMessage()
            //                       {
            //                           StatusCode = HttpStatusCode.OK
            //                       })
            //                       .Verifiable();

            //            // use real http client with mocked handler here
            //var httpClient = new HttpClient(handlerMock.Object)
            //            {
            //                BaseAddress = new Uri("http://test.com/"),
            //            };




            // create the mock client factory mock
            var httpClientFactoryMock = new Mock<IHttpClientFactory>();

            // setup the method call
            httpClientFactoryMock.Setup(x => x.CreateClient(""))
                                 .Returns(new HttpClient());




            BeerListController controller = new BeerListController(httpClientFactoryMock.Object, _hostingEnv.Object, _fileSystem.Object);



            //         BeerListController controller = new BeerListController(httpClient, _hostingEnv.Object, _fileSystem.Object);



            //            BeerListController controller = new BeerListController(_httpClient.Object, _hostingEnv.Object, _fileSystem.Object);

            var r  = controller.GetAsync("buzz").Result;

            Assert.Fail();
        }
    }
}