﻿using AutoMapper;
using Stash.Project.BasicDto;
using Stash.Project.IBusinessRealizeAppService.BusinessDto;
using Stash.Project.IBasicService.BasicDto;
using Stash.Project.Stash.BasicData.Model;
using Stash.Project.Stash.BusinessManage.Model;

namespace Stash.Project;

public class ProjectApplicationAutoMapperProfile : Profile
{
    public ProjectApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        CreateMap<StoreDto, StoreTale>().ReverseMap();
        #region  业务
        //采购
        CreateMap<PurchaseTable, PurchaseTableDto>().ReverseMap();
        //采购退货表
        CreateMap<PurchaseReturnGoodsTable, PurchaseReturnGoodsTableDto>().ReverseMap();
        //采购产品关系
        CreateMap<PurchaseProductRelationshipTable, PurchaseProductRelationshipTableDto>().ReverseMap();
        //销售表
        CreateMap<SellTable, SellTableDto>().ReverseMap();
        //销售产品关系表
        CreateMap<SellProductRelationshipTable, SellProductRelationshipTableDto>().ReverseMap();
        //销售退货表
        CreateMap<SalesReturnsTable, SalesReturnsTableDto>().ReverseMap();
        #endregion
        CreateMap<StorageLocationDto, StorageLocationTable>().ReverseMap();
    }
}
