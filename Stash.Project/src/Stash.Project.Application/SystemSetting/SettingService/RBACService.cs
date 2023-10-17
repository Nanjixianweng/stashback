using AutoMapper;
using Stash.Project.Stash.SystemSetting.Model;
using Stash.Project.SystemSetting.Dto.SettingDto;
using Stash.Project.SystemSetting.ISettingService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;
using Yitter.IdGenerator;

namespace Stash.Project.SystemSetting.SettingService
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
        public async Task<List<RoleInfoDto>> GetRoleListAsync()
        {
            var list = await _role.ToListAsync();
            return ObjectMapper.Map<List<RoleInfo>, List<RoleInfoDto>>(list);
        }

        /// <summary>
        /// 部门列表
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<SectorInfoDto>> GetSectorListAsync(long? fid = 0)
        {
            var list = (await _sector.ToListAsync())
                .WhereIf(fid != 0, x => x.Sector_FatherId == fid);
            return ObjectMapper.Map<List<SectorInfo>, List<SectorInfoDto>>((List<SectorInfo>)list);
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
        public async Task<ApiResult> GetUserListAsync(string? userName, string? jobNember, long Sector_Id, long Role_Id)
        {
            var list = (await _user.ToListAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(userName), x => x.User_RealName.Contains(userName))
                .WhereIf(!string.IsNullOrWhiteSpace(jobNember), x => x.User_JobNumber.Contains(jobNember))
                .WhereIf(Sector_Id != 0, x => x.Sector_Id == Sector_Id)
                .WhereIf(Role_Id != 0, x => x.Role_Id == Role_Id);
            return new ApiResult
            {
                code = ResultCode.Success,
                msg = ResultMsg.RequestSuccess,
                data = list,
                count = 0
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