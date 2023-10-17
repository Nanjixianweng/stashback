namespace Stash.Project.SettingDto
{
    /// <summary>
    /// 角色权限表
    /// </summary>
    public class RoleAccessInfoDto
    {
        /// <summary>
        /// 角色权限编号
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 权限id
        /// </summary>
        public long Access_Id { get; set; }

        /// <summary>
        /// 角色id
        /// </summary>
        public long Role_Id { get; set; }
    }
}