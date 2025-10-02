using Microsoft.AspNetCore.Mvc;
using Mottu.Application.Services;
using Mottu.Domain.DTOs;
using Mottu.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mottu.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MotoristasController : ControllerBase
    {
        private readonly IMotoristaService _motoristaService;
        private readonly LinkGenerator _linkGenerator;

        public MotoristasController(IMotoristaService motoristaService, LinkGenerator linkGenerator)
        {
            _motoristaService = motoristaService;
            _linkGenerator = linkGenerator;
        }

        [HttpGet(Name = "GetMotoristas")]
        public async Task<ActionResult<IEnumerable<MotoristaDto>>> GetMotoristas([FromQuery] PaginationParams paginationParams)
        {
            var motoristas = await _motoristaService.GetAllMotoristasAsync(paginationParams);
            foreach (var motorista in motoristas)
            {
                motorista.Links.Add(_linkGenerator.GenerateSelfLink("GetMotorista", new { id = motorista.Id }, "GET"));
                motorista.Links.Add(_linkGenerator.GenerateLink("UpdateMotorista", new { id = motorista.Id }, "PUT", "PUT"));
                motorista.Links.Add(_linkGenerator.GenerateLink("DeleteMotorista", new { id = motorista.Id }, "DELETE", "DELETE"));
            }
            return Ok(motoristas);
        }

        [HttpGet("{id}", Name = "GetMotorista")]
        public async Task<ActionResult<MotoristaDto>> GetMotorista(int id)
        {
            var motorista = await _motoristaService.GetMotoristaByIdAsync(id);
            if (motorista == null)
            {
                return NotFound();
            }
            motorista.Links.Add(_linkGenerator.GenerateSelfLink("GetMotorista", new { id = motorista.Id }, "GET"));
            motorista.Links.Add(_linkGenerator.GenerateLink("UpdateMotorista", new { id = motorista.Id }, "PUT", "PUT"));
            motorista.Links.Add(_linkGenerator.GenerateLink("DeleteMotorista", new { id = motorista.Id }, "DELETE", "DELETE"));
            return Ok(motorista);
        }

        [HttpPost(Name = "CreateMotorista")]
        public async Task<ActionResult<MotoristaDto>> PostMotorista(MotoristaDto motoristaDto)
        {
            await _motoristaService.AddMotoristaAsync(motoristaDto);
            motoristaDto.Links.Add(_linkGenerator.GenerateSelfLink("GetMotorista", new { id = motoristaDto.Id }, "GET"));
            motoristaDto.Links.Add(_linkGenerator.GenerateLink("UpdateMotorista", new { id = motoristaDto.Id }, "PUT", "PUT"));
            motoristaDto.Links.Add(_linkGenerator.GenerateLink("DeleteMotorista", new { id = motoristaDto.Id }, "DELETE", "DELETE"));
            return CreatedAtAction(nameof(GetMotorista), new { id = motoristaDto.Id }, motoristaDto);
        }

        [HttpPut("{id}", Name = "UpdateMotorista")]
        public async Task<IActionResult> PutMotorista(int id, MotoristaDto motoristaDto)
        {
            if (id != motoristaDto.Id)
            {
                return BadRequest();
            }
            await _motoristaService.UpdateMotoristaAsync(motoristaDto);
            return NoContent();
        }

        [HttpDelete("{id}", Name = "DeleteMotorista")]
        public async Task<IActionResult> DeleteMotorista(int id)
        {
            await _motoristaService.DeleteMotoristaAsync(id);
            return NoContent();
        }
    }
}
