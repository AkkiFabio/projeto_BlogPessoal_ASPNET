using System;
using System.Threading.Tasks;

namespace BlogAPI.Src.Repositorios
{
    public interface ICrud
    {
        Task Created(Object obj);
        Task<Object> Read();
        Task Updated();
        Task Deleted();
    }
}
