using Stash.Project.IBusinessRealizeAppService;
using Stash.Project.IBusinessRealizeAppService.BusinessDto;
using Stash.Project.Stash.BusinessManage.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;
using Yitter.IdGenerator;

namespace Stash.Project.BusinessService
{
    public class BusinessService : ApplicationService, IBusinessAppService
    {
        private readonly  IRepository<PurchaseTable,long> _purchaseTableRepository;

        public BusinessService(IRepository<PurchaseTable, long> purchaseTableRepository)
        {
            _purchaseTableRepository = purchaseTableRepository;
        }

        #region 采购
        /// <summary>
        /// 采购  添加
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ResultApi<PurchaseTableDto>> CreatePurchaseTableAsync(PurchaseTableDto dto)
        {
            dto.Id = YitIdHelper.NextId();
            var pt = ObjectMapper.Map<PurchaseTableDto, PurchaseTable>(dto);
            var todoItem = await _purchaseTableRepository.InsertAsync(pt);
            var ptModel = ObjectMapper.Map<PurchaseTable, PurchaseTableDto>(todoItem);
            var api = new ResultApi<PurchaseTableDto>();
            if (todoItem!=null)
            {
                api.Code = 200;
                api.Data = ptModel;
            }
            else
            {
                api.Code = 500;
            }
            return api;
        }
        /// <summary>
        /// 采购  删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ResultApi<bool>> DeletePurchaseTableAsync(long id)
        {
            var ptDto = await FindPurchaseTableAsync(id);
            var api = new ResultApi<bool>();
            api.Code = 500;
            if (ptDto.Code == 200)
            {
                var pt = ObjectMapper.Map<PurchaseTableDto, PurchaseTable>(ptDto.Data);
                await _purchaseTableRepository.DeleteAsync(pt);
                api.Code = 200;
                api.Data = true;
            }
            return api;
        }
        /// <summary>
        /// 采购  精确查询单条数据
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ResultApi<PurchaseTableDto>> FindPurchaseTableAsync(long Id)
        {
            var pt= await _purchaseTableRepository.FindAsync(Id);
            var api = new ResultApi<PurchaseTableDto>();
            if (pt != null)
            {
                var ptModel = ObjectMapper.Map<PurchaseTable, PurchaseTableDto>(pt);
                api.Code = 200;
                api.Data = ptModel;
            }
            else
            {
                api.Code = 500;
            }
            return api;
        }

        /// <summary>
        /// 采购  列表
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ResultApi<List<PurchaseTableDto>>> GetPurchaseTableListAsync()
        {
            var items = await _purchaseTableRepository.GetListAsync();
             
            var api = new ResultApi<List<PurchaseTableDto>>();
            api.Code = 500;
            if (items != null)
            {
                var list= ObjectMapper.Map<List<PurchaseTable>, List<PurchaseTableDto>>(items);
                api.Code = 200;
                api.Data = list;
            }
            return api;
        }
        /// <summary>
        /// 采购  修改
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ResultApi<PurchaseTableDto>> UpdatePurchaseTableAsync(PurchaseTableDto dto)
        {
            var pt = ObjectMapper.Map<PurchaseTableDto, PurchaseTable>(dto);
            var todoItem = await _purchaseTableRepository.UpdateAsync(pt);
            var ptModel = ObjectMapper.Map<PurchaseTable, PurchaseTableDto>(todoItem);
            var api = new ResultApi<PurchaseTableDto>();
            api.Code = 500;
            if (todoItem != null)
            {
                api.Code = 200;
                api.Data = ptModel;
            }
            return api;
        }
        #endregion

    }
}
