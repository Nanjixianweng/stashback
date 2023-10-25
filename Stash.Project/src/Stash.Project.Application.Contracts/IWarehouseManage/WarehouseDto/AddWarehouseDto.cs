using System;
using System.Collections.Generic;
using System.Text;
using Stash.Project.Stash.TableStatus;

namespace Stash.Project.IWarehouseManage.WarehouseDto
{
    public class AddWarehouseDto
    {
        public List<PutStorageDto> putStorageDto { get; set; }
        public StashProductDto stashProductDto { get; set; }
    }
    public class PutStorageDto
    {
        public long Id { get; set; }
        /// <summary>
        /// 入库单类型编号
        /// </summary>
        public long PutStorageType_Id { get; set; }
        /// <summary>
        /// 关联订单号
        /// </summary>
        public long PutStorage_OrderId { get; set; }
        /// <summary>
        /// 供应商编号
        /// </summary>
        public long PutStorage_SupplierId { get; set; }
        /// <summary>
        /// 供应商名称
        /// </summary>
        public string? PutStorage_Name { get; set; }
        /// <summary>
        /// 供应商联系人
        /// </summary>
        public string? PutStorage_ContactPerson { get; set; }
        /// <summary>
        /// 供应商联系方式
        /// </summary>
        public string? PutStorage_Phone { get; set; }
        /// <summary>
        /// 制单人
        /// </summary>
        public string? Operator_Name { get; set; }
        /// <summary>
        /// 制单时间
        /// </summary>
        public DateTime Operator_Date { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string? PutStorage_Remark { get; set; }
    }
    public class StashProductDto
    {
        public long Ids { get; set; }
        /// <summary>
        /// 产品编号
        /// </summary>
        public long Product_Id { get; set; }
        /// <summary>
        /// 入库单编号
        /// </summary>
        public long PutStorage_Id { get; set; }
        /// <summary>
        /// 批次
        /// </summary>
        public int PutStorage_Lot { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        public decimal PutStorage_Price { get; set; }
        /// <summary>
        /// 入库数
        /// </summary>
        public int PutStorage_Num { get; set; }
        /// <summary>
        /// 总价
        /// </summary>
        public decimal PutStorage_SumPrice { get; set; }
        /// <summary>
        /// 库位
        /// </summary>
        public string? PutStorage_Position { get; set; }
    }
}
