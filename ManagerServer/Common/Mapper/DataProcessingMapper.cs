//using Common.Model;
//using ManagerServer.Common.Enum;
//using ManagerServer.Database.Entity;
//using ManagerServer.Model.DataDisplay;

//namespace ManagerServer.Common.Mapper
//{
//    public static class DataProcessingMapper
//    {
//        public static DataProcessingDisplayModel ToDataModel(this DataEntity entity)
//        {
//            return new DataProcessingDisplayModel()
//            {
//                RetrieveAt = entity.RetrieveAt,
//                Payload = entity.Payload,
//                Mode = entity.Mode,
//                Topic = entity.Topic,
//            };
//        }
//        public static DataSensorDisplayModel ToDataSensorModel(this DataEntity entity)
//        {

//            var result = new DataSensorDisplayModel();
//            if (entity.Topic == TopicType.Temperature.ToString())
//            {
//                result.Tempature = double.Parse(entity.Payload); 
//                result.ValueDate = entity.RetrieveAt;

//            }
//            else if(entity.Topic == TopicType.Moisture.ToString())
//            {
//                result.Moisture = double.Parse(entity.Payload);
//                result.ValueDate = entity.RetrieveAt;

//            }
//            else if(entity.Topic == TopicType.Humidity.ToString())
//            {
//                result.Humidity = double.Parse(entity.Payload);
//                result.ValueDate = entity.RetrieveAt;

//            }
//            return result;
//        }
//    }
//}
