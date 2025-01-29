using Ambrosuite.ApiService.Data;
using Ambrosuite.ApiService.Entities;
using Ambrosuite.ApiService.EntitiesDTO;
using Ambrosuite.ApiService.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ambrosuite.ApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccesoUsuariosController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public AccesoUsuariosController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<AccesoUsuario>>> GetAllAccesoUsuario()
        {
            var accesos = await _context.AccesoUsuarios.Include(p => p.usuario).ToListAsync();
            return Ok(accesos);
        }

        [HttpGet("usuario/{usuarioId}")]
        public async Task<ActionResult<List<AccesoUsuario>>> GetAccesoUsuarioByUsuario(int usuarioId)
        {
            var accesosUsuario = await _context.AccesoUsuarios
                .Include(p => p.usuario)
                .Where(p => p.usuario_id == usuarioId)
                .ToListAsync();
            return Ok(accesosUsuario);
        }
        [HttpPost]
        public async Task<ActionResult<AccesoUsuarioDTO>> AddAccesoUsuario(AccesoUsuarioDTO p_accesoUsuario)
        {
            var accesoUsuario = _mapper.Map<AccesoUsuario>(p_accesoUsuario);
            _context.AccesoUsuarios.Add(accesoUsuario);
            await _context.SaveChangesAsync();
            var accesoUsuarioResultDto = _mapper.Map<AccesoUsuarioDTO>(p_accesoUsuario);
            return Ok(accesoUsuarioResultDto);
        }
    }
}
