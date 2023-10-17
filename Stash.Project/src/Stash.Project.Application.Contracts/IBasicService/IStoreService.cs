
using Stash.Project.IBasicService.BasicDto;
using System.Collections.Generic;
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
        /// 仓库查询
        /// </summary>
        /// <returns></returns>
        Task<ApiResult> GetStoreAsync();

        /// <summary>
        /// 仓库条件查询
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<ApiResult> CreateStoreListAsync(StoreinquireDto dto);

        /// <summary>
        /// 删除仓库
        /// </summary>
        /// <param name="storeid"></param>
        /// <returns></returns>
        Task<ApiResult> DeleteStoreAsync(long storeid);

        /// <summary>
        /// 修改仓库
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
