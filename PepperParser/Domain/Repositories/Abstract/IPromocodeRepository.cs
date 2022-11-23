using Microsoft.AspNetCore.Mvc;
using PepperParser.Domain.Implementation;

namespace PepperParser.Domain.Repositories.Abstract
{
    public interface IPromocodeRepository
    {
        void UpdateAllPromocode(List<string> urls, [FromServices] IConfiguration config);
        List<Promocode> GetPromocodeByAgregatorName(string agregatorName);
    }
}