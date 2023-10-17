using Stash.Project.IBasicService.BasicDto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Stash.Project.IBasicService
{
    public interface ICustomerService : IApplicationService
    {
        /// <summary>
        /// 客户新增
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<ApiResult> CreateCustomerAsync(CustomerDto dto);

        /// <summary>
        /// 客户数据
        /// </summary>
        /// <returns></returns>
        Task<ApiResult> GetCustomerAsync();

        /// <summary>
        /// 客户条件查询
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<ApiResult> CreateCustomerListAsync(CustomerinquireDto dto);

        /// <summary>
        /// 删除客户
        /// </summary>
        /// <param name="customerid"></param>
        /// <returns></returns>
        Task<ApiResult> DeleteCustomerAsync(long customerid);

        /// <summary>
        /// 修改客户
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<ApiResult> UpdateCustomerAsync(CustomerDto dto);

        /// <summary>
        /// 查询指定客户信息
        /// </summary>
        /// <param name="customerid"></param>
        /// <returns></returns>
        Task<ApiResult> GetCustomerInfoAsync(long customerid);
    }
}
