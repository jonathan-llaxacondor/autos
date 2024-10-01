using GARP.Application.DTO.Auto;
using GARP.Application.DTO.Generic;
using GARP.Application.Interface.Persistence;
using GARP.Application.Interface.UseCases;
using GARP.Application.Validators;
using GARP.Transversal.Common;

namespace GARP.Application.UseCases
{
    public class AutoService : IAutoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly DtoCUAutoValidator _dtoCUAutoValidator;

        public AutoService(IUnitOfWork unitOfWork, DtoCUAutoValidator dtoCUAutoValidator)
        {
            _unitOfWork = unitOfWork;
            _dtoCUAutoValidator = dtoCUAutoValidator;
        }

        public async Task<RZGeneralStatus> CreateUpdate(DtoCUAuto dto)
        {
            RZGeneralStatus res = new();
            var validation = _dtoCUAutoValidator.Validate(dto);
            if (!validation.IsValid)
            {
                res.Ok = false;
                res.Mensaje = "Errores de Validación";
                res.Errors = RZErrorValidationsHelper.getErrorMessages(validation.Errors);
                return res;
            }

            try
            {
                res = await _unitOfWork.Auto.CreateUpdate(dto);
            }
            catch (Exception ex)
            {
                res.Ok = false;
                res.Mensaje = $"{ex.Message} error en CreateUpdate.";
            }
            return res;
        }

        public async Task<RZOneResponse<DtoGetAutoById?>> GetBydId(int id)
        {
            RZOneResponse<DtoGetAutoById?> res = new();
            try
            {
                res.Result = await _unitOfWork.Auto.GetById(id);
                res.Ok = true;
                res.Mensaje = "Búsqueda satisfactoria.";
            }
            catch (Exception ex)
            {
                res.Ok = false;
                res.Mensaje = $"{ex.Message} error en GetBydId.";
            }
            return res;
        }

        public async Task<RZGeneralStatus> Disable(int id)
        {
            RZGeneralStatus res = new();
            try
            {
                var item = await _unitOfWork.Auto.GetById(id);

                if(item == null)
                {
                    res.Mensaje = "El registro no existe.";
                    return res;
                }

                res = await _unitOfWork.Auto.Disable(id);
            }
            catch (Exception ex)
            {
                res.Ok = false;
                res.Mensaje = $"{ex.Message} error en Disable.";
            }
            return res;
        }

        public async Task<RZGeneralResponse<DtoGetListAuto>> GetList()
        {
            RZGeneralResponse<DtoGetListAuto> res = new();
            try
            {
                res.Data = await _unitOfWork.Auto.GetList();
                res.Ok = true;
                res.Mensaje = "Listado cargado correctamente.";
            }
            catch (Exception ex)
            {
                res.Ok = false;
                res.Mensaje = $"{ex.Message} error en GetList.";
            }
            return res;
        }

        public async Task<RZGeneralResponse<DtoGetListComboIntDescripcion>> GetListComboMarca()
        {
            RZGeneralResponse<DtoGetListComboIntDescripcion> res = new();
            try
            {
                res.Data = await _unitOfWork.Auto.GetListComboMarca();
                res.Ok = true;
                res.Mensaje = "Listado cargado correctamente.";
            }
            catch (Exception ex)
            {
                res.Ok = false;
                res.Mensaje = $"{ex.Message} error en GetListComboMarca.";
            }
            return res;
        }

        public async Task<RZGeneralResponse<DtoGetListComboIntDescripcion>> GetListComboTipoAuto()
        {
            RZGeneralResponse<DtoGetListComboIntDescripcion> res = new();
            try
            {
                res.Data = await _unitOfWork.Auto.GetListComboTipoAuto();
                res.Ok = true;
                res.Mensaje = "Listado cargado correctamente.";
            }
            catch (Exception ex)
            {
                res.Ok = false;
                res.Mensaje = $"{ex.Message} error en GetListComboTipoAuto.";
            }
            return res;
        }
    }
}