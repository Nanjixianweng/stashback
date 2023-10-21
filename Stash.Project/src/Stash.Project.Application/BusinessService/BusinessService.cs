using AutoMapper;
using Stash.Project.IBasicService.BasicDto;
using Stash.Project.IBusinessRealizeAppService;
using Stash.Project.IBusinessRealizeAppService.BusinessDto;
using Stash.Project.Stash.BasicData.Model;
using Stash.Project.Stash.BusinessManage.Model;
using Stash.Project.Stash.TableStatus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;
using Yitter.IdGenerator;

namespace Stash.Project.BusinessService
{
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
        private readonly  IRepository<SupplierTable,long> _supplierTableRepository;
        public BusinessService(IRepository<PurchaseTable, long> purchaseTableRepository, IRepository<PurchaseProductRelationshipTable, long> purchaseProductRelationshipRepository, IRepository<PurchaseReturnGoodsTable, long> purchaseReturnGoodsTableRepository, IRepository<ProductTable, long> productTableRepository, IRepository<SupplierTable, long> supplierTableRepository)
        {
            _purchaseTableRepository = purchaseTableRepository;
            _purchaseProductRelationshipRepository = purchaseProductRelationshipRepository;
            _purchaseReturnGoodsTableRepository = purchaseReturnGoodsTableRepository;
            _productTableRepository = productTableRepository;
            _supplierTableRepository = supplierTableRepository;
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
            dto.PTD.DocumentPreparationTime = new DateTime();
            List<PurchaseProductRelationshipTable>? pprt = new List<PurchaseProductRelationshipTable>();
            var pr=new List<ProductTable>();
            foreach (var p in dto.PD)
            {
                var prModel = await _productTableRepository.FindAsync(p.Id);
                if (prModel != null&&p.Num<= prModel.Num)
                {
                    pprt.Add(ObjectMapper.Map<PurchaseProductRelationshipTableDto, PurchaseProductRelationshipTable>(new PurchaseProductRelationshipTableDto
                    {
                        Id = YitIdHelper.NextId(),
                        ProductId = p.Id,
                        PurchaseId = dto.PTD.Id,
                        TotalPrice = p.Price * p.Num,
                        Number=p.Num,
                        EnterOrNot = false,
                        Status = Stash.TableStatus.PurchaseProductRelationshipStatus.未入库
                    }));
                    prModel.Num-=p.Num;
                    pr.Add(prModel);
                }
            }
            var ptd = ObjectMapper.Map<PurchaseTableDto,PurchaseTable>(dto.PTD);
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
        /// 采购 列表 显示
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ResultApi<List<DisplayPurchasingDto>>> CreatePurchaseListAsync(GetPurchaseInquireDto dto)
        {
             var pprt=(await _purchaseProductRelationshipRepository.GetListAsync()).WhereIf(dto.Status != 0,x=>x.Status.Equals((PurchaseProductRelationshipStatus)dto.Status));
             var pr=(await _productTableRepository.GetListAsync())
                .WhereIf(dto.ProductId != 0,x=>x.Id.Equals(dto.ProductId))
                .WhereIf(!string.IsNullOrWhiteSpace(dto.ProductName),x=>x.ProductName.Contains(dto.ProductName));
             var su=await _supplierTableRepository.GetListAsync();
             var pu = (await _purchaseTableRepository.GetListAsync()).WhereIf(dto.PurchaseId != 0,x=>x.Id.Equals(dto.PurchaseId));
             var TableList = from a in pprt
                            join b in pr on a.ProductId equals b.Id
                            join c in su on b.DefaultSupplier equals c.Id
                            join d in pu on a.PurchaseId equals d.Id
                            select new DisplayPurchasingDto
                            {
                                PurchaseId = a.PurchaseId,
                                ProductName=b.ProductName,
                                ProductId=b.Id,
                                Specification=b.Specification,
                                Unit=b.Unit,
                                Price=b.Price,
                                Num=a.Number,
                                DefaultSupplier=b.DefaultSupplier,
                                SupplierName=c.SupplierName,
                                TotalPrice=a.TotalPrice,
                                Status=a.Status,
                                EnterOrNot=a.EnterOrNot,
                                ReturnGoods=a.ReturnGoods,
                                DocumentPreparationTime=d.DocumentPreparationTime,
                            };
            var totalCount=TableList.Count();   
            TableList=TableList.Skip((dto.PageIndex - 1) * dto.PageSize).Take(dto.PageSize).ToList();
            var api = new ResultApi<List<DisplayPurchasingDto>>();
            if (TableList.Count()!=0)
            {
                api.Code = 0;
                api.Data = TableList.ToList();
                api.Count = totalCount;
            }
            else
            {
                api.Code = 500;
                api.Data = null;
            }
            return api;
        }
        /// <summary>
        /// 数量的更改
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        public async Task<ResultApi<string>> GetPuraseFindAsync(long Id,int num)
        {
            var pprt= await _purchaseProductRelationshipRepository.FindAsync(Id);
            pprt.Number=num;
            var pprtUpdate=await _purchaseProductRelationshipRepository.UpdateAsync(pprt);
            var api = new ResultApi<string>();
            if (pprtUpdate!=null)
            {
                api.Code = 200;
                api.Data = "编辑成功!";
            }
            else
            {
                api.Code = 500;
                api.Data = "编辑失败!";
            }
            return api;
        }
        

        #endregion

    }
}
