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
    public class ProductCategoryService : ApplicationService, IProductCategoryService
    {
        public readonly IRepository<ProductCategoryTable, long> _productcategory;
        public readonly IMapper _mapper;

        public ProductCategoryService(IRepository<ProductCategoryTable, long> productcategory, IMapper mapper)
        {
            _productcategory = productcategory;
            _mapper = mapper;
        }

        /// <summary>
        /// 产品类别新增
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<ApiResult> CreateProductCategoryAsync(ProductCategoryDto dto)
        {
            YitIdHelper.SetIdGenerator(new IdGeneratorOptions());
            dto.Id = YitIdHelper.NextId();
            dto.CreationTime = DateTime.Now;
            var info = _mapper.Map<ProductCategoryDto, ProductCategoryTable>(dto);
            var res = await _productcategory.InsertAsync(info);
            if (res == null)
            {
                return new ApiResult { code = ResultCode.Error, msg = ResultMsg.AddError, data = res };
            }
            return new ApiResult { code = ResultCode.Success, msg = ResultMsg.AddSuccess, data = res };
        }

        /// <summary>
        /// 删除产品类别
        /// </summary>
        /// <param name="productcategoryid"></param>
        /// <returns></returns>
        public async Task<ApiResult> DeleteProductCategoryAsync(long productcategoryid)
        {
            var res = await _productcategory.FirstOrDefaultAsync(x => x.Id == productcategoryid);

            await _productcategory.DeleteAsync(productcategoryid);

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
        /// 查询指定产品类别信息
        /// </summary>
        /// <param name="productcategoryid"></param>
        /// <returns></returns>
        public async Task<ApiResult> GetProductCategoryInfoAsync(long productcategoryid)
        {
            var res = await _productcategory.FirstOrDefaultAsync(x => x.Id == productcategoryid);

            return new ApiResult
            {
                code = ResultCode.Success,
                msg = ResultMsg.RequestSuccess,
                data = res
            };
        }

        /// <summary>
        /// 产品类别条件查询
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<ApiResult> GetProductCategoryListAsync(ProductCategoryInquireDto dto)
        {
            var list = (await _productcategory.GetListAsync())
                .WhereIf(dto.productcategoryid != 0, x => x.Id == dto.productcategoryid)
                .WhereIf(!string.IsNullOrEmpty(dto.productcategoryname), x => x.ClassName.Contains(dto.productcategoryname));


            var totalcount = list.Count();

            var res = list.OrderByDescending(x=>x.CreationTime).Skip((dto.pageIndex - 1) * dto.pageSize).Take(dto.pageSize).ToList();

            return new ApiResult { code = ResultCode.Success, msg = ResultMsg.RequestSuccess, data = res, count = totalcount };
        }

        /// <summary>
        /// 修改产品类别
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<ApiResult> UpdateProductCategoryAsync(ProductCategoryDto dto)
        {
            var info = _mapper.Map<ProductCategoryDto, ProductCategoryTable>(dto);
            var res = await _productcategory.UpdateAsync(info);
            if (res == null)
            {
                return new ApiResult { code = ResultCode.Error, msg = ResultMsg.UpdateError, data = res };
            }
            return new ApiResult { code = ResultCode.Success, msg = ResultMsg.UpdateSuccess, data = res };
        }

        /// <summary>
        /// 产品类别查询
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult> GetProductCategoryAsync()
        {
            var list = await _productcategory.GetListAsync();

            return new ApiResult { code = ResultCode.Success, msg = ResultMsg.RequestSuccess, data = list };
        }
    }
}
