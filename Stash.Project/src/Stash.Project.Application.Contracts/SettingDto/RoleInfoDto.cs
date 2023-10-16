using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;

namespace Stash.Project.SettingDto
{
    /// <summary>
    /// 角色表
    /// </summary>
    public class RoleInfoDto
    {
        /// <summary>
        /// 角色编号
        /// </summary>
        public long Id { get; set; }

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