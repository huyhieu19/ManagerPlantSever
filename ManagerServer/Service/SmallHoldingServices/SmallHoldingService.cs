using Common.Model;
using ManagerServer.Common.Enum;
using ManagerServer.Common.Mapper;
using ManagerServer.Database;
using ManagerServer.Database.Entity;
using ManagerServer.Model;
using ManagerServer.Model.DataDisplay;
using ManagerServer.Model.Owner;
using ManagerServer.Model.SMH;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace ManagerServer.Service.SmallHoldingServices
{
    public class SmallHoldingService : ISmallHoldingService
    {
        private readonly ManagerDbContext context;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<AppUser> userManager;

        public SmallHoldingService(ManagerDbContext context, RoleManager<IdentityRole> roleManager
            , UserManager<AppUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }
        public async Task<bool> AddSMH(SmallHoldingQueryModel queryModel)
        {
            var data = new SmallHoldingEntity();
            if (queryModel.FarmId != null)
            {
                data = new SmallHoldingEntity()
                {
                    NameSmallHolding = queryModel.FarmName,
                    FarmId = queryModel.FarmId,
                };
            }
            else
            {
                data = new SmallHoldingEntity()
                {
                    NameSmallHolding = queryModel.FarmName,

                };
            }

            await context.AddAsync(data);
            var a = await context.SaveChangesAsync() > 0 ? true : false;
            return a;
        }

        public Task<bool> DeleteSHM(SmallHoldingQueryModel queryModel)
        {
            throw new NotImplementedException();
        }

        public Task<bool> EditSHM(SmallHoldingQueryModel queryModel)
        {
            throw new NotImplementedException();
        }

        public async Task<PagingReponseModel<SmallHoldingDiplayModel>> GetAllSHM(SmallHoldingQueryModel queryModel)
        {
            var datas = from data in
                            await context.SmallHoldingEntities
                            .Skip((queryModel.PageNumber.Value - 1) * queryModel.PageSize.Value)
                            .Take(queryModel.PageSize.Value).ToListAsync()
                        select data.ToSMHModel();
            var reponse = new PagingReponseModel<SmallHoldingDiplayModel>()
            {
                Data = datas,
                PageNumber = queryModel.PageNumber,
                PageSize = queryModel.PageSize,
                Total = datas.Count()
            };
            return reponse;

        }

        public async Task<SmallHoldingDiplayModel> GetSMHById(SmallHoldingQueryModel queryModel)
        {
            var result = await context.SmallHoldingEntities.FindAsync(queryModel.Id);

            return result.ToSMHModel();
        }
        public async Task<PagingReponseModel<SmallHoldingDiplayModel>> GetSMHByFarmId(SmallHoldingQueryModel queryModel)
        {
            var query = context.SmallHoldingEntities.AsQueryable();
            var response = new PagingReponseModel<SmallHoldingDiplayModel>()
            {
                Data = from data in await query
                        .Skip((queryModel.PageNumber.Value - 1) * queryModel.PageSize.Value)
                        .Take(queryModel.PageSize.Value).ToListAsync()
                       where data.FarmId == queryModel.FarmId
                       select data.ToSMHModel(),
                PageNumber = queryModel.PageNumber,
                PageSize = queryModel.PageSize,
                Total = await query.CountAsync()
            };
            return response;
        }

        public async Task<PagingReponseModel<OwnerDisplayModel>> GetAllUser(SmallHoldingQueryModel queryModel)
        {
          
            return null;

        }

        public async Task<DataSensorDisplayModel> GetDataSensorRealTime(SmallHoldingQueryModel queryModel)
        {
           
          
            var datatable = from data in
                                await context.DataProcessingEntites.ToListAsync()
                            where data.Mode == "R"
                            where data.SmallHoldingId == queryModel.Id
                            where data.RetrieveAt <= queryModel.ToDate && data.RetrieveAt >= queryModel.FromDate
                            group data by data.Topic into proc
                            select new
                            {
                                Topic = proc.Key,
                                Data = proc.Average(p => double.Parse(p.Payload)),
                            }
                            ;
            var result = new DataSensorDisplayModel();
            foreach (var intem in datatable)
            {
                if (intem.Topic == TopicType.Temperature.ToString())
                {
                    result.Tempature = intem.Data;
                }
                if (intem.Topic == TopicType.Moisture.ToString())
                {
                    result.Moisture = intem.Data;
                }
                if (intem.Topic == TopicType.Humidity.ToString())
                {
                    result.Humidity = intem.Data;
                }
            }
            return result;
        }

        public async Task<List<DataSensorDisplayModel>> GetDataSensorByDate(SmallHoldingQueryModel queryModel)
        {
            return null;
        }
        public async Task<bool> AddUserFromSH(SmallHoldingQueryModel queryModel)
        {
            try
            {
                var tempUser = await userManager.FindByIdAsync(queryModel.UserId);
                var tempSh = await context.SmallHoldingEntities.ToListAsync();
                var sh = tempSh.Where(p=>p.Id==queryModel.Id).FirstOrDefault();
                if (tempUser == null && sh == null)
                {
                    return false;
                }
               
                return await context.SaveChangesAsync()>0?true:false;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;

            }

        }
    }
}
