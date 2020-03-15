namespace Crm.Domains.Contracts
{
    public interface IPagedRequest
    {
        int PageNumber { get; set; }
        int MaximumRowsPerPage { get; set; }
    }
}
