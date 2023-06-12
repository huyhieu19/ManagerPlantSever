using ManagerServer.Common.Enum;

namespace ManagerServer.Model
{
    public class BaseQueryModel
    {
        public string searchTerm { get; set; } = string.Empty;
        public FilterType filterType { get; set; } = FilterType.None;

    }
}
