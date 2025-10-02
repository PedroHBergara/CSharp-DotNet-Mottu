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
    public class FuncionariosController : ControllerBase
    {
        private readonly IFuncionarioService _funcionarioService;
        private readonly LinkGenerator _linkGenerator;

        public FuncionariosController(IFuncionarioService funcionarioService, LinkGenerator linkGenerator)
        {
            _funcionarioService = funcionarioService;
            _linkGenerator = linkGenerator;
        }

        [HttpGet(Name = "GetFuncionarios")]
        public async Task<ActionResult<IEnumerable<FuncionarioDto>>> GetFuncionarios([FromQuery] PaginationParams paginationParams)
        {
            var funcionarios = await _funcionarioService.GetAllFuncionariosAsync(paginationParams);
            foreach (var funcionario in funcionarios)
            {
                funcionario.Links.Add(_linkGenerator.GenerateSelfLink("GetFuncionario", new { id = funcionario.Id }, "GET"));
                funcionario.Links.Add(_linkGenerator.GenerateLink("UpdateFuncionario", new { id = funcionario.Id }, "PUT", "PUT"));
                funcionario.Links.Add(_linkGenerator.GenerateLink("DeleteFuncionario", new { id = funcionario.Id }, "DELETE", "DELETE"));
            }
            return Ok(funcionarios);
        }

        [HttpGet("{id}", Name = "GetFuncionario")]
        public async Task<ActionResult<FuncionarioDto>> GetFuncionario(int id)
        {
            var funcionario = await _funcionarioService.GetFuncionarioByIdAsync(id);
            if (funcionario == null)
            {
                return NotFound();
            }
            funcionario.Links.Add(_linkGenerator.GenerateSelfLink("GetFuncionario", new { id = funcionario.Id }, "GET"));
            funcionario.Links.Add(_linkGenerator.GenerateLink("UpdateFuncionario", new { id = funcionario.Id }, "PUT", "PUT"));
            funcionario.Links.Add(_linkGenerator.GenerateLink("DeleteFuncionario", new { id = funcionario.Id }, "DELETE", "DELETE"));
            return Ok(funcionario);
        }

        [HttpPost(Name = "CreateFuncionario")]
        public async Task<ActionResult<FuncionarioDto>> PostFuncionario(FuncionarioDto funcionarioDto)
        {
            await _funcionarioService.AddFuncionarioAsync(funcionarioDto);
            funcionarioDto.Links.Add(_linkGenerator.GenerateSelfLink("GetFuncionario", new { id = funcionarioDto.Id }, "GET"));
            funcionarioDto.Links.Add(_linkGenerator.GenerateLink("UpdateFuncionario", new { id = funcionarioDto.Id }, "PUT", "PUT"));
            funcionarioDto.Links.Add(_linkGenerator.GenerateLink("DeleteFuncionario", new { id = funcionarioDto.Id }, "DELETE", "DELETE"));
            return CreatedAtAction(nameof(GetFuncionario), new { id = funcionarioDto.Id }, funcionarioDto);
        }

        [HttpPut("{id}", Name = "UpdateFuncionario")]
        public async Task<IActionResult> PutFuncionario(int id, FuncionarioDto funcionarioDto)
        {
            if (id != funcionarioDto.Id)
            {
                return BadRequest();
            }
            await _funcionarioService.UpdateFuncionarioAsync(funcionarioDto);
            return NoContent();
        }

        [HttpDelete("{id}", Name = "DeleteFuncionario")]
        public async Task<IActionResult> DeleteFuncionario(int id)
        {
            await _funcionarioService.DeleteFuncionarioAsync(id);
            return NoContent();
        }
    }
}
