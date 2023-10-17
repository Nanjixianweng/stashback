using Stash.Project.IBasicService.BasicDto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Stash.Project.IBasicService
{
    public interface IProductService : IApplicationService
    {
        /// <summary>
        /// 产品新增
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<ApiResult> CreateStoreAsync(ProductDto dto);

        /// <summary>
        /// 产品查询
        /// </summary>
        /// <returns></returns>
        Task<ApiResult> GetStoreAsync();

        /// <summary>
        /// 产品条件查询
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<ApiResult> GetStoreListAsync(ProductInquireDto dto);

        /// <summary>
        /// 删除产品
        /// </summary>
        /// <param name="productid"></param>
        /// <returns></returns>
        Task<ApiResult> DeleteStoreAsync(long productid);

        /// <summary>
        /// 修改产品
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<ApiResult> UpdateStoreAsync(ProductDto dto);

        /// <summary>
        /// 查询指定产品信息
        /// </summary>
        /// <param name="productid"></param>
        /// <returns></returns>
        Task<ApiResult> GetStoreInfoAsync(long productid);
    }
}
