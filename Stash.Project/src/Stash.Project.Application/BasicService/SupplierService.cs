using AutoMapper;
using Stash.Project.IBasicService;
using Stash.Project.IBasicService.BasicDto;
using Stash.Project.Stash.BasicData.Model;
using Stash.Project.Stash.Dictionary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        public readonly IRepository<DictionaryTable, long> _dictionary;
        public readonly IMapper _mapper;

        public SupplierService(IMapper mapper, IRepository<SupplierTable, long> supplier,IRepository<DictionaryTable, long> dictionary)
        {
            _mapper = mapper;
            _supplier = supplier;
            _dictionary = dictionary;
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
        public async Task<ApiResult> GetSupplierListAsync(SupplierInquireDto dto)
        {
            var supplier = await _supplier.GetListAsync();
            var dictionary = await _dictionary.GetListAsync();

            var list = from a in supplier
                        join b in dictionary
                        on a.SupplierType equals b.Id
                        where (dto.supplierid == 0 || a.Id.Equals(dto.supplierid)) &&
                        (string.IsNullOrEmpty(dto.suppliername) || a.SupplierName.Contains(dto.suppliername)) &&
                        (dto.suppliertype == 0 || a.SupplierType.Equals(dto.suppliertype)) &&
                        (string.IsNullOrEmpty(dto.telephone) || a.Telephone.Contains(dto.telephone))
                        select new ShowSupplierDto
                        {
                            Id = a.Id,
                            SupplierName = a.SupplierName,
                            SupplierType = a.SupplierType,
                            SupplierTypeName = b.Dictionary_Name,
                            Telephone = a.Telephone,
                            Fax = a.Fax,
                            Mailbox = a.Mailbox,
                            ContactPerson = a.ContactPerson,
                            Address = a.Address,
                            CreationTime = a.CreationTime,
                            Description = a.Description,
                        };

            var totalcount = list.Count();

            var res = list.OrderByDescending(x=>x.CreationTime).Skip((dto.pageIndex - 1) * dto.pageSize).Take(dto.pageSize).ToList();

            return new ApiResult { code = ResultCode.Success, msg = ResultMsg.RequestSuccess, data = res, count = totalcount };
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
