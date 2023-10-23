using Stash.Project.ISystemSetting;
using Stash.Project.ISystemSetting.SettingDto;
using Stash.Project.Stash.SystemSetting.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;
using Yitter.IdGenerator;

namespace Stash.Project.SettingService
{
    /// <summary>
    /// RBAC控制器
    /// </summary>
    public class RBACService : ApplicationService, IRBACService
    {
        private readonly IRepository<UserInfo, long> _user;
        private readonly IRepository<SectorInfo, long> _sector;
        private readonly IRepository<RoleInfo, long> _role;
        private readonly IRepository<AccessInfo, long> _access;
        private readonly IRepository<RoleUserInfo, long> _roleuser;
        private readonly IRepository<RoleAccessInfo, long> _roleaccess;

        public RBACService(IRepository<UserInfo, long> user, IRepository<SectorInfo, long> sector, IRepository<RoleInfo, long> role, IRepository<RoleUserInfo, long> roleuser, IRepository<RoleAccessInfo, long> roleaccess, IRepository<AccessInfo, long> access)
        {
            _user = user;
            _sector = sector;
            _role = role;
            _roleuser = roleuser;
            _roleaccess = roleaccess;
            _access = access;
        }

        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<ApiResult> CreateUserAsync(UserInfoCreateDto dto)
        {
            YitIdHelper.SetIdGenerator(new IdGeneratorOptions());

            dto.Id = YitIdHelper.NextId();
            dto.User_JobNumber = DateTime.Now.ToString("yyyyMMddHHmmss");

            var info = ObjectMapper.Map<UserInfoCreateDto, UserInfo>(dto);
            var res = await _user.InsertAsync(info);

            var userRole = new RoleUserInfoDto();
            userRole.Id = YitIdHelper.NextId();
            userRole.User_Id = dto.Id;
            userRole.Role_Id = dto.Role_Id;

            var ruinfo = ObjectMapper.Map<RoleUserInfoDto, RoleUserInfo>(userRole);
            var urres = await _roleuser.InsertAsync(ruinfo);
          

            return new ApiResult
            {
                code = ResultCode.Success,
                msg = ResultMsg.AddSuccess,
                data = res
            };
        }

