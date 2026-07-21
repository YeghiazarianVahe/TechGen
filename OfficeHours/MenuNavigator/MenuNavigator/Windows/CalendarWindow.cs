using MenuNavigator.Interfaces;
using MenuNavigator.Models;
using MenuNavigator.Services;

namespace MenuNavigator.Windows;

public class CalendarWindow : BaseWindow
{
    public CalendarWindow(
        NavigationController navigate,
        IFileSaver fileSaver)
        : base("Select available date", new List<MenuItem>())
    {
        List<DatetimeSlot> slots = fileSaver.Load();

        for (int i = 1; i <= 7; i++)
        {
            DateTime candidate = DateTime.Now.Date.AddDays(i);

            DatetimeSlot? existingSlot = null;

            foreach (DatetimeSlot slot in slots)
            {
                if (slot.Date.Date == candidate)
                {
                    existingSlot = slot;
                    break;
                }
            }

            if (existingSlot is not null && !existingSlot.IsAvailable)
                continue;
            
            MenuItems.Add(new MenuItem(candidate.ToString("dd/MM/yyyy"), () =>
                {
                    if (existingSlot is not null)
                        existingSlot.IsAvailable = false;
                    else
                        slots.Add(new DatetimeSlot(candidate, false));
                    

                    fileSaver.Save(slots);
                    navigate.GoToPreviousWindow();
                }));
        }
    }
}