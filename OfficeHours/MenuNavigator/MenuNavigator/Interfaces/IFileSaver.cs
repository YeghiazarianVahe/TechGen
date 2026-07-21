using MenuNavigator.Models;

namespace MenuNavigator.Interfaces;

public interface IFileSaver
{
    List<DatetimeSlot> Load();
    void Save(List<DatetimeSlot> slots);
}