using Ambrosuite.ApiService.Entities;
using Ambrosuite.ApiService.EntitiesDTO;
using AutoMapper;

namespace Ambrosuite.ApiService.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Usuario, UsuarioDTO>()
                .ForMember(dest => dest.Rol, opt => opt.MapFrom(src => src.Rol));

            CreateMap<Rol, RolDTO>().ReverseMap();

            CreateMap<UsuarioCreateUpdateDTO, Usuario>();

            CreateMap<RolCreateUpdateDTO, Rol>();

            // ProductoFinal
            CreateMap<ProductoFinal, ProductoFinalDTO>()
                .ForMember(dest => dest.recetas, opt => opt.MapFrom(src => src.recetas));
            CreateMap<ProductoFinalDTO, ProductoFinal>();

            CreateMap<ProductoFinalCreateUpdateDTO, ProductoFinal>();

            // Ingrediente
            CreateMap<Ingrediente, IngredienteDTO>()
                .ForMember(dest => dest.recetas, opt => opt.MapFrom(src => src.recetas));
            CreateMap<IngredienteDTO, Ingrediente>();

            CreateMap<IngredienteCreateUpdateDTO, Ingrediente>();

            // Receta
            CreateMap<Receta, RecetaDTO>()
                .ForMember(dest => dest.productoFinal, opt => opt.MapFrom(src => src.productoFinal))
                .ForMember(dest => dest.ingrediente, opt => opt.MapFrom(src => src.ingrediente));
            CreateMap<RecetaDTO, Receta>();

            CreateMap<RecetaCreateUpdateDTO, Receta>();
            CreateMap<RecetaCreateUpdateDTO, RecetaDTO>();

            // Mesa 
            CreateMap<Mesa, MesaDTO>();
            CreateMap<MesaCreateUpdateDTO, Mesa>();

            // Pedido
            CreateMap<Pedido, PedidoDTO>()
                .ForMember(dest => dest.mesa_id, opt => opt.MapFrom(src => src.mesa.id))
                .ForMember(dest => dest.usuario_id, opt => opt.MapFrom(src => src.usuario.id));
            CreateMap<PedidoDTO, Pedido>();

            CreateMap<PedidoCreateUpdateDTO, Pedido>();
            CreateMap<PedidoCreateUpdateDTO, PedidoDTO>();

            // PedidoDetalle
            CreateMap<PedidoDetalle, PedidoDetalleDTO>()
                .ForMember(dest => dest.producto_id, opt => opt.MapFrom(src => src.producto.id))
                .ForMember(dest => dest.pedido_id, opt => opt.MapFrom(src => src.pedido.id));
            CreateMap<PedidoDetalleDTO, PedidoDetalle>();
            CreateMap<PedidoDetalleCreateUpdateDTO, PedidoDetalle>();

            // Categoria Gasto
            CreateMap<CategoriaGasto, CategoriaGastoDTO>();
            CreateMap<CategoriaGastoCreateUpdateDTO, CategoriaGasto>();

            // Caja
            CreateMap<Caja, CajaDTO>();
            CreateMap<CajaCreateUpdateDTO, Caja>();

            // Gasto
            CreateMap<Gasto, GastoDTO>()
                .ForMember(dest => dest.caja_id, opt => opt.MapFrom(src => src.mesa.id))
                .ForMember(dest => dest.usuario_id, opt => opt.MapFrom(src => src.usuario.id))
                .ForMember(dest => dest.categoria_gasto_id, opt => opt.MapFrom(src => src.categoria_gasto.id));
            CreateMap<GastoCreateUpdateDTO, Gasto>();
        }

        public class ListTypeConverter<TSource, TDestination> : ITypeConverter<List<TSource>, List<TDestination>>
        {
            public List<TDestination> Convert(List<TSource> source, List<TDestination> destination, ResolutionContext context)
            {
                return source.Select(item => context.Mapper.Map<TDestination>(item)).ToList();
            }
        }
    }
}
