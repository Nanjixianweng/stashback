using AutoMapper;
using Stash.Project.IBasicService;
using Stash.Project.IBasicService.BasicDto;
using Stash.Project.Stash.BasicData.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Yitter.IdGenerator;

namespace Stash.Project.BasicService
{
    public class UnitService : ApplicationService, IUnitService
    {
        public readonly IRepository<UnitTable, long> _unit;
        public readonly IMapper _mapper;

        public UnitService(IRepository<UnitTable, long> unit,IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }

        /// <summary>
        /// 单位新增
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<ApiResult> CreateUnitAsync(UnitDto dto)
        {
            YitIdHelper.SetIdGenerator(new IdGeneratorOptions());
            dto.Id = YitIdHelper.NextId();
            var info = _mapper.Map<UnitDto, UnitTable>(dto);
            var res = await _unit.InsertAsync(info);
            if (res == null)
            {
                return new ApiResult { code = ResultCode.Error, msg = ResultMsg.AddError, data = res };
            }
            return new ApiResult { code = ResultCode.Success, msg = ResultMsg.AddSuccess, data = res };
        }

        /// <summary>
        /// 删除单位
        /// </summary>
        /// <param name="unitid"></param>
        /// <returns></returns>
        public async Task<ApiResult> DeleteUnitAsync(long unitid)
        {
            var res = await _unit.FirstOrDefaultAsync(x => x.Id == unitid);

            await _unit.DeleteAsync(unitid);

            if (res != null)
            {
                return new ApiResult
                {
                    code = ResultCode.Success,
                    msg = ResultMsg.DeleteSuccess,
                    data = res
                };
            }

            return new ApiResult
            {
                code = ResultCode.Error,
                msg = ResultMsg.DeleteError,
                data = res
            };
        }

        /// <summary>
        /// 查询指定单位信息
        /// </summary>
        /// <param name="unitid"></param>
        /// <returns></returns>
        public async Task<ApiResult> GetUnitInfoAsync(long unitid)
        {
            var res = await _unit.FirstOrDefaultAsync(x => x.Id == unitid);

            return new ApiResult
            {
                code = ResultCode.Success,
                msg = ResultMsg.RequestSuccess,
                data = res
            };
        }

        /// <summary>
        /// 单位条件查询
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<ApiResult> GetUnitListAsync(UnitInquireDto dto)
        {
            var list = (await _unit.GetListAsync())
                .WhereIf(dto.unitnumber != 0, x => x.Id == dto.unitnumber)
                .WhereIf(!string.IsNullOrEmpty(dto.unitname), x => x.UnitName.Contains(dto.unitname));


            var totalcount = list.Count();

            var res = list.Skip((dto.pageIndex - 1) * dto.pageSize).Take(dto.pageSize).ToList();

            return new ApiResult { code = ResultCode.Success, msg = ResultMsg.RequestSuccess, data = res, count = totalcount };
        }

        /// <summary>
        /// 修改单位
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<ApiResult> UpdateUnitAsync(UnitDto dto)
        {
            var info = _mapper.Map<UnitDto, UnitTable>(dto);
            var res = await _unit.UpdateAsync(info);
            if (res == null)
            {
                return new ApiResult { code = ResultCode.Error, msg = ResultMsg.UpdateError, data = res };
            }
            return new ApiResult { code = ResultCode.Success, msg = ResultMsg.UpdateSuccess, data = res };
        }

        /// <summary>
        /// 单位查询
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult> GetStoreAsync()
        {
            var list = await _unit.GetListAsync();

            return new ApiResult { code = ResultCode.Success, msg = ResultMsg.RequestSuccess, data = list };
        }
    }
}
