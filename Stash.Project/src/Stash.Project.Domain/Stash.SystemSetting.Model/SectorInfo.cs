using Volo.Abp.Domain.Entities;

namespace Stash.Project.Stash.SystemSetting.Model
{
    /// <summary>
    /// 部门表
    /// </summary>
    public class SectorInfo : BasicAggregateRoot<long>
    {
        /// <summary>
        /// 部门名称
        /// </summary>
        public string Sector_Name { get; set; }

        /// <summary>
        /// 上级部门
        /// </summary>
        public long Sector_FatherId { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool Sector_IsDel { get; set; }
    }
}