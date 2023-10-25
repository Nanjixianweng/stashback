using Volo.Abp.Domain.Entities;

namespace Stash.Project.Stash.WarehouseManage.Model
{
    /// <summary>
    /// 仓库产品关系表
    /// </summary>
    public class StashProductTable : BasicAggregateRoot<long>
    {
        /// <summary>
        /// 产品编号
        /// </summary>
        public long Product_Id { get; set; }
        /// <summary>
        /// 入库单编号
        /// </summary>
        public long PutStorage_Id { get; set; }
        /// <summary>
        /// 批次
        /// </summary>
        public string PutStorage_Lot { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        public decimal PutStorage_Price { get; set; }
        /// <summary>
        /// 入库数
        /// </summary>
        public int PutStorage_Num { get; set; }
        /// <summary>
        /// 总价
        /// </summary>
        public decimal PutStorage_SumPrice { get; set; }
        /// <summary>
        /// 库位
        /// </summary>
        public string? PutStorage_Position { get; set; }
    }
}
