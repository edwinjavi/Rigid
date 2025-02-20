namespace Rigid.Models
{
    public class Document
    {

        //Se cambia la estructura para parecerse a GetFile
        public string Id { get; set; } // Cambiado a string para reflejar el tipo UUID
        public string FileName { get; set; }
        public string FileType { get; set; }
        public string Customer { get; set; }
        public string Location { get; set; }
        public string Url { get; set; } // URL donde está almacenado el archivo
        public DateTime UploadedAt { get; set; }
    }
}
