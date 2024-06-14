using Desafio.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Infrastructure.Contexts
{
    public interface ISqlContext<T> where T : class
    {
        Task<List<T>> ReadAll();

        Task<T> Read(int id);

        Task<T> Replace(T product);

        Task<T> Place(T product);

        Task<T> Remove(int id);

    }
}
