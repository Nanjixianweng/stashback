using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace Stash.Project.Stash.BasicData.Model
{
    /// <summary>
    /// 产品表
    /// </summary>
    public class ProductTable : BasicAggregateRoot<long>
    {

        /// <summary>
        /// 产品名称
        /// </summary>
        public string? ProductName { get; set; }

        /// <summary>
        /// 厂家编码
        /// </summary>
        public string? ManufacturerCode { get; set; }

        /// <summary>
        /// 内部编码
        /// </summary>
        public string? InternalCoding { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public long Unit { get; set; }

        /// <summary>
        /// 类别
        /// </summary>
        public long Category { get; set; }

        /// <summary>
        /// 上限值
        /// </summary>
        public int UpperLimitValue { get; set; }

        /// <summary>
        /// 下限值
        /// </summary>
        public int LowerLimitValue { get; set; }

        /// <summary>
        /// 规格
        /// </summary>
        public string? Specification { get; set; }

        /// <summary>
        /// 价格
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// /重量
        /// </summary>
        public decimal Weight { get; set; }

        /// <summary>
        /// 默认仓库
        /// </summary>
        public long DefaultRepository { get; set; }

        /// <summary>
        /// 默认库位
        /// </summary>
        public long DefaultLibraryLocation { get; set; }

        /// <summary>
        /// 默认供应商
        /// </summary>
        public long DefaultSupplier { get; set; }

        /// <summary>
        /// 默认客户
        /// </summary>
        public long DefaultCustomer { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string? Description { get; set; }
    }
}
