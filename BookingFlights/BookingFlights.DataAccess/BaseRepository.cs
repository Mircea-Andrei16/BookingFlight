using BookingFlights.Abstractions.Repository;
using BookingFlights.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingFlights.DataAccess
{
    public class BaseRepository<T> : IBaseRepository<T> where T : EntityClass
    {
        protected readonly BookingFlightsDbContext dbContext;

        public BaseRepository(BookingFlightsDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public T Add(T element)
        {
           var returnEntity = dbContext.Set<T>().Add(element)
                                    .Entity;

            dbContext.SaveChanges();

            return returnEntity;
        }

        public void Delete(Guid id)
        {
            var item = GetById(id);

            dbContext.Set<T>().Remove(item);

            dbContext.SaveChanges();
        }

        public virtual ICollection<T> GetAll()
        {
            return dbContext.Set<T>().ToList();
        }

        public T GetById(Guid id)
        {
          return  dbContext.Set<T>().Single(entity => entity.Id == id);
        }

        public T Update(T elementToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
