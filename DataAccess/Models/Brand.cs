namespace DataAccess.Models
{
    public class Brand
    {
        public int BrandID { get; set; }
        public string? Username { get; set; }
        public string? Name { get; set; }
        public string? Image { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? CompanyURl { get; set; }
        public string? Address { get; set; }
        public int? StatusID { get; set; }
        public string? Currency { get; set; }
        public double? Tax { get; set; }
        public double? DeliveryCharges { get; set; }
        public long? BusinessKey { get; set; }
        public string? LastUpdateBy { get; set; }
        public System.DateTime LastUpdatedDate { get; set; }
    }
}
