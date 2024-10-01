using GARP.Application.DTO.Auto;
using GARP.Application.DTO.Generic;
using GARP.Transversal.Common;
using System.Threading.Tasks;

namespace GARP.Application.Interface.UseCases
{
    public interface IAutoService
    {
        Task<RZGeneralStatus> CreateUpdate(DtoCUAuto dto);
        Task<RZOneResponse<DtoGetAutoById?>> GetBydId(int id);
        Task<RZGeneralStatus> Disable(int id);
        Task<RZGeneralResponse<DtoGetListAuto>> GetList();
        Task<RZGeneralResponse<DtoGetListComboIntDescripcion>> GetListComboMarca();
        Task<RZGeneralResponse<DtoGetListComboIntDescripcion>> GetListComboTipoAuto();
    }
}
