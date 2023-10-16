using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;

namespace Stash.Project.SettingDto
{
    /// <summary>
    /// 角色用户表
    /// </summary>
    public class RoleUserInfoDto
    {
        /// <summary>
        /// 角色用户编号
        /// </summary>
        public long Id { get; set; }

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