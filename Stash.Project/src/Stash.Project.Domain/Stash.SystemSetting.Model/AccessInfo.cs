﻿using System;
using Volo.Abp.Domain.Entities;

namespace Stash.Project.Stash.SystemSetting.Model
{
    /// <summary>
    /// 权限表
    /// </summary>
    public class AccessInfo : BasicAggregateRoot<long>
    {

        /// <summary>
        /// 权限名称
        /// </summary>
       public string Access_Name { get; set; }

        /// <summary>
        /// 父级id
        /// </summary>
        public long Access_FatherId { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public string Access_Type { get; set; }

        /// <summary>
        /// 样式
        /// </summary>
       public string Access_Icon { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public string? Access_Sort { get; set; }

        /// <summary>
        /// 路由
        /// </summary>
       public string Access_Route { get; set; }


        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime Access_CreateTime { get; set; }

        /// <summary>
        /// 按钮权限
        /// </summary>
        public long Access_Button { get; set; }
    }
}