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
    /// 单位表
    /// </summary>
    public class UnitTable : BasicAggregateRoot<long>
    {

        /// <summary>
        /// 单位名称
        /// </summary>
        public string? UnitName { get; set; }
    }
}
