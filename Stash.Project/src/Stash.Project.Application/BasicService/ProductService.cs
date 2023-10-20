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
    public class ProductService : ApplicationService, IProductService
    {
        public readonly IRepository<ProductTable, long> _product;
        public readonly IMapper _mapper;
        
        public ProductService(IRepository<ProductTable, long> product, IMapper mapper)
        {
            _product = product;
            _mapper = mapper;
        }

        /// <summary>
        /// 产品新增
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<ApiResult> CreateProductAsync(ProductDto dto)
        {
            YitIdHelper.SetIdGenerator(new IdGeneratorOptions());
            dto.Id = YitIdHelper.NextId();
            dto.Num = 1;
            var info = _mapper.Map<ProductDto, ProductTable>(dto);
            var res = await _product.InsertAsync(info);
            if (res == null)
            {
                return new ApiResult { code = ResultCode.Error, msg = ResultMsg.AddError, data = res };
            }
            return new ApiResult { code = ResultCode.Success, msg = ResultMsg.AddSuccess, data = res };
        }

        /// <summary>
        /// 删除产品
        /// </summary>
        /// <param name="productid"></param>
        /// <returns></returns>
        public async Task<ApiResult> DeleteProductAsync(long productid)
        {
            var res = await _product.FirstOrDefaultAsync(x => x.Id == productid);

            await _product.DeleteAsync(productid);

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
        /// 产品查询
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult> PostProductListAsync(ProductMesDto dto)
        {
            var list =(await _product.GetListAsync())
                .WhereIf(dto.idsMes.Count!=0,x=> dto.idsMes.Contains(x.Id))
                .WhereIf(dto.Id!=0,x=>x.Id.Equals(dto.Id))
                .WhereIf(!string.IsNullOrWhiteSpace( dto.ProductName),x=>x.ProductName.Equals(dto.ProductName));
            var totalCount=list.Count();
            list=list.Skip((dto.pageIndex - 1)* dto.pageSize).Take(dto.pageSize).ToList();
            return new ApiResult { code=ResultCode.Success,msg=ResultMsg.RequestSuccess, data = list,count= totalCount };
        }

        /// <summary>
        /// 查询指定产品信息
        /// </summary>
        /// <param name="productid"></param>
        /// <returns></returns>
        public async Task<ApiResult> GetProductInfoAsync(long productid)
        {
            var res = await _product.FirstOrDefaultAsync(x => x.Id == productid);

            return new ApiResult
            {
                code = ResultCode.Success,
                msg = ResultMsg.RequestSuccess,
                data = res
            };
        }

        /// <summary>
        /// 产品条件查询
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public Task<ApiResult> GetProductListAsync(ProductInquireDto dto)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// 修改产品
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<ApiResult> UpdateProductAsync(ProductDto dto)
        {
            var info = _mapper.Map<ProductDto, ProductTable>(dto);
            var res = await _product.UpdateAsync(info);
            if (res == null)
            {
                return new ApiResult { code = ResultCode.Error, msg = ResultMsg.UpdateError, data = res };
            }
            return new ApiResult { code = ResultCode.Success, msg = ResultMsg.UpdateSuccess, data = res };
        }
    }
}
