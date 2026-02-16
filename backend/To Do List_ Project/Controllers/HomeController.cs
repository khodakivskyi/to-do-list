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
        var storageTypeId = GetCurrentStorage();
        await InitPage(storageTypeId);

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddTask(TaskModel task)
    {
        var storageTypeId = GetCurrentStorage();
        await _taskService.AddTaskAsync(task, storageTypeId);

        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> MarkTaskAsComplete(int id)
    {
        var storageTypeId = GetCurrentStorage();
        await _taskService.MarkTaskAsCompleteAsync(id, storageTypeId);

        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> ClearTasks()
    {
        var storageTypeId = GetCurrentStorage();
        await _taskService.ClearTasksAsync(storageTypeId);

        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult ChooseStorage(string storage)
    {
        HttpContext.Session.SetString("StorageType", storage);

        return RedirectToAction("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }


    private int GetCurrentStorage()
    {
        var storageTypeStr = HttpContext.Session.GetString("StorageTypeId") ?? "1";
        return int.Parse(storageTypeStr);
    }

    private async Task InitPage(int storageTypeId)
    {
        ViewBag.SelectedStorage = storageTypeId;
        ViewBag.Categories = await _categoryService.GetCategoriesAsync(storageTypeId);
        ViewBag.ActiveTasks = await _taskService.GetTasksByCompletionStatusAsync(1, storageTypeId);
        ViewBag.CompletedTasks = await _taskService.GetTasksByCompletionStatusAsync(2, storageTypeId);
    }
}
