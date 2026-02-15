using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using todo.Models;
using todo.Services.Interfaces;

namespace todo.Controllers;

public class HomeController : Controller
{
    private readonly ITaskService _taskService;
    private readonly ICategoryService _categoryService;

    public HomeController(ITaskService taskService, ICategoryService categoryService)
    {
        _taskService = taskService;
        _categoryService = categoryService;
    }

    public async Task<IActionResult> Index()
    {
        var storageType = GetCurrentStorage();
        await InitPage(storageType);

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Task_Add(TaskModel task)
    {
        var storageType = GetCurrentStorage();
        await _taskService.AddTaskAsync(task, storageType);

        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Is_Completed(int id)
    {
        var storageType = GetCurrentStorage();
        await _taskService.MarkTaskAsCompleteAsync(id, storageType);

        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> ClearTasks()
    {
        var storageType = GetCurrentStorage();
        await _taskService.ClearTasksAsync(storageType);

        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult Storage_Choose(string storage)
    {
        HttpContext.Session.SetString("StorageType", storage);

        return RedirectToAction("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }


    private string GetCurrentStorage()
    {
        return HttpContext.Session.GetString("StorageType") ?? "sql";
    }

    private async Task InitPage(string storageType)
    {
        ViewBag.SelectedStorage = storageType;
        ViewBag.Categories = await _categoryService.GetCategoriesAsync(storageType);
        ViewBag.ActiveTasks = await _taskService.GetTasksByCompletionStatusAsync(false, storageType);
        ViewBag.CompletedTasks = await _taskService.GetTasksByCompletionStatusAsync(true, storageType);
    }
}
