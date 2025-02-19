namespace Rigid.Models
{
    public class License
    {
        public int Id { get; set; }
        public string LicenseKey { get; set; }
        public string Product { get; set; }
        public string Customer { get; set; }
        public string Location { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsActive { get; set; }
    }
}
