using Stash.Project.IBasicService.BasicDto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Stash.Project.IBasicService
{
    public interface IStorageLocationService : IApplicationService
    {
        /// <summary>
        /// 库位新增
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<ApiResult> CreateStorageLocationAsync(StorageLocationDto dto);

        /// <summary>
        /// 库位条件查询
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<ApiResult> GetStorageLocationListAsync(StorageLocationinquireDto dto);

        /// <summary>
        /// 库位查询
        /// </summary>
        /// <returns></returns>
        Task<ApiResult> GetStorageLocationAsync();

        /// <summary>
        /// 删除库位
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<ApiResult> DeleteStorageLocationAsync(string ids);

        /// <summary>
        /// 修改库位
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<ApiResult> UpdateStorageLocationAsync(StorageLocationDto dto);

        /// <summary>
        /// 查询指定库位信息
        /// </summary>
        /// <param name="storageid"></param>
        /// <returns></returns>
        Task<ApiResult> GetStorageLocationInfoAsync(long storageid);
    }
}
