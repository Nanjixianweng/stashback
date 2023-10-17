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
    }
}
