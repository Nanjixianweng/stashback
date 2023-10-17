using System;
using System.Collections.Generic;
using System.Text;

namespace Stash.Project.IBasicService.BasicDto
{
    public class StorageLocationinquireDto
    {
        public long storagelocationid {  get; set; }

        public string? storagelocationname { get; set; }

        public long storeid { get; set; }

        public int pageIndex { get; set; }

        public int pageSize { get; set; }
    }
}
