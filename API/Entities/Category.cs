namespace API.Entities
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int ParentCategoryId { get; set; }

        public int UserId { get; set; }

        public string OperationTypeId { get; set; }
    }
}