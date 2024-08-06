namespace ApiCatalogoTeste2.Models.Mappings;

public static class ProdutosDTOExtenxions
{
    public static ProdutoDTO? ToProdutoDTO(this Produto produto)
    {
        if (produto is null)
            return null;

        return new ProdutoDTO()
        {
            Id = produto.Id,
            Nome = produto.Nome,
            Descricao = produto.Descricao,
            Valor = produto.Valor,
            CategoriaId = produto.CategoriaId,
        };
    }

    public static Produto? ToProduto(this ProdutoDTO produtoDto)
    {
        if (produtoDto is null)
            return null;

        return new Produto()
        {
            Id = produtoDto.Id,
            Nome = produtoDto.Nome,
            Descricao = produtoDto.Descricao,
            Valor = produtoDto.Valor,
            CategoriaId = produtoDto.CategoriaId
        };
    }

    public static List<ProdutoDTO> ToProdutosDTOList(this IEnumerable<Produto> produtos)
    {
        if (produtos is null || !produtos.Any())
        {
            return new List<ProdutoDTO>();
        }

        return produtos.Select(x => new ProdutoDTO()
        {
            Id = x.Id,
            Nome = x.Nome,
            Descricao = x.Descricao,
            Valor = x.Valor,
            CategoriaId = x.CategoriaId,
        }).ToList();

    }
}
