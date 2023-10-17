using System;
using System.Collections.Generic;
using System.Text;

namespace Stash.Project.IBasicService.BasicDto
{
    public class CustomerDto
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
    }
}
