using System;

namespace Stash.Project.IBusinessRealizeAppService.BusinessDto
{
    /// <summary>
    /// 销售表DTO
    /// </summary>
    public class SellTableDto
    {
        public long Id { get; set; }
        /// <summary>
        ///  单据类型
        /// </summary>
        public long DocumentType { get; set; }

        /// <summary>
        /// 关联订单号 
        /// </summary>
        public string? AssociatedOrderNumber { get; set; }

        /// <summary>
        /// 供应商名
        /// </summary>
        public string? SupplierName { get; set; }

        /// <summary>
        /// 客户名称 
        /// </summary>
        public string? CustomerName { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        public string? ContactPerson { get; set; }

        /// <summary>
        /// 电话 
        /// </summary>
        public string? Telephone { get; set; }

        /// <summary>
        /// 收货日 
        /// </summary>
        public DateTime? DeliveryDate { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }

        /// <summary>
        /// 制单人
        /// </summary>
        public string? Documenter { get; set; }

        /// <summary>
        /// 制单时间  
        /// </summary>
        public DateTime? DocumentPreparationTime { get; set; }
    }
}
