

namespace TeamTaskManagementAPI.Domain.BindingModels.Response
{
    public class PagingModel
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 50;
        public string SortColumn { get; set; } = "CreatedDate";
        public string SortOrder { get; set; } = "desc";
    }
}
