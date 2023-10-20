using Stash.Project.IBasicService.BasicDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stash.Project.IBusinessRealizeAppService.BusinessDto
{
    /// <summary>
    /// 采购添加
    /// </summary>
    public class PurchaseDto
    {
        /// <summary>
        /// 采购表
        /// </summary>
        public PurchaseTableDto? PTD { get; set; }
        /// <summary>
        /// 产品表
        /// </summary>
        public List<ProductDto>? PD { get; set; }
    }
}
