using App.Doc.Dto;

namespace App.Doc.Repository.Repositories.ChannelCenter
{
    public interface IChannelCenterRepository
    {
        Task<List<ChannelCenterDto>> GetAllActiveChannelCenters();
    }
}
