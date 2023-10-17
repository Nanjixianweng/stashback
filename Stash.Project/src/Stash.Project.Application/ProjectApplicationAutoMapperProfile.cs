using AutoMapper;
using Stash.Project.IBasicService.BasicDto;
using Stash.Project.Stash.BasicData.Model;

namespace Stash.Project;

public class ProjectApplicationAutoMapperProfile : Profile
{
    public ProjectApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        CreateMap<StoreDto, StoreTale>().ReverseMap();
        CreateMap<StorageLocationDto, StorageLocationTable>().ReverseMap();
        CreateMap<SupplierDto, SupplierTable>().ReverseMap();
        CreateMap<UnitDto, UnitTable>().ReverseMap();
        CreateMap<ProductCategoryDto, ProductCategoryTable>().ReverseMap();
        CreateMap<ProductDto, ProductTable>().ReverseMap();
        CreateMap<ContactDto, ContactTable>().ReverseMap();
        CreateMap<CustomerDto, CustomerTable>().ReverseMap();
    }
}
