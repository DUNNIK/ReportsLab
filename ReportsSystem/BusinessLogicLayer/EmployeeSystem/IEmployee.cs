
namespace ReportsLab.BusinessLogicLayer.EmployeeSystem
{
    public interface IEmployee
    {
        string Id { get; }
        string Hierarchy();
    }
}