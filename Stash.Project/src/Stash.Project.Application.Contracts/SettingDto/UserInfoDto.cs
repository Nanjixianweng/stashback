using System;

namespace Stash.Project.SettingDto
{
    /// <summary>
    /// 用户表
    /// </summary>
    public class UserInfoDto
    {

        /// <summary>
        /// 用户编号
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string User_Name { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string User_Password { get; set; }

        /// <summary>
        /// 工号
        /// </summary>
        public string User_JobNumber { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary>
        public string User_RealName { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string User_Email { get; set; }

        /// <summary>
        /// 手机
        /// </summary>
        public string User_Mobilephone { get; set; }

        /// <summary>
        /// 登录次数
        /// </summary>
        public int User_LoginNum { get; set; }

        /// <summary>
        /// 是否锁定
        /// </summary>
        public bool User_IsLock { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool User_IsDel { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime User_CreateTime { get; set; }

        /// <summary>
        /// 固定电话
        /// </summary>
        public string User_Telephone { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string USer_Remarks { get; set; }

        /// <summary>
        /// 部门id
        /// </summary>
        public long Sector_Id { get; set; }

        /// <summary>
        /// 角色id
        /// </summary>
        public long Role_Id { get; set; }
    }
}