namespace Domain
{

    public class DomainEntity
    {
        public DomainEntity(int v1, string v2, string v3)
        {
            Id = v1;
            FirstName = v2;
            LastName = v3;
        }

        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}

