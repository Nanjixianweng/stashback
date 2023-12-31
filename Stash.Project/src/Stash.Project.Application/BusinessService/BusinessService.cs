﻿using AutoMapper;
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
    /// <summary>
    /// 采购控制器
    /// </summary>
    public class BusinessService : ApplicationService, IBusinessAppService
    {
        /// <summary>
        /// 采购表
        /// </summary>
        private readonly IRepository<PurchaseTable, long> _purchaseTableRepository;
        /// <summary>
        /// 采购产品关系表
        /// </summary>
        private readonly IRepository<PurchaseProductRelationshipTable, long> _purchaseProductRelationshipRepository;
        /// <summary>
        /// 采购退货表
        /// </summary>
        private readonly IRepository<PurchaseReturnGoodsTable, long> _purchaseReturnGoodsTableRepository;
        /// <summary>
        /// 产品表
        /// </summary>
        private readonly IRepository<ProductTable, long> _productTableRepository;
        /// <summary>
        /// 供应商
        /// </summary>
        private readonly IRepository<SupplierTable, long> _supplierTableRepository;
        /// <summary>
        /// 销售表
        /// </summary>
        private readonly IRepository<SellTable, long> _sellTableRepository;
        /// <summary>
        /// 销售产品关系表
        /// </summary>
        private readonly IRepository<SellProductRelationshipTable, long> _sellProductRelationshipRepository;
        /// <summary>
        /// 销售退货表
        /// </summary>
        private readonly IRepository<SalesReturnsTable, long> _salesReturnsTableRepository;
        public BusinessService(IRepository<PurchaseTable, long> purchaseTableRepository, IRepository<PurchaseProductRelationshipTable, long> purchaseProductRelationshipRepository, IRepository<PurchaseReturnGoodsTable, long> purchaseReturnGoodsTableRepository, IRepository<ProductTable, long> productTableRepository, IRepository<SupplierTable, long> supplierTableRepository, IRepository<SellTable, long> sellTableRepository, IRepository<SellProductRelationshipTable, long> sellProductRelationshipRepository, IRepository<SalesReturnsTable, long> salesReturnsTableRepository)
        {
            _purchaseTableRepository = purchaseTableRepository;
            _purchaseProductRelationshipRepository = purchaseProductRelationshipRepository;
            _purchaseReturnGoodsTableRepository = purchaseReturnGoodsTableRepository;
            _productTableRepository = productTableRepository;
            _supplierTableRepository = supplierTableRepository;
            _sellTableRepository = sellTableRepository;
            _sellProductRelationshipRepository = sellProductRelationshipRepository;
            _salesReturnsTableRepository = salesReturnsTableRepository;
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
            dto.PTD.Id = YitIdHelper.NextId();
            List<PurchaseProductRelationshipTable>? pprt = new List<PurchaseProductRelationshipTable>();
            foreach (var p in dto.PD)
            {
                var prModel = await _productTableRepository.FindAsync(p.Id);
                if (prModel != null && p.Num <= prModel.Num)
                {
                    pprt.Add(ObjectMapper.Map<PurchaseProductRelationshipTableDto, PurchaseProductRelationshipTable>(new PurchaseProductRelationshipTableDto
                    {
                        Id = YitIdHelper.NextId(),
                        ProductId = p.Id,
                        PurchaseId = dto.PTD.Id,
                        TotalPrice = p.Price * p.Num,
                        Number = p.Num,
                        EnterOrNot = false,
                        Status = Stash.TableStatus.PurchaseProductRelationshipStatus.采购中
                    }));
                }
            }
            var ptd = ObjectMapper.Map<PurchaseTableDto, PurchaseTable>(dto.PTD);
            //采购表添加
            var pur = await _purchaseTableRepository.InsertAsync(ptd);
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
            var pprt = (await _purchaseProductRelationshipRepository.GetListAsync()).WhereIf(dto.Status != 0, x => x.Status.Equals((PurchaseProductRelationshipStatus)dto.Status));
            var pr = (await _productTableRepository.GetListAsync())
               .WhereIf(dto.ProductId != 0, x => x.Id.Equals(dto.ProductId))
               .WhereIf(!string.IsNullOrWhiteSpace(dto.ProductName), x => x.ProductName.Contains(dto.ProductName));
            var su = await _supplierTableRepository.GetListAsync();
            var pu = (await _purchaseTableRepository.GetListAsync()).WhereIf(dto.PurchaseId != 0, x => x.Id.Equals(dto.PurchaseId));
            var TableList = from a in pprt
                            join b in pr on a.ProductId equals b.Id
                            join c in su on b.DefaultSupplier equals c.Id
                            join d in pu on a.PurchaseId equals d.Id
                            select new DisplayPurchasingDto
                            {
                                PurchaseId = a.PurchaseId,
                                ProductName = b.ProductName,
                                ProductId = b.Id,
                                Specification = b.Specification,
                                Unit = b.Unit,
                                Price = b.Price,
                                Num = a.Number,
                                DefaultSupplier = b.DefaultSupplier,
                                SupplierName = c.SupplierName,
                                TotalPrice = a.TotalPrice,
                                Status = a.Status,
                                EnterOrNot = a.EnterOrNot,
                                ReturnGoods = a.ReturnGoods,
                                DocumentPreparationTime = d.DocumentPreparationTime,
                            };
            var totalCount = TableList.Count();
            TableList = TableList.Skip((dto.PageIndex - 1) * dto.PageSize).Take(dto.PageSize).ToList();
            var api = new ResultApi<List<DisplayPurchasingDto>>();
            if (TableList.Count() != 0)
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
        /// 采购订单 具体查询
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        public async Task<ResultApi<PurchaseDto>> GetPuraseFindAsync(long Id)
        {
            //获取采购表
            var pu = await _purchaseTableRepository.FindAsync(Id);
            //获取采购关系表
            var pprt = (await _purchaseProductRelationshipRepository.ToArrayAsync()).WhereIf(Id != null, x => x.PurchaseId.Equals(Id)).Select(x => x.ProductId);
            var api = new ResultApi<PurchaseDto>();
            if (pu != null && pprt.Count() > 0)
            {
                //查询产品表
                var pr = (await _productTableRepository.ToArrayAsync()).Where(
                     x => pprt.Contains(x.Id));
                api.Code = 0;
                api.Data = new PurchaseDto
                {
                    PTD = ObjectMapper.Map<PurchaseTable,
                    PurchaseTableDto>(pu),
                    PD = ObjectMapper.Map<List<ProductTable>, List<
                    ProductDto>>(pr.ToList())
                };
            }
            else
            {
                api.Code = 500;
                api.Data = null;
            }
            return api;
        }

        public async Task PostPuraseFindEdit112233(PurchaseTableDto dto)
        {
            var ptd = ObjectMapper.Map<PurchaseTableDto, PurchaseTable>(dto);
            //采购表修改
            var pur = await _purchaseTableRepository.UpdateAsync(ptd);

            await _purchaseTableRepository.UpdateAsync(pur);
        }


        /// <summary>
        /// 采购 编辑
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<ResultApi<string>> PostPuraseFindEdit(PurchaseDto dto)
        {
            //var a = (_purchaseTableRepository.ToListAsync()).Result;
            var api = new ResultApi<string>();
                var ptd = ObjectMapper.Map<PurchaseTableDto, PurchaseTable>(dto.PTD);

                //采购表修改
                var pur =await _purchaseTableRepository.UpdateAsync(ptd);

                List<PurchaseProductRelationshipTable>? pprt = new List<PurchaseProductRelationshipTable>();
                foreach (var p in dto.PD)
                {
                    var prModel =await _purchaseProductRelationshipRepository.FirstOrDefaultAsync(x => x.ProductId.Equals(p.Id) && x.PurchaseId.Equals(dto.PTD.Id));

                    if (prModel != null)
                    {
                        prModel.TotalPrice = p.Price * p.Num;
                        prModel.Number = p.Num;
                        pprt.Add(prModel);
                    }
                }
                //采购产品关系表批量修改
                await _purchaseProductRelationshipRepository.UpdateManyAsync(pprt);

                if (pur != null)
                {
                    api.Code = 200;
                    api.Data = "修改成功";
                }
                else
                {
                    api.Code = 500;
                    api.Data = "修改失败";
                }

            return api;
        }


        #endregion
        #region 销售
        /// <summary>
        /// 销售 添加
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ResultApi<string>> CreateSellAsync(SellDto dto)
        {
            dto.Std.Id = YitIdHelper.NextId();
            List<SellProductRelationshipTable>? pprt = new List<SellProductRelationshipTable>();
            foreach (var p in dto.PD)
            {
                var prModel = await _productTableRepository.FindAsync(p.Id);
                if (prModel != null && p.Num <= prModel.Num)
                {
                    pprt.Add(ObjectMapper.Map<SellProductRelationshipTableDto, SellProductRelationshipTable>(new SellProductRelationshipTableDto
                    {
                        Id = YitIdHelper.NextId(),
                        ProductId = p.Id,
                        SellId = dto.Std.Id,
                        TotalPrice = p.Price * p.Num,
                        Number = p.Num,
                        EnterOrNot = false,
                        Status = Stash.TableStatus.SellProductRelationshipStatus.销售中
                    }));
                }
            }
            var ptd = ObjectMapper.Map<SellTableDto, SellTable>(dto.Std);
            //采购表添加
            var pur = await _sellTableRepository.InsertAsync(ptd);
            //采购产品关系表批量添加
            await _sellProductRelationshipRepository.InsertManyAsync(pprt);
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
        /// 销售 列表 显示
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ResultApi<List<DisplayPurchasingDto>>> CreateSellListAsync(GetPurchaseInquireDto dto)
        {
            var pprt = (await _sellProductRelationshipRepository.GetListAsync()).WhereIf(dto.Status != 0, x => x.Status.Equals((SellProductRelationshipStatus)dto.Status));
            var pr = (await _productTableRepository.GetListAsync())
               .WhereIf(dto.ProductId != 0, x => x.Id.Equals(dto.ProductId))
               .WhereIf(!string.IsNullOrWhiteSpace(dto.ProductName), x => x.ProductName.Contains(dto.ProductName));
            var su = await _supplierTableRepository.GetListAsync();
            var pu = (await _sellTableRepository.GetListAsync()).WhereIf(dto.PurchaseId != 0, x => x.Id.Equals(dto.PurchaseId));
            var TableList = from a in pprt
                            join b in pr on a.ProductId equals b.Id
                            join c in su on b.DefaultSupplier equals c.Id
                            join d in pu on a.SellId equals d.Id
                            select new DisplayPurchasingDto
                            {
                                PurchaseId = a.SellId,
                                ProductName = b.ProductName,
                                ProductId = b.Id,
                                Specification = b.Specification,
                                Unit = b.Unit,
                                Price = b.Price,
                                Num = a.Number,
                                DefaultSupplier = b.DefaultSupplier,
                                SupplierName = c.SupplierName,
                                TotalPrice = a.TotalPrice,
                                Status = (PurchaseProductRelationshipStatus)a.Status,
                                EnterOrNot = a.EnterOrNot,
                                ReturnGoods = a.ReturnGoods,
                                DocumentPreparationTime = d.DocumentPreparationTime,
                            };
            var totalCount = TableList.Count();
            TableList = TableList.Skip((dto.PageIndex - 1) * dto.PageSize).Take(dto.PageSize).ToList();
            var api = new ResultApi<List<DisplayPurchasingDto>>();
            if (TableList.Count() != 0)
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
        /// 采购订单 具体查询
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        public async Task<ResultApi<SellDto>> GetSellFindAsync(long Id)
        {
            //获取销售表
            var pu = await _sellTableRepository.FindAsync(Id);
            //获取销售关系表
            var pprt = (await _sellProductRelationshipRepository.ToArrayAsync()).WhereIf(Id != null, x => x.SellId.Equals(Id)).Select(x => x.ProductId);
            var api = new ResultApi<SellDto>();
            if (pu != null && pprt.Count() > 0)
            {
                //查询产品表
                var pr = (await _productTableRepository.ToArrayAsync()).Where(
                     x => pprt.Contains(x.Id));
                api.Code = 0;
                api.Data = new SellDto
                {
                    Std = ObjectMapper.Map<SellTable,
                    SellTableDto>(pu),
                    PD = ObjectMapper.Map<List<ProductTable>, List<
                    ProductDto>>(pr.ToList())
                };
            }
            else
            {
                api.Code = 500;
                api.Data = null;
            }
            return api;
        }
        /// <summary>
        /// 销售 编辑
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<ResultApi<string>> PostSellFindEdit(SellDto dto)
        {
            var pprt = new List<SellProductRelationshipTable>();
            foreach (var p in dto.PD)
            {
                var prModel = await _sellProductRelationshipRepository.FirstOrDefaultAsync(x => x.ProductId.Equals(p.Id) && x.SellId.Equals(dto.Std.Id));
                if (prModel != null)
                {
                    prModel.TotalPrice = p.Price * p.Num;
                    prModel.Number = p.Num;
                    pprt.Add(prModel);
                }
            }
            var ptd = ObjectMapper.Map<SellTableDto, SellTable>(dto.Std);
            //采购表修改
            var pur = await _sellTableRepository.UpdateAsync(ptd);
            //采购产品关系表批量修改
            await _sellProductRelationshipRepository.UpdateManyAsync(pprt);
            var api = new ResultApi<string>();
            if (pur != null)
            {
                api.Code = 200;
                api.Data = "修改成功";
            }
            else
            {
                api.Code = 500;
                api.Data = "修改失败";
            }
            return api;
        }

       
        #endregion
    }

}