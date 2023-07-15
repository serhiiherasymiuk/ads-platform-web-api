using Ardalis.Specification;
using Core.Entities;

namespace Core.Specifications
{
    public static class Advertisments
    {
        public class ById : Specification<Advertisment>
        {
            public ById(int id)
            {
                Query
                    .Where(x => x.Id == id)
                    .Include(x => x.AdvertismentImages);
            }
        }
        public class All : Specification<Advertisment>
        {
            public All()
            {
                Query
                    .Include(x => x.AdvertismentImages);
            }
        }
        public class BySubcategoryId : Specification<Advertisment>
        {
            public BySubcategoryId(int subcategoryId)
            {
                Query
                    .Where(x => x.SubcategoryId == subcategoryId)
                    .Include(x => x.AdvertismentImages);
            }
        }
        public class ByCategoryId : Specification<Advertisment>
        {
            public ByCategoryId(int categoryId)
            {
                Query
                    .Where(x => x.Subcategory.CategoryId == categoryId)
                    .Include(x => x.AdvertismentImages);
            }
        }

    }
}
