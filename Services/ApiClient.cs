using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.IO;
using System.Linq;
using KnowledgeBase.Client.Services.Models;

namespace KnowledgeBase.Client.Services
{
    public class ApiClient : IDisposable
    {
        private readonly HttpClient _httpClient;
        private string _baseUrl = "https://localhost:7015";
        private string _token = string.Empty;

        public ApiClient()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_baseUrl);
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.Timeout = TimeSpan.FromSeconds(30);
        }

        public void SetToken(string token)
        {
            _token = token;
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }
            else
            {
                _httpClient.DefaultRequestHeaders.Authorization = null;
            }
        }

        public void ClearToken()
        {
            _token = string.Empty;
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }

        public bool IsAuthenticated => !string.IsNullOrEmpty(_token);

        // ==================== АУТЕНТИФИКАЦИЯ ====================

        public async Task<string?> LoginAsync(string login, string password)
        {
            try
            {
                var loginDto = new { login, password };
                var response = await _httpClient.PostAsJsonAsync("/api/Auth/login", loginDto);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<JsonElement>();
                    if (result.TryGetProperty("token", out var tokenProperty))
                    {
                        return tokenProperty.GetString();
                    }
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    throw new ApiException($"Ошибка авторизации: {response.StatusCode} - {error}");
                }
                return null;
            }
            catch (HttpRequestException ex)
            {
                throw new ApiException($"Ошибка сети: {ex.Message}", ex);
            }
        }

        public async Task<bool> RegisterAsync(string login, string password)
        {
            try
            {
                var registerDto = new { login, password };
                var response = await _httpClient.PostAsJsonAsync("/api/Auth/register", registerDto);
                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException ex)
            {
                throw new ApiException($"Ошибка сети: {ex.Message}", ex);
            }
        }

        // ==================== СТАТЬИ ====================

        public async Task<List<Article>> GetArticlesAsync()
        {
            return await GetAsync<List<Article>>("/api/Articles") ?? new List<Article>();
        }

        public async Task<Article?> GetArticleAsync(int id)
        {
            return await GetAsync<Article>($"/api/Articles/{id}");
        }

        public async Task<List<Article>> GetArticlesBySectionAsync(int sectionId)
        {
            return await GetAsync<List<Article>>($"/api/Articles/section/{sectionId}") ?? new List<Article>();
        }

        public async Task<Article?> CreateArticleAsync(CreateArticleDto articleDto)
        {
            return await PostAsync<Article>("/api/Articles", articleDto);
        }

        public async Task<Article?> UpdateArticleAsync(int id, UpdateArticleDto articleDto)
        {
            return await PutAsync<Article>($"/api/Articles/{id}", articleDto);
        }

        public async Task<bool> DeleteArticleAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"/api/Articles/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<List<Article>> SearchArticlesAsync(string query)
        {
            var encodedQuery = Uri.EscapeDataString(query);
            return await GetAsync<List<Article>>($"/api/Articles/search?query={encodedQuery}") ?? new List<Article>();
        }

        // ==================== РАЗДЕЛЫ ====================

        public async Task<List<Section>> GetSectionsAsync()
        {
            return await GetAsync<List<Section>>("/api/Sections") ?? new List<Section>();
        }

        public async Task<List<Section>> GetSectionsTreeAsync()
        {
            return await GetAsync<List<Section>>("/api/Sections/tree") ?? new List<Section>();
        }

        public async Task<Section?> GetSectionAsync(int id)
        {
            return await GetAsync<Section>($"/api/Sections/{id}");
        }

        public async Task<Section?> CreateSectionAsync(CreateSectionDto sectionDto)
        {
            return await PostAsync<Section>("/api/Sections", sectionDto);
        }

        public async Task<Section?> UpdateSectionAsync(int id, UpdateSectionDto sectionDto)
        {
            return await PutAsync<Section>($"/api/Sections/{id}", sectionDto);
        }

        public async Task<bool> DeleteSectionAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"/api/Sections/{id}");
            return response.IsSuccessStatusCode;
        }

        // ==================== ИЗОБРАЖЕНИЯ ====================

        public async Task<List<ArticleImage>> GetImagesByArticleAsync(int articleId)
        {
            return await GetAsync<List<ArticleImage>>($"/api/Images/article/{articleId}") ?? new List<ArticleImage>();
        }

        public async Task<ArticleImage?> UploadImageAsync(int articleId, string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                    throw new FileNotFoundException($"Файл не найден: {filePath}");

                var fileName = Path.GetFileName(filePath);
                var fileBytes = await File.ReadAllBytesAsync(filePath);

                using var content = new MultipartFormDataContent();
                content.Add(new StringContent(articleId.ToString()), "articleId");

                var fileContent = new ByteArrayContent(fileBytes);
                fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/octet-stream");

                content.Add(fileContent, "file", fileName);

                var response = await _httpClient.PostAsync("/api/Images/upload", content);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<ArticleImage>();
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    throw new ApiException($"Ошибка загрузки: {response.StatusCode} - {error}");
                }
            }
            catch (Exception ex) when (ex is not ApiException)
            {
                throw new ApiException($"Ошибка при загрузке изображения: {ex.Message}", ex);
            }
        }

        // НОВЫЙ МЕТОД: Загрузка изображения без привязки к статье (для вставки в контент)
        public async Task<string> UploadImageAsync(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                    throw new FileNotFoundException($"Файл не найден: {filePath}");

                var fileName = Path.GetFileName(filePath);
                var fileBytes = await File.ReadAllBytesAsync(filePath);

                using var content = new MultipartFormDataContent();
                var fileContent = new ByteArrayContent(fileBytes);
                fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/octet-stream");

                content.Add(fileContent, "file", fileName);

                var response = await _httpClient.PostAsync("/api/Images/upload-content", content);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<JsonElement>();
                    if (result.TryGetProperty("fullUrl", out var urlProperty))
                    {
                        return urlProperty.GetString() ?? "";
                    }
                    else if (result.TryGetProperty("filePath", out var filePathProperty))
                    {
                        // Возвращаем полный URL для вставки в контент
                        var relativePath = filePathProperty.GetString();
                        return $"{_baseUrl}{relativePath}";
                    }
                }

                var error = await response.Content.ReadAsStringAsync();
                throw new ApiException($"Ошибка загрузки: {response.StatusCode} - {error}");
            }
            catch (Exception ex) when (ex is not ApiException)
            {
                throw new ApiException($"Ошибка при загрузке изображения: {ex.Message}", ex);
            }
        }

        public async Task<bool> DeleteImageAsync(int imageId)
        {
            var response = await _httpClient.DeleteAsync($"/api/Images/{imageId}");
            return response.IsSuccessStatusCode;
        }

        // ==================== ВСПОМОГАТЕЛЬНЫЕ МЕТОДЫ ====================

        private async Task<T?> GetAsync<T>(string url)
        {
            try
            {
                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<T>();
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    throw new UnauthorizedAccessException("Требуется авторизация. Пожалуйста, войдите заново.");
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return default;
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new ApiException($"HTTP ошибка {response.StatusCode}: {errorContent}");
                }
            }
            catch (HttpRequestException ex)
            {
                throw new ApiException($"Ошибка сети при запросе {url}: {ex.Message}", ex);
            }
        }

        private async Task<T?> PostAsync<T>(string url, object data)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(url, data);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<T>();
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new ApiException($"HTTP ошибка {response.StatusCode}: {errorContent}");
                }
            }
            catch (HttpRequestException ex)
            {
                throw new ApiException($"Ошибка сети при запросе {url}: {ex.Message}", ex);
            }
        }

        private async Task<T?> PutAsync<T>(string url, object data)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync(url, data);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<T>();
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new ApiException($"HTTP ошибка {response.StatusCode}: {errorContent}");
                }
            }
            catch (HttpRequestException ex)
            {
                throw new ApiException($"Ошибка сети при запросе {url}: {ex.Message}", ex);
            }
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
            GC.SuppressFinalize(this);
        }
    }

    public class ApiException : Exception
    {
        public ApiException(string message) : base(message) { }
        public ApiException(string message, Exception innerException) : base(message, innerException) { }
    }
}