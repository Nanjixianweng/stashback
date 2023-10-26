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



        public WarehouseService(IRepository<StashProductTable> stashPro, IRepository<PutStorageTable> putStor, IRepository<OutStorageTable> outStor, IRepository<PutStorageStateTable> warState, IRepository<DocumentType> documentType, IRepository<StorageLocationTable> storageLocation, IRepository<PurchaseProductRelationshipTable> purchaseProduct)
        {
            _stashPro = stashPro;
            _putStor = putStor;
            _outStor = outStor;
            _warState = warState;
            _documentType = documentType;
            _storageLocation = storageLocation;
            _purchaseProduct = purchaseProduct;
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

            var defuid = _storageLocation.FirstOrDefaultAsync(x => x.DefaultrorNot == true).Id;

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
    }
}
