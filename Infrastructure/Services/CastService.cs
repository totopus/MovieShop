using ApplicationCore.Models;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Infrastructure.Services
{
    public class CastService : ICastService
    {
        private readonly ICastRepository _castRepository;
        public CastService(ICastRepository castRepository)
        {
            _castRepository = castRepository;
        }
     
        public async Task<CastResponseModel> GetCastDetails(int id)
        {
            var cast = await _castRepository.GetById(id);
            if (cast == null)
            {
                throw new Exception($"No Cast Found for this {id}");
            }
            var castDetails = new CastResponseModel()
            {
                Id = cast.Id,
                Name = cast.Name,
                Gender = cast.Gender,
                TmdbUrl = cast.TmdbUrl,
                ProfilePath = cast.ProfilePath
            };
            return castDetails;
        }
    }
}
