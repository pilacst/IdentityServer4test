using App.Doc.DbAccess;
using App.Doc.Dto;
using Microsoft.EntityFrameworkCore;

namespace App.Doc.Repository.Repositories.ChannelCenter
{
    public class ChannelCenterRepository : IChannelCenterRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ChannelCenterRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<ChannelCenterDto>> GetAllActiveChannelCenters()
        {
            return await _dbContext.ChannelCenter.Where(cc => cc.IsActive == true).Select(ccd => new ChannelCenterDto
            {
                Id= ccd.Id,
                Name= ccd.Name,
            }).ToListAsync();
        }
    }
}
