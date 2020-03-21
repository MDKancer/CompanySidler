using Building;
using Entity.Employee;
using Enums;
using ProjectPackage;

namespace Zenject.SceneContext.Signals
{
    public struct ApplyEmployeeSignal
    {
        public EntityType employeeType;
    }
    public struct QuitEmployeeSignal
    {
        public Employee employee;
    }
    public struct StartProjectSignal
    {
        public Project project;
    }
    public struct CloseProjectSignal
    {
        public Project project;
    }

    public struct CurrentCompanySignal
    {
        public Company company;
    }
}