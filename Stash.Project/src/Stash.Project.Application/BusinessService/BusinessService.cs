using AutoMapper;
using Stash.Project.IBasicService.BasicDto;
using Stash.Project.IBusinessRealizeAppService;
using Stash.Project.IBusinessRealizeAppService.BusinessDto;
using Stash.Project.Stash.BasicData.Model;
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
    /// <summary>
    /// 采购控制器
    /// </summary>
    public class BusinessService : ApplicationService, IBusinessAppService
    {
        /// <summary>
        /// 采购表
        /// </summary>
        private readonly  IRepository<PurchaseTable,long> _purchaseTableRepository;
        /// <summary>
        /// 采购产品关系表
        /// </summary>
        private readonly  IRepository<PurchaseProductRelationshipTable,long> _purchaseProductRelationshipRepository;
        /// <summary>
        /// 采购退货表
        /// </summary>
        private readonly  IRepository<PurchaseReturnGoodsTable,long> _purchaseReturnGoodsTableRepository;
        /// <summary>
        /// 产品表
        /// </summary>
        private readonly  IRepository<ProductTable,long> _productTableRepository;
       
        public BusinessService(IRepository<PurchaseTable, long> purchaseTableRepository, IRepository<PurchaseProductRelationshipTable, long> purchaseProductRelationshipRepository, IRepository<PurchaseReturnGoodsTable, long> purchaseReturnGoodsTableRepository, IRepository<ProductTable, long> productTableRepository)
        {
            _purchaseTableRepository = purchaseTableRepository;
            _purchaseProductRelationshipRepository = purchaseProductRelationshipRepository;
            _purchaseReturnGoodsTableRepository = purchaseReturnGoodsTableRepository;
            _productTableRepository = productTableRepository;
        }



        #region 采购
        /// <summary>
        /// 添加 采购
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ResultApi<string>> CreatePurchaseAsync(PurchaseDto dto)
        {
            dto.PTD.Id=YitIdHelper.NextId();
            var pprt = new List<PurchaseProductRelationshipTable>();
            var pr=new List<ProductTable>();
            foreach (var p in dto.PD)
            {
                var prModel = await _productTableRepository.FindAsync(dto.PTD.Id);
                if (prModel != null&&p.Num<= prModel.Num)
                {
                    pprt.Add(ObjectMapper.Map<PurchaseProductRelationshipTableDto, PurchaseProductRelationshipTable>(new PurchaseProductRelationshipTableDto
                    {
                        Id = YitIdHelper.NextId(),
                        ProductId = p.Id,
                        PurchaseId = dto.PTD.Id,
                        TotalPrice = p.Price * p.Num,
                        EnterOrNot = false,
                        Status = Stash.TableStatus.PurchaseProductRelationshipStatus.未入库
                    }));
                    prModel.Num-=p.Num;
                    pr.Add(prModel);
                }
            }
            var ptd = ObjectMapper.Map<PurchaseTableDto,PurchaseTable>(dto.PTD);
            var pd = ObjectMapper.Map<List<ProductDto>,List<ProductTable>>(dto.PD);
            //采购表添加
            var pur= await _purchaseTableRepository.InsertAsync(ptd);
            //产品批量修改
            await _productTableRepository.UpdateManyAsync(pr);
            //采购产品关系表批量添加
            await _purchaseProductRelationshipRepository.InsertManyAsync(pprt);
            var api = new ResultApi<string>();
            if (pur != null)
            {
                api.Code = 200;
                api.Data = "添加成功";
            }
            else
            {
                api.Code = 500;
                api.Data = "添加失败";
            }
            return api;
        }
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
