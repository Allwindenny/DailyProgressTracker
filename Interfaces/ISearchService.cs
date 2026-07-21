using DailyProgressTracker.Models;

namespace DailyProgressTracker.Interfaces;

public interface ISearchService
{
    TaskItem? FindMostRelevantTask(string question);
}