using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace Stash.Project.Stash.WarehouseManage.Model
{
    /// <summary>
    /// 出库单表
    /// </summary>
    public class OutStorageTable : BasicAggregateRoot<long>
    {
        /// <summary>
        /// 入库单类型编号
        /// </summary>
        public long OutStorageType_Id { get; set; }
        /// <summary>
        /// 关联订单号
        /// </summary>
        public long OutStorage_OrderId { get; set; }
        /// <summary>
        /// 供应商编号
        /// </summary>
        public long OuttStorage_SupplierId { get; set; }
        /// <summary>
        /// 供应商名称
        /// </summary>
        public string ? OutStorage_Name { get; set; }
        /// <summary>
        /// 供应商联系人
        /// </summary>
        public string? OutStorage_ContactPerson { get; set; }
        /// <summary>
        /// 供应商联系方式
        /// </summary>
        public string? OutStorage_Phone { get; set; }
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
        public string? OutStorage_Remark { get; set; }
    }
}
