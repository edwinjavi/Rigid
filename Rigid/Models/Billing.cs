namespace Rigid.Models
{
    public class Billing
    {

        //Cambiando estructura para parecer a PurchaseOrders
        public string Id { get; set; }
        public string Supplier { get; set; }
        public string Number { get; set; }
        public string Status { get; set; }
        public int ProductsCount { get; set; }
        public decimal Total { get; set; }
        public DateTime OrderedDate { get; set; }
        public DateTime? ReceivedDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool IsArchived { get; set; }
    }
}
