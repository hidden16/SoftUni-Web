using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskBoardApp.Data;
using TaskBoardApp.Models.Task;

namespace TaskBoardApp.Controllers
{
    public class TasksController : Controller
    {
        private TaskBoardAppDbContext context;
        public TasksController(TaskBoardAppDbContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            TaskFormModel taskModel = new TaskFormModel()
            {
                Boards = GetBoards()
            };
            return View(taskModel);
        }

        [HttpPost]
        public IActionResult Create(TaskFormModel taskModel)
        {
            if (!GetBoards().Any(b => b.Id == taskModel.BoardId))
            {
                ModelState.AddModelError(nameof(taskModel.BoardId), "Board does not exist.");
            }

            string currentUserId = GetUserId();
            Data.Entitites.Task task = new Data.Entitites.Task()
            {
                Title = taskModel.Title,
                Description = taskModel.Description,
                CreatedOn = DateTime.Now,
                BoardId = taskModel.BoardId,
                UserId = currentUserId
            };
            context.Tasks.Add(task);
            context.SaveChanges();

            var boards = context.Boards;

            return RedirectToAction("All", "Boards");
        }

        public IActionResult Details(int id)
        {
            var task = context.Tasks
                .Where(t => t.Id == id)
                .Select(t => new TaskDetailsViewModel()
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    CreatedOn = t.CreatedOn.ToString("dd/MM/yyyy HH:mm"),
                    Board = t.Board.Name,
                    User = t.User.UserName
                })
                .FirstOrDefault();

            if (task == null)
            {
                return BadRequest();
            }

            return View(task);
        }

        public IActionResult Edit(int id)
        {
            Data.Entitites.Task task = context.Tasks.Find(id);
            if (task == null)
            {
                return BadRequest();
            }

            var currentUserId = GetUserId();

            if (currentUserId != task.UserId)
            {
                return Unauthorized();
            }

            TaskFormModel taskModel = new TaskFormModel()
            {
                Title = task.Title,
                Description = task.Description,
                BoardId = task.BoardId ?? 0,
                Boards = GetBoards()
            };

            return View(taskModel);
        } 

        [HttpPost]
        public IActionResult Edit(int id, TaskFormModel taskModel)
        {
            Data.Entitites.Task task = context.Tasks.Find(id);
            if (task == null)
            {
                return BadRequest();
            }

            var currentUserId = GetUserId();

            if (currentUserId != task.UserId)
            {
                return Unauthorized();
            }

            if (!GetBoards().Any(b=>b.Id == taskModel.BoardId))
            {
                ModelState.AddModelError(nameof(taskModel.BoardId), "Board does not exist.");
            }

            task.Title = taskModel.Title;
            task.Description = taskModel.Description;
            task.BoardId=taskModel.BoardId;

            context.SaveChanges();

            return RedirectToAction("All", "Boards");
        }

        public IActionResult Delete(int id)
        {
            Data.Entitites.Task task = context.Tasks.Find(id);

            if (task == null)
            {
                return BadRequest();
            }

            var currentUserId = GetUserId();

            if (currentUserId != task.UserId)
            {
                return Unauthorized();
            }

            var taskModel = new TaskViewModel()
            {
                Title = task.Title,
                Description = task.Description,
                User = task.UserId
            };

            return View(taskModel);
        }

        [HttpPost]
        public IActionResult Delete(TaskViewModel taskModel)
        {
            Data.Entitites.Task task = context.Tasks.Find(taskModel.Id);

            if (task == null)
            {
                return BadRequest();
            }

            var currentUserId = GetUserId();

            if (currentUserId != task.UserId)
            {
                return Unauthorized();
            }

            context.Tasks.Remove(task);
            context.SaveChanges();
            return RedirectToAction("All", "Boards");
        }
        private IEnumerable<TaskBoardModel> GetBoards()
            => this.context.Boards
            .Select(b => new TaskBoardModel()
            {
                Id = b.Id,
                Name = b.Name,
            });

        private string GetUserId()
            => User.FindFirstValue(ClaimTypes.NameIdentifier);
    }
}
