using Volo.Abp.Domain.Entities;

namespace Stash.Project.Stash.BasicData.Model
{
    /// <summary>
    /// 承运商表
    /// </summary>
    public class CarrierTable : BasicAggregateRoot<long>
    {

        /// <summary>
        /// 承运商名称
        /// </summary>
        public string? NameofCarrier { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }
}
