using FileUploader.Application.Handlers.FileUploadHandler;
using FileUploader.Domain.Models;
using FileUploader.Domain.Ports;
using FileUploader.Test.Utilities;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace FileUploader.Test
{
    public class FileUploaderTest
    {
        //private TestUtility _serviceProvider;

        private Mock<IFileUploadPort> _fileUploadPort;

        [SetUp]
        public void Setup()
        {
            //var webHost = WebHost.CreateDefaultBuilder()
            //    .UseStartup<Startup>()
            //    .Build();
            //_serviceProvider = new TestUtility(webHost);

            _fileUploadPort = new Mock<IFileUploadPort>();
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }

        [Test]
        public async Task ToUploadFile_ShouldCreateFile_WhenFileValidated()
        {
            var sevenThousandItems = new byte[7000];
            for (int i = 0; i < sevenThousandItems.Length; i++)
            {
                sevenThousandItems[i] = 0x20;
            }

            var request = new FileUploadHandlerRequest()
            {
                FileName = "TestFile.txt",
                FileSize = 3000,
                FileType = ".txt",
                FileContent = sevenThousandItems
            };

            var fileUploadResponse = new FileUploadResponse();
            fileUploadResponse.Data.IsFileUploaded = true;

            _fileUploadPort.Setup(x => x.CreateFileAsync(It.IsAny<FileUploader.Domain.Models.FileInfo>())).ReturnsAsync(fileUploadResponse);
            var fileUploadHandler = new FileUploadHandler(TestUtility.GetIConfigurationRoot(), _fileUploadPort.Object);
            FileUploadResponse result = await fileUploadHandler.HandleAsync(request);

            var actual = result.Data.IsFileUploaded;
            const bool expected = true;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public async Task ToUploadFile_ShouldNotCreateFile_WhenFileTypeIsNotValidated()
        {
            var sevenThousandItems = new byte[7000];
            for (int i = 0; i < sevenThousandItems.Length; i++)
            {
                sevenThousandItems[i] = 0x20;
            }

            var request = new FileUploadHandlerRequest()
            {
                FileName = "TestFile.txt",
                FileSize = 3000,
                FileType = ".png",
                FileContent = sevenThousandItems
            };

            var fileUploadResponse = new FileUploadResponse();
            fileUploadResponse.Data.IsFileUploaded = true;

            _fileUploadPort.Setup(x => x.CreateFileAsync(It.IsAny<FileUploader.Domain.Models.FileInfo>())).ReturnsAsync(fileUploadResponse);
            var fileUploadHandler = new FileUploadHandler(TestUtility.GetIConfigurationRoot(), _fileUploadPort.Object);
            FileUploadResponse result = await fileUploadHandler.HandleAsync(request);

            var actual = result.Data.IsFileUploaded;
            const bool expected = false;
            Assert.AreEqual(expected, actual);
        }
    }
}