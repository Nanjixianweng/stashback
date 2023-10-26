using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Stash.Project.IWarehouseManage.WarehouseDto;

namespace Stash.Project.IWarehouseManage
{
    public interface IWarehouseService:IApplicationService
    {
        Task<ApiResult> GetDocumentTypeAsync();

        Task<ApiResult> PostAddPutWarehouse(AddWarehouseDto obj);

        Task<ApiResult> PostAddOutWarehouse(AddWarehouseDto obj);
    }
}
