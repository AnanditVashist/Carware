namespace Carware.Models
{
    public class Photo
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public Car Car { get; set; }
        public byte[] PhotoBlob { get; set; }
    }
}
