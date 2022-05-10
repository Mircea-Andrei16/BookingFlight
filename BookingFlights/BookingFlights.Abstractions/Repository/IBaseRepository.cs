using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingFlights.Abstractions.Repository
{
    public interface IBaseRepository<T>
    {

        T GetById(Guid id);

        ICollection<T> GetAll();

        T Add(T element);

        void Delete(Guid id);

        T Update(T elementToUpdate);

    }
}
