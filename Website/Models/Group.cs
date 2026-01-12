namespace Website.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public ICollection<PersonGroup> PersonGroups { get; set; } = new List<PersonGroup>();


    }
}
