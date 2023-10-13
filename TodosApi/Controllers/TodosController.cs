using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

public class TodosController : Controller
{
    private readonly string _apiBaseUrl = "https://challenge.photier.com";
    private readonly string _token = "afeaf6c673486ad7159a51eff511e2ed";

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

    //[HttpPost("complete")]
    //public IActionResult CompleteChallenge(string path_to_zip_file)
    //{
    //    using (var httpClient = new HttpClient())
    //    {
    //        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

    //        byte[] fileBytes = System.IO.File.ReadAllBytes(path_to_zip_file);

    //        ByteArrayContent byteContent = new ByteArrayContent(fileBytes);

    //        MultipartFormDataContent form = new MultipartFormDataContent();
    //        form.Add(byteContent, "file", System.IO.Path.GetFileName(path_to_zip_file));

    //        var response = httpClient.PostAsync($"{_apiBaseUrl}/complete", form).Result;

    //        if (response.IsSuccessStatusCode)
    //        {
    //            string responseContent = response.Content.ReadAsStringAsync().Result;
    //            return Ok(responseContent);
    //        }
    //        else
    //        {
    //            return BadRequest("Görev tamamlama isteği başarısız oldu. Hata kodu: " + (int)response.StatusCode);
    //        }
    //    }
    //}

    //[HttpPost("complete")]
    //public IActionResult CompleteChallenge([FromForm] string code)
    //{
    //    // Dosya yolunu belirleyin
    //    var filePath = @"C:\TodosApi.rar";

    //    using (var httpClient = new HttpClient())
    //    {
    //        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

    //        using (var form = new MultipartFormDataContent())
    //        {
    //            // Add the code
    //            var stringContent = new StringContent(code);
    //            stringContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
    //            {
    //                Name = "code"
    //            };
    //            form.Add(stringContent);

    //            // Dosyayı belirtilen yoldan açın
    //            using (var fileStream = new FileStream(filePath, FileMode.Open))
    //            {
    //                // Add the zip file
    //                var streamContent = new StreamContent(fileStream);
    //                streamContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
    //                {
    //                    Name = "file",
    //                    FileName = Path.GetFileName(filePath)
    //                };
    //                form.Add(streamContent);

    //                var response = httpClient.PostAsync($"{_apiBaseUrl}/complete", form).Result;

    //                if (response.IsSuccessStatusCode)
    //                {
    //                    string responseContent = response.Content.ReadAsStringAsync().Result;
    //                    return Ok(responseContent);
    //                }
    //                else
    //                {
    //                    return BadRequest("Görev tamamlama isteği başarısız oldu. Hata kodu: " + (int)response.StatusCode);
    //                }
    //            }
    //        }
    //    }
    //}
    private static readonly HttpClient client = new HttpClient();

    [HttpPost("complete")]
    public async Task<IActionResult> CompleteChallenge()
    {
        var _apiBaseUrl = "https://challenge.photier.com";
        var _token = "afeaf6c673486ad7159a51eff511e2ed";
        var _code = "RAMEN";

        var request = new HttpRequestMessage(HttpMethod.Post, $"{_apiBaseUrl}/complete");
        request.Headers.Add("Authorization", $"Bearer {_token}");

        var content = new MultipartFormDataContent();
        content.Add(new StringContent(_code), "code");

        // Dosyanızın yolu
        var filePath = @"C:\qq.rar";
        var fileContent = new ByteArrayContent(await System.IO.File.ReadAllBytesAsync(filePath));
        fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("multipart/form-data");

        // Dosyanızın adı
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




    [HttpPost("upload")]
    public IActionResult Upload(IFormFile file)
    {
        return (IActionResult)Task.CompletedTask;
    }



}