using Stash.Project.Stash.TableStatus;
using Volo.Abp.Domain.Entities;

namespace Stash.Project.Stash.BusinessManage.Model
{
    /// <summary>
    /// 销售产品关系表
    /// </summary>
    public class SellProductRelationshipTable : BasicAggregateRoot<long>
    {
        /// <summary>
        /// 销售单号
        /// </summary>
        public long SellId { get; set; }
        /// <summary>
        /// 产品ID  
        /// </summary>
        public long ProductId { get; set; }

        /// <summary>
        /// 状态 
        /// </summary>
        public SellProductRelationshipStatus Status { get; set; }

        /// <summary>
        /// 是否入账
        /// </summary>
        public bool EnterOrNot { get; set; }

        /// <summary>
        /// 退货
        /// </summary>
        public bool ReturnGoods { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// 总价
        /// </summary>
        public decimal TotalPrice { get; set; }
    }
}
