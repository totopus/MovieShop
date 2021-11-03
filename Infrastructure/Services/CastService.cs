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
     
        public async Task<CastDetailsResponseModel> GetCastDetails(int id)
        {
            var cast = await _castRepository.GetCastDetails(id);
            if (cast == null)
            {
                throw new Exception($"No Cast Found for this {id}");
            }
            var castDetails = new CastDetailsResponseModel()
            {
                Id = cast.Id,
                Name = cast.Name,
                Gender = cast.Gender,
                TmdbUrl = cast.TmdbUrl,
                ProfilePath = cast.ProfilePath,
            };

            foreach (var casts in cast.Movies)
            {
                castDetails.Casts.Add(new CastResponseModel {
                    Character = casts.Character
                     }); 

            }
            return castDetails;
        }

      
    }
}
