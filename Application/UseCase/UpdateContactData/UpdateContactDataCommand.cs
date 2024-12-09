namespace Application.UseCase.UpdateContactData
{
    public class UpdateContactDataCommand
    {
        public string Identification { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public UpdateContactDataCommand(string identification, string email, string phone)
        {
            Identification = identification;
            Email = email;
            Phone = phone;
        }
    }
}
