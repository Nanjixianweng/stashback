using Volo.Abp.Domain.Entities;

namespace Stash.Project.Stash.WarehouseManage.Model
{
    /// <summary>
    /// 入库单状态表
    /// </summary>
    public class PutStorageStateTable : BasicAggregateRoot<long>
    {
        /// <summary>
        /// 入库单名称
        /// </summary>
        public string? PutStorageState_Name { get; set; }
    }
}
