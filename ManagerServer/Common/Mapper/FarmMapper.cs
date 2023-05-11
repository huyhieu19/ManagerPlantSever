using Common.Model.Farm;
using ManagerServer.Database.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Mapper
{
    public static class FarmMapper
    {
        public static FarmDisplayModel ToFarmModel(this FarmEntity entity)
        {
            return new FarmDisplayModel()
            {
                Id = entity.Id,
                Decription = entity.Decription, 
                OwnerId = entity.OwnerId,
                Name = entity.Name,
            };
        }
    }
}
