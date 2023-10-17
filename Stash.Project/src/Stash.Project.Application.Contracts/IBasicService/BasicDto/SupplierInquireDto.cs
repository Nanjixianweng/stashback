using System;
using System.Collections.Generic;
using System.Text;

namespace Stash.Project.IBasicService.BasicDto
{
    public class SupplierInquireDto
    {
        public long supplierid { get; set; }

        public string? suppliername { get; set;}

        public int suppliertype { get; set;}

        public string? telephone { get; set;}

        public int pageIndex { get; set; }

        public int pageSize { get; set; }
    }
}
