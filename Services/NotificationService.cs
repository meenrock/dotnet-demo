using CorePush.Google;
using dotnet_demo.Helpers;
using dotnet_demo.Models.Notification;
using dotnet_demo.Repositories;
using dotnet_demo.Constant;
using Microsoft.Extensions.Options;
using System.Data;
using System.Net.Http.Headers;
using static dotnet_demo.Models.Notification.GoogleNotificationResponse;

namespace dotnet_demo.Services
{
    public interface INotificationService
    {
        Task<NotificationResponse> SendNotification(NotificationModel notificationModel);
        bool Register(string token, string username);
        bool Unregister(string username);
    }
    public class NotificationService : INotificationService
    {
        private readonly AppSettings _appSettings;
        private readonly BaseRepository _baseRepository;

        public NotificationService(IOptions<AppSettings> appSettings, ILogger<BaseRepository> logger)
        {
            _appSettings = appSettings.Value;
            _baseRepository = new BaseRepository(appSettings, logger);
        }

        public bool Register(string token, string username)
        {
            return _baseRepository.Execute(StoredProcedure.SUBSCRIPTION,
                new
                {
                    token,
                    username
                },
                CommandType.StoredProcedure);
        }

        public async Task<NotificationResponse> SendNotification(NotificationModel notificationModel)
        {
            NotificationResponse response = new NotificationResponse();
            try
            {
                if (notificationModel.IsAndroiodDevice)
                {
                    /* FCM Sender (Android Device) */
                    HttpClient httpClient = new HttpClient();

                    string authorizationKey = string.Format("keyy={0}", _appSettings.serverKey);
                    string deviceToken = notificationModel.DeviceId;

                    httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", authorizationKey);
                    httpClient.DefaultRequestHeaders.Accept
                            .Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    DataPayload dataPayload = new DataPayload();
                    dataPayload.Title = notificationModel.Title;
                    dataPayload.Body = notificationModel.Body;

                    GoogleNotificationResponse notification = new GoogleNotificationResponse();
                    notification.Data = dataPayload;
                    notification.Notification = dataPayload;

                    var fcm = new FcmSender(new FcmSettings { SenderId = _appSettings.senderId, ServerKey = _appSettings.serverKey }, httpClient);
                    var fcmSendResponse = await fcm.SendAsync(deviceToken, notification);

                    if (fcmSendResponse.IsSuccess())
                    {
                        response.IsSuccess = true;
                        response.Message = "Notification sent successfully";
                        return response;
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.Message = fcmSendResponse.Results[0].Error;
                        return response;
                    }
                }
                else
                {
                    //IOS Message Service
                }
                return response;
            }
            catch (Exception)
            {
                response.IsSuccess = false;
                response.Message = "Something went wrong";
                return response;
            }
        }

        public bool Unregister(string username)
        {
            return _baseRepository.Execute(StoredProcedure.UNSUBSCRIPTION,
                     new
                     {
                         username
                     },
                     CommandType.StoredProcedure);
        }

    }
}
