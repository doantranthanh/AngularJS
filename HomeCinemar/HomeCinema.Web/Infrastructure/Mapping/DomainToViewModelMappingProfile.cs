using System.Linq;
using AutoMapper;
using HomeCinema.Entities;
using HomeCinema.Web.Models;

namespace HomeCinema.Web.Infrastructure.Mapping
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "DomainToViewModelMappings"; }
        }

        protected override void Configure()
        {
            Mapper.CreateMap<Movie, MovieViewModel>().
                ForMember(vm => vm.Genre, map => map.MapFrom(m => m.Genre.Name)).
                ForMember(vm => vm.GenreId, map => map.MapFrom(m => m.Genre.ID)).
                ForMember(vm => vm.IsAvailable, map => map.MapFrom(m => m.Stocks.Any(s => s.IsAvailable)));
            
            Mapper.CreateMap<Genre, GenreViewModel>().
                ForMember(vm => vm.NumberOfMovies, map => map.MapFrom(g => g.Movies.Count()));
        }
        
    }
}