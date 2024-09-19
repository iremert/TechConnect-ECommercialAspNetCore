using TechConnect.EL.Concrete;

namespace TechConnect.BLL.Concrete
{
    public interface ISliderManager
    {
        Task TCreateAsync(Slider t);
        Task TDeleteAsync(string id);
        Task<List<Slider>> TGetAllAsync();
        Task<Slider> TGetByIdAsync(string id);
        Task TUpdateAsync(Slider t, string id);
    }
}