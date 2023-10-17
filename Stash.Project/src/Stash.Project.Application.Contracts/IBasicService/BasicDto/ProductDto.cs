using System;
using System.Collections.Generic;
using System.Text;

namespace Stash.Project.IBasicService.BasicDto
{
    public class ProductDto
    {
        /// <summary>
        /// 产品编号
        /// </summary>
        public long Id { get; set; }

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
        /// 数量
        /// </summary>
        public int Num { get; set; }

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
