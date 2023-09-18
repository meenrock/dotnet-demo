using dotnet_demo.Helpers;
using dotnet_demo.Models.Notification;
using dotnet_demo.Repositories;
using dotnet_demo.Services;
using dotnet_demo.Constant;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Data;

namespace dotnet_demo.Controllers
{
    [ApiController]
    [Route("api/notification")]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        private readonly BaseRepository _baseRepository;

        public NotificationController(INotificationService notificationService, IOptions<AppSettings> appSettings, ILogger<BaseRepository> logger)
        {
            _notificationService = notificationService;
            _baseRepository = new BaseRepository(appSettings,logger);
        }

        [Route("send")]
        [HttpPost]
        public async Task<IActionResult> SendNotification(NotificationRequest request)
        {
            try
            {
                var tasks = new List<Task>();

                foreach (var username in request.username)
                {
                    var subscription = _baseRepository.GetList<SubscriptionModel>(StoredProcedure.FETCH_USER_SUBSCRIPTION, new { username }, CommandType.StoredProcedure);
                    
                    foreach (var sub in subscription)
                    {
                        tasks.Add(_notificationService.SendNotification(
                                new NotificationModel
                                {
                                    DeviceId = sub.Token,
                                    IsAndroiodDevice = true,
                                    Title = request.title,
                                    Body = request.body,
                                }
                            ));
                        //await _notificationService.SendNotification(new NotificationModel { DeviceId = sub.Token, IsAndroiodDevice = true, Title = request.title, Body = request.body });
                    }
                }

                await Task.WhenAll(tasks);

                return Ok();

            }
            catch (Exception)
            {

                throw;
            }
            
        }

        [Route("register")]
        [HttpGet]
        public IActionResult Register([FromQuery] string token, [FromQuery] string username)
        {
            var result = _notificationService.Register(token, username);
            return Ok(result);
        }

        [Route("unregister")]
        [HttpGet]
        public IActionResult Unregister([FromQuery] string username)
        {
            var result = _notificationService.Unregister(username);
            return Ok(result);
        }


    }
}
