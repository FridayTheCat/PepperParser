namespace PepperParser.Domain.Interface
{
    public interface IPromocode
    {
        string IdOnPepper { get; set; }
        string Name { get; set; }
        string AvalibleTo { get; set; }
        string Code { get; set; }
    }
}