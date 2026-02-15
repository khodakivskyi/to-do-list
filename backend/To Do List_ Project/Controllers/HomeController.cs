using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using todo.Models;
using todo.Services.Interfaces;

namespace todo.Controllers;

public class HomeController : Controller
{
    private readonly ITaskService _taskService;

    private readonly Repositories.SQLRepositories.SqlCategoryRepository _sqlCategoryRepository;

    private readonly Repositories.XMLRepositories.XmlTaskRepository _xmlTaskRepository;
    private readonly Repositories.XMLRepositories.XmlCategoryRepository _xmlCategoryRepository;

    public HomeController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    public IActionResult Index()
    {
        var storage = GetCurrentStorage();
        ViewBag.SelectedStorage = storage;

        if(storage == "SQL")
        {
            var categories = _sqlCategoryRepository.GetCategories();
            ViewBag.Categories = categories;

            var activeTasks = _sqlTaskRepository.GetActiveTasks();
            var completedTasks = _sqlTaskRepository.GetCompletedTasks();

            ViewBag.activeTasks = activeTasks;
            ViewBag.completedTasks = completedTasks;
        }
        else
        {
            var categories = _xmlCategoryRepository.GetCategories();
            ViewBag.Categories = categories;

            var activeTasks = _xmlTaskRepository.GetActiveTasks();
            var completedTasks = _xmlTaskRepository.GetCompletedTasks();

            ViewBag.activeTasks = activeTasks;
            ViewBag.completedTasks = completedTasks;
        }   

        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    [HttpPost]
    public IActionResult Task_Add(TaskModel task)
    {
        if (ModelState.IsValid)
        {
            var storage = GetCurrentStorage();

            if (storage == "SQL")
                _sqlTaskRepository.AddTask(task);

            else
                _xmlTaskRepository.AddTask(task);

            return RedirectToAction("Index");
        }

        return View();
    }
    [HttpPost]
    public IActionResult Is_Completed(int taskId)
    {
        try
        {
            var storage = GetCurrentStorage();

            TaskModel? task = storage == "SQL"
        ? _sqlTaskRepository.GetTaskById(taskId)
        : _xmlTaskRepository.GetTaskById(taskId);

            if (task == null)
                return NotFound();

            task.Is_Completed = !task.Is_Completed;
            task.Completed_At = DateTime.Now;

            if (storage == "SQL")
                _sqlTaskRepository.UpdateTask(task);
            else
                _xmlTaskRepository.UpdateTask(task);

            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return View("Error");
        }
    }

    [HttpPost]
    public IActionResult CleanTasks()
    {
        try
        {
            var storage = GetCurrentStorage();

            if (storage == "SQL")
            {
                _sqlTaskRepository.ClearTasks();
            }
            else
            {
                _xmlTaskRepository.ClearTasks();
            }

            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return View("Error");
        }
    }
    [HttpPost]
    public IActionResult Storage_Choose(string storage)
    {
        Console.WriteLine($"[Storage_Choose] Selected: {storage}");

        if (storage=="SQL" || storage=="XML")
            HttpContext.Session.SetString("StorageType", storage);

        return RedirectToAction("Index");
    }
    private string GetCurrentStorage()
    {
        return HttpContext.Session.GetString("StorageType") ?? "SQL";
    }
}
