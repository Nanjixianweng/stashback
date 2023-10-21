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
        /// <summary>
        /// 采购添加
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<ResultApi<string>> CreatePurchaseAsync(PurchaseDto dto);  
        /// <summary>
        /// 采购列表
        /// </summary>
        /// <returns></returns>
        Task<ResultApi<List<DisplayPurchasingDto>>> CreatePurchaseListAsync (GetPurchaseInquireDto dto);
        /// <summary>
        ///  采购 数量的更改
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        Task<ResultApi<string>> GetPuraseFindAsync (long Id, int num);
        #endregion
        #region 销售

        #endregion

    }
}
