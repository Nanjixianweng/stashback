using System;
using Volo.Abp.Domain.Entities;
namespace Stash.Project.Stash.WarehouseManage.Model
{
    /// <summary>
    /// 入库单表
    /// </summary>
    public class PutStorageTable : BasicAggregateRoot<long>
    {
        /// <summary>
        /// 入库单类型编号
        /// </summary>
        public long PutStorageType_Id { get; set; }
        /// <summary>
        /// 关联订单号
        /// </summary>
        public long PutStorage_OrderId { get; set; }
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
}
