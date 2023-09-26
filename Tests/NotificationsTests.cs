using dotnet_demo.Helpers;
using dotnet_demo.Models.Notification;
using dotnet_demo.Services;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Moq;
using System.Net.Http.Headers;

namespace dotnet_demo.Tests
{
    public class NotificationsTests
    {
        [Fact]
        public async Task TestSendNotification_Andriod()
        {
            //arrange - mock the data and input
            var httpClientMock = new Mock<HttpClient>();
            var appSettings = new AppSettings { serverKey = "YourServerKey", senderId = "YourSenderId" };
            var notificationModel = new NotificationModel
            {
                IsAndroiodDevice = true,
                Title = "Test Title",
                Body = "Test Body",
                DeviceId = "TestDeviceId"
            };
            var notificationService = new NotificationService(httpClientMock.Object, appSettings);

            httpClientMock.Setup(client => client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", It.IsAny<string>()));
            httpClientMock.Setup(client => client.DefaultRequestHeaders.Accept.Add(It.IsAny<MediaTypeWithQualityHeaderValue>()));
            httpClientMock
                .Setup(client => client.PostAsync(It.IsAny<Uri>(), It.IsAny<HttpContent>(), CancellationToken.None))
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK));


            //act
            var response = await notificationService.SendNotification(notificationModel);


            //assert
            Assert.True(response.IsSuccess);
            Assert.Equal("Notification sent successfully", response.Message);
        }


    }
}
