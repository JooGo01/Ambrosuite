using Ambrosuite.ApiService.Controllers;
using Ambrosuite.ApiService.Data;
using Ambrosuite.ApiService.Entities;
using Ambrosuite.ApiService.EntitiesDTO;
using Ambrosuite.ApiService.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace Ambrosuite.ApiService.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public UsuarioService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Usuario> CrearUsuarioAsync(RegistroDTO usuarioDto)
        {
            using var sha256 = SHA256.Create();
            byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(usuarioDto.contrasenia));
            string hashedPassword = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();

            // Creación del nuevo usuario
            // Rol id 5 va a ser el rol para el usuario que se registre default en la base de datos
            var usuario = new Usuario
            {
                nombre = usuarioDto.nombre,
                apellido = usuarioDto.apellido,
                fecha_nacimiento = usuarioDto.fechaNacimiento,
                email = usuarioDto.email,
                cuil = usuarioDto.cuil,
                contrasenia = hashedPassword,
                rol_id = 5,
                baja = 0
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return usuario;
        }

        public async Task<Usuario> LoginAsync(string email, string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var passwordHash = BitConverter.ToString(sha256.ComputeHash(Encoding.UTF8.GetBytes(password))).Replace("-", "").ToLowerInvariant();
                return await _context.Usuarios.FirstOrDefaultAsync(u => u.email == email && u.contrasenia == passwordHash);
            }
        }
    }
}
