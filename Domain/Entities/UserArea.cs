namespace Domain.Entities
{
    public class UserArea
    {
        public string UserIdentification { get; set; }
        public User User { get; set; }
        public string AreaIdentification { get; set; }
        public Area Area { get; set; }
    }
}