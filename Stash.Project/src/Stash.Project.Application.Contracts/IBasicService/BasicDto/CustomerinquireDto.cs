using System;
using System.Collections.Generic;
using System.Text;

namespace Stash.Project.IBasicService.BasicDto
{
    public class CustomerinquireDto
    {
        public long customerid {  get; set; }

        public string? customername { get; set; }

        public string? telephone { get; set; }

        public int pageIndex { get; set; }

        public int pageSize { get; set; }
    }
}
