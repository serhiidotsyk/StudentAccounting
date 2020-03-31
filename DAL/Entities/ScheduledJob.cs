namespace DAL.Entities
{
    public class ScheduledJob
    {
        public string Id { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
    }
}
