using System;

namespace Stash.Project.IBusinessRealizeAppService.BusinessDto
{
    /// <summary>
    /// 销售退货表DTO
    /// </summary>
    public class SalesReturnsTableDto
    {
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
