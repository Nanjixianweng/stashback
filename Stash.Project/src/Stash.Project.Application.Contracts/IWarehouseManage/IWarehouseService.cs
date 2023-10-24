using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Stash.Project.IWarehouseManage
{
    public interface IWarehouseService:IApplicationService
    {
        Task<ApiResult> GetDocumentTypeAsync();
    }
}
