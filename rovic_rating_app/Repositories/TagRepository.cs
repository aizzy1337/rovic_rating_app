using Microsoft.EntityFrameworkCore;
using rovic_rating_app.Data;
using rovic_rating_app.Models;
using rovic_rating_app.Repositories.Interfaces;

namespace rovic_rating_app.Repositories
{
    public class TagRepository : Repository<Tag>, ITagRepository
    {
        private readonly DataContext _context;

        public TagRepository(DataContext context)
            : base(context)
        {
            _context = context;
        }

    }
}
