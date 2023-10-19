using AutoMapper;
using Stash.Project.IBasicService;
using Stash.Project.IBasicService.BasicDto;
using Stash.Project.Stash.BasicData.Model;
using Stash.Project.Stash.Dictionary.Model;
using Stash.Project.Stash.SystemSetting.Model;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Yitter.IdGenerator;

namespace Stash.Project.BasicService
{
    public class StoreService : ApplicationService, IStoreService
    {
        public readonly IRepository<StoreTale, long> _store;
        public readonly IRepository<DictionaryTable, long> _dictionary;
        public readonly IRepository<SectorInfo, long> _scetor;
        public readonly IMapper _mapper;

        public StoreService(IRepository<StoreTale, long> store, IMapper mapper, IRepository<DictionaryTable, long> dictionary, IRepository<SectorInfo, long> scetor)
        {
            _store = store;
            _mapper = mapper;
            _dictionary = dictionary;
            _scetor = scetor;
        }

        /// <summary>
        /// 仓库新增
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<ApiResult> CreateStoreAsync(StoreDto dto)
        {
            YitIdHelper.SetIdGenerator(new IdGeneratorOptions());
            dto.Id = YitIdHelper.NextId();
            dto.DefaultrorNot = false;
            dto.WhethertoDisable = false;
            var info = _mapper.Map<StoreDto, StoreTale>(dto);
            var res = await _store.InsertAsync(info);
            if (res == null)
            {
                return new ApiResult { code = ResultCode.Error, msg = ResultMsg.AddError, data = res };
            }
            return new ApiResult { code = ResultCode.Success, msg = ResultMsg.AddSuccess, data = res };
        }

        /// <summary>
        /// 删除仓库
        /// </summary>
        /// <param name="storeid"></param>
        /// <returns></returns>
        public async Task<ApiResult> DeleteStoreAsync(long storeid)
        {
            var res = await _store.FirstOrDefaultAsync(x => x.Id == storeid);

            await _store.DeleteAsync(storeid);

            if(res != null)
            {
                return new ApiResult
                {
                    code = ResultCode.Success,
                    msg = ResultMsg.DeleteSuccess,
                    data = res
                };
            }

            return new ApiResult
            {
                code = ResultCode.Error,
                msg = ResultMsg.DeleteError,
                data = res
            };

        }

        /// <summary>
        /// 查询指定仓库信息
        /// </summary>
        /// <param name="storeid"></param>
        /// <returns></returns>
        public async Task<ApiResult> GetStoreInfoAsync(long storeid)
        {
            var res = await _store.FirstOrDefaultAsync(x => x.Id == storeid);

            return new ApiResult
            {
                code = ResultCode.Success,
                msg = ResultMsg.RequestSuccess,
                data = res
            };
        }

        /// <summary>
        /// 仓库条件查询
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<ApiResult> GetStoreListAsync(StoreinquireDto dto)
        {
            var store = await _store.GetListAsync();
            var dictionary = await _dictionary.GetListAsync();
            var scetor = await _scetor.GetListAsync();

            var list = from a in store
                       join b in dictionary
                       on a.StoreType equals b.Id
                       join c in scetor
                       on a.DepartmentId equals c.Id
                       where (dto.number == 0 || a.Id.Equals(dto.number)) &&
                       (string.IsNullOrEmpty(dto.name) || a.StoreName.Contains(dto.name)) &&
                       (dto.departmentid == 0 || a.DepartmentId.Equals(dto.departmentid)) &&
                       (dto.storetype == 0 || a.StoreType.Equals(dto.storetype))
                       select new ShowStoreDto
                       {
                           Id = a.Id,
                           StoreName = a.StoreName,
                           LeaseTime = a.LeaseTime,
                           StoreType = a.StoreType,
                           StoreTypeName = b.Dictionary_Name,
                           DepartmentId = a.DepartmentId,
                           DepartmentName = c.Sector_Name,
                           Effect = a.Effect,
                           Area = a.Area,
                           Address = a.Address,
                           ContactPerson = a.ContactPerson,
                           TelePhone = a.TelePhone,
                           WhethertoDisable = a.WhethertoDisable,
                           DefaultrorNot = a.DefaultrorNot
                       };


            var totalcount = list.Count();

            var res = list.Skip((dto.pageIndex - 1) * dto.pageSize).Take(dto.pageSize).ToList();

            return new ApiResult { code = ResultCode.Success, msg = ResultMsg.RequestSuccess, data = res, count = totalcount };
        }

        /// <summary>
        /// 修改仓库
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<ApiResult> UpdateStoreAsync(StoreDto dto)
        {
            var info = _mapper.Map<StoreDto, StoreTale>(dto);
            var res = await _store.UpdateAsync(info);
            if (res == null)
            {
                return new ApiResult { code = ResultCode.Error, msg = ResultMsg.UpdateError, data = res };
            }
            return new ApiResult { code = ResultCode.Success, msg = ResultMsg.UpdateSuccess, data = res };
        }

        /// <summary>
        /// 仓库查询
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult> GetStoreAsync()
        {
            var list = await _store.GetListAsync();

            return new ApiResult { code=ResultCode.Success,msg=ResultMsg.RequestSuccess,data = list};
        }

        /// <summary>
        /// 字典表查询
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult> GetDictionaryAsync()
        {
            var list = await _dictionary.GetListAsync();

            return new ApiResult { code = ResultCode.Success, msg = ResultMsg.RequestSuccess, data = list };
        }
    }
}
