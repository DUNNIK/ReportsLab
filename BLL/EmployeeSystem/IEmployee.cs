namespace ReportsLab.EmployeeSystem
{
    public interface IEmployee
    {
        DAL.Infrastructure.IEmployee EmployeeInfo { get; }
        string Id { get; }
        string Hierarchy();
    }
}