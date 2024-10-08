﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApiCatalogoTeste2.Models;

public class Produto
{
    [Key][Column("id")] public int Id { get; set; }
    [Required][StringLength(100)][Column("nome")] public string? Nome { get; set; }
    [StringLength(80)][Column("descricao")] public string? Descricao { get; set; }
    [Required][Column("valor")] public float Valor { get; set; }
    [Column("categoria_id")] public int CategoriaId { get; set; }

}

public class ProdutoDTO
{
    public int Id { get; set; }
    public string? Nome { get; set; }
    public string? Descricao { get; set; }
    public float Valor { get; set; }
    public int CategoriaId { get; set; }
}

public class ProdutoDTOUpdateRequest : IValidatableObject
{
    public string? Nome { get; set; }
    public float Valor { get; set; }
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (this.Nome is null)
        {
            yield return new ValidationResult("o nome deve ser informado", new[] {nameof(this.Nome)});
        }
    }
}

public class ProdutoDTOUpdateResponse
{
    public int Id { get; set; }
    public string? Nome { get; set; }
    public string? Descricao { get; set; }
    public float Valor { get; set; }
    public int CategoriaId { get; set; }
}