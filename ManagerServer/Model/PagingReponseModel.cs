namespace ManagerServer.Model
{
    public class PagingReponseModel<T> where T : class 
    {
        public IEnumerable<T?>? Data { get; set; }
        public int? Total { get; set; }
        public int? PageSize { get; set; }
        public int? PageNumber { get; set; }
        
    }
}
