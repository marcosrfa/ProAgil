
using System.Linq;
using AutoMapper;
using ProAgil.Domain;
using ProAgil.WebAPI.DTOS;

namespace ProAgil.WebAPI.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Evento, EventoDTO>()
                .ForMember(destination => destination.Palestrantes, option => {
                    option.MapFrom(source => source.PalestranteEventos.Select(pe => pe.Palestrante).ToList());
                }).ReverseMap();
            CreateMap<Palestrante, PalestranteDTO>()
                .ForMember(destination => destination.Eventos, options => {
                    options.MapFrom(source => source.PalestranteEventos.Select(pe => pe.Evento).ToList());
                }).ReverseMap();
            CreateMap<Lote, LoteDTO>().ReverseMap();
            CreateMap<RedeSocial, RedeSocialDTO>().ReverseMap();
        }
    }
}