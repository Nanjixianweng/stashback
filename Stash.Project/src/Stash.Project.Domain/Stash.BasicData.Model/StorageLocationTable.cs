using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace Stash.Project.Stash.BasicData.Model
{
    /// <summary>
    /// 库位表
    /// </summary>
    public class StorageLocationTable : BasicAggregateRoot<long>
    {

        /// <summary>
        /// 库位名称
        /// </summary>
        public string? LibraryLocationName { get; set; }

        /// <summary>
        /// 库位类型
        /// </summary>
        public int StorageLocationType { get; set; }

        /// <summary>
        /// 仓库
        /// </summary>
        public long Stash {  get; set; }

        /// <summary>
        /// 是否禁用
        /// </summary>
        public bool WhethertoDisable { get; set; }

        /// <summary>
        /// 是否默认
        /// </summary>
        public bool DefaultrorNot { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }
    }
}
