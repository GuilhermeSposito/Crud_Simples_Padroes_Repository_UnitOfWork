using ApiCatalogoTeste2.Context;
using ApiCatalogoTeste2.Filters.Paginacao;
using ApiCatalogoTeste2.Models;
using ApiCatalogoTeste2.Models.Mappings;
using ApiCatalogoTeste2.Repositorys.Generics;
using ApiCatalogoTeste2.Repositorys.Produtos;
using ApiCatalogoTeste2.Repositorys.UnitOfWork;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogoTeste2.Controllers;

[Route("/api/v1/[controller]")]
[ApiController]
public class ProdutosController : Controller
{
    private readonly IUnitOfWork _ufw;
    private readonly IMapper _mapper;

    public ProdutosController(IUnitOfWork ufw, IMapper mapper)
    {
        _ufw = ufw;
        _mapper = mapper;
    }

    [HttpGet("/api/v1/[controller]/categoria/{id:int}")]
    public async Task<ActionResult<List<Produto>>> GetProdutoPorCategoria(int id)
    {
        IEnumerable<Produto> Produtos = _ufw.ProdutosRepository.GetProdutoPorCategoria(id);

        return Ok(Produtos);
    }


    [HttpGet("Pagination")]
    public async Task<ActionResult<List<Produto>>> GetProdutoPagination([FromQuery] ProdutosParameters produtoParams)
    {
        IEnumerable<Produto> Produtos = _ufw.ProdutosRepository.GetProdutosPaginado(produtoParams);

        return Ok(Produtos);

    }


    [HttpGet]
    [Authorize]
    public async Task<ActionResult<List<ProdutoDTO>>> GetProdutos()
    {
        IEnumerable<Produto> produtos = _ufw.ProdutosRepository.GettAll();

        List<ProdutoDTO> produtosDTO = _mapper.Map<List<ProdutoDTO>>(produtos);

        if (produtos is null)
            return NotFound("Nenhum produto Encontrado na base de dados");

        return produtosDTO;
    }

    [HttpGet("{id:int}", Name = ("ObterProduto"))]
    public async Task<ActionResult<ProdutoDTO>> GetProduto(int id)
    {
        ProdutoDTO? produtos = _ufw.ProdutosRepository.GettAll()
           .Select(p => new ProdutoDTO()
           {
               Id = p.Id,
               Nome = p.Nome,
               Descricao = p.Descricao,
               Valor = p.Valor,
               CategoriaId = p.CategoriaId,
           }).FirstOrDefault(x => x.Id == id);

        if (produtos is null)
            return NotFound("Nenhum produto com esse ID Encontrado na base de dados");

        return Ok(produtos);
    }

    [HttpPost]
    public async Task<ActionResult<ProdutoDTO>> PostProduto(ProdutoDTO produto)
    {
        if (produto is null)
            return BadRequest("Por Favor Insira um produto para ser inserido");

        Produto newProduct = new Produto()
        {
            Nome = produto.Nome,
            Descricao = produto.Descricao,
            Valor = produto.Valor,
            CategoriaId = produto.CategoriaId,
        };

        var NewProductInject = _ufw.ProdutosRepository.Create(newProduct);
        _ufw.Commit();

        return new CreatedAtRouteResult("ObterProduto", new { id = NewProductInject.Id }, NewProductInject);
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult<ProdutoDTOUpdateResponse>> UpdateProdutoNome(int id, JsonPatchDocument<ProdutoDTOUpdateRequest> patchProdutoDTO )
    {
        if (patchProdutoDTO is null || id <= 0)
            return BadRequest();

        var produto = _ufw.ProdutosRepository.Get(x => x.Id == id);

        if (produto is null)
            return NotFound("Produto solicitado, Não foi encontrado na base de dados");

        var produtoUpdateRequest = _mapper.Map<ProdutoDTOUpdateRequest>(produto);

        patchProdutoDTO.ApplyTo(produtoUpdateRequest, ModelState);

        if(!ModelState.IsValid || !TryValidateModel(produtoUpdateRequest))
           return BadRequest(ModelState);

        _mapper.Map(produtoUpdateRequest, produto);

        _ufw.ProdutosRepository.Update(produto);
        _ufw.Commit();

        return _mapper.Map<ProdutoDTOUpdateResponse>(produto);
    }


    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateProduto(int id, Produto produto)
    {
        if (id != produto.Id)
        {
            return BadRequest("Dados Invalidos");
        }

        bool ProdutoFind = _ufw.ProdutosRepository.GettAll().Any(x => x.Id == id);
        if (!ProdutoFind)
            return BadRequest("Produto não encontrado no banco de dados");

        if (produto.Nome is null || produto.Descricao is null)
            return BadRequest("Dados invalidos!");


        Produto? CategoriaModificada = _ufw.ProdutosRepository.Update(new Produto()
        {
            Id = produto.Id,
            Nome = produto.Nome,
            Descricao = produto.Descricao,
            Valor = produto.Valor,
            CategoriaId = produto.CategoriaId
        });
        _ufw.Commit();


        if (CategoriaModificada is not null)
            return Ok("Produto modificado com sucesso!");

        return BadRequest("Falha de atualização de produto");
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteProduto(int id)
    {
        Produto? Produto = _ufw.ProdutosRepository.Get(x => x.Id == id);

        if (Produto is null)
            return NotFound("Produto não encontrado");

        Produto Deletado = _ufw.ProdutosRepository.Delete(Produto);

        if (Deletado is not null)
            _ufw.Commit();
        return Ok("Produto deletado com sucesso");

        return BadRequest("Erro ao deletar produto");
    }
}
