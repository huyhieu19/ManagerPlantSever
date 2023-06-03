using ManagerServer.Database.Entity;
using ManagerServer.Model.Owner;

namespace ManagerServer.Common.Mapper
{
    public static class OwnerMapper
    {
        public static OwnerDisplayModel ToOwnerModel(this AppUser appUser)
        {
            return new OwnerDisplayModel ()
            {
                FirstName = appUser.FirstName,
                LastName = appUser.LastName,
            };
        }
    }
}
