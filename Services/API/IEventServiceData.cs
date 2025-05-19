namespace Services.API
{
    public interface IEventServiceData
    {
        int id { get; }
        int stateId { get; }
        int userId { get; }
        int quantityChanged { get; set; }
    }
}
