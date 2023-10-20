using System;
using System.Collections.Generic;
using System.Text;

namespace Stash.Project.IBasicService.BasicDto
{
    public class ProductMesDto
    {
        public List<long> idsMes { get; set; }
        /// <summary>
        /// 产品编号
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        public string? ProductName { get; set; }
        public int pageIndex { get; set; }
        public int pageSize { get; set; }
    }
}
