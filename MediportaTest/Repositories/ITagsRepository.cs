using MediportaTest.Controllers;
using MediportaTest.Models;

namespace MediportaTest.Repositories;

public interface ITagsRepository
{
    Task<IReadOnlyCollection<Tag>> GetAllTags(int pageSize, int page, TagOrder order, TagSort sort);
    Task RefreshTags();
}