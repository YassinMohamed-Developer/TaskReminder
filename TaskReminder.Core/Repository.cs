using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

public class JsonRepository<T> where T : class
{
    private readonly object _lock = new();
    private readonly string _filePath;

    public JsonRepository(string filePath)
    {
        _filePath = filePath;

        // Ensure the JSON file exists
        if (!File.Exists(_filePath))
        {
            File.WriteAllText(_filePath, "[]");
        }
    }

    public List<T> GetAll()
    {
        var jsonData = File.ReadAllText(_filePath);
        return JsonSerializer.Deserialize<List<T>>(jsonData) ?? new List<T>();
    }

    public T GetOne(Func<T, bool> predicate)
    {
        return GetAll().FirstOrDefault(predicate);
    }

    public void Add(T item)
    {
        lock (_lock)
        {
            var items = GetAll();
            var itemWithSameId = items.FirstOrDefault(existingItem =>
                typeof(T).GetProperty("Id")?.GetValue(existingItem)?.Equals(
                    typeof(T).GetProperty("Id")?.GetValue(item)
                ) == true);

            if (itemWithSameId != null)
            {
                throw new Exception("An item with the same Id already exists.");
            }
            items.Add(item);
            Save(items);
        }
    }

    public void Update(Func<T, bool> predicate, T updatedItem)
    {
        lock (_lock)
        {
            var items = GetAll();

            // Find the index of the item to update
            var index = items.FindIndex(x => predicate(x));
            if (index < 0)
            {
                throw new Exception("Item not found.");
            }

            // Check if the updated item's Id conflicts with another item's Id
            var updatedItemId = typeof(T).GetProperty("Id")?.GetValue(updatedItem);
            var conflict = items.Any(existingItem =>
                !predicate(existingItem) && // Ignore the item being updated
                typeof(T).GetProperty("Id")?.GetValue(existingItem)?.Equals(updatedItemId) == true);

            if (conflict)
            {
                throw new Exception("Another item with the same Id already exists.");
            }

            // Update the item
            items[index] = updatedItem;
            Save(items);
        }
    }

    public void Remove(Func<T, bool> predicate)
    {
        lock (_lock)
        {
            var items = GetAll();
            var itemToRemove = items.FirstOrDefault(predicate);

            if (itemToRemove != null)
            {
                items.Remove(itemToRemove);
                Save(items);
            }
            else
            {
                throw new Exception("Item not found");
            }
        }
    }

    private void Save(List<T> items)
    {
        lock (_lock)
        {
            var jsonData = JsonSerializer.Serialize(items, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, jsonData);
        }
    }


}
