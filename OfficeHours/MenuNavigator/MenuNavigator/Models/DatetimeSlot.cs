namespace MenuNavigator.Models;

public class DatetimeSlot
{
    public DateTime Date { get; }
    public bool IsAvailable { get; set; }

    public DatetimeSlot(DateTime date, bool isAvailable)
    {
        Date = date;
        IsAvailable = isAvailable;
    }
}