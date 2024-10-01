using GARP.Application.DTO.Auto;
using GARP.Application.Interface.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace GARP.Servicios.Controllers.WS_General
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutoController : ControllerBase
    {
        private readonly IAutoService _autoService;

        public AutoController(IAutoService autoService)
        {
            _autoService = autoService;
        }

        [HttpGet("combo/marca")]
        public async Task<IActionResult> GetListComboMarca()
        {
            return Ok(await _autoService.GetListComboMarca());

        }

        [HttpGet("combo/tipo-auto")]
        public async Task<IActionResult> GetListComboTipoAuto()
        {
            return Ok(await _autoService.GetListComboTipoAuto());

        }

        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            return Ok(await _autoService.GetList());
        }

        [HttpPost]
        public async Task<IActionResult> CreateUpdate([FromBody] DtoCUAuto dto)
        {
            return Ok(await _autoService.CreateUpdate(dto));
        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> Disable([FromRoute] int id)
        {
            return Ok(await _autoService.Disable(id));
        }
    }
}
