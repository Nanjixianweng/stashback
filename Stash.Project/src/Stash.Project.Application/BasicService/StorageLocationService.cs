using AutoMapper;
using Stash.Project.IBasicService;
using Stash.Project.IBasicService.BasicDto;
using Stash.Project.Stash.BasicData.Model;
using Stash.Project.Stash.Dictionary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Yitter.IdGenerator;

namespace Stash.Project.BasicService
{
    /// <summary>
    /// 库位控制器
    /// </summary>
    public class StorageLocationService : ApplicationService, IStorageLocationService
    {
        public readonly IRepository<StorageLocationTable, long> _storage;
        public readonly IRepository<StoreTale, long> _store;
        public readonly IRepository<DictionaryTable, long> _dictionary;
        public readonly IMapper _mapper;

        public StorageLocationService(IRepository<StorageLocationTable, long> storage, IMapper mapper, IRepository<StoreTale, long> store, IRepository<DictionaryTable, long> dictionary)
        {
            _storage = storage;
            _mapper = mapper;
            _store = store;
            _dictionary = dictionary;
        }

        /// <summary>
        /// 库位新增
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<ApiResult> CreateStorageLocationAsync(StorageLocationDto dto)
        {
            YitIdHelper.SetIdGenerator(new IdGeneratorOptions());
            dto.Id = YitIdHelper.NextId();
            dto.CreationTime = DateTime.Now;
            var info = _mapper.Map<StorageLocationDto, StorageLocationTable>(dto);
            var res = await _storage.InsertAsync(info);
            if (res == null)
            {
                return new ApiResult { code = ResultCode.Error, msg = ResultMsg.AddError, data = res };
            }
            return new ApiResult { code = ResultCode.Success, msg = ResultMsg.AddSuccess, data = res };
        }

        /// <summary>
        /// 删除库位
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<ApiResult> DeleteStorageLocationAsync(string ids)
        {
            var storageid = ids.Split(',');

            if (storageid == null)
            {
                return new ApiResult
                {
                    code = ResultCode.Error,
                    msg = ResultMsg.RequestError,
                    data = ""
                };
            }

            foreach (var id in storageid)
            {
                await _storage.DeleteAsync(Convert.ToInt64(id));
            }

            return new ApiResult
            {
                code = ResultCode.Success,
                msg = ResultMsg.RequestSuccess,
                data = ""
            };
        }

        /// <summary>
        /// 查询指定库位信息
        /// </summary>
        /// <param name="storageid"></param>
        /// <returns></returns>
        public async Task<ApiResult> GetStorageLocationInfoAsync(long storageid)
        {
            var res = await _storage.FirstOrDefaultAsync(x => x.Id == storageid);

            return new ApiResult
            {
                code = ResultCode.Success,
                msg = ResultMsg.RequestSuccess,
                data = res
            };
        }

        /// <summary>
        /// 库位条件查询
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<ApiResult> GetStorageLocationListAsync(StorageLocationinquireDto dto)
        {
            var storage = await _storage.GetListAsync();
            var store = await _store.GetListAsync();
            var dictionary = await _dictionary.GetListAsync();

            var list = from a in storage
                        join b in store
                        on a.Stash equals b.Id
                        join c in dictionary
                        on a.StorageLocationType equals c.Id
                        where (dto.storagelocationid == 0 || a.Id.Equals(dto.storagelocationid)) &&
                        (string.IsNullOrEmpty(dto.storagelocationname) || a.LibraryLocationName.Contains(dto.storagelocationname)) &&
                        (dto.storeid == 0 || a.Stash.Equals(dto.storeid))
                        select new ShowStorageLocationDto
                        {
                            Id = a.Id,
                            LibraryLocationName = a.LibraryLocationName,
                            StorageLocationType = a.StorageLocationType,
                            StorageLocationTypeName = c.Dictionary_Name,
                            Stash = a.Stash,
                            StoreName = b.StoreName,
                            WhethertoDisable = a.WhethertoDisable,
                            DefaultrorNot = a.DefaultrorNot,
                            CreationTime = a.CreationTime,
                        };


            var totalcount = list.Count();

            var res = list.OrderByDescending(x=>x.CreationTime).Skip((dto.pageIndex - 1) * dto.pageSize).Take(dto.pageSize).ToList();

            return new ApiResult { code = ResultCode.Success, msg = ResultMsg.RequestSuccess, data = res, count = totalcount };
        }

        /// <summary>
        /// 修改库位
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<ApiResult> UpdateStorageLocationAsync(StorageLocationDto dto)
        { 
            var info = _mapper.Map<StorageLocationDto, StorageLocationTable>(dto);
            var res = await _storage.UpdateAsync(info);
            if (res == null)
            {
                return new ApiResult { code = ResultCode.Error, msg = ResultMsg.UpdateError, data = res };
            }
            return new ApiResult { code = ResultCode.Success, msg = ResultMsg.UpdateSuccess, data = res };
        }

        /// <summary>
        /// 库位查询
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult> GetStorageLocationAsync()
        {
            var list = await _storage.GetListAsync();

            return new ApiResult { code = ResultCode.Success, msg = ResultMsg.RequestSuccess, data = list };
        }
    }
}
