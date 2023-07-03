using Ardalis.Specification;
using Core.Entities;

namespace Core.Specifications
{
    public static class Categories
    {
        public class ById : Specification<Category>
        {
            public ById(int id)
            {
                Query
                    .Where(x => x.Id == id)
                    .Include(x => x.Subcategories);
            }
        }
        public class All : Specification<Category>
        {
            public All()
            {
                Query
                    .Include(x => x.Subcategories);
            }
        }
    }
}
