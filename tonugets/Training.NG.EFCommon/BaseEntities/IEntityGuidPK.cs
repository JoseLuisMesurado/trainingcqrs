using Training.NG.EFCommon.BaseEntities;

namespace Training.NG.EFCommon;

public interface IEntityGuidPK: IBaseEntity
{
    public Guid Id { get; set; }
}
