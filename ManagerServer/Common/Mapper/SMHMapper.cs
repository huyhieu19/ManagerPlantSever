using ManagerServer.Database.Entity;
using ManagerServer.Model.SMH;

namespace ManagerServer.Common.Mapper
{
    public static class SMHMapper
    {
        public static SmallHoldingDiplayModel ToSMHModel(this SmallHoldingEntity entity)
        {
            return new SmallHoldingDiplayModel()
            {
                Id = entity.Id,
                FarmId = entity.FarmId,
                Name = entity.NameSmallHolding,
                Decription = entity.Decription,
            };
        }
    }
}
