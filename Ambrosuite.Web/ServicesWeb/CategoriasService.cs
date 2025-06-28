using Ambrosuite.ApiService.Entities;
using Ambrosuite.ApiService.EntitiesDTO;
using System.Diagnostics;

namespace Ambrosuite.Web.ServicesWeb
{
    public class CategoriasService
    {
        private readonly HttpClient _httpClient;

        public CategoriasService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            //_httpClient.BaseAddress = new Uri("https://localhost:5001");
            _httpClient.BaseAddress = new Uri("http://localhost:5000");
        }

        public async Task<List<Categoria>> GetCategoriaAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<Categoria>>("/api/Categorias")
                    ?? new List<Categoria>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching tables: {ex.Message}");
                throw;
            }
        }

        public async Task updateCategoriaAsync(Categoria p_categoria)
        {
            try
            {
                CategoriaCreateUpdateDTO categoriaCreateUpdateDTO = new CategoriaCreateUpdateDTO
                {
                    nombre = p_categoria.nombre,
                    estado = 0
                };

                var response = await _httpClient.PutAsJsonAsync("/api/Categorias/" + p_categoria.id, categoriaCreateUpdateDTO);
                Debug.WriteLine(response);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating table: {ex.Message}");
                throw;
            }
        }

        public async Task deleteCategoriaAsync(Categoria categoria)
        {
            try
            {
                CategoriaCreateUpdateDTO categoriaCreateUpdateDTO = new CategoriaCreateUpdateDTO
                {
                    nombre = categoria.nombre,
                    estado = 1
                };

                var response = await _httpClient.PutAsJsonAsync("/api/Categorias/" + categoria.id, categoriaCreateUpdateDTO);
                Debug.WriteLine(response);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating table: {ex.Message}");
                throw;
            }
        }

        public async Task agregarCategoriaAsync(Categoria p_categoria)
        {
            try
            {
                CategoriaCreateUpdateDTO categoriaCreateUpdateDTO = new CategoriaCreateUpdateDTO
                {
                    nombre = p_categoria.nombre,
                    estado = 0
                };

                var response = await _httpClient.PostAsJsonAsync("/api/Categorias", categoriaCreateUpdateDTO);
                Debug.WriteLine(response);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding table: {ex.Message}");
                throw;
            }
        }
    }
}
