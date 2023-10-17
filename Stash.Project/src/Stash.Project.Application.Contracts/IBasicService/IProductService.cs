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
        Task<ApiResult> CreateProductAsync(ProductDto dto);

        /// <summary>
        /// 产品查询
        /// </summary>
        /// <returns></returns>
        Task<ApiResult> GetProductAsync();

        /// <summary>
        /// 产品条件查询
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<ApiResult> GetProductListAsync(ProductInquireDto dto);

        /// <summary>
        /// 删除产品
        /// </summary>
        /// <param name="productid"></param>
        /// <returns></returns>
        Task<ApiResult> DeleteProductAsync(long productid);

        /// <summary>
        /// 修改产品
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<ApiResult> UpdateProductAsync(ProductDto dto);

        /// <summary>
        /// 查询指定产品信息
        /// </summary>
        /// <param name="productid"></param>
        /// <returns></returns>
        Task<ApiResult> GetProductInfoAsync(long productid);
    }
}
