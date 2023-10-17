using AutoMapper;
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
    public class SupplierService : ApplicationService, ISupplierService
    {
        public readonly IRepository<SupplierTable, long> _supplier;
        public readonly IMapper _mapper;

        public SupplierService(IMapper mapper,IRepository<SupplierTable,long> supplier)
        {
            _mapper = mapper;
            _supplier = supplier;
        }

        /// <summary>
        /// 供应商新增
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<ApiResult> CreateSupplierAsync(SupplierDto dto)
        {
            YitIdHelper.SetIdGenerator(new IdGeneratorOptions());
            dto.Id = YitIdHelper.NextId();
            dto.CreationTime  = DateTime.Now;
            var info = _mapper.Map<SupplierDto, SupplierTable>(dto);
            var res = await _supplier.InsertAsync(info);
            if (res == null)
            {
                return new ApiResult { code = ResultCode.Error, msg = ResultMsg.AddError, data = res };
            }
            return new ApiResult { code = ResultCode.Success, msg = ResultMsg.AddSuccess, data = res };
        }

        /// <summary>
        /// 删除供应商
        /// </summary>
        /// <param name="supplierid"></param>
        /// <returns></returns>
        public async Task<ApiResult> DeleteSupplierAsync(long supplierid)
        {
            var res = await _supplier.FirstOrDefaultAsync(x => x.Id == supplierid);
            
            await _supplier.DeleteAsync(supplierid);

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
        /// 查询指定供应商信息
        /// </summary>
        /// <param name="supplierid"></param>
        /// <returns></returns>
        public async Task<ApiResult> GetSupplierInfoAsync(long supplierid)
        {
            var res = await _supplier.FirstOrDefaultAsync(x => x.Id == supplierid);

            return new ApiResult
            {
                code = ResultCode.Success,
                msg = ResultMsg.RequestSuccess,
                data = res
            };
        }

        /// <summary>
        /// 供应商条件查询
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<ApiResult> CreateSupplierListAsync(SupplierInquireDto dto)
        {
            var list = (await _supplier.GetListAsync())
                .WhereIf(dto.supplierid != 0, x => x.Id == dto.supplierid)
                .WhereIf(!string.IsNullOrEmpty(dto.suppliername), x => x.SupplierName.Contains(dto.suppliername))
                .WhereIf(dto.suppliertype != 0, x => x.SupplierType == dto.suppliertype)
                .WhereIf(!string.IsNullOrEmpty(dto.telephone),x=>x.Telephone.Contains(dto.telephone));


            var totalcount = list.Count();

            list = list.OrderByDescending(x=>x.CreationTime).Skip((dto.pageIndex - 1) * dto.pageSize).Take(dto.pageSize).ToList();

            if (list == null)
            {
                return new ApiResult { code = ResultCode.Error, msg = ResultMsg.RequestError, data = list, count = totalcount };
            }
            return new ApiResult { code = ResultCode.Success, msg = ResultMsg.RequestSuccess, data = list, count = totalcount };
        }

        /// <summary>
        /// 修改供应商
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<ApiResult> UpdateSupplierAsync(SupplierDto dto)
        {
            var info = _mapper.Map<SupplierDto, SupplierTable>(dto);
            var res = await _supplier.UpdateAsync(info);
            if (res == null)
            {
                return new ApiResult { code = ResultCode.Error, msg = ResultMsg.UpdateError, data = res };
            }
            return new ApiResult { code = ResultCode.Success, msg = ResultMsg.UpdateSuccess, data = res };
        }

        /// <summary>
        /// 供应商查询
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult> GetSupplierAsync()
        {
            var list = await _supplier.GetListAsync();

            return new ApiResult { code = ResultCode.Success, msg = ResultMsg.RequestSuccess, data = list };
        }
    }
}
