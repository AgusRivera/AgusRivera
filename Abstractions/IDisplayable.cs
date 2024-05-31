using AMVTravels.Contexts;
using AMVTravels.Models.Dtos;

namespace AMVTravels.Abstractions
{
    public interface IDisplayable<T>
    {
        Task<T>ShowData(AppDbContext context);
    }
}
