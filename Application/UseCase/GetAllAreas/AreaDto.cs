namespace Application.UseCase.GetAllAreas
{
    public class AreaDto
    {
        public int Identification { get; set; }
        public string Name { get; set; }

        public AreaDto(int identification, string name)
        {
            Identification = identification;
            Name = name;
        }
    }
}
