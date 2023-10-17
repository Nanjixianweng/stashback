using System;
using System.Collections.Generic;
using System.Text;

namespace Stash.Project.IBasicService.BasicDto
{
    public class ProductCategoryDto
    {
        /// <summary>
        /// 类别编号
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 类别名称
        /// </summary>
        public string? ClassName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }
}
