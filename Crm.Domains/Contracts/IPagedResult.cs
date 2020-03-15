namespace Crm.Domains.Contracts
{
    public interface IPagedResult
    {
        public int TotalPages { get; set; }
        public int PageNumber { get; set; }
    }
}
