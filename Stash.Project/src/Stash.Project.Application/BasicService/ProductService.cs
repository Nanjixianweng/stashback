using AutoMapper;
using Stash.Project.IBasicService;
using Stash.Project.IBasicService.BasicDto;
using Stash.Project.Stash.BasicData.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Specifications;
using Yitter.IdGenerator;
using static System.Formats.Asn1.AsnWriter;

namespace Stash.Project.BasicService
{
    /// <summary>
    /// 产品控制器
    /// </summary>
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
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<ApiResult> DeleteProductAsync(string ids)
        {
            var productid = ids.Split(',');

            if (productid == null)
            {
                return new ApiResult
                {
                    code = ResultCode.Error,
                    msg = ResultMsg.RequestError,
                    data = ""
                };
            }

            foreach (var id in productid)
            {
                await _product.DeleteAsync(Convert.ToInt64(id));
            }

            return new ApiResult
            {
                code = ResultCode.Success,
                msg = ResultMsg.RequestSuccess,
                data = ""
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
        public async Task<ApiResult> GetProductListAsync(ProductInquireDto dto)
        {
            var  product = await _product.GetListAsync();
            var  store = await _store.GetListAsync();
            var  customer = await _customer.GetListAsync();
            var  productcategory = await _productcategory.GetListAsync();
            var  storage = await _storage.GetListAsync();
            var  supplier = await _supplier.GetListAsync();
            var  unit = await _unit.GetListAsync();

            var list = from a in product
                       join b in unit
                       on a.Unit equals b.Id
                       join c in productcategory
                       on a.Category equals c.Id
                       join d in store
                       on a.DefaultRepository equals d.Id
                       join e in storage
                       on a.DefaultLibraryLocation equals e.Id
                       join f in supplier
                       on a.DefaultSupplier equals f.Id
                       join g in customer
                       on a.DefaultCustomer equals g.Id
                       where (dto.productid == 0 || a.Id.Equals(dto.productid)) &&
                       (string.IsNullOrWhiteSpace(dto.productname) || a.ProductName.Contains(dto.productname)) &&
                       (dto.suppliertype == 0 || f.SupplierType.Equals(dto.suppliertype))
                       select new ShowProductDto
                       {
                           Id = a.Id,
                           ProductName = a.ProductName,
                           ManufacturerCode = a.ManufacturerCode,
                           InternalCoding = a.InternalCoding,
                           Unit = a.Unit,
                           UnitName = b.UnitName,
                           Category = a.Category,
                           CategoryN = c.ClassName,
                           UpperLimitValue = a.UpperLimitValue,
                           LowerLimitValue = a.LowerLimitValue,
                           Num = a.Num,
                           Specification = a.Specification,
                           Price = a.Price,
                           Weight = a.Weight,
                           DefaultRepository = a.DefaultRepository,
                           DefaultRepositoryName = d.StoreName,
                           DefaultLibraryLocation = a.DefaultLibraryLocation,
                           DefaultLibraryLocationName = e.LibraryLocationName,
                           DefaultSupplier = a.DefaultSupplier,
                           DefaultSupplierName = f.SupplierName,
                           DefaultCustomer = a.DefaultCustomer,
                           DefaultCustomerName = g.CustomerName,
                           Description = a.Description,
                       };

            var totalcount = list.Count();

            var res = list.Skip((dto.pageIndex - 1) * dto.pageSize).Take(dto.pageSize).ToList();

            return new ApiResult { code = ResultCode.Success, msg = ResultMsg.RequestSuccess, data = res, count = totalcount };
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
