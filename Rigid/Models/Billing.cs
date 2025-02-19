namespace Rigid.Models
{
    public class Billing
    {
        public int Id { get; set; }
        public string InvoiceNumber { get; set; }
        public string Customer { get; set; }
        public string Location { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }  
        public DateTime DueDate { get; set; }
    }
}
