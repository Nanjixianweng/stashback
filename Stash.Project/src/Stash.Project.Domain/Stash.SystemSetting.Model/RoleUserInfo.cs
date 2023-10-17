using Volo.Abp.Domain.Entities;

namespace Stash.Project.Stash.SystemSetting.Model
{
    /// <summary>
    /// 角色用户表
    /// </summary>
    public class RoleUserInfo : BasicAggregateRoot<long>
    {

        /// <summary>
        /// 用户id
        /// </summary>
        public long User_Id { get; set; }

        /// <summary>
        /// 角色id
        /// </summary>
        public long Role_Id { get; set; }
    }
}