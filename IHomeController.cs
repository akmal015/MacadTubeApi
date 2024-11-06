using Microsoft.AspNetCore.Mvc;

public interface IHomeController
{
    IActionResult Index();
    Task<IActionResult> Search(string query);
}