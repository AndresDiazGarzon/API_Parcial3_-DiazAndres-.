namespace HotelNetwork.DAL.Entities
{
    public interface ICity
    {
        DateTime CreateDate { get; }
        Guid Id { get; }
        string Name { get; set; }
        State? State { get; set; }
        Guid StateId { get; set; }
    }
}