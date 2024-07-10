using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiCatalogoTeste2.Migrations
{
    /// <inheritdoc />
    public partial class InseriCategorias : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("INSERT INTO categorias (nome,descricao) VALUES ('BEBIDAS', 'BEBIDAS.JPEG')");
            mb.Sql("INSERT INTO categorias (nome,descricao) VALUES ('LANCHES', 'LANCHES.JPEG')");
            mb.Sql("INSERT INTO categorias (nome,descricao) VALUES ('PIZZAS', 'PIZZAS.JPEG')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("Delete from categorias");
        }
    }
}
