using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TodoList.Data;
using System;
using System.Linq;

namespace TodoList.Models;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new TodoContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<TodoContext>>()))
        {
            // Look for any tasks.
            if (context.Todo.Any())
            {
                return;   // DB has been seeded
            }
            context.Todo.AddRange(
                new Todo
                {
                    Task = "Study DotNet Bootcamp Level 6",
                    PlannedDate = DateTime.Parse("2024-4-02"),
                    DueDate = DateTime.Parse("2024-4-05"),
                    Priority = "P1",
                    Done = true
                },
                new Todo
                {
                    Task = "Do the Exercise 1 from DotNet Bootcamp Level 6",
                    PlannedDate = DateTime.Parse("2024-4-04"),
                    Priority = "P2"
                }
            );
            context.SaveChanges();
        }
    }
}