namespace ReportsLab.BusinessLogicLayer.EmployeeSystem
{
    public interface IEmployee
    {
        DAL.StorageLayer.Infrastructure.IEmployee EmployeeInfo { get; }
        string Id { get; }
        string Hierarchy();
    }
}