using Volo.Abp.Domain.Entities;

namespace Stash.Project.Stash.WarehouseManage.Model
{
    /// <summary>
    /// 单据类型表
    /// </summary>
    public class DocumentType : BasicAggregateRoot<long>
    {
        /// <summary>
        /// 单据类型名称
        /// </summary>
        public string Document_Name { get; set; }
    }
}
