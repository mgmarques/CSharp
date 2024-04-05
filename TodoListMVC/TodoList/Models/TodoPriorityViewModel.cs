using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace TodoList.Models;

public class TodoPriorityViewModel
{
    public List<Todo>? Todos { get; set; }
    public SelectList? Priorities { get; set; }
    public string? TodoPriority { get; set; }
    public string? SearchString { get; set; }
}