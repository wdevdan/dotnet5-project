namespace DW.Company.Entities.Entity
{
    public class Leather
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int? FileItemId { get; set; }
        public int? LeatherTypeId { get; set; }

        public virtual FileItem FileItem { get; set; }
        public virtual LeatherType LeatherType { get; set; }
    }
}
