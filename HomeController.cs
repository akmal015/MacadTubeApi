using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

public class HomeController : Controller
{
    // Home Page (GET request)
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Search()
    {
        return View(); // This will render the Search.cshtml view
    }


    // Search YouTube API (POST request)
    [HttpPost]
    public async Task<IActionResult> Search(string query)
    {
        var youtubeService = new YouTubeService(new BaseClientService.Initializer()
        {
            ApiKey = "AIzaSyBqZmxJNWOPRNUbY7eKEl2RNKJYUq3waOg",
            ApplicationName = this.GetType().ToString()
        });

        var searchRequest = youtubeService.Search.List("snippet");
        searchRequest.Q = query;
        searchRequest.MaxResults = 10;

        var searchResponse = await searchRequest.ExecuteAsync();

        var searchResults = searchResponse.Items.Select(item => new
        {
            Title = item.Snippet.Title,
            Thumbnail = item.Snippet.Thumbnails.Default__.Url,
            Description = item.Snippet.Description,
            VideoId = item.Id.VideoId
        }).ToList();

        return View("Results", searchResults); // Passing search results to Results.cshtml
    }
    public IActionResult Results(List<YouTubeVideo> searchResults)
    {
        return View(searchResults);
    }
}