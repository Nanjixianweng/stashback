using System;
using System.Collections.Generic;
using System.Text;

namespace Stash.Project.IBasicService.BasicDto
{
    public class ProductInquireDto
    {
        public long productid { get; set; }

        public string? productname { get; set; }

        public int suppliertype { get; set; }

        public int pageIndex { get; set; }

        public int pageSize { get; set; }
    }
}
