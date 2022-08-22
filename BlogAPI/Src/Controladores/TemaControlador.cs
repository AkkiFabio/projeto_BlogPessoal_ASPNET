using BlogAPI.Src.Modelos;
using BlogAPI.Src.Repositorios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BlogAPI.Src.Controladores
{
    [ApiController]
    [Route("api/Temas")]
    [Produces("application/json")]
    public class TemaControlador : ControllerBase
    {

        #region Atributos

        private readonly ITema _repositorio;

        #endregion

        #region Construtores
        public TemaControlador(ITema repositorio)
        {
            _repositorio = repositorio;
        }
        #endregion
        #region Métodos            

        /// <summary>
        /// Pegar todos Temas
        /// </summary>        
        /// <returns>ActionResult</returns>
        /// <response code="200">Retorna a lista de Temas</response>
        /// <response code="204">Resultado vazio</response>
        [HttpGet]
        [Authorize]
        public async Task<ActionResult> PegarTodosTemasAsync()
        {
            var lista = await _repositorio.PegarTodosTemasAsync();
            if (lista.Count < 1) return NoContent();
            return Ok(lista);
        }

        /// <summary>
        /// Pegar tema pelo id
        /// </summary>
        /// <param name="idTema">id do Tema</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Retorna o Tema</response>
        /// <response code="404">Tema não existente</response>
        [HttpGet("id/{idTema}")]
        [Authorize]
        public async Task<ActionResult> PegarTemaPeloIdAsync([FromRoute] int idTema)
        {
            try
            {
                return Ok(await _repositorio.PegarTemaPeloIdAsync(idTema));
            }
            catch (Exception ex)
            {
                return NotFound(new { Mensagem = ex.Message });
            }
        }

        /// <summary>
        /// Criar novo Tema
        /// </summary>
        /// <param name="tema">Contrutor para criar tema</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        /// POST /api/Temas
        /// {
        /// "descricao": "Economia",
        /// }
        ///
        /// </remarks>
        /// <response code="201">Retorna tema criado</response>
        /// <response code="400">Retorna erro na criação</response>
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> NovoTemaAsync([FromBody] Tema tema)
        {
            try
            {
                await _repositorio.NovoTemaAsync(tema);
                return Created($"api/Temas", tema);
            }
            catch(Exception ex)
            {
                return BadRequest (new { Mensagem = ex.Message });
            }
        }

        /// <summary>
        /// Atualizar Tema
        /// </summary>
        /// <param name="tema">Contrutor para atualizar tema</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        /// PUT /api/Temas
        /// {
        /// "descricao": "Economia",
        /// }
        ///
        /// </remarks>
        /// <response code="200">Retorna tema atualizado</response>
        /// <response code="400">Retorna erro na atualização</response>
        [HttpPut]
        [Authorize(Roles ="ADMINISTRADOR")]
        public async Task<ActionResult> AtualizarTemaAsync([FromBody] Tema tema)
        {
            try
            {
                await _repositorio.AtualizarTemaAsync(tema);
                return Ok(tema);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Mensagem = ex.Message });
            }
        }

        /// <summary>
        /// Deletar Tema pelo id
        /// </summary>
        /// <param name="idTema">Contrutor para deletar tema</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        /// DELETE /api/Temas/deletar/{idTema}
        /// 
        /// </remarks>
        /// <response code="204">Retorna tema deletado</response>
        /// <response code="404">Retorna tema não existente</response>
        [HttpDelete("deletar/{idTema}")]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<ActionResult> DeletarTema([FromRoute] int idTema)
        {
            try
            {
                await _repositorio.DeletarTemaAsync(idTema);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(new { Mensagem = ex.Message });
            }
        } 

        #endregion

    }


}
