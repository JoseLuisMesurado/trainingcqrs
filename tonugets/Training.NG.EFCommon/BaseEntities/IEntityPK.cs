namespace Training.NG.EFCommon.BaseEntities
{
    public interface IEntityPK<T> : IBaseEntity
    {
        T Id { get; set; }
    }
}
