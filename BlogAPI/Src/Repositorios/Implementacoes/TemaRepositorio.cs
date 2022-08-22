using BlogAPI.Src.Contextos;
using BlogAPI.Src.Modelos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAPI.Src.Repositorios.Implementacoes
{
    /// <summary>
    /// <para>Resumo: Classe responsavel por implementar ITema</para>
    /// <para>Criado por: Fabio</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 18/08/2022</para>
    /// </summary>
    public class TemaRepositorio : ITema
    {
        #region Atributos

        private readonly BlogPessoalContexto _contexto;

        #endregion

        public TemaRepositorio(BlogPessoalContexto contexto)
        {
            _contexto = contexto;
        }

        #region Métodos

        /// <summary>
        /// <para>Resumo: Método assíncrono para atualizar um tema</para>
        /// </summary>
        /// <param name="tema">Construtor para atualizar um tema</param>
        public async Task AtualizarTemaAsync(Tema tema)
        {
            if (!ExisteDescricao(tema.Descricao)) throw new Exception("Descrição já existente no sistema");

            var auxiliar = await PegarTemaPeloIdAsync(tema.Id);
            auxiliar.Descricao = tema.Descricao;
            _contexto.Temas.Update(auxiliar);
            await _contexto.SaveChangesAsync();
        }
        /// <summary>
        /// <para>Resumo: Método assíncrono para deletar um tema pelo id</para>
        /// </summary>
        /// <param name="id">Construtor para deletar um tema</param>
        public async Task DeletarTemaAsync(int id)
        {
            _contexto.Temas.Remove(await PegarTemaPeloIdAsync(id));
            await _contexto.SaveChangesAsync();
        }
        /// <summary>
        /// <para>Resumo: Método assíncrono para salvar um novo tema</para>
        /// </summary>
        /// <param name="tema">Construtor para cadastrar tema</param>
        public async Task NovoTemaAsync(Tema tema)
        {
            if (!ExisteDescricao(tema.Descricao)) throw new Exception("Descrição já existente no sistema");
            
            await _contexto.Temas.AddAsync(new Tema
            {
                Descricao = tema.Descricao
            });
            await _contexto.SaveChangesAsync();

            
        }
        /// <summary>
        /// <para>Resumo: Método assíncrono para buscar um tema pelo id</para>
        /// </summary>
        /// <param name="id">Construtor para buscar um tema pelo id</param>
        public async Task<Tema> PegarTemaPeloIdAsync(int id)
        {
            if(!ExisteId(id)) throw new Exception("Id do tema não encontrado");
            return await _contexto.Temas.FirstOrDefaultAsync(t => t.Id == id);

            //Função Auxiliar
            bool ExisteId(int id)
            {
                var auxiliar = _contexto.Temas.FirstOrDefault(t => t.Id == id);
                return auxiliar != null;
            }
        }
        /// <summary>
        /// <para>Resumo: Método assíncrono para buscar todos temas</para>
        /// </summary>        
        public async Task<List<Tema>> PegarTodosTemasAsync()
        {
            return await _contexto.Temas.ToListAsync();
        }

        //Função Auxiliar
        bool ExisteDescricao(string descricao)
        {
            var auxiliar = _contexto.Temas.FirstOrDefault(t => t.Descricao == descricao);
            return auxiliar != null;
        }
        #endregion
    }
}
