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
                    .Where(x => x.Id == id);
            }
        }
        public class ByParentId : Specification<Category>
        {
            public ByParentId(int parentId)
            {
                Query
                    .Where(x => x.ParentId == parentId);
            }
        }
        public class All : Specification<Category>
        {
            public All()
            {
                Query
                    .Include(x => x.Parent);
            }
        }
        public class Head : Specification<Category>
        {
            public Head()
            {
                Query
                    .Where(x => x.ParentId == null);
            }
        }
    }
}
