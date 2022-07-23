namespace DW.Company.Entities.Dto
{
    public class FileDto
    {
        public string ContentType { get; set; }
        public byte[] Content { get; set; }
        public string FileDownloadName { get; set; }
    }
}
