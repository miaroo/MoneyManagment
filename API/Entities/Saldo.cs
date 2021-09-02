namespace API.Entities
{
    public class Saldo
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string Name { get; set; }

        public double Money { get; set; }

        public AppUser AppUser { get; set; }

        public List<Operation> Operation { get; set; }
    }
}