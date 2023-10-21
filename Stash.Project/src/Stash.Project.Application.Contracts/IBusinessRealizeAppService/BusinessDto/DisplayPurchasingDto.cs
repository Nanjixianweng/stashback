using Stash.Project.Stash.TableStatus;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stash.Project.IBusinessRealizeAppService.BusinessDto
{
    public class DisplayPurchasingDto
    {
        /// <summary>
        /// 采购单号
        /// </summary>
        public long PurchaseId { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        public string? ProductName { get; set; }
        /// <summary>
        /// 产品编号
        /// </summary>
        public long ProductId { get; set; }
        /// <summary>
        /// 产品规格
        /// </summary>
        public string? Specification { get; set; }
        /// <summary>
        /// 产品单位
        /// </summary>
        public long Unit { get; set; }
        /// <summary>
        /// 产品价格
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int Num { get; set; }
        /// <summary>
        /// 默认供应商
        /// </summary>
        public long DefaultSupplier { get; set; }
        /// <summary>
        /// 供应商名称
        /// </summary>
        public string? SupplierName { get; set; }
        /// <summary>
        /// 总价
        /// </summary>
        public decimal TotalPrice { get; set; }
        /// <summary>
        /// 状态 
        /// </summary>
        public PurchaseProductRelationshipStatus Status { get; set; }
        /// <summary>
        /// 是否入账
        /// </summary>
        public bool EnterOrNot { get; set; }
        /// <summary>
        /// 退货
        /// </summary>
        public bool ReturnGoods { get; set; }
        /// <summary>
        /// 制单时间  
        /// </summary>
        public DateTime? DocumentPreparationTime { get; set; }
    }
}
