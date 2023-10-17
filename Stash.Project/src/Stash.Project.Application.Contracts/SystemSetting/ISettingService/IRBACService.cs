using Stash.Project.SystemSetting.Dto.SettingDto;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Stash.Project.SystemSetting.ISettingService
{
    public interface IRBACService : IApplicationService
    {
        /// <summary>
        /// 部门列表
        /// </summary>
        /// <returns></returns>
        Task<List<SectorInfoDto>> GetSectorListAsync(long? fid);

        /// <summary>
        /// 角色列表
        /// </summary>
        /// <returns></returns>
        Task<List<RoleInfoDto>> GetRoleListAsync();

        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<ApiResult> CreateUserAsync(UserInfoDto dto);

        /// <summary>
        /// 用户列表查询
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<ApiResult> GetUserListAsync(string? userName, string? jobNember, long Sector_Id, long Role_Id);

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
        Task<ApiResult> UpdateUserAsync(UserInfoDto dto);

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
    }
}