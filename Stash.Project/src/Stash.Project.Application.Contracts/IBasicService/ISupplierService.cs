using Stash.Project.IBasicService.BasicDto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Stash.Project.IBasicService
{
    public interface ISupplierService : IApplicationService
    {
        /// <summary>
        /// 供应商新增
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<ApiResult> CreateSupplierAsync(SupplierDto dto);

        /// <summary>
        /// 供应商查询
        /// </summary>
        /// <returns></returns>
        Task<ApiResult> GetSupplierAsync();

        /// <summary>
        /// 供应商条件查询
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<ApiResult> GetSupplierListAsync(SupplierInquireDto dto);

        /// <summary>
        /// 删除供应商
        /// </summary>
        /// <param name="supplierid"></param>
        /// <returns></returns>
        Task<ApiResult> DeleteSupplierAsync(long supplierid);

        /// <summary>
        /// 修改供应商
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<ApiResult> UpdateSupplierAsync(SupplierDto dto);

        /// <summary>
        /// 查询指定供应商信息
        /// </summary>
        /// <param name="supplierid"></param>
        /// <returns></returns>
        Task<ApiResult> GetSupplierInfoAsync(long supplierid);
    }
}
