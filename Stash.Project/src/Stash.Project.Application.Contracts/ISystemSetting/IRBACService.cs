using Stash.Project.ISystemSetting.SettingDto;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Stash.Project.ISystemSetting
{
    public interface IRBACService : IApplicationService
    {
        /// <summary>
        /// 部门列表
        /// </summary>
        /// <returns></returns>
        Task<ApiResult> GetSectorListAsync(long? fid);

        /// <summary>
        /// 角色列表
        /// </summary>
        /// <returns></returns>
        Task<ApiResult> GetRoleListAsync();

        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<ApiResult> CreateUserAsync(UserInfoCreateDto dto);

        /// <summary>
        /// 用户列表查询
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<ApiResult> GetUserListAsync(string? userName, string? jobNember, long?sectorId, long?roleId,int pageIndex,int pageSize);

        /// <summary>
        /// 用户信息反填
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        Task<ApiResult> GetUserInfoAsync(long uid);

        /// <summary>
        /// 编辑用户
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<ApiResult> UpdateUserAsync(UserInfoCreateDto dto);

        /// <summary>
        /// 逻辑删除用户
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        Task<ApiResult> DeleteUserAsync(long uid);

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="rid"></param>
        /// <returns></returns>
        Task<ApiResult> DeleteRoleAsync(long rid);
        /// <summary>
        /// 批量删除用户
        /// </summary>
        /// <param name="Ids">字符编号</param>
        /// <returns></returns>
        Task<ApiResult> DeleteBatchAsync(string Ids);


        #region 部门CRUD

        /// <summary>
        /// 新增部门
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<ApiResult> CreateSectorAsync(SectorInfoDto dto);

        /// <summary>
        /// 部门查询
        /// </summary>
        /// <param name="sectorName"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        Task<ApiResult> QuerySectorAsync(string? sectorName, string? remark);

        /// <summary>
        /// 部门信息反填
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        Task<ApiResult> GetSectorInfoAsync(long sid);

        /// <summary>
        /// 编辑部门
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<ApiResult> UpdateSectorAsync(SectorInfoDto dto);

        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        Task<ApiResult> DeleteSectorAsync(long sid);

        #endregion

    }
}