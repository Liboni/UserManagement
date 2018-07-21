
namespace UserManagement.QueryParameters
{
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    public class PaginationParameters
    {
        [BindRequired]
        public int Skip { get; set; }
        [BindRequired]
        public int Take { get; set; }
    }
}
