using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace Stash.Project.Stash.BusinessManage.Model
{
    /// <summary>
    /// 销售退货表
    /// </summary>
    public class SalesReturnsTable : BasicAggregateRoot<long>
    {
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
