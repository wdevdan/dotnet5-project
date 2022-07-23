using System;

namespace DW.Company.Entities.Dto
{
    public class FileItemDto
    {
        public int Id { get; set; }
        public string Extension { get; set; }
        public string OriginName { get; set; }
        public string CurrentName { get; set; }
        public string Path { get; set; }
        public string Md5 { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
