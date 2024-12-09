using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Ports;

namespace Application.UseCase.GetAllAreas
{
    [Handler]
    public class GetAllAreasHandler
    {
        private readonly IAreaRepository _areaRepository;

        public GetAllAreasHandler(IAreaRepository areaRepository)
        {
            _areaRepository = areaRepository;
        }

        public async Task<List<AreaDto>> Handle()
        {
            List<Area> areas = await _areaRepository.GetAllAreasAsync();
            return areas.ConvertAll(x => new AreaDto(x.Identification, x.Name));
        }
    }
}
