using AutoMapper;
using Stash.Project.IBasicService;
using Stash.Project.IBasicService.BasicDto;
using Stash.Project.Stash.BasicData.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Yitter.IdGenerator;

namespace Stash.Project.BasicService
{
    /// <summary>
    /// 联系人控制器
    /// </summary>
    public class ContactService : ApplicationService, IContactService
    {
        public readonly IRepository<ContactTable, long> _contact;
        public readonly IMapper _mapper;

        public ContactService(IRepository<ContactTable, long> contact, IMapper mapper)
        {
            _contact = contact;
            _mapper = mapper;
        }

        /// <summary>
        /// 联系人新增
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<ApiResult> CreateContactAsync(ContactDto dto)
        {
            YitIdHelper.SetIdGenerator(new IdGeneratorOptions());
            dto.Id = YitIdHelper.NextId();
            dto.CreationTime = DateTime.Now;
            var info = _mapper.Map<ContactDto, ContactTable>(dto);
            var res = await _contact.InsertAsync(info);
            if (res == null)
            {
                return new ApiResult { code = ResultCode.Error, msg = ResultMsg.AddError, data = res };
            }
            return new ApiResult { code = ResultCode.Success, msg = ResultMsg.AddSuccess, data = res };
        }

        /// <summary>
        /// 删除联系人
        /// </summary>
        /// <param name="contactid"></param>
        /// <returns></returns>
        public async Task<ApiResult> DeleteContactAsync(long contactid)
        {
            var res = await _contact.FirstOrDefaultAsync(x => x.Id == contactid);

            await _contact.DeleteAsync(contactid);

            if (res != null)
            {
                return new ApiResult
                {
                    code = ResultCode.Success,
                    msg = ResultMsg.DeleteSuccess,
                    data = res
                };
            }

            return new ApiResult
            {
                code = ResultCode.Error,
                msg = ResultMsg.DeleteError,
                data = res
            };
        }

        /// <summary>
        /// 客户下的联系人查询
        /// </summary>
        /// <param name="customerid"></param>
        /// <returns></returns>
        public async Task<ApiResult> GetContactListAsync(long customerid)
        {
            var list = (await _contact.GetListAsync()).Where(x=>x.CustomerNumber == customerid).ToList();

            if (list == null)
            {
                return new ApiResult { code = ResultCode.Error, msg = ResultMsg.RequestError, data = list };
            }
            return new ApiResult { code = ResultCode.Success, msg = ResultMsg.RequestSuccess, data = list };
        }
    }
}
