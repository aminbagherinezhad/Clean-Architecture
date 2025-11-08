using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Notification.Application.DTOs;
using Notification.Application.Interfaces;

namespace Notification.Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _service;
        private readonly ILogger<NotificationController> _logger;

        public NotificationController(INotificationService service, ILogger<NotificationController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpPost("config")]
        public async Task<IActionResult> SetConfig([FromBody] SetConfigRequest req, CancellationToken cancellationToken)
        {
            await _service.SetConfigAsync(req.DefaultType, cancellationToken);
            return Ok();
        }

        [HttpPost("send")]
        public async Task<IActionResult> Send([FromBody] SendNotificationRequest req, CancellationToken cancellationToken)
        {
            var res = await _service.SendAsync(req, cancellationToken);
            return Ok(res);
        }

        [HttpPost("send-custom")]
        public async Task<IActionResult> SendCustom([FromBody] SendNotificationRequest req, CancellationToken cancellationToken)
        {
            var res = await _service.SendAsync(req, cancellationToken);
            return Ok(res);
        }

        [HttpGet("history")]
        public async Task<IActionResult> History(CancellationToken cancellationToken)
        {
            var list = await _service.GetHistoryAsync(cancellationToken);
            return Ok(list);
        }
    }

    public record SetConfigRequest(string DefaultType);
}
