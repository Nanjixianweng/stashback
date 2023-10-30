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
        ///  采购列表具体查询
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        Task<ResultApi<PurchaseDto>> GetPuraseFindAsync(long Id);
        /// <summary>
        /// 采购 编辑
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<ResultApi<string>> PostPuraseFindEdit(PurchaseDto dto);
        #endregion
        #region 销售
        /// <summary>
        /// 采购添加
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<ResultApi<string>> CreateSellAsync(SellDto dto);
        /// <summary>
        /// 采购列表
        /// </summary>
        /// <returns></returns>
        Task<ResultApi<List<DisplayPurchasingDto>>> CreateSellListAsync(GetPurchaseInquireDto dto);
        /// <summary>
        /// 采购订单 具体查询
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        Task<ResultApi<SellDto>> GetSellFindAsync(long Id);
        /// <summary>
        /// 销售 编辑
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<ResultApi<string>> PostSellFindEdit(SellDto dto);
        #endregion

    }
}
