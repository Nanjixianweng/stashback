using System;
using System.Collections.Generic;
using System.Text;

namespace Stash.Project.IBasicService.BasicDto
{
    public class UnitInquireDto
    {
        public long? unitnumber {  get; set; }

        public string? unitname { get; set; }

        public int pageIndex { get; set; }

        public int pageSize { get; set; }
    }
}
