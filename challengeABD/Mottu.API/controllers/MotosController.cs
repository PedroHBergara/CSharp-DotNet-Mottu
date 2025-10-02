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
    public class MotosController : ControllerBase
    {
        private readonly IMotoService _motoService;
        private readonly LinkGenerator _linkGenerator;

        public MotosController(IMotoService motoService, LinkGenerator linkGenerator)
        {
            _motoService = motoService;
            _linkGenerator = linkGenerator;
        }

        [HttpGet(Name = "GetMotos")]
        public async Task<ActionResult<IEnumerable<MotoDto>>> GetMotos([FromQuery] PaginationParams paginationParams)
        {
            var motos = await _motoService.GetAllMotosAsync(paginationParams);
            foreach (var moto in motos)
            {
                moto.Links.Add(_linkGenerator.GenerateSelfLink("GetMoto", new { id = moto.Id }, "GET"));
                moto.Links.Add(_linkGenerator.GenerateLink("UpdateMoto", new { id = moto.Id }, "PUT", "PUT"));
                moto.Links.Add(_linkGenerator.GenerateLink("DeleteMoto", new { id = moto.Id }, "DELETE", "DELETE"));
            }
            return Ok(motos);
        }

        [HttpGet("{id}", Name = "GetMoto")]
        public async Task<ActionResult<MotoDto>> GetMoto(int id)
        {
            var moto = await _motoService.GetMotoByIdAsync(id);
            if (moto == null)
            {
                return NotFound();
            }
            moto.Links.Add(_linkGenerator.GenerateSelfLink("GetMoto", new { id = moto.Id }, "GET"));
            moto.Links.Add(_linkGenerator.GenerateLink("UpdateMoto", new { id = moto.Id }, "PUT", "PUT"));
            moto.Links.Add(_linkGenerator.GenerateLink("DeleteMoto", new { id = moto.Id }, "DELETE", "DELETE"));
            return Ok(moto);
        }

        [HttpPost(Name = "CreateMoto")]
        public async Task<ActionResult<MotoDto>> PostMoto(MotoDto motoDto)
        {
            await _motoService.AddMotoAsync(motoDto);
            motoDto.Links.Add(_linkGenerator.GenerateSelfLink("GetMoto", new { id = motoDto.Id }, "GET"));
            motoDto.Links.Add(_linkGenerator.GenerateLink("UpdateMoto", new { id = motoDto.Id }, "PUT", "PUT"));
            motoDto.Links.Add(_linkGenerator.GenerateLink("DeleteMoto", new { id = motoDto.Id }, "DELETE", "DELETE"));
            return CreatedAtAction(nameof(GetMoto), new { id = motoDto.Id }, motoDto);
        }

        [HttpPut("{id}", Name = "UpdateMoto")]
        public async Task<IActionResult> PutMoto(int id, MotoDto motoDto)
        {
            if (id != motoDto.Id)
            {
                return BadRequest();
            }
            await _motoService.UpdateMotoAsync(motoDto);
            return NoContent();
        }

        [HttpDelete("{id}", Name = "DeleteMoto")]
        public async Task<IActionResult> DeleteMoto(int id)
        {
            await _motoService.DeleteMotoAsync(id);
            return NoContent();
        }
    }
}