        /// <summary>
        /// 编辑用户
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<ApiResult> UpdateUserAsync(UserInfoCreateDto dto)
        {

            var ruinfo = await _roleuser.FindAsync(x => x.User_Id == dto.Id);
            ruinfo.User_Id = dto.Id;
            ruinfo.Role_Id= dto.Role_Id;
            await _roleuser.UpdateAsync(ruinfo);

            var info = ObjectMapper.Map<UserInfoCreateDto, UserInfo>(dto);
            await _user.UpdateAsync(info);
            return new ApiResult
            {
                code = ResultCode.Success,
                msg = ResultMsg.UpdateSuccess,
                data = info
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
        /// 用户列表
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<ApiResult> GetUserListAsync(string? userName, string? jobNember, long? sectorId, long? roleId, int pageIndex, int pageSize)
        {
            var user = await _user.GetListAsync();
            var role = await _role.GetListAsync();
            var sector = await _sector.GetListAsync();
            var list = from u in user
                       join r in role on u.Role_Id equals r.Id
                       join s in sector on u.Sector_Id equals s.Id
                       where (string.IsNullOrWhiteSpace(userName) || u.User_RealName.Contains(userName))
                        && (string.IsNullOrWhiteSpace(jobNember) || u.User_JobNumber.Contains(jobNember))
                        && (sectorId == 0 || sectorId == null || u.Sector_Id == sectorId)
                        && (roleId == 0 || roleId == null || u.Role_Id == roleId)
                        && u.User_IsDel == false
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
        /// 批量删除用户
        /// </summary>
        /// <param name="Ids">字符编号</param>
        /// <returns></returns>
        public async Task<ApiResult> DeleteBatchAsync(string Ids)
        {
            var id = Ids.Split(',');

            foreach (var item in id)
            {
                var obj = await _user.FirstOrDefaultAsync(x => x.Id == Convert.ToInt64(item));

                obj.User_IsDel = true;

                _user.UpdateAsync(obj);
            }

            return new ApiResult()
            {
                code = ResultCode.Success,
                count = 0,
                data = null,
                msg = ResultMsg.RequestSuccess
            };
        }

        /// <summary>
        /// 递归查询
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public async Task<List<AccessInfoDto>> GetRBACAsync(long uid)
        {
            var roleUserList = await _roleuser.GetListAsync();
            var roleAccessList = await _roleaccess.GetListAsync();
            var accessList = await _access.GetListAsync();

            var list = from a in roleUserList
                       join b in roleAccessList on a.Role_Id equals b.Role_Id
                       join c in accessList on b.Access_Id equals c.Id
                       where a.User_Id == uid
                       select c;

            var alllist = list.Select(x => new AccessInfoDto
            {
                Access_Button = x.Access_Button,
                Access_CreateTime = x.Access_CreateTime,
                Access_FatherId = x.Access_FatherId,
                Access_Icon = x.Access_Icon,
                Access_Name = x.Access_Name,
                Access_Route = x.Access_Route,
                Access_Sort = x.Access_Sort,
                Access_Type = x.Access_Type,
                Id = x.Id,
                AIDtoList = null
            }).Distinct().ToList();

            return await GetRBACA2sync(alllist, 0);
        }

        public async Task<List<AccessInfoDto>> GetRBACA2sync(List<AccessInfoDto> obj, long Pid)
        {
            var tasks = obj.Where(x => x.Access_FatherId == Pid)
                           .Select(async x =>
                           {
                               var newAccessInfo = new AccessInfoDto
                               {
                                   Access_FatherId = x.Access_FatherId,
                                   Access_Button = x.Access_Button,
                                   Access_CreateTime = x.Access_CreateTime,
                                   Access_Icon = x.Access_Icon,
                                   Access_Name = x.Access_Name,
                                   Access_Route = x.Access_Route,
                                   Access_Sort = x.Access_Sort,
                                   Access_Type = x.Access_Type,
                                   Id = x.Id,
                                   AIDtoList = await GetRBACA2sync(obj, x.Id)
                               };
                               return newAccessInfo;
                           }).ToList();
            var result = await Task.WhenAll(tasks);
            return result.ToList();
        }

        #region 部门CRUD

        /// <summary>
        /// 新增部门
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<ApiResult> CreateSectorAsync(SectorInfoDto dto)
        {
            YitIdHelper.SetIdGenerator(new IdGeneratorOptions());
            dto.Id = YitIdHelper.NextId();
            dto.Sector_CreateTime = DateTime.Now;
            dto.Sector_Remark = dto.Sector_CreateTime.ToString("yyyyMMdd") + "-" + dto.Sector_Name + "-" + dto.Sector_FatherId;
            dto.Sector_IsDel = false;
            var info = ObjectMapper.Map<SectorInfoDto, SectorInfo>(dto);
            var res = await _sector.InsertAsync(info);

            return new ApiResult
            {
                code = ResultCode.Success,
                msg = ResultMsg.AddSuccess,
                data = res
            };
        }

        /// <summary>
        /// 部门查询
        /// </summary>
        /// <param name="sectorName"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        public async Task<ApiResult> GetQuerySectorAsync(string? sectorName, string? remark)
        {
            var list = (await _sector.ToListAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(sectorName), x => x.Sector_Name.Contains(sectorName))
                .WhereIf(!string.IsNullOrWhiteSpace(remark), x => x.Sector_Remark.Contains(remark))
                .Where(x => x.Sector_IsDel == false);
            return new ApiResult
            {
                code = ResultCode.Success,
                msg = ResultMsg.RequestSuccess,
                data = list,
                count = 0,
            };
        }

        /// <summary>
        /// 部门信息反填
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public async Task<ApiResult> GetSectorInfoAsync(long sid)
        {
            var info = await _sector.FirstOrDefaultAsync(x => x.Id == sid);
            return new ApiResult
            {
                code = ResultCode.Success,
                msg = ResultMsg.RequestSuccess,
                data = info
            };
        }

        /// <summary>
        /// 编辑部门
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<ApiResult> UpdateSectorAsync(SectorInfoDto dto)
        {
            var info = ObjectMapper.Map<SectorInfoDto, SectorInfo>(dto);
            var res = await _sector.UpdateAsync(info);
            return new ApiResult
            {
                code = ResultCode.Success,
                msg = ResultMsg.UpdateSuccess,
                data = res
            };
        }

        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public async Task<ApiResult> DeleteSectorAsync(long sid)
        {
            var info = await _sector.FirstOrDefaultAsync(x => x.Id == sid);
            info.Sector_IsDel = true;
            var res = await _sector.UpdateAsync(info);
            return new ApiResult
            {
                code = ResultCode.Success,
                msg = ResultMsg.DeleteSuccess,
                data = res
            };
        }

        #endregion 部门CRUD
    }
}