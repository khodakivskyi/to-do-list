using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using To_Do_List__Project.Models;
using To_Do_List__Project.Repositories;

namespace To_Do_List__Project.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly TaskRepository _taskRepository;
    private readonly CategoryRepository _categoryRepository;

    public HomeController(ILogger<HomeController> logger, TaskRepository taskRepository, CategoryRepository categoryRepository)
    {
        _logger = logger;
        _taskRepository = taskRepository;
        _categoryRepository = categoryRepository;
    }

    public IActionResult Index()
    {
        _categoryRepository.AddDefaultCategories();
        var categories = _categoryRepository.GetCategories();
        ViewBag.Categories = categories;

        var activeTasks = _taskRepository.GetActiveTasks();
        ViewBag.activeTasks = activeTasks;

        var completedTasks = _taskRepository.GetCompletedTasks();
        ViewBag.completedTasks = completedTasks;

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
            _taskRepository.AddTask(task);

            var activeTasks = _taskRepository.GetActiveTasks();
            ViewBag.Tasks = activeTasks;

            return RedirectToAction("Index");
        }

        return View();
    }
    [HttpPost]
    public IActionResult Is_Completed(int taskId)
    {
        try
        {
            var task = _taskRepository.GetTaskById(taskId); 

            if (task != null)
            {
                task.Is_Completed = !task.Is_Completed;
                task.Completed_At = DateTime.Now;
                _taskRepository.UpdateTask(task);

                return RedirectToAction("Index");
            }

            return NotFound();
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
            _taskRepository.CleanTasks();
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return View("Error");
        }
    }
}
