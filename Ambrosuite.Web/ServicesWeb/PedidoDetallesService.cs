﻿using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Ambrosuite.ApiService.Entities;
using Ambrosuite.ApiService.EntitiesDTO;

namespace Ambrosuite.Web.ServicesWeb
{
    public class PedidoDetalleService
    {
        private readonly HttpClient _httpClient;

        public PedidoDetalleService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            //_httpClient.BaseAddress = new Uri("https://localhost:5001");
            _httpClient.BaseAddress = new Uri("http://localhost:5000");
        }

        public async Task<List<PedidoDetalle>> GetDetallePedidoByIdAsync(int pedidoId) {
            List<PedidoDetalle>? pedido = await _httpClient.GetFromJsonAsync<List<PedidoDetalle>>($"api/pedidodetalles/pedido/{pedidoId}");
            return pedido;
        }
        public async Task updatePedidoDetalleAsync(PedidoDetalle p_pedidoDetalle)
        {
            try
            {
                PedidoDetalleCreateUpdateDTO pedidoDetalleCreateUpdateDTO = new PedidoDetalleCreateUpdateDTO
                {
                    cantidad = p_pedidoDetalle.cantidad,
                    producto_id = p_pedidoDetalle.producto_id,
                    pedido_id = p_pedidoDetalle.pedido_id,
                    estado=0
                };
                
                var response = await _httpClient.PutAsJsonAsync("/api/pedidodetalles/" + p_pedidoDetalle.id, pedidoDetalleCreateUpdateDTO);
                Debug.WriteLine(response);
                response.EnsureSuccessStatusCode();


                // Calcular el total a partir de los detalles del pedido

                List<PedidoDetalle>? pedido = await _httpClient.GetFromJsonAsync<List<PedidoDetalle>>($"api/pedidodetalles/pedidoAct/{p_pedidoDetalle.pedido_id}");
                double total = (double)pedido.Sum(detalle => detalle.cantidad * detalle.producto.precio);

                Pedido? ped = await _httpClient.GetFromJsonAsync<Pedido>($"api/pedidos/{p_pedidoDetalle.pedido_id}");
                ped.total = total;

                PedidoCreateUpdateDTO pedCreateUpdateDTO = new PedidoCreateUpdateDTO {
                    total = ped.total,
                    estado = 0,
                    mesa_id = ped.mesa_id,
                    usuario_id = ped.usuario_id
                };

                await _httpClient.PutAsJsonAsync("/api/pedidos/" + p_pedidoDetalle.pedido_id, pedCreateUpdateDTO);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating Pedido Detalle: {ex.Message}");
                throw;
            }
        }

        public async Task deletePedidoDetalleAsync(PedidoDetalle p_pedidoDetalle)
        {
            try
            {
                PedidoDetalleCreateUpdateDTO pedidoDetalleCreateUpdateDTO = new PedidoDetalleCreateUpdateDTO
                {
                    cantidad = p_pedidoDetalle.cantidad,
                    producto_id = p_pedidoDetalle.producto_id,
                    pedido_id = p_pedidoDetalle.pedido_id,
                    estado = 1
                };

                var response = await _httpClient.PutAsJsonAsync("/api/pedidodetalles/" + p_pedidoDetalle.id, pedidoDetalleCreateUpdateDTO);
                Debug.WriteLine(response);
                response.EnsureSuccessStatusCode();

                // Calcular el total a partir de los detalles del pedido

                List<PedidoDetalle>? pedido = await _httpClient.GetFromJsonAsync<List<PedidoDetalle>>($"api/pedidodetalles/pedidoAct/{p_pedidoDetalle.pedido_id}");
                double total = (double)pedido.Sum(detalle => detalle.cantidad * detalle.producto.precio);

                Pedido? ped = await _httpClient.GetFromJsonAsync<Pedido>($"api/pedidos/{p_pedidoDetalle.pedido_id}");
                ped.total = total;

                PedidoCreateUpdateDTO pedCreateUpdateDTO = new PedidoCreateUpdateDTO
                {
                    total = ped.total,
                    estado = 0,
                    mesa_id = ped.mesa_id,
                    usuario_id = ped.usuario_id
                };

                await _httpClient.PutAsJsonAsync("/api/pedidos/" + p_pedidoDetalle.pedido_id, pedCreateUpdateDTO);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting Pedido Detalle: {ex.Message}");
                throw;
            }
        }

        public async Task agregarPedidoDetalleAsync(PedidoDetalle p_pedidoDetalle)
        {
            try
            {
                // Verificar si el registro ya existe
                var existingPedidoDetalle = await _httpClient.GetFromJsonAsync<PedidoDetalle>($"/api/pedidodetalles/{p_pedidoDetalle.pedido_id}/{p_pedidoDetalle.producto_id}");

                if (existingPedidoDetalle.id != 0)
                {
                    //en caso de que ya exista el registro, se actualiza a estado 0 y cambia la cantidad
                    p_pedidoDetalle.id = existingPedidoDetalle.id;
                    p_pedidoDetalle.cantidad += existingPedidoDetalle.cantidad;
                    updatePedidoDetalleAsync(p_pedidoDetalle);
                    return;
                }

                PedidoDetalleCreateUpdateDTO pedidoDetalleCreateUpdateDTO = new PedidoDetalleCreateUpdateDTO
                {
                    cantidad = p_pedidoDetalle.cantidad,
                    producto_id = p_pedidoDetalle.producto_id,
                    pedido_id = p_pedidoDetalle.pedido_id,
                    estado = 0
                };

                var response = await _httpClient.PostAsJsonAsync("/api/pedidodetalles/", pedidoDetalleCreateUpdateDTO);
                Debug.WriteLine(response);
                response.EnsureSuccessStatusCode();

                // Calcular el total a partir de los detalles del pedido

                List<PedidoDetalle>? pedido = await _httpClient.GetFromJsonAsync<List<PedidoDetalle>>($"api/pedidodetalles/pedidoAct/{p_pedidoDetalle.pedido_id}");
                double total = (double)pedido.Sum(detalle => detalle.cantidad * detalle.producto.precio);

                Pedido? ped = await _httpClient.GetFromJsonAsync<Pedido>($"api/pedidos/{p_pedidoDetalle.pedido_id}");
                ped.total = total;

                PedidoCreateUpdateDTO pedCreateUpdateDTO = new PedidoCreateUpdateDTO
                {
                    total = ped.total,
                    estado = 0,
                    mesa_id = ped.mesa_id,
                    usuario_id = ped.usuario_id
                };

                await _httpClient.PutAsJsonAsync("/api/pedidos/" + p_pedidoDetalle.pedido_id, pedCreateUpdateDTO);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error agregando Pedido Detalle: {ex.Message}");
                throw;
            }
        }
    }
}
