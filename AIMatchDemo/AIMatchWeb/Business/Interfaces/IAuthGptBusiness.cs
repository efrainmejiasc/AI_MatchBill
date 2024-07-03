using System.Threading.Tasks;

namespace AIMatchWeb.Business.Interfaces
{
    public interface IAuthGptBusiness
    {
        Task<TOutput> PostHttpRequestAuthApiGpt<TInput, TOutput>(TInput model);
    }
}
