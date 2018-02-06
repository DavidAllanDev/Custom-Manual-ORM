namespace Custom.Manual.ORM.Base.Interfaces
{
    public interface IIdentifiable<T>
    {
        T Id { get; set; }
    }
}
