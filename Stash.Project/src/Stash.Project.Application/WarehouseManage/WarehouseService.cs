using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Stash.Project.IWarehouseManage;
using Volo.Abp.Domain.Repositories;
using Stash.Project.Stash.WarehouseManage.Model;
using Microsoft.AspNetCore.Mvc;
using Yitter.IdGenerator;
using Stash.Project.IWarehouseManage.WarehouseDto;
using Volo.Abp.ObjectMapping;
using Stash.Project.Stash.BasicData.Model;
using Stash.Project.Stash.BusinessManage.Model;

namespace Stash.Project.WarehouseManage
{
    /// <summary>
    /// 仓库管理
    /// </summary>
    public class WarehouseService : ApplicationService, IWarehouseService
    {
        /// <summary>
        /// 仓库产品关系
        /// </summary>
        private readonly IRepository<StashProductTable> _stashPro;
        /// <summary>
        /// 入库单表
        /// </summary>
        private readonly IRepository<PutStorageTable> _putStor;
        /// <summary>
        /// 出库单表
        /// </summary>
        private readonly IRepository<OutStorageTable> _outStor;
        /// <summary>
        /// 库单状态表
        /// </summary>
        private readonly IRepository<PutStorageStateTable> _warState;
        /// <summary>
        /// 单据类型表
        /// </summary>
        private readonly IRepository<DocumentType> _documentType;
        /// <summary>
        /// 库位表
        /// </summary>
        private readonly IRepository<StorageLocationTable> _storageLocation;
        /// <summary>
        /// 销售单状态
        /// </summary>
        private readonly IRepository<PurchaseProductRelationshipTable> _purchaseProduct;
        /// <summary>
        /// 产品表
        /// </summary>
        private readonly IRepository<ProductTable> productTable;
        /// <summary>
        /// 供应商表
        /// </summary>
        private readonly IRepository<SupplierTable> _supplierTable;



        public WarehouseService(IRepository<StashProductTable> stashPro, IRepository<PutStorageTable> putStor, IRepository<OutStorageTable> outStor, IRepository<PutStorageStateTable> warState, IRepository<DocumentType> documentType, IRepository<StorageLocationTable> storageLocation, IRepository<PurchaseProductRelationshipTable> purchaseProduct, IRepository<ProductTable> productTable, IRepository<SupplierTable> supplierTable)
        {
            _stashPro = stashPro;
            _putStor = putStor;
            _outStor = outStor;
            _warState = warState;
            _documentType = documentType;
            _storageLocation = storageLocation;
            _purchaseProduct = purchaseProduct;
            this.productTable = productTable;
            _supplierTable = supplierTable;
        }

        /// <summary>
        /// 查询单据类型
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult> GetDocumentTypeAsync()
        {
            var list = await _documentType.ToListAsync();

            return new ApiResult()
            {
                data = list,
            };
        }
        /// <summary>
        /// 添加入库信息
        /// </summary>
        /// <param name="obj">入库信息</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ApiResult> PostAddPutWarehouse(AddWarehouseDto obj)
        {
            obj.putStorageDto.Id = YitIdHelper.NextId();

            //批次生成
            Random random = new Random();

            var ints = "";

            for (int i = 0; i < 4; i++)
            {
                ints += random.Next(0, 9);
            }

            //批次生成规则:年月日时分秒+随机数*4
            var rodNum = DateTime.Now.ToString("yyyyMMddHHmmss") + ints;

            var defuid = (await _storageLocation.FirstOrDefaultAsync(x => x.DefaultrorNot == true)).Id;

            //关系表
            foreach (var item in obj.stashProductDto)
            {
                item.Id = YitIdHelper.NextId();

                item.PutStorage_Id = obj.putStorageDto.Id;

                item.PutStorage_Lot = rodNum;

                item.PutStorage_Position = defuid.ToString();
            }

            //入库表
            PutStorageTable putStorageTable = new PutStorageTable();

            var flies = putStorageTable.GetType().GetProperty("Id");

            if (flies != null)
            {
                flies.SetValue(putStorageTable, obj.putStorageDto.Id);
            }
            putStorageTable.Operator_Date = obj.putStorageDto.Operator_Date;
            putStorageTable.Operator_Name = obj.putStorageDto.Operator_Name;
            putStorageTable.PutStorageType_Id = obj.putStorageDto.PutStorageType_Id;
            putStorageTable.PutStorage_OrderId = obj.putStorageDto.PutStorage_OrderId;
            putStorageTable.PutStorage_Remark = obj.putStorageDto.PutStorage_Remark;

            await _putStor.InsertAsync(putStorageTable);

            var info = ObjectMapper.Map<List<StashProductDto>, List<StashProductTable>>(obj.stashProductDto);

            await _stashPro.InsertManyAsync(info);

            //更改采购状态
            var stateList = (await _purchaseProduct.ToListAsync()).WhereIf(putStorageTable.PutStorage_OrderId != null, x => x.PurchaseId == putStorageTable.PutStorage_OrderId);

            foreach (var item in stateList)
            {
                item.Status = Stash.TableStatus.PurchaseProductRelationshipStatus.全部入库;
            }

            return new ApiResult()
            {
                code = 200,
            };
        }
        /// <summary>
        /// 入库查询
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult> PostPutStorageAsync(GetPutListDto obj)
        {
            //入库单表
            var putStor = await _putStor.GetListAsync();

            //库单产品关系
            var stashPro = await _stashPro.GetListAsync();

            //产品表
            var product = await productTable.GetListAsync();

            //库位表
            var storageLocation = await _storageLocation.GetListAsync();

            //单据类型表
            var documentType = await _documentType.ToListAsync();

            //供应商表
            var supplierTable = await _supplierTable.ToListAsync();

            var list = from a in putStor
                       join b in stashPro on a.Id equals b.PutStorage_Id
                       join c in product on b.Product_Id equals c.Id
                       join d in storageLocation on Convert.ToInt64(b.PutStorage_Position) equals d.Id
                       join e in documentType on a.PutStorageType_Id equals e.Id
                       join f in supplierTable on c.DefaultSupplier equals f.Id
                       select new PutListQueryDto
                       {
                           Product_Id = c.Id,
                           Num = c.Num,
                           Operator_Name = a.Operator_Name,
                           ProductName = c.ProductName,
                           PutStorage_Id = b.PutStorage_Id,
                           PutStorage_Lot = b.PutStorage_Lot,
                           Specification = c.Specification,
                           LibraryLocationName = d.LibraryLocationName,
                           PutStorageType_Name = e.Document_Name,
                           SupplierName = f.SupplierName,
                           putStorage_Type = e.Id,
                       };

            if (obj.putStorage_Id != null && obj.putStorage_Id != 0)
            {
                list = list.Where(x => x.PutStorage_Id == obj.putStorage_Id).AsQueryable();
            }
            if (obj.putStorage_Type != null && obj.putStorage_Type != 0)
            {
                list = list.Where(x => x.putStorage_Type == obj.putStorage_Type).AsQueryable();
            }

            var dataCount = list.Count();

            list = list.Skip((obj.pageIndext - 1) * obj.pageSize).Take(obj.pageSize).ToList();

            return new ApiResult() { code = 0, data = list, count = dataCount };
        }
    }
}
