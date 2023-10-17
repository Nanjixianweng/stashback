using System;
using Volo.Abp.Domain.Entities;

namespace Stash.Project.Stash.SystemSetting.Model
{
    /// <summary>
    /// 角色表
    /// </summary>
    public class RoleInfo : BasicAggregateRoot<long>
    {

        /// <summary>
        /// 角色名称
        /// </summary>
        public string Role_Name { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime Role_CreateTime { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Role_Remark { get; set; }
    }
}