namespace Core.Entities
{
    public class AdvertismentImage
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public int AdvertismentId { get; set; }
        public Advertisment Advertisment { get; set; }
    }
}
