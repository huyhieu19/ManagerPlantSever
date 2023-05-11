namespace ManagerServer.Common
{
    public static class GenerateData<T> where T : class
    {
        public static List<T> CreateListData(int number)
        {
            var list = new List<T>();
            var type = typeof(T);
            var proprerties = type.GetProperties();


            for (int i = 0; i <= number; i++)
            {
                var obj = Activator.CreateInstance<T>();
                foreach (var prop in proprerties)
                {
                    Guid newGuid = Guid.NewGuid();
                    string guidString = newGuid.ToString("N").Substring(0, 10);
                    if (prop.PropertyType == typeof(string))
                    {
                        string itemAdd = "";
                        if (prop.Name.ToLower().Contains("topic"))
                        {
                            var rnd = new Random().Next(1,4);
                            string topic= "";
                            if (rnd == 1) {
                                topic = "Humidity";
                            }
                            else if (rnd == 2) {
                                topic = "Moisture";
                            } 
                            else  {
                                topic = "Tempature";
                            }
                            itemAdd = topic;
                        }
                        else if (prop.Name.ToLower().Contains("mode"))
                        {
                             itemAdd = new Random().Next(1,100)%2==0?"R":"W";
                        }

                        else if (prop.Name.ToLower().Contains("payload"))
                        {
                            itemAdd = $"{new Random().NextDouble() * (100.3 - 1.4)}";
                        }
                        else
                        {
                            itemAdd = prop.Name + "_" + guidString + $"{new Random().Next(1, 1000)}";
                        }
                        prop.SetValue(obj, itemAdd); 
                    }
                    if (prop.PropertyType == typeof(int) || prop.PropertyType == typeof(int?))
                    {

                        if (prop.Name.ToLower() == "id")
                        {

                        }

                        if (prop.Name.ToLower() == "smallholdingid")
                        {
                            int itemAdd = new Random().Next(4, 7);
                            prop.SetValue(obj, itemAdd);
                        }
                        else
                        {
                            int itemAdd = new Random().Next(1, 100);
                            prop.SetValue(obj, itemAdd);
                        }
                         

                    }
                    if (prop.PropertyType == typeof(double) || prop.PropertyType == typeof(double?))
                    {

                        double itemAdd = new Random().NextDouble() * (100.3 - 1.4);
                        prop.SetValue(obj, itemAdd);
                    }
                    if (prop.PropertyType == typeof(DateTime?))
                    {
                        prop.SetValue(obj, DateTime.Now.AddHours(new Random().Next(-24, 24)));

                    }
                    if (prop.PropertyType == typeof(DateTime))
                    {
                        prop.SetValue(obj, DateTime.Now.AddHours(new Random().Next(-24, 24)));

                    }

                }
                list.Add(obj);
            }


            return list;
        }
    }
}
