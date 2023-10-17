using System;

namespace Stash.Project.IBusinessRealizeAppService.BusinessDto
{
    /// <summary>
    /// 采购退货表 DTO
    /// </summary>
    public class PurchaseReturnGoodsTableDto
    {
        /// <summary>
        /// 采购表ID
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 产品Id
        /// </summary>
        public long ProductId { get; set; }
        /// <summary>
        /// 采购单号
        /// </summary>
        public long SellId { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime AddTime { get; set; }
    }
}
