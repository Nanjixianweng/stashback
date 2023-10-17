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
#pragma warning disable CS8618 // 在退出构造函数时，不可为 null 的字段必须包含非 null 值。请考虑声明为可以为 null。
        public string Sector_Name { get; set; }
#pragma warning restore CS8618 // 在退出构造函数时，不可为 null 的字段必须包含非 null 值。请考虑声明为可以为 null。

        /// <summary>
        /// 上级部门
        /// </summary>
#pragma warning disable CS8618 // 在退出构造函数时，不可为 null 的字段必须包含非 null 值。请考虑声明为可以为 null。
        public string Sector_FatherId { get; set; }
#pragma warning restore CS8618 // 在退出构造函数时，不可为 null 的字段必须包含非 null 值。请考虑声明为可以为 null。

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool Sector_IsDel { get; set; }
    }
}