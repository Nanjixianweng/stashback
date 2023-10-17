using System;
using System.Collections.Generic;
using System.Text;

namespace Stash.Project.IBasicService.BasicDto
{
    public class UnitDto
    {
        /// <summary>
        /// 单位编号
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 单位名称
        /// </summary>
        public string? UnitName { get; set; }
    }
}
