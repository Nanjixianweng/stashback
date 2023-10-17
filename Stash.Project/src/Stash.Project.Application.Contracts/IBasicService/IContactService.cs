using Stash.Project.IBasicService.BasicDto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Stash.Project.IBasicService
{
    public interface IContactService : IApplicationService
    {
        /// <summary>
        /// 联系人新增
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<ApiResult> CreateContactAsync(ContactDto dto);

        /// <summary>
        /// 客户下的联系人查询
        /// </summary>
        /// <param name="customerid"></param>
        /// <returns></returns>
        Task<ApiResult> GetContactListAsync(long customerid);

        /// <summary>
        /// 删除联系人
        /// </summary>
        /// <param name="contactid"></param>
        /// <returns></returns>
        Task<ApiResult> DeleteContactAsync(long contactid);
    }
}
