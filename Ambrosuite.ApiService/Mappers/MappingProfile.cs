﻿using Ambrosuite.ApiService.Entities;
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
                .ForMember(dest => dest.producto_final, opt => opt.MapFrom(src => src.producto_final))
                .ForMember(dest => dest.ingrediente, opt => opt.MapFrom(src => src.ingrediente));

            CreateMap<RecetaDTO, Receta>()
                .ForMember(dest => dest.producto_final, opt => opt.MapFrom(src => src.producto_final))
                .ForMember(dest => dest.ingrediente, opt => opt.MapFrom(src => src.ingrediente));

            CreateMap<RecetaCreateUpdateDTO, Receta>();
            CreateMap<Receta, RecetaCreateUpdateDTO>();
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
                .ForMember(dest => dest.pedido_id, opt => opt.MapFrom(src => src.pedido.id))
                .ForMember(dest => dest.nombreProducto, opt => opt.MapFrom(src => src.producto.nombre));
            CreateMap<PedidoDetalleDTO, PedidoDetalle>();
            CreateMap<PedidoDetalleCreateUpdateDTO, PedidoDetalle>();

            // Categoria Gasto
            CreateMap<CategoriaGasto, CategoriaGastoDTO>();
            CreateMap<CategoriaGastoCreateUpdateDTO, CategoriaGasto>();

            // Categoria Producto
            CreateMap<CategoriaProducto, CategoriaProductoDTO>()
                .ForMember(dest=>dest.producto_id, opt=> opt.MapFrom(src => src.producto_id))
                .ForMember(dest=>dest.categoria_id, opt => opt.MapFrom(src => src.categoria_id));
            CreateMap<CategoriaProductoCreateUpdateDTO, CategoriaProducto>();

            //Categoria
            CreateMap<Categoria, CategoriaDTO>();
            CreateMap<CategoriaCreateUpdateDTO, Categoria>();

            // Caja
            CreateMap<Caja, CajaDTO>();
            CreateMap<CajaCreateUpdateDTO, Caja>();

            
            // Gasto
            CreateMap<Gasto, GastoDTO>()
                .ForMember(dest => dest.caja_id, opt => opt.MapFrom(src => src.caja.id))
                .ForMember(dest => dest.usuario_id, opt => opt.MapFrom(src => src.usuario.id))
                .ForMember(dest => dest.categoria_gasto_id, opt => opt.MapFrom(src => src.categoria_gasto.id));
            CreateMap<GastoCreateUpdateDTO, Gasto>();
            CreateMap<GastoCreateUpdateDTO, GastoDTO>();

            //CajaPedido
            CreateMap<CajaPedido, CajaPedidoDTO>()
                .ForMember(dest => dest.caja_id, opt => opt.MapFrom(src => src.caja.id))
                .ForMember(dest => dest.pedido_id, opt => opt.MapFrom(src => src.pedido.id));
            CreateMap<CajaPedidoCreateUpdateDTO, CajaPedido>();
            CreateMap<CajaPedidoCreateUpdateDTO, CajaPedidoDTO>();


            // CajaMovimiento
            CreateMap<CajaMovimiento, CajaMovimientoDTO>()
                .ForMember(dest => dest.caja_id, opt => opt.MapFrom(src => src.caja.id))
                .ForMember(dest => dest.usuario_id, opt => opt.MapFrom(src => src.usuario.id));
            CreateMap<CajaMovimientoCreateUpdateDTO, CajaMovimiento>();
            CreateMap<CajaMovimientoCreateUpdateDTO, CajaMovimientoDTO>();

            //MetodoPago
            CreateMap<MetodoPago, MetodoPagoDTO>();
            CreateMap<MetodoPagoCreateUpdateDTO, MetodoPago>();

            //TipoFactura
            CreateMap<TipoFactura, TipoFacturaDTO>();
            CreateMap<TipoFacturaCreateUpdateDTO, TipoFactura>();

            //Inventario
            CreateMap<Inventario, InventarioDTO>()
                .ForMember(dest => dest.ingrediente_id, opt => opt.MapFrom(src => src.ingrediente_id));
            CreateMap<InventarioCreateUpdateDTO, InventarioDTO>();
            CreateMap<InventarioDTO, InventarioCreateUpdateDTO>();
            CreateMap<InventarioCreateUpdateDTO, Inventario>();

            //PedidoFacturacion
            CreateMap<PedidoFacturacion, PedidoFacturacionDTO>()
                .ForMember(dest => dest.pedido_id, opt => opt.MapFrom(src => src.pedido.id))
                .ForMember(dest => dest.facturacion_id, opt => opt.MapFrom(src => src.facturacion.id));

            CreateMap<PedidoFacturacionCreateUpdateDTO, PedidoFacturacion>();

            //Facturacion
            CreateMap<Facturacion, FacturacionDTO>()
                .ForMember(dest => dest.metodo_pago_id, opt => opt.MapFrom(src => src.metodoPago.id))
                .ForMember(dest => dest.tipo_factura_id, opt => opt.MapFrom(src => src.tipoFactura.id));
            CreateMap<FacturacionCreateUpdateDTO, Facturacion>();

            //FacturacionDetalle
            CreateMap<FacturacionDetalle, FacturacionDetalleDTO>()
                .ForMember(dest => dest.facturacion_id, opt => opt.MapFrom(src => src.facturacion.id))
                .ForMember(dest => dest.producto_id, opt => opt.MapFrom(src => src.producto.id));
            CreateMap<FacturacionDetalleCreateUpdateDTO, FacturacionDetalle>();
            CreateMap<FacturacionDetalleCreateUpdateDTO, FacturacionDetalleDTO>();

            //AccesoUsuario
            CreateMap<AccesoUsuario, AccesoUsuarioDTO>()
                .ForMember(dest => dest.usuario_id, opt => opt.MapFrom(src => src.usuario.id));
            CreateMap<AccesoUsuarioCreateUpdateDTO, AccesoUsuario>();
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
