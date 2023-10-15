using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace Stash.Project.Stash.WarehouseManage.Model
{
    /// <summary>
    /// 入库单状态表
    /// </summary>
    public class PutStorageStateTable : BasicAggregateRoot<long>
    {
        /// <summary>
        /// 入库单编号
        /// </summary>
        public long PutStorageState_Id { get; set; }
        /// <summary>
        /// 入库单名称
        /// </summary>
        public long PutStorageState_Name { get; set; }
    }
}
