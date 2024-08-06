namespace ApiCatalogoTeste2.Filters.Paginacao
{
    public abstract class QueryStringPagination<T> : List<T> where T : class
    {
        const int MaxPageSize = 50;
        public int PageNumber { get; set; } = 1;
        public int _pageSize;
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
}
