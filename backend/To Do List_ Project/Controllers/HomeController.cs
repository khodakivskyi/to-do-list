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
    public async Task<IActionResult> MarkTaskAsComplete(string taskIdStr)
    {
        var storageTypeId = GetCurrentStorage();
        var id = int.TryParse(taskIdStr, out int parsedId) ? parsedId : 0;
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
        if (!int.TryParse(storage, out int StorageTypeId)) StorageTypeId = 1;

        HttpContext.Session.SetInt32("StorageTypeId", StorageTypeId);

        return RedirectToAction("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }


    private int GetCurrentStorage()
    {
        return HttpContext.Session.GetInt32("StorageTypeId") ?? 1;
    }

    private async Task InitPage(int storageTypeId)
    {
        ViewBag.SelectedStorageId = storageTypeId;
        ViewBag.Categories = await _categoryService.GetCategoriesAsync(storageTypeId);
        ViewBag.ActiveTasks = await _taskService.GetTasksByCompletionStatusAsync(1, storageTypeId);
        var completed = await _taskService.GetTasksByCompletionStatusAsync(2, storageTypeId);
        Console.WriteLine($"Completed tasks count: {completed.Count()}");
        ViewBag.CompletedTasks = completed;

    }
}
