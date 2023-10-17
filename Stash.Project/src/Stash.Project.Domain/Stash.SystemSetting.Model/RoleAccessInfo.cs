using Volo.Abp.Domain.Entities;

namespace Stash.Project.Stash.SystemSetting.Model
{
    /// <summary>
    /// 角色权限表
    /// </summary>
    public class RoleAccessInfo : BasicAggregateRoot<long>
    {

        /// <summary>
        /// 权限id
        /// </summary>
        public long Access_Id { get; set; }

        /// <summary>
        /// 角色id
        /// </summary>
        public long Role_Id { get; set; }
    }
}