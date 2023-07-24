using Ardalis.Specification;
using Core.Entities;

namespace Core.Specifications
{
    public static class Advertisements
    {
        public class ById : Specification<Advertisement>
        {
            public ById(int id)
            {
                Query
                    .Where(x => x.Id == id)
                    .Include(x => x.AdvertisementImages);
            }
        }
        public class All : Specification<Advertisement>
        {
            public All()
            {
                Query
                    .Include(x => x.AdvertisementImages);
            }
        }
        public class ByCategoryId : Specification<Advertisement>
        {
            public ByCategoryId(int categoryId)
            {
                Query
                    .Where(x => x.CategoryId == categoryId)
                    .Include(x => x.AdvertisementImages);
            }
        }
        public class ByCategoryName : Specification<Advertisement>
        {
            public ByCategoryName(string categoryName)
            {
                Query
                    .Where(x => x.Category.Name == categoryName)
                    .Include(x => x.AdvertisementImages);
            }
        }
        public class ByUserId : Specification<Advertisement>
        {
            public ByUserId(string userId)
            {
                Query
                    .Where(x => x.UserId == userId)
                    .Include(x => x.AdvertisementImages);
            }
        }
        public class ByDateDescending : Specification<Advertisement>
        {
            public ByDateDescending()
            {
                Query
                    .OrderByDescending(x => x.CreationDate)
                    .Include(x => x.AdvertisementImages);
            }
        }
    }
}
