using System;
using System.Collections.Generic;
using System.Text;

namespace Stash.Project.IBasicService.BasicDto
{
    public class ContactDto
    {
        /// <summary>
        /// 联系人编号
        /// </summary>
        public long Id { get; set; }

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
        public string? Telephone { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 客户编号
        /// </summary>
        public long CustomerNumber { get; set; }
    }
}
