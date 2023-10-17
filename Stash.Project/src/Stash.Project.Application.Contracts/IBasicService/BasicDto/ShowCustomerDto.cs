using System;
using System.Collections.Generic;
using System.Text;

namespace Stash.Project.IBasicService.BasicDto
{
    public class ShowCustomerDto
    {
        /// <summary>
        /// 客户编号
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 客户名称
        /// </summary>
        public string? CustomerName { get; set; }

        /// <summary>
        /// 传真
        /// </summary>
        public string? Fax { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public string? Telephone { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string? Mailbox { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }

        /// <summary>
        /// 联系人名称
        /// </summary>
        public string? ContactName { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public string? Contactphone { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }
    }
}
