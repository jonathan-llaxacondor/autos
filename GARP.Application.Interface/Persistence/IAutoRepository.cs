using GARP.Application.DTO.Auto;
using GARP.Application.DTO.Generic;
using GARP.Transversal.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GARP.Application.Interface.Persistence
{
    public interface IAutoRepository
    {
        Task<RZGeneralStatus> CreateUpdate(DtoCUAuto dto);
        Task<DtoGetAutoById?> GetById(int id);
        Task<RZGeneralStatus> Disable(int id);
        Task<IEnumerable<DtoGetListAuto>> GetList();
        Task<IEnumerable<DtoGetListComboIntDescripcion>> GetListComboMarca();
        Task<IEnumerable<DtoGetListComboIntDescripcion>> GetListComboTipoAuto();
    }
}
