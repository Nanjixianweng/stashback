using AutoMapper;
using Stash.Project.ISystemSetting;
using Stash.Project.ISystemSetting.SettingDto;
using Stash.Project.Stash.SystemSetting.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Yitter.IdGenerator;

namespace Stash.Project.SettingService
{
    public class RBACService : ApplicationService, IRBACService
    {
        public readonly IRepository<UserInfo, long> _user;
        public readonly IRepository<SectorInfo, long> _sector;
        public readonly IRepository<RoleInfo, long> _role;
        public readonly IMapper _mapper;

        public RBACService(IRepository<UserInfo, long> user, IRepository<SectorInfo, long> sector, IRepository<RoleInfo, long> role, IMapper mapper)
        {
            _user = user;
            _sector = sector;
            _role = role;
            _mapper = mapper;
        }

        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<ApiResult> CreateUserAsync(UserInfoDto dto)
        {
            YitIdHelper.SetIdGenerator(new IdGeneratorOptions());
            dto.Id = YitIdHelper.NextId();
            var info = _mapper.Map<UserInfoDto, UserInfo>(dto);
            var res = await _user.InsertAsync(info);
            return new ApiResult
            {
                code = ResultCode.Success,
                msg = ResultMsg.AddSuccess,
                data = res
            };
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="rid"></param>
        /// <returns></returns>
        public async Task<ApiResult> DeleteRoleAsync(long rid)
        {
            var role = await _role.GetAsync(rid);
            if (role != null)
            {
                await _role.DeleteAsync(role);
                return new ApiResult
                {
                    code = ResultCode.Success,
                    msg = ResultMsg.DeleteSuccess,
                };
            }
            else
            {
                return new ApiResult
                {
                    code = ResultCode.Error,
                    msg = ResultMsg.DeleteError,
                };
            }
        }

        /// <summary>
        /// 逻辑删除用户
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public async Task<ApiResult> DeleteUserAsync(long uid)
        {
            var info = await _user.FirstOrDefaultAsync(x => x.Id == uid);
            info.User_IsDel = true;
            var res = await _user.UpdateAsync(info);
            return new ApiResult
            {
                code = ResultCode.Success,
                msg = ResultMsg.DeleteSuccess,
                data = res
            };
        }

        /// <summary>
        /// 角色列表
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult> GetRoleListAsync()
        {
            var list = await _role.ToListAsync();
            return new ApiResult
            {
                code = ResultCode.Success,
                msg = ResultMsg.RequestSuccess,
                data = list
            };
        }

        /// <summary>
        /// 部门列表
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ApiResult> GetSectorListAsync(long? fid = 0)
        {
            var list = (await _sector.ToListAsync())
                .WhereIf(fid != 0, x => x.Sector_FatherId == fid);
            return new ApiResult
            {
                code = ResultCode.Success,
                msg = ResultMsg.RequestSuccess,
                data = list
            };
        }

        /// <summary>
        /// 用户信息反填
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public async Task<ApiResult> GetUserInfoAsync(long uid)
        {
            var info = await _user.FirstOrDefaultAsync(x => x.Id == uid);
            return new ApiResult
            {
                code = ResultCode.Success,
                msg = ResultMsg.RequestSuccess,
                data = info
            };
        }

        /// <summary>
        /// 用户列表
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<ApiResult> GetUserListAsync(string? userName, string? jobNember, long sectorId, long roleId, int pageIndex, int pageSize)
        {
            var user = await _user.GetListAsync();
            var role = await _role.GetListAsync();
            var sector = await _sector.GetListAsync();
            var list = from u in user
                       join r in role on u.Role_Id equals r.Id
                       join s in sector on u.Sector_Id equals s.Id
                       where (string.IsNullOrWhiteSpace(userName) || u.User_RealName.Contains(userName))
                        && (string.IsNullOrWhiteSpace(jobNember) || u.User_JobNumber.Contains(jobNember))
                        && (sectorId == 0 || u.Sector_Id == sectorId)
                        && (roleId == 0 || u.Role_Id == roleId)
                       select new
                       {
                           id = u.Id,
                           user_Name = u.User_Name,
                           user_Password = u.User_Password,
                           user_JobNumber = u.User_JobNumber,
                           user_RealName = u.User_RealName,
                           user_Email = u.User_Email,
                           user_Mobilephone = u.User_Mobilephone,
                           user_LoginNum = u.User_LoginNum,
                           user_IsLock = u.User_IsLock,
                           user_IsDel = u.User_IsDel,
                           user_CreateTime = u.User_CreateTime,
                           user_Telephone = u.User_Telephone,
                           user_Remarks = u.User_Remarks,
                           sector_Id = u.Sector_Id,
                           sector_Name = s.Sector_Name,
                           role_Id = u.Role_Id,
                           role_Name = r.Role_Name
                       };
            int dataCount = list.Count();

            list = list.Skip((pageIndex - 1) * pageSize).Take(pageSize);

            return new ApiResult
            {
                code = ResultCode.Success,
                msg = ResultMsg.RequestSuccess,
                data = list,
                count = dataCount,
            };
        }

        /// <summary>
        /// 编辑用户
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<ApiResult> UpdateUserAsync(UserInfoDto dto)
        {
            var info = _mapper.Map<UserInfoDto, UserInfo>(dto);
            var res = await _user.UpdateAsync(info);
            return new ApiResult
            {
                code = ResultCode.Success,
                msg = ResultMsg.UpdateSuccess,
                data = res
            };
        }
    }
}