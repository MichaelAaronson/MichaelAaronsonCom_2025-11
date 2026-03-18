namespace Website.Models
{
    public class JobDetailSkill
    {
        public int JobDetailId { get; set; }
        public virtual JobDetail JobDetail { get; set; } = null!;
        public int JobSkillId { get; set; }
        public virtual JobSkill JobSkill { get; set; } = null!;
    }
}
