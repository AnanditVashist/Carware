namespace Carware.ViewModels
{
    public class PhotoViewModel
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public CarViewModel Car { get; set; }
        public byte[] PhotoBlob { get; set; }
    }
}
