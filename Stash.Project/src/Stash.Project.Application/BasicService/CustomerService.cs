using AutoMapper;
using Stash.Project.IBasicService;
using Stash.Project.IBasicService.BasicDto;
using Stash.Project.Stash.BasicData.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Yitter.IdGenerator;

namespace Stash.Project.BasicService
{
    public class CustomerService : ApplicationService, ICustomerService
    {
        public readonly IRepository<CustomerTable, long> _customer;
        public readonly IRepository<ContactTable, long> _contact;
        public readonly IMapper _mapper;
        public CustomerService(IRepository<CustomerTable, long> customer, IRepository<ContactTable, long> contact,IMapper mapper)
        {
            _customer = customer;
            _contact = contact;
            _mapper = mapper;
        }

        /// <summary>
        /// 客户新增
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<ApiResult> CreateCustomerAsync(CustomerDto dto)
        {
            YitIdHelper.SetIdGenerator(new IdGeneratorOptions());
            dto.Id = YitIdHelper.NextId();
            var info = _mapper.Map<CustomerDto, CustomerTable>(dto);
            var res = await _customer.InsertAsync(info);
            if (res == null)
            {
                return new ApiResult { code = ResultCode.Error, msg = ResultMsg.AddError, data = res };
            }
            return new ApiResult { code = ResultCode.Success, msg = ResultMsg.AddSuccess, data = res };
        }

        /// <summary>
        /// 删除客户
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<ApiResult> DeleteCustomerAsync(string ids)
        {
            var customerid = ids.Split(',');

            if (customerid == null)
            {
                return new ApiResult
                {
                    code = ResultCode.Error,
                    msg = ResultMsg.RequestError,
                    data = ""
                };
            }

            foreach (var id in customerid)
            {
                await _customer.DeleteAsync(Convert.ToInt64(id));
            }

            return new ApiResult
            {
                code = ResultCode.Success,
                msg = ResultMsg.RequestSuccess,
                data = ""
            };
        }

        /// <summary>
        /// 客户数据
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult> GetCustomerAsync()
        {
            var list = await _customer.GetListAsync();

            return new ApiResult { code=ResultCode.Success,msg=ResultMsg.RequestSuccess, data = list};
        }

        /// <summary>
        /// 查询指定客户信息 
        /// </summary>
        /// <param name="customerid"></param>
        /// <returns></returns>
        public async Task<ApiResult> GetCustomerInfoAsync(long customerid)
        {
            var res = await _customer.FirstOrDefaultAsync(x => x.Id == customerid);

            return new ApiResult
            {
                code = ResultCode.Success,
                msg = ResultMsg.RequestSuccess,
                data = res
            };
        } 

        /// <summary>
        /// 客户条件查询
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<ApiResult> GetCustomerListAsync(CustomerinquireDto dto)
        {
            var customer = await _customer.GetListAsync();
            
            var contact = await _contact.GetListAsync();

 
            var list =  from a in customer
                             join b in contact
                             on a.Id equals b.CustomerNumber
                        where (dto.customerid == 0 || a.Id.Equals(b.CustomerNumber)) &&
                        (string.IsNullOrEmpty(dto.customername) || a.CustomerName.Contains(dto.customername)) &&
                        (string.IsNullOrEmpty(dto.telephone) || a.Telephone.Contains(dto.telephone))
                        select new ShowCustomerDto
                             {
                                 Id = a.Id,
                                 CustomerName = a.CustomerName,
                                 Telephone = a.Telephone,
                                 Fax = a.Fax,
                                 Mailbox = a.Mailbox,
                                 Remark = a.Remark,
                                 ContactName = b.ContactName,
                                 Address = b.Address,
                                 Contactphone = b.Telephone,
                                 CreationTime = b.CreationTime,
                             };

            var totalcount = list.Count();

            var res = list.Skip((dto.pageIndex - 1) * dto.pageSize).Take(dto.pageSize).ToList();

            return new ApiResult { code = ResultCode.Success, msg = ResultMsg.RequestSuccess, data = res, count = totalcount };
        }


        /// <summary>
        /// 修改客户
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<ApiResult> UpdateCustomerAsync(CustomerDto dto)
        {
            var info = _mapper.Map<CustomerDto, CustomerTable>(dto);
            var res = await _customer.UpdateAsync(info);
            if (res == null)
            {
                return new ApiResult { code = ResultCode.Error, msg = ResultMsg.UpdateError, data = res };
            }
            return new ApiResult { code = ResultCode.Success, msg = ResultMsg.UpdateSuccess, data = res };
        }
    }
}
