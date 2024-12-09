namespace Application.UseCase.AssignUserToArea
{
    public class AssignUserToAreaCommand
    {
        public string UserIdentification { get; set; }
        public int AreaIdentification { get; set; }

        public AssignUserToAreaCommand(string userIdentification, int areaIdentification)
        {
            UserIdentification = userIdentification;
            AreaIdentification = areaIdentification;
        }
    }
}
