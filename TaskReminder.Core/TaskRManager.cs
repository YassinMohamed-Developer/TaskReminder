using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TaskReminderTest;

public class TaskRManager
{
    private readonly JsonRepository<TaskR> _repository;
    private readonly ConcurrentDictionary<string, CancellationTokenSource> _cancellationTokens = new();
    private readonly object _lock = new();

    public TaskRManager(string filePath)
    {
        _repository = new JsonRepository<TaskR>(filePath);
    }

    public void AddTask(TaskR task)
    {
        _repository.Add(task);
        StartReminderTask(task);
    }

    public void UpdateTask(string id, TaskR updatedTask)
    {
        _repository.Update(t => t.Id == id, updatedTask);
        RestartReminderTask(updatedTask);
    }

    public void RemoveTask(string id)
    {
        _repository.Remove(t => t.Id == id);
        StopReminderTask(id);
    }

    public List<TaskR> GetAllTasks() => _repository.GetAll();

    private void StartReminderTask(TaskR task)
    {
        var cts = new CancellationTokenSource();
        _cancellationTokens[task.Id] = cts;

        Task.Run(async () =>
        {
            try
            {
                var delay = (task.RemindMeAt - DateTime.Now).TotalMilliseconds;
                if (delay > 0)
                    await Task.Delay((int)delay, cts.Token);

                if (!cts.Token.IsCancellationRequested&&!task.IsDead)
                {
                    Console.Beep(1000, 500);
                    Console.WriteLine($"Reminder: '{task.Name}' is due at {DateTime.Now}. Description: {task.Description}");
                    task.IsDead = true;
                    _repository.Update(t => t.Id == task.Id, task);
                }
            }
            catch (TaskCanceledException)
            {
                Console.WriteLine($"Task reminder for '{task.Name}' was cancelled.");
            }
        }, cts.Token);
    }

    private void RestartReminderTask(TaskR task)
    {
        StopReminderTask(task.Id);
        StartReminderTask(task);
    }

    private void StopReminderTask(string id)
    {
        if (_cancellationTokens.TryRemove(id, out var cts))
        {
            cts.Cancel();
            cts.Dispose();
        }
    }

    public void LoadAllTasks()
    {
        foreach (var task in _repository.GetAll())
        {
            StartReminderTask(task);
        }
    }
}
