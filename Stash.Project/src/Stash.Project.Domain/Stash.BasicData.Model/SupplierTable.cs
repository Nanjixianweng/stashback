using System;
using Volo.Abp.Domain.Entities;

namespace Stash.Project.Stash.BasicData.Model
{
    /// <summary>
    /// 供应商表
    /// </summary>
    public class SupplierTable : BasicAggregateRoot<long>
    {

        /// <summary>
        /// 供应商名称
        /// </summary>
        public string? SupplierName { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public int SupplierType { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public string? Telephone { get; set; }

        /// <summary>
        /// 传真
        /// </summary>
        public string? Fax { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string? Mailbox { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        public string? ContactPerson { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 描述
        /// </summary>- 
        public string? Description { get; set; }
    }
}
