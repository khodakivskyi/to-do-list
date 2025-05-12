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
        var categories = _categoryRepository.GetCategories();
        ViewBag.Categories = categories;

        var activeTasks = _taskRepository.GetActiveTasks();
        ViewBag.Tasks = activeTasks;

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
            _categoryRepository.AddDefaultCategories();
            _taskRepository.AddTask(task);

            var activeTasks = _taskRepository.GetActiveTasks();
            ViewBag.Tasks = activeTasks;

            return RedirectToAction("Index");
        }

        return View();
    }
    [HttpPost]
    public IActionResult Is_Compleated(int taskId)
    {
        try
        {
            var task = _taskRepository.GetTaskById(taskId); 

            if (task != null)
            {
                task.Is_Complited = !task.Is_Complited; 

   
                _taskRepository.UpdateTask(task);

                return RedirectToAction("Index");
            }

            return View();
        }
        catch (Exception ex)
        {
            // Логування помилки або повідомлення про помилку
            Console.WriteLine(ex.ToString());
            return View("Error");
        }
    }
}
