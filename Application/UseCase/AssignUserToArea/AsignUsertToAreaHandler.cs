using System.Threading.Tasks;
using Domain.Services;

namespace Application.UseCase.AssignUserToArea
{
    [Handler]
    public class AssignUserToAreaHandler
    {
        private readonly AreaService _areaService;

        public AssignUserToAreaHandler(AreaService areaService)
        {
            _areaService = areaService;
        }

        public async Task Handle(AssignUserToAreaCommand command)
        {
            await _areaService.AssignUserToAreaAsync(command.UserIdentification, command.AreaIdentification);
        }
    }
}
