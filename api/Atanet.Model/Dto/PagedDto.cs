namespace Atanet.Model.Dto
{
    using Atanet.Model.Interfaces;
    using Microsoft.AspNetCore.Mvc;

    public class PagedDto : IPaged
    {
        [FromQuery(Name = "page")]
        public int PageNumber { get; set; }

        [FromQuery(Name = "pageSize")]
        public int PageSize { get; set; }
    }
}
