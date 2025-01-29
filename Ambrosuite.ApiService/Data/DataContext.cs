using Ambrosuite.ApiService.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ambrosuite.ApiService.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<ProductoFinal> ProductosFinales { get; set; }
        public DbSet<Ingrediente> Ingredientes { get; set; }
        public DbSet<Receta> Recetas { get; set; }
        public DbSet<PedidoDetalle> PedidoDetalles { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Mesa> Mesas { get; set; }
        public DbSet<CategoriaGasto> CategoriaGastos { get; set; }
        public DbSet<Gasto> Gastos { get; set; }
        public DbSet<Caja> Cajas { get; set; }
        public DbSet<CajaPedido> CajaPedidos { get; set; }
        public DbSet<CajaMovimiento> CajaMovimientos { get; set; }
        public DbSet<MetodoPago> MetodoPagos { get; set; }
        public DbSet<TipoFactura> TipoFacturas { get; set; }
        public DbSet<Inventario> Inventarios { get; set; }
        public DbSet<PedidoFacturacion> PedidoFacturaciones { get; set; }
        public DbSet<Facturacion> Facturaciones { get; set; }
        public DbSet<FacturacionDetalle> FacturacionDetalles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>()
                .HasOne (u => u.Rol)
                .WithMany(r => r.Usuarios)
                .HasForeignKey (u => u.rol_id);

            modelBuilder.Entity<Receta>()
            .HasKey(r => new { r.id, r.productoFinalId, r.ingredienteId });

            modelBuilder.Entity<Receta>()
                .HasOne(r => r.productoFinal)
                .WithMany(p => p.recetas)
                .HasForeignKey(r => r.productoFinalId);

            modelBuilder.Entity<Receta>()
                .HasOne(r => r.ingrediente)
                .WithMany(i => i.recetas)
                .HasForeignKey(r => r.ingredienteId);

            modelBuilder.Entity<Pedido>()
                .HasOne(r=> r.mesa)
                .WithMany()
                .HasForeignKey(r => r.mesa_id);

            modelBuilder.Entity<Pedido>()
                .HasOne(r => r.usuario)
                .WithMany()
                .HasForeignKey(r=> r.usuario_id);

            modelBuilder.Entity<PedidoDetalle>()
                .HasOne(pd => pd.producto)
                .WithMany()
                .HasForeignKey(pd => pd.producto_id);

            modelBuilder.Entity<PedidoDetalle>()
                .HasOne(pd => pd.pedido)
                .WithMany()
                .HasForeignKey(pd => pd.pedido_id);


            modelBuilder.Entity<Gasto>()
                .HasOne(u => u.usuario)
                .WithMany()
                .HasForeignKey(g => g.usuario_id);

            modelBuilder.Entity<Gasto>()
                .HasOne(u => u.categoria_gasto)
                .WithMany()
                .HasForeignKey(g => g.categoria_gasto_id);

            modelBuilder.Entity<Gasto>()
                .HasOne(g => g.caja)
                .WithMany()
                .HasForeignKey(g => g.caja_id);

            modelBuilder.Entity<CajaPedido>()
                .HasOne(cp => cp.caja)
                .WithMany()
                .HasForeignKey(cp => cp.caja_id);

            modelBuilder.Entity<CajaPedido>()
                .HasOne(cp => cp.pedido)
                .WithMany()
                .HasForeignKey(cp => cp.pedido_id);

            modelBuilder.Entity<CajaMovimiento>()
                .HasOne(cm => cm.caja)
                .WithMany()
                .HasForeignKey(cm => cm.caja_id);

            modelBuilder.Entity<CajaMovimiento>()
                .HasOne(cm => cm.usuario)
                .WithMany()
                .HasForeignKey(cm => cm.usuario_id);

            modelBuilder.Entity<Inventario>()
                .HasOne(i => i.ingrediente)
                .WithMany()
                .HasForeignKey(i => i.ingrediente_id);

            modelBuilder.Entity<PedidoFacturacion>()
                .HasOne(pf => pf.pedido)
                .WithMany()
                .HasForeignKey(pf => pf.pedido_id);

            modelBuilder.Entity<PedidoFacturacion>()
                .HasOne(pf => pf.facturacion)
                .WithMany()
                .HasForeignKey(pf => pf.facturacion_id);


            modelBuilder.Entity<Facturacion>()
                .HasOne(f => f.metodoPago)
                .WithMany()
                .HasForeignKey(f => f.metodo_pago_id);

            modelBuilder.Entity<Facturacion>()
                .HasOne(f => f.tipoFactura)
                .WithMany()
                .HasForeignKey(f => f.tipo_factura_id);

            modelBuilder.Entity<FacturacionDetalle>()
                .HasOne(fd => fd.facturacion)
                .WithMany()
                .HasForeignKey(fd => fd.facturacion_id);

            modelBuilder.Entity<FacturacionDetalle>()
                .HasOne(fd => fd.producto)
                .WithMany()
                .HasForeignKey(fd => fd.producto_id);

            base.OnModelCreating(modelBuilder);
        }
    }
}
