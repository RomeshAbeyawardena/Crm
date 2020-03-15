namespace Crm.Domains.Contracts
{
    public interface ICustomer
    {
        int? Id { get; set; }
        string FirstName { get; set; }
        string MiddleName { get; set; }
        string LastName { get; set; }
    }
}
