using Ardalis.Specification;
using Core.Entities;

namespace Core.Specifications
{
    public static class Subcategories
    {
        public class ById : Specification<Subcategory>
        {
            public ById(int id)
            {
                Query
                    .Where(x => x.Id == id);
            }
        }
        public class All : Specification<Subcategory>
        {
            public All()
            {
                Query
                    .OrderBy(x => x.Advertisments.Count);
            }
        }
        public class ByCategoryId : Specification<Subcategory>
        {
            public ByCategoryId(int categoryId)
            {
                Query
                    .Where(c => c.CategoryId == categoryId);
            }
        }
    }
}
