using System;
using System.Collections.Generic;
using System.Text;

namespace Stash.Project.IBusinessRealizeAppService.BusinessDto
{
    /// <summary>
    /// 采购列表查询
    /// </summary>
    public class GetPurchaseInquireDto
    {
        /// <summary>
        /// 采购ID
        /// </summary>
        public long PurchaseId { get; set; }
        /// <summary>
        /// 产品ID
        /// </summary>
        public long ProductId { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
