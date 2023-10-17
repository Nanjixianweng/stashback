using Stash.Project.BasicDto;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Stash.Project.IBasicService
{
    public interface IStoreService : IApplicationService
    {
        /// <summary>
        /// 仓库新增
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<ApiResult> CreateStoreAsync(StoreDto dto);

        /// <summary>
        /// 仓库条件查询
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<ApiResult> GetStoreListAsync(StoreinquireDto dto);

        /// <summary>
        /// 删除仓储
        /// </summary>
        /// <param name="storeid"></param>
        /// <returns></returns>
        Task<ApiResult> DeleteStoreAsync(long storeid);

        /// <summary>
        /// 修改仓储
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<ApiResult> UpdateStoreAsync(StoreDto dto);

        /// <summary>
        /// 查询指定仓库信息
        /// </summary>
        /// <param name="storeid"></param>
        /// <returns></returns>
        Task<ApiResult> GetStoreInfoAsync(long storeid);
    }
}
