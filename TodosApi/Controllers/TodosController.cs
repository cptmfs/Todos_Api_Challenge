using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

public class TodosController : Controller
{
    private readonly string _apiBaseUrl = "https://challenge.photier.com";
    private readonly string _token = "afeaf6c673486ad7159a51eff511e2ed";
    private static readonly HttpClient client = new HttpClient();

    [HttpGet("items")] // HELLO
    public IActionResult GetTodoItems()
    {
        using (var httpClient = new HttpClient())
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

            var response = httpClient.GetAsync($"{_apiBaseUrl}/todos").Result;

            if (response.IsSuccessStatusCode)
            {
                string responseContent = response.Content.ReadAsStringAsync().Result;
                return Ok(responseContent);
            }
            else
            {
                return BadRequest("GET isteği başarısız oldu. Hata kodu: " + (int)response.StatusCode);
            }
        }
    }

    [HttpGet("search")] // query = DOUGLAS  XX query = 42 
    public IActionResult SearchTodoItems(string query)
    {
        using (var httpClient = new HttpClient())
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

            var response = httpClient.GetAsync($"{_apiBaseUrl}/todos/search?query={query}").Result;

            if (response.IsSuccessStatusCode)
            {
                string responseContent = response.Content.ReadAsStringAsync().Result;
                return Ok(responseContent);
            }
            else
            {
                return BadRequest("Arama isteği başarısız oldu. Hata kodu: " + (int)response.StatusCode);
            }
        }
    }

    [HttpDelete("{id}")] // [HttpDelete("id")]
    public IActionResult DeleteTodoItem(string id)
    {
        using (var httpClient = new HttpClient())
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

            var response = httpClient.DeleteAsync($"{_apiBaseUrl}/todos?id={id}").Result;

            if (response.IsSuccessStatusCode)
            {
                string responseContent = response.Content.ReadAsStringAsync().Result;
                return Ok(responseContent);
            }
            else
            {
                return BadRequest("Silme isteği başarısız oldu. Hata kodu: " + (int)response.StatusCode);
            }
        }
    }

    [HttpPost("start")]
    public IActionResult StartChallenge(string email)
    {
        using (var httpClient = new HttpClient())
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

            var response = httpClient.PostAsync($"{_apiBaseUrl}/todos/start", null).Result;

            if (response.IsSuccessStatusCode)
            {
                string responseContent = response.Content.ReadAsStringAsync().Result;
                return Ok(responseContent);
            }
            else
            {
                return BadRequest("İlk görev tamamlanamadı. Hata kodu: " + (int)response.StatusCode);
            }
        }
    }

    [HttpPost("complete")] // MENEMEN
    public async Task<IActionResult> CompleteChallenge([FromBody] string code)
    {
        var _apiBaseUrl = "https://challenge.photier.com";
        var _token = "afeaf6c673486ad7159a51eff511e2ed";
        var _code = code;

        var request = new HttpRequestMessage(HttpMethod.Post, $"{_apiBaseUrl}/complete");
        request.Headers.Add("Authorization", $"Bearer {_token}");

        var content = new MultipartFormDataContent();
        content.Add(new StringContent(_code), "code");

        var filePath = @"C:\TodosApi.rar";
        var fileContent = new ByteArrayContent(await System.IO.File.ReadAllBytesAsync(filePath));
        fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("multipart/form-data");

        content.Add(fileContent, "file", "file.zip");

        request.Content = content;

        var response = await client.SendAsync(request);

        if (response.IsSuccessStatusCode)
        {
            return Ok();
        }
        else
        {
            return StatusCode((int)response.StatusCode);
        }
    }
}