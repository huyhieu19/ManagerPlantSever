//using Common.Model;
//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Common.Mapper
//{
//    public static class MapperData
//    {
//        public static DataProcessingModel ToDataProcessModel(this RawData datamodel)
//        {
//            var arrtemp = datamodel.Topic.Split("/");
//            return new DataProcessingModel()
//            {
//                FarmId = arrtemp[1],
//                ZoneId = arrtemp[2],
//                Topic = arrtemp[3],
//                Payload = datamodel.Payload,
//                RetrieveAt = datamodel.RetreveAt,
//            };
//        }
//    }
//}
