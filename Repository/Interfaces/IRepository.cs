using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IRepository<T>
    {
        public T GetById(long Id);

        public void Add(T entity);

        public void Update(T entity);

        public void Delete(long Id);
    }
}
