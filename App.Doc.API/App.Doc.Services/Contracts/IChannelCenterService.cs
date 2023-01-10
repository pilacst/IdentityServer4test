using App.Doc.Dto;

namespace App.Doc.Services.Contracts
{
    public interface IChannelCenterService
    {
        Task<List<ChannelCenterDto>> GetAllActiveChannelCenters();
    }
}
