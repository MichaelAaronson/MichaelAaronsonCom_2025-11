namespace Website.Models
{
    public class PersonGroup
    {
        public int PersonId { get; set; }
        public Person Person { get; set; } = null!;
        public int GroupId { get; set; }
        public Group Group { get; set; } = null!;
    }
}
