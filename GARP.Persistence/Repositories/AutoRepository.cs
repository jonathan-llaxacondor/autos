using Dapper;
using GARP.Application.DTO.Auto;
using GARP.Application.DTO.Generic;
using GARP.Application.Interface.Persistence;
using GARP.Persistence.Contexts;
using GARP.Transversal.Common;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace GARP.Persistence.Repositories
{
    public class AutoRepository : IAutoRepository
    {
        private readonly DapperContext _dapperContext;

        public AutoRepository(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public async Task<RZGeneralStatus> CreateUpdate(DtoCUAuto dto)
        {
            using var conn = _dapperContext.CreateConnection();
            DynamicParameters parameters = new();
            parameters.Add("@ID", dto.Id, DbType.Int32);
            parameters.Add("@IDMARCA", dto.IdMarca, DbType.Int32);
            parameters.Add("@IDTIPOAUTO", dto.IdTipoAuto, DbType.Int32);
            parameters.Add("@MODELO", dto.Modelo, DbType.String);
            parameters.Add("@ANIO", dto.Anio, DbType.String);
            parameters.Add("@COLOR", dto.Color, DbType.String);
            parameters.Add("@PRECIO", dto.Precio, DbType.Decimal);
            parameters.Add("@KILOMETRAJE", dto.Kilometraje, DbType.Decimal);

            return await conn.QuerySingleAsync<RZGeneralStatus>("SP_CU_AUTO", param: parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task<DtoGetAutoById?> GetById(int id)
        {
            using var conn = _dapperContext.CreateConnection();
            DynamicParameters parameters = new();
            parameters.Add("@ID", id, DbType.Int32);

            return await conn.QuerySingleOrDefaultAsync<DtoGetAutoById?>("SP_GET_AUTO_BY_ID", param: parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task<RZGeneralStatus> Disable(int id)
        {
            using var conn = _dapperContext.CreateConnection();
            DynamicParameters parameters = new();
            parameters.Add("@ID", id, DbType.Int32);

            return await conn.QuerySingleAsync<RZGeneralStatus>("SP_DISABLE_AUTO", param: parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<DtoGetListAuto>> GetList()
        {
            using var conn = _dapperContext.CreateConnection();
            return await conn.QueryAsync<DtoGetListAuto>("SP_GET_LIST_AUTO", commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<DtoGetListComboIntDescripcion>> GetListComboMarca()
        {
            using var conn = _dapperContext.CreateConnection();
            return await conn.QueryAsync<DtoGetListComboIntDescripcion>("SP_GET_LIST_COMBO_MARCA", commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<DtoGetListComboIntDescripcion>> GetListComboTipoAuto()
        {
            using var conn = _dapperContext.CreateConnection();
            return await conn.QueryAsync<DtoGetListComboIntDescripcion>("SP_GET_LIST_COMBO_TIPOAUTO", commandType: CommandType.StoredProcedure);
        }
    }
}
