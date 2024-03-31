using MediportaTest.Models;

namespace MediportaTest.Repositories;

class TagsRepository : ITagsRepository
{
    public Task<IReadOnlyCollection<Tag>> GetAllTags()
    {
        throw new NotImplementedException();
    }
}