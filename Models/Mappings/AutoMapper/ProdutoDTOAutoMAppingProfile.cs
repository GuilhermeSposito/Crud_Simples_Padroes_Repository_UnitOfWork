using AutoMapper;

namespace ApiCatalogoTeste2.Models.Mappings.AutoMapper;

public class ProdutoDTOAutoMAppingProfile : Profile
{
    public ProdutoDTOAutoMAppingProfile()
    {
        CreateMap<Produto, ProdutoDTO>().ReverseMap();
        CreateMap<Produto, ProdutoDTOUpdateRequest>().ReverseMap();
        CreateMap<Produto, ProdutoDTOUpdateResponse>().ReverseMap();
    }
}
