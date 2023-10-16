using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;

namespace Stash.Project.SettingDto
{
    /// <summary>
    /// 部门表
    /// </summary>
    public class SectorInfoDto
    {
        /// <summary>
        /// 部门编号
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        public string Sector_Name { get; set; }

        /// <summary>
        /// 上级部门
        /// </summary>
        public string Sector_FatherId { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool Sector_IsDel { get; set; }
    }
}