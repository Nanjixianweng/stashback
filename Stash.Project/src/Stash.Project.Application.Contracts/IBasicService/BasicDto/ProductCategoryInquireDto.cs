using System;
using System.Collections.Generic;
using System.Text;

namespace Stash.Project.IBasicService.BasicDto
{
    public class ProductCategoryInquireDto
    {
        public long? productcategoryid { get; set; }

        public string? productcategoryname { get; set;}

        public int pageIndex { get; set; }

        public int pageSize { get; set; }
    }
}
