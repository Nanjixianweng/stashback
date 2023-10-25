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

        public WarehouseService(IRepository<StashProductTable> stashPro, IRepository<PutStorageTable> putStor, IRepository<OutStorageTable> outStor, IRepository<PutStorageStateTable> warState, IRepository<DocumentType> documentType)
        {
            _stashPro = stashPro;
            _putStor = putStor;
            _outStor = outStor;
            _warState = warState;
            _documentType = documentType;
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

            var rodNum = DateTime.Now.ToString("yyyyMMddHHmmss")+ ints;

            foreach (var item in obj.stashProductDto)
            {
                item.Id = YitIdHelper.NextId();

                item.PutStorage_Id = obj.putStorageDto.Id;

                item.PutStorage_Lot = rodNum;
            }

            PutStorageTable putStorageTable = new PutStorageTable();

           var flies= putStorageTable.GetType().GetProperty("Id");

            if (flies!=null)
            {
                flies.SetValue(putStorageTable, obj.putStorageDto.Id);
            }
            putStorageTable.Operator_Date = obj.putStorageDto.Operator_Date;
            putStorageTable.Operator_Name = obj.putStorageDto.Operator_Name;
            putStorageTable.PutStorageType_Id = obj.putStorageDto.PutStorageType_Id;
            putStorageTable.PutStorage_OrderId = obj.putStorageDto.PutStorage_OrderId;
            putStorageTable.PutStorage_Remark = obj.putStorageDto.PutStorage_Remark;

            await _putStor.InsertAsync(putStorageTable);

          var info=  ObjectMapper.Map<List<StashProductDto>,List<StashProductTable>>(obj.stashProductDto);

            await _stashPro.InsertManyAsync(info);

            return new ApiResult() { 
             code = 200,
            };
        }
    }
}
