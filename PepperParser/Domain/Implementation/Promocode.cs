using PepperParser.Domain.Interface;

namespace PepperParser.Domain.Implementation
{
    //Модель промокода в БД
    public class Promocode : IPromocode
    {
        public int Id { get; set; }
        public string IdOnPepper { get; set; }
        public string Name { get; set; }
        public string AvalibleTo { get; set; }
        public string Code { get; set; }
        public string Agregator { get; set; }
        public bool Continued { get; set; }
    }
}
