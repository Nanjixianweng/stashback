﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Stash.Project.IBasicService.BasicDto
{
    public class StoreinquireDto
    {
        public long? number { get; set; }

        public string? name { get; set; }

        public long? departmentid { get; set; }

        public int? storetype { get; set; }

        public int pageIndex { get; set; }

        public int pageSize { get; set; }
    }
}
