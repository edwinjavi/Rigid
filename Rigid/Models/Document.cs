namespace Rigid.Models
{
    public class Document
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }  
        public string Customer { get; set; }
        public string Location { get; set; }
        public string Url { get; set; }  // URL donde está almacenado el archivo
        public DateTime UploadedAt { get; set; }
    }
}
