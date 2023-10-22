using Stash.Project.IBasicService.BasicDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stash.Project.IBusinessRealizeAppService.BusinessDto
{
    /// <summary>
    /// 销售 添加 Dto
    /// </summary>
    public class SellDto
    {
        /// <summary>
        /// 销售表
        /// </summary>
        public SellTableDto Std { get; set; }
        /// <summary>
        /// 产品表
        /// </summary>
        public List<ProductDto>? PD { get; set; }
    }
}
