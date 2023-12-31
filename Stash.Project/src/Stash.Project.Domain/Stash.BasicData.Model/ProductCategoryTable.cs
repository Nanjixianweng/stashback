﻿using System;
using Volo.Abp.Domain.Entities;

namespace Stash.Project.Stash.BasicData.Model
{
    /// <summary>
    /// 产品类别表
    /// </summary>
    public class ProductCategoryTable : BasicAggregateRoot<long>
    {

        /// <summary>
        /// 类别名称
        /// </summary>
        public string? ClassName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }
}
