using System;

namespace Stash.Project.BasicDto
{
    public class StoreDto
    {
        /// <summary>
        /// 仓库编号
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 仓库名称
        /// </summary>
        public string? StoreName { get; set; }

        /// <summary>
        /// 租赁时间
        /// </summary>
        public DateTime LeaseTime { get; set; }

        /// <summary>
        /// 仓库类型
        /// </summary>
        public int StoreType { get; set; }

        /// <summary>
        /// 所属部门
        /// </summary>
        public long DepartmentId { get; set; }

        /// <summary>
        /// 作用
        /// </summary>
        public string? Effect { get; set; }

        /// <summary>
        /// 面积
        /// </summary>
        public decimal Area { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        public string? ContactPerson { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public string? TelePhone { get; set; }

        /// <summary>
        /// 是否禁用
        /// </summary>
        public bool WhethertoDisable { get; set; }

        /// <summary>
        /// 是否默认
        /// </summary>
        public bool DefaultrorNot { get; set; }
    }
}
