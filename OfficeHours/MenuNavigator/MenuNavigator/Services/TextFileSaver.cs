using MenuNavigator.Interfaces;
using MenuNavigator.Models;

namespace MenuNavigator.Services;

public class TextFileSaver : IFileSaver
{
    private const string FilePath = "calendar.txt";
    
    public List<DatetimeSlot> Load()
    {
        if (!File.Exists(FilePath))
        {
            return new List<DatetimeSlot>();
        }
        string[] lines = File.ReadAllLines(FilePath);
        List<DatetimeSlot> slots = new List<DatetimeSlot>();

        foreach (string line in lines)
        {
            string[] parts = line.Split(',');
            slots.Add(new  DatetimeSlot(DateTime.Parse(parts[0]), bool.Parse(parts[1])));
        }

        return slots;
    }

    public void Save(List<DatetimeSlot> slots)
    {
        string[] lines = new string[slots.Count];
        for (int i = 0; i < slots.Count; i++)
        {
            lines[i] = $"{slots[i].Date},{slots[i].IsAvailable}";
        }
        File.WriteAllLines(FilePath, lines);
    }
}