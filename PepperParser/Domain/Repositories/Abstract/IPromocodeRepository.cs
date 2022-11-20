using PepperParser.Domain.Implementation;

namespace PepperParser.Domain.Repositories.Abstract
{
    public interface IPromocodeRepository
    {
        void UpdateAllPromocode(List<string> urls);
        List<Promocode> GetPromocodeByAgregatorName(string agregatorName);
    }
}