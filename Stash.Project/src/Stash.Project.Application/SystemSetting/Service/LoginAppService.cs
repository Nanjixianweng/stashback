﻿using Stash.Project.SystemSetting.Dto;
using Stash.Project.Stash.SystemSetting.Model;
using Stash.Project.SystemSetting.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Stash.Project.SystemSetting.Service
{
    /// <summary>
    /// 登录控制器
    /// </summary>
    public class LoginAppService : ApplicationService, ILoginAppService
    {
        private readonly IRepository<UserInfo, long> _repository;

        public LoginAppService(IRepository<UserInfo, long> repository)
        {
            _repository = repository;
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task<ApiResult> LoginShow(LoginDto obj)
        {
            var list = await _repository.FirstOrDefaultAsync(x => x.User_Name == obj.uname && x.User_Password == obj.pwd);

            if (list == null)
            {
                return new ApiResult
                {
                    code = ResultCode.Error,
                    data = list,
                    msg = ResultMsg.RequestError,
                    count = 0
                };
            }
            return new ApiResult
            {
                code = ResultCode.Success,
                data = list,
                msg = ResultMsg.RequestSuccess,
                count = 0
            };
        }
    }
}
