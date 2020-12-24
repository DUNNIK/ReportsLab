namespace ReportsLab.BusinessLogicLayer.EmployeeSystem
{
    public interface ISubordinate : IEmployee
    {
        void GetNewDirector(IDirector director);
        bool IsThereADirecter();
        IDirector Director();
    }
}