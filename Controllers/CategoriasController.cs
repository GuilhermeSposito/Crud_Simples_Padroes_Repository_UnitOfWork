using ApiCatalogoTeste2.Context;
using ApiCatalogoTeste2.Models;
using ApiCatalogoTeste2.Repositorys.Categorias;
using ApiCatalogoTeste2.Repositorys.Generics;
using ApiCatalogoTeste2.Repositorys.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace ApiCatalogoTeste2.Controllers;

[Route("/api/v1/[controller]")]
[ApiController]
public class CategoriasController : Controller
{
    private readonly IUnitOfWork _ufw;

    public CategoriasController(IUnitOfWork ufw)
    {
        _ufw = ufw;
    }

    [HttpGet]
    public async Task<ActionResult<CategoriaDTO>> GetCategorias()
    {
        IEnumerable<Categoria> Categorias = _ufw.CategoriasRepository.GettAll();

        if (Categorias is null)
            return NotFound("Categorias Não Encontradas");

        return Ok(Categorias);
    }

    [HttpGet("{id:int}", Name = ("ObterCategoria"))]
    public async Task<ActionResult<CategoriaDTO>> GetCategoria(int id)
    {

        Categoria? categoria = _ufw.CategoriasRepository.Get(x=> x.Id == id);


        if (categoria is null)
            return NotFound("Categoria encontrada não foi encontrada no banco de dados");


        return Ok(categoria);
    }

    [HttpPost]
    public async Task<ActionResult<CategoriaDTO>> PostCategoria(CategoriaDTO categoria)
    {
        if (categoria is null || categoria.Nome is null || categoria.Descricao is null)
        {
            return BadRequest("Insira uma categoria Valida");
        }

        Categoria CategoriaAtualizada = _ufw.CategoriasRepository.Create(new Categoria() { Nome = categoria.Nome, Descricao = categoria.Descricao });
        _ufw.Commit();

        return new CreatedAtRouteResult("ObterCategoria", new { id = CategoriaAtualizada.Id }, CategoriaAtualizada);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<Categoria>> UpdateCategoria(int id, CategoriaDTO categoriaDto)
    {
        if (id != categoriaDto.Id)
        {
            return BadRequest("Dados Invalidos");
        }

        Categoria? CategoriaFind = _ufw.CategoriasRepository.Get(x=> x.Id == id);
        if (CategoriaFind is null)
            return BadRequest("Produto não encontrado no banco de dados");

        if(categoriaDto.Nome is null || categoriaDto.Descricao is null)
            return BadRequest("Dados invalidos!");


        Categoria CategoriaModificada = _ufw.CategoriasRepository.Update(new Categoria()
        {
            Id = categoriaDto.Id,
            Nome = categoriaDto.Nome,
            Descricao = categoriaDto.Descricao,          
        });
        _ufw.Commit();

        return Ok(CategoriaModificada);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<Categoria>> DeleteCategoria(int id)
    {
        Categoria? Categoria = _ufw.CategoriasRepository.Get(x => x.Id == id);

        if (Categoria is null)
        {
            return BadRequest("Categoria informada errada ou não encontrada!");
        }

        Categoria CategoriaRemovida = _ufw.CategoriasRepository.Delete(Categoria);
        _ufw.Commit();

        return CategoriaRemovida; 
    }
}
