using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiCatalogoTeste2.Migrations
{
    /// <inheritdoc />
    public partial class AdicionandoProdutos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("INSERT INTO produtos (nome,descricao,valor, categoria_id) VALUES ('COCA-COLA LATA','BEBIDA GELADA DE 350ML',5.50,1)");
            mb.Sql("INSERT INTO produtos (nome,descricao,valor,categoria_id) VALUES ('BACON','DELICIOSO LANCHE DA BACON',29.50,2)");
            mb.Sql("INSERT INTO produtos (nome,descricao,valor,categoria_id) VALUES ('PIZZA MARGUERITA','PIZZA COM QUEIJO E MOLHO DE TOMATE',45.50,3)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("DELETE FROM produtos");
        }
    }
}
