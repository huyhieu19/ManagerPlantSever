namespace ManagerServer.Model
{
    public class PagingQueryModel
    {
        public int? PageSize { get; set; } = 50;
        public int? PageNumber { get; set; } = 1;

    }
}
