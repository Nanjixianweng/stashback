using Stash.Project.IBasicService.BasicDto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Stash.Project.IBasicService
{
    public interface IProductCategoryService : IApplicationService
    {
        /// <summary>
        /// 产品类别新增
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<ApiResult> CreateProductCategoryAsync(ProductCategoryDto dto);

        /// <summary>
        /// 产品类别条件查询
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<ApiResult> CreateProductCategoryListAsync(ProductCategoryInquireDto dto);

        /// <summary>
        /// 产品类别查询
        /// </summary>
        /// <returns></returns>
        Task<ApiResult> GetProductCategoryAsync();

        /// <summary>
        /// 删除产品类别
        /// </summary>
        /// <param name="productcategoryid"></param>
        /// <returns></returns>
        Task<ApiResult> DeleteProductCategoryAsync(long productcategoryid);

        /// <summary>
        /// 修改产品类别
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<ApiResult> UpdateProductCategoryAsync(ProductCategoryDto dto);

        /// <summary>
        /// 查询指定产品类别信息
        /// </summary>
        /// <param name="productcategoryid"></param>
        /// <returns></returns>
        Task<ApiResult> GetProductCategoryInfoAsync(long productcategoryid);
    }
}
