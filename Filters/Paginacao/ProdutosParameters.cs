namespace ApiCatalogoTeste2.Filters.Paginacao;

public class ProdutosParameters
{
    const int MaxPageSize = 50;
    public int PageNumber { get; set; } = 1;
    public int _pageSize = MaxPageSize;
    public int PageSize
    {
        get
        {
            return _pageSize;   
        }
        set
        {
            _pageSize = (value > MaxPageSize) ? MaxPageSize : value;    
        }

    }
}
