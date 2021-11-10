using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalRAPI.Hubs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SignalRAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DownLoadController : ControllerBase
    {
        private readonly IHubContext<ChatHub> _HubContext;

        private IHttpClientFactory _httpClientFactory;
        public DownLoadController(IHubContext<ChatHub> HubContext,IHttpClientFactory httpClientFactory)
        {
            _HubContext = HubContext;
            _httpClientFactory = httpClientFactory;
        }

        // 根据文件名下载文件
        [HttpGet]

        public async Task<IActionResult> DownloadfromBytes(string filename)
        {
            Console.WriteLine(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"files\\{filename}"));
            byte[] byteArr = await System.IO.File.ReadAllBytesAsync(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"files\\{filename}"));
            string mimeType = "application/octet-stream";
            return new FileContentResult(byteArr, mimeType)
            {
                FileDownloadName = filename
            };
        }
        
        [HttpPost]
        public async void DownLoadFromUrl([FromQuery] string url,string name)
        {
            if (!System.IO.Directory.Exists(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "files")))
            {
                System.IO.Directory.CreateDirectory(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "files"));
            }
            if (!System.IO.File.Exists(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"files\\{name}")))
            {
                var client = _httpClientFactory.CreateClient();
                byte[] fileBytes = await client.GetByteArrayAsync(url);
                System.IO.File.WriteAllBytes(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"files\\{name}"), fileBytes);
                _HubContext.Clients.All.SendAsync("download", name);
            }
            _HubContext.Clients.All.SendAsync("download", name);
        }
        // POST api/<DownLoadController>
        [HttpPost]
        public void Post([FromBody] string value)
        {

        }
    }
}
