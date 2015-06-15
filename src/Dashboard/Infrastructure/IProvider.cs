using System;
using System.Threading.Tasks;

namespace Dashboard.Infrastructure
{
    public interface IProvider<T>
    {
        Task<T> GetById(Guid id);
    }
}