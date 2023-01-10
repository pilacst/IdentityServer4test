using App.Doc.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.Doc.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ChannelCenterController : ControllerBase
    {
        private readonly IChannelCenterService _channelCenterService;
        public ChannelCenterController(IChannelCenterService channelCenterService) {
            _channelCenterService = channelCenterService;
        }

        [HttpGet]
        [Route("GetAllActiveChannelCenters")]
        public async Task<IActionResult> GetAllActiveChannelCenters()
        {
            
            var channelCenters = await _channelCenterService.GetAllActiveChannelCenters();
            return Ok(channelCenters.ToList());
        }
    }
}
