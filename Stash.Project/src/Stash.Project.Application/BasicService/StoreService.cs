using AutoMapper;
using Stash.Project.BasicDto;
using Stash.Project.IBasicService;
using Stash.Project.Stash.BasicData.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Yitter.IdGenerator;

namespace Stash.Project.BasicService
{
    public class StoreService : ApplicationService, IStoreService
    {
        public readonly IRepository<StoreTale, long> _store;
        public readonly IMapper _mapper;

        public StoreService(IRepository<StoreTale, long> store, IMapper mapper)
        {
            _store = store;
            _mapper = mapper;
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
            var info = _mapper.Map<StoreDto, StoreTale>(dto);
            var res = await _store.InsertAsync(info);
            if (res == null)
            {
                return new ApiResult { code = ResultCode.Error, msg = ResultMsg.AddError, data = res };
            }
            return new ApiResult { code = ResultCode.Success, msg = ResultMsg.AddSuccess, data = res };
        }

        /// <summary>
        /// 删除仓储
        /// </summary>
        /// <param name="storeid"></param>
        /// <returns></returns>
        public async Task<ApiResult> DeleteStoreAsync(long storeid)
        {
            await _store.DeleteAsync(storeid);

            var res = await _store.FirstOrDefaultAsync(x => x.Id == storeid);

            if (res == null)
            {
                return new ApiResult
                {
                    code = ResultCode.Error,
                    msg = ResultMsg.DeleteError,
                    data = res
                };
            }

            return new ApiResult
            {
                code = ResultCode.Success,
                msg = ResultMsg.DeleteSuccess,
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
            var list = (await _store.ToListAsync())
                .WhereIf(dto.number != 0, x => x.Id == dto.number)
                .WhereIf(!string.IsNullOrEmpty(dto.name), x => x.StoreName.Contains(dto.name))
                .WhereIf(dto.departmentid != 0, x => x.DepartmentId == dto.departmentid)
                .WhereIf(dto.storetype != 0, x => x.StoreType == dto.storetype);


            var totalcount = list.Count();

            list = list.Skip((dto.pageIndex - 1) * dto.pageSize).Take(dto.pageSize).ToList();

            if (list == null)
            {
                return new ApiResult { code = ResultCode.Error, msg = ResultMsg.RequestError, data = list, count = totalcount };
            }
            return new ApiResult { code = ResultCode.Success, msg = ResultMsg.RequestSuccess, data = list, count = totalcount };
        }

        /// <summary>
        /// 修改仓储
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
    }
}
