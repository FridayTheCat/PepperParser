namespace PepperParser.Domain.Interface
{
    public interface IFeedback
    {
        string Name { get; set; }
        string Email { get; set; }
        string Message { get; set; }
    }
}