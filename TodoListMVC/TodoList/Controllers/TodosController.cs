using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TodoList.Data;
using TodoList.Models;
using NLog;

namespace TodoList.Controllers
{
    public class TodosController : Controller
    {
        private readonly TodoContext _context;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public TodosController(TodoContext context)
        {
            _context = context;
        }

        // GET: Todos with search todos by priority or task
        public async Task<IActionResult> Index(string todoPriority, string searchString)
        {
            if (_context.Todo == null)
            {
                _logger.Error("Entity set 'TodoContext.Todo'  is null.");
                return Problem("Entity set 'TodoContext.Todo'  is null.");
            }

            // Use LINQ to get list of Priorities.
            IQueryable<string> priorityQuery = from t in _context.Todo
                                            orderby t.Priority
                                            select t.Priority;
            var todos = from t in _context.Todo
                        select t;

            if (!string.IsNullOrEmpty(searchString))
            {
                todos = todos.Where(s => s.Task!.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(todoPriority))
            {
                todos = todos.Where(x => x.Priority == todoPriority);
            }

            var todoPriorityVM = new TodoPriorityViewModel
            {
                Priorities = new SelectList(await priorityQuery.Distinct().ToListAsync()),
                Todos = await todos.ToListAsync()
            };
            _logger.Info("Has Todos.");
            return View(todoPriorityVM);
        }

        // GET: Todos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                _logger.Info("Id not informed");
                return NotFound();
            }

            var todo = await _context.Todo
                .FirstOrDefaultAsync(m => m.Id == id);
            if (todo == null)
            {
                _logger.Info("Id {Id} Task not found.", id);
                return NotFound();
            }
            _logger.Info("Todo Deteails {Todo} from id {Id} task.", todo, id);
            return View(todo);
        }

        // GET: Todos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Todos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Task,PlannedDate,DueDate,Priority,Done")] Todo todo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(todo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            _logger.Info("Task created. {Todo}", todo);
            return View(todo);
        }

        // GET: Todos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                _logger.Warn("Id not informed");
                return NotFound();
            }

            var todo = await _context.Todo.FindAsync(id);
            if (todo == null)
            {
                _logger.Warn("Id {Id} Task not found.", id);
                return NotFound();
            }
            _logger.Info("Found task: {Todo}", todo);
            return View(todo);
        }

        // POST: Todos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Task,PlannedDate,DueDate,Priority,Done")] Todo todo)
        {
            if (id != todo.Id)
            {
                _logger.Warn("Id {Id} informed is different from the task.", id);
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(todo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    if (!TodoExists(todo.Id))
                    {
                        _logger.Warn("Id {Id} Task not found. {Error}", id, ex);
                        return NotFound();
                    }
                    else
                    {
                        _logger.Error("Error when try to update the task Id {Id}. {Error}", id, ex);
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            _logger.Info("Found task: {Todo}", todo);
            return View(todo);
        }

        // GET: Todos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                _logger.Warn("Id not informed");
                return NotFound();
            }

            var todo = await _context.Todo
                .FirstOrDefaultAsync(m => m.Id == id);
            if (todo == null)
            {
                _logger.Warn("Id {Id} Task not found.", id);
                return NotFound();
            }
            _logger.Info("Deleted task {Todo} from {Id} ", todo, id);
            return View(todo);
        }

        // POST: Todos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var todo = await _context.Todo.FindAsync(id);
            if (todo != null)
            {
                _context.Todo.Remove(todo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TodoExists(int id)
        {
            return _context.Todo.Any(e => e.Id == id);
        }
    }
}
