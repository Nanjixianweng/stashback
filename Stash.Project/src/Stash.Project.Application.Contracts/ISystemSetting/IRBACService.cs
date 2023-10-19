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
        Task<ApiResult> GetUserListAsync(string? userName, string? jobNember, long sectorId, long roleId);

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

        ///// <summary>
        ///// 获取用户权限列表
        ///// </summary>
        ///// <param name="uid"></param>
        ///// <returns></returns>
        //Task<ApiResult> GetUserAccessAsync(long uid);

    }
}