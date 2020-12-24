﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InovaWebApi.Domains;
using InovaWebApi.Interfaces;
using InovaWebApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InovaWebApi.Controllers
{
    //gerar resposta json
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class CandidaturaController : ControllerBase
    {
        private ICandidaturaRepository _candidaturaRepository { get; set; }

        public CandidaturaController()
        {
            _candidaturaRepository = new CandidaturaRepository();
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                // Caso seja encontrado, retorna o candidatura buscado
                return Ok(_candidaturaRepository.GetAll());
            }
            catch (Exception error)
            {

                return BadRequest(error);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                Candidatura candidaturaBuscado = _candidaturaRepository.GetById(id);
                if (candidaturaBuscado != null)
                {
                    return Ok(candidaturaBuscado);
                }
                return NotFound("Candidatura não encontrada");

            }
            catch (Exception error)
            {

                return BadRequest(error);
            }

        }

        [HttpGet("vaga/{id}")]
        public IActionResult GetByVaga(int id)
        {
            try
            {
                return Ok(_candidaturaRepository.GetByVaga(id));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet("aluno/{id}")]
        public IActionResult ListarPorIdAluno(int id)
        {
            try
            {
                return Ok(_candidaturaRepository.GetByIdAluno(id));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        //Cadastro
        [Authorize(Roles = "Aluno")]
        [HttpPost]
        public IActionResult Post(Candidatura candidatura)
        {
            try
            {

                //if (_candidaturaRepository.GetByIdAluno(Convert.ToInt32(candidatura.IdAluno)) == null)
                //{
                    _candidaturaRepository.Add(candidatura);
                    return Ok();
                //}

                //return BadRequest("Você já se candidatou para esta vaga");

            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        //Atualiza
        [HttpPut("{id}")]
        public IActionResult Put(int id, Candidatura candidatura)
        {
            try
            {
                Candidatura candidaturaBuscada = _candidaturaRepository.GetById(id);
                if (candidaturaBuscada != null)
                {
                    Candidatura candidaturaAtualizada = new Candidatura
                    {
                        Contratado = candidatura.Contratado,
                        IdStatusCandidatura = candidatura.IdStatusCandidatura
                    };

                    _candidaturaRepository.Update(id, candidaturaAtualizada);
                    return Ok();
                }
                return NotFound();
            }
            catch (Exception error)
            {

                return BadRequest(error);
            }
        }

        //Deletar uma candidatura 
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                Candidatura candidaturaBuscado = _candidaturaRepository.GetById(id);
                if (candidaturaBuscado != null)
                {
                    _candidaturaRepository.Delete(id);
                    return Ok();
                }
                return NotFound();
            }
            catch (Exception error)
            {

                return BadRequest(error);
            }
        }
    }
}
