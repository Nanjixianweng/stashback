using Stash.Project.IBasicService.BasicDto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Stash.Project.IBasicService
{
    public interface IUnitService : IApplicationService
    {
        /// <summary>
        /// 单位新增
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<ApiResult> CreateUnitAsync(UnitDto dto);

        /// <summary>
        /// 单位查询
        /// </summary>
        /// <returns></returns>
        Task<ApiResult> GetStoreAsync();

        /// <summary>
        /// 单位条件查询
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<ApiResult> CreateUnitListAsync(UnitInquireDto dto);

        /// <summary>
        /// 删除单位
        /// </summary>
        /// <param name="unitid"></param>
        /// <returns></returns>
        Task<ApiResult> DeleteUnitAsync(long unitid);

        /// <summary>
        /// 修改单位
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<ApiResult> UpdateUnitAsync(UnitDto dto);

        /// <summary>
        /// 查询指定单位信息
        /// </summary>
        /// <param name="unitid"></param>
        /// <returns></returns>
        Task<ApiResult> GetUnitInfoAsync(long unitid);
    }
}
