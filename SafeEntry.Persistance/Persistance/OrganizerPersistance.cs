using SafeEntry.Persistance.Interfaces;
using SafeEntry.Persistance.Models;

namespace SafeEntry.Persistance.Persistance
{
    public class OrganizerPersistance : IOrganizerPersistance
    {
        public Task<IEnumerable<OrganizerModel>> GetAll(int id)
        {
            throw new NotImplementedException();
        }

        public Task<OrganizerModel> Insert(OrganizerModel model)
        {
            throw new NotImplementedException();
        }
    }
}
