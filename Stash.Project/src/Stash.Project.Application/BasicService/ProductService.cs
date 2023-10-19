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
using static System.Formats.Asn1.AsnWriter;

namespace Stash.Project.BasicService
{
    public class ProductService : ApplicationService, IProductService
    {
        public readonly IRepository<ProductTable, long> _product;
        public readonly IRepository<StoreTale, long> _store;
        public readonly IRepository<CustomerTable, long> _customer;
        public readonly IRepository<ProductCategoryTable, long> _productcategory;
        public readonly IRepository<StorageLocationTable, long> _storage;
        public readonly IRepository<SupplierTable, long> _supplier;
        public readonly IRepository<UnitTable, long> _unit;
        public readonly IMapper _mapper;
        public ProductService(IRepository<ProductTable, long> product, IMapper mapper, IRepository<StoreTale, long> store, IRepository<CustomerTable, long> customer, IRepository<ProductCategoryTable, long> productcategory, IRepository<StorageLocationTable, long> storage, IRepository<SupplierTable, long> supplier, IRepository<UnitTable, long> unit)
        {
            _product = product;
            _mapper = mapper;
            _store = store;
            _customer = customer;
            _productcategory = productcategory;
            _storage = storage;
            _supplier = supplier;
            _unit = unit;
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
        public async Task<ApiResult> GetProductAsync()
        {
            var list = await _product.GetListAsync();

            return new ApiResult { code=ResultCode.Success,msg=ResultMsg.RequestSuccess, data = list};
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
        public async Task<ApiResult> GetProductListAsync(ProductInquireDto dto)
        {
            var  product = await _product.GetListAsync();
            var  store = await _store.GetListAsync();
            var  customer = await _customer.GetListAsync();
            var  productcategory = await _productcategory.GetListAsync();
            var  storage = await _storage.GetListAsync();
            var  supplier = await _supplier.GetListAsync();
            var  unit = await _unit.GetListAsync();

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
