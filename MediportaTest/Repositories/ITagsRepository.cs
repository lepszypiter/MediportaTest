using MediportaTest.Models;

namespace MediportaTest.Repositories;

public interface ITagsRepository
{
    Task<IReadOnlyCollection<Tag>> GetAllTags();
}