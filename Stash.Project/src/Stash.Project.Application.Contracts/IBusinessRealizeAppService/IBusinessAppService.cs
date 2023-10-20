using Stash.Project.IBusinessRealizeAppService.BusinessDto;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Stash.Project.IBusinessRealizeAppService
{
    /// <summary>
    /// 业务
    /// </summary>
    public interface IBusinessAppService : IApplicationService
    {
        #region 采购
        Task<ResultApi<PurchaseTableDto>> CreatePurchaseTableAsync(PurchaseTableDto dto);
        Task<ResultApi<List<PurchaseTableDto>>> GetPurchaseTableListAsync();
        Task<ResultApi<bool>> DeletePurchaseTableAsync(long Id);
        Task<ResultApi<PurchaseTableDto>> UpdatePurchaseTableAsync(PurchaseTableDto dto);
        Task<ResultApi<PurchaseTableDto>> FindPurchaseTableAsync(long Id);
        /// <summary>
        /// 采购添加
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<ResultApi<string>> CreatePurchaseAsync(PurchaseDto dto);  
        #endregion

    }
}
