﻿using AutoMapper;
using Stash.Project.IBasicService;
using Stash.Project.IBasicService.BasicDto;
using Stash.Project.Stash.BasicData.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Yitter.IdGenerator;

namespace Stash.Project.BasicService
{
    public class StorageLocationService : ApplicationService, IStorageLocationService
    {
        public readonly IRepository<StorageLocationTable, long> _storage;
        public readonly IMapper _mapper;

        public StorageLocationService(IRepository<StorageLocationTable, long> storage, IMapper mapper)
        {
            _storage = storage;
            _mapper = mapper;
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
        /// <param name="storageid"></param>
        /// <returns></returns>
        public async Task<ApiResult> DeleteStorageLocationAsync(long storageid)
        {
            var res = await _storage.FirstOrDefaultAsync(x => x.Id == storageid);

            await _storage.DeleteAsync(storageid);

            if (res != null)
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
        public async Task<ApiResult> CreateStorageLocationListAsync(StorageLocationinquireDto dto)
        {
            var list = (await _storage.GetListAsync())
                .WhereIf(dto.storagelocationid != 0, x => x.Id == dto.storagelocationid)
                .WhereIf(!string.IsNullOrEmpty(dto.storagelocationname), x => x.LibraryLocationName.Contains(dto.storagelocationname))
                .WhereIf(dto.storeid != 0, x => x.Stash == dto.storeid);


            var totalcount = list.Count();

            list = list.OrderByDescending(x=>x.CreationTime).Skip((dto.pageIndex - 1) * dto.pageSize).Take(dto.pageSize).ToList();

            if (list == null)
            {
                return new ApiResult { code = ResultCode.Error, msg = ResultMsg.RequestError, data = list, count = totalcount };
            }
            return new ApiResult { code = ResultCode.Success, msg = ResultMsg.RequestSuccess, data = list, count = totalcount };
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
