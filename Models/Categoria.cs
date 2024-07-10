using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiCatalogoTeste2.Models;

public class Categoria
{
    public Categoria()
    {
        Produtos = new Collection<Produto>();   
    }

    [Key][Column("id")]public int Id { get; set; }
    [Column("nome")]public string? Nome { get; set; }
    [Column("descricao")]public string? Descricao { get; set; }
    public ICollection<Produto> Produtos { get; set; }  
}

public class CategoriaDTO
{
    public int Id { get; set; }
    public string? Nome { get; set; }
    public string? Descricao { get; set; }
}