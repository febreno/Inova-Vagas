﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InovaWebApi.Domains;
using InovaWebApi.Interfaces;
using InovaWebApi.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InovaWebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private IUsuarioRepository _usuarioRepository;

        public UsuarioController()
        {
            _usuarioRepository = new UsuarioRepository();
        }

        [HttpPost("cadastroUsuario")]
        public IActionResult Post(Usuario novoUsuario)
        {
            try
            {
                int teste = _usuarioRepository.Cadastrar(novoUsuario);
                return StatusCode(201, teste);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_usuarioRepository.ListarTodos());
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }
    }
}
