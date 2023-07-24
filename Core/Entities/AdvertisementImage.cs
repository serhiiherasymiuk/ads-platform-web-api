namespace Core.Entities
{
    public class AdvertisementImage
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public int AdvertisementId { get; set; }
        public Advertisement Advertisement { get; set; }
    }
}
