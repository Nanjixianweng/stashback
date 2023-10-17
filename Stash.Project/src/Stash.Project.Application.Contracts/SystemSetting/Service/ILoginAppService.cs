using Stash.Project.SystemSetting.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Stash.Project.SystemSetting.Service
{
    public interface ILoginAppService : IApplicationService
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        Task<ApiResult> LoginShow(LoginDto obj);
    }
}
