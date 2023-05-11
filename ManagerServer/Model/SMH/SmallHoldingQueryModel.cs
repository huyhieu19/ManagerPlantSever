namespace ManagerServer.Model.SMH
{
    public class SmallHoldingQueryModel: PagingQueryModel
    {
        public int? Id { get; set; }
        public int? FarmId { get; set; }
        public string? UserId { get; set; }
        public string? FarmName { get; set;}
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}
