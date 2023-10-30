using System;
using System.Collections.Generic;
using System.Text;

namespace Stash.Project.IWarehouseManage.WarehouseDto
{
    public class PutListQueryDto
    {
        /// <summary>
        /// 入库订单号
        /// </summary>
        public long PutStorage_Id { get;set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        public string? ProductName { get; set; }
        /// <summary>
        /// 产品编号
        /// </summary>
        public long Product_Id { get;set; }
        /// <summary>
        /// 批次
        /// </summary>
        public string PutStorage_Lot { get; set; }
        /// <summary>
        /// 规格
        /// </summary>
        public string? Specification { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int Num { get; set; }
        /// <summary>
        /// 库位名称
        /// </summary>
        public string? LibraryLocationName { get; set; }
        /// <summary>
        /// 入库单类型名称
        /// </summary>
        public string?PutStorageType_Name { get; set; }
        /// <summary>
        /// 入库单据类型
        /// </summary>
        public long putStorage_Type { get;set; }
        /// <summary>
        /// 供应商名称
        /// </summary>
        public string? SupplierName { get; set; }
        /// <summary>
        /// 制单人
        /// </summary>
        public string? Operator_Name { get; set; }
    }
}
