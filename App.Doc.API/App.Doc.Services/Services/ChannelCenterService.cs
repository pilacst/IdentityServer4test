using App.Doc.Dto;
using App.Doc.Repository.Repositories.ChannelCenter;
using App.Doc.Services.Contracts;

namespace App.Doc.Services.Services
{
    public class ChannelCenterService : IChannelCenterService
    {
        private readonly IChannelCenterRepository _channelCenterRepository;

        public ChannelCenterService(IChannelCenterRepository channelCenterRepository) {
            _channelCenterRepository = channelCenterRepository;
        }

        public async Task<List<ChannelCenterDto>> GetAllActiveChannelCenters()
        {
            return await _channelCenterRepository.GetAllActiveChannelCenters();
        }
    }
}
