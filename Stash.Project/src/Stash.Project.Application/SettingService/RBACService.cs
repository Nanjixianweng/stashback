using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
        public readonly IRepository<AccessInfo, long> _access;
        public readonly IRepository<RoleAccessInfo, long> _roleaccess;
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
        public async Task<ApiResult> CreateUserAsync(UserInfoCreateDto dto)
        {
            YitIdHelper.SetIdGenerator(new IdGeneratorOptions());

            dto.Id = YitIdHelper.NextId();

            dto.User_JobNumber = DateTime.Now.ToString("yyyyMMddHHmmss");

            var info = _mapper.Map<UserInfoCreateDto, UserInfo>(dto);

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
        /// 编辑用户
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<ApiResult> UpdateUserAsync(UserInfoCreateDto dto)
        {
            var info = _mapper.Map<UserInfoCreateDto, UserInfo>(dto);
            var res = await _user.UpdateAsync(info);
            return new ApiResult
            {
                code = ResultCode.Success,
                msg = ResultMsg.UpdateSuccess,
                data = res
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

        ///// <summary>
        ///// 获取用户权限列表
        ///// </summary>
        ///// <param name="uid"></param>
        ///// <returns></returns>
        //public async Task<ApiResult> GetUserAccessAsync(long uid)
        //{
        //    var userInfo = await _user.FirstOrDefaultAsync(x => x.Id == uid);
        //    var menu = await _roleaccess.GetListAsync(x => x.Role_Id == userInfo.Role_Id);
        //    List<AccessInfoDto> result = new List<AccessInfoDto>();
        //    foreach (var item in menu)
        //    {
        //        var alist = await _access.FirstOrDefaultAsync(x => x.Id == item.Access_Id);
        //        AccessInfoDto access = new AccessInfoDto();
        //        access.Id = item.Id;
        //        access.Access_Name = item;
        //        access.Access_FatherId = item.Access_FatherId;
        //        access.Access_Type = item.
        //        access.Access_Icon = item.
        //        access.Access_Sort = item.
        //        access.Access_Route = item.
        //        access.Access_CreateTime= item.
        //        access.Access_Button= item.
        //        access.AIDtoList = item.
        //    }

        //    return new ApiResult
        //    {
        //        code = ResultCode.Success,
        //        msg = ResultMsg.RequestSuccess,
        //        data = list
        //    };
        //}

        ///// <summary>
        ///// 获取权限父级菜单
        ///// </summary>
        ///// <param name="fid"></param>
        ///// <returns></returns>
        //private List<AccessInfoDto> GetAccessFidAsync(List<AccessInfoDto> menus,int fid)
        //{
        //    var list = menus
        //        .Where(x => x.Access_FatherId == fid)
        //        .Select(a => new AccessInfoDto
        //        {
        //            Id=a.Id,
        //            Access_Name=a.Access_Name,
        //            Access_FatherId=a.Access_FatherId,
        //            Access_Type = a.Access_Type,
        //            Access_Icon = a.Access_Icon,
        //            Access_Sort = a.Access_Sort,
        //            Access_Route = a.Access_Route,
        //            Access_CreateTime= a.Access_CreateTime,
        //            Access_Button= a.Access_Button,
        //            AIDtoList= GetAccessFidAsync(menus, fid)
        //        });
        //    return list.ToList();
        //}

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
        public async Task<ApiResult> QuerySectorAsync(string? sectorName, string? remark)
        {
            var list = (await _sector.ToListAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(sectorName), x => x.Sector_Name.Contains(sectorName))
                .WhereIf(!string.IsNullOrWhiteSpace(remark), x => x.Sector_Remark.Contains(remark));
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