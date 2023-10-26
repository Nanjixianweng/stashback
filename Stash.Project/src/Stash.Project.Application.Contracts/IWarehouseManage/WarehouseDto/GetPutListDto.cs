using System;
using System.Collections.Generic;
using System.Text;

namespace Stash.Project.IWarehouseManage.WarehouseDto
{
    public class GetPutListDto
    {
        /// <summary>
        /// 入库单号
        /// </summary>
        public long?putStorage_Id { get; set; } 
        /// <summary>
        /// 入库单类型编号
        /// </summary>
        public int?putStorage_Type { get; set; }
        public int pageIndext { get; set; }
        public int pageSize { get; set; }   
    }
}
