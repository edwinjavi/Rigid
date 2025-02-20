namespace Rigid.Models
{
    public class License
    {

        //Cambiamos el mapeo para parecer a ServiceContracts
        public string Id { get; set; }   // id del contrato de servicio
        public string ClientId { get; set; }
        public string ClientName { get; set; }
        public string ClientNumber { get; set; }
        public string ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string ProjectNumber { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public string Status { get; set; }
        public decimal PricePerMonth { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime PaymentDueDate { get; set; }
        public DateTime CanceledDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
