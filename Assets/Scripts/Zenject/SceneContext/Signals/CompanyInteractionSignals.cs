using BuildingPackage;
using Enums;
using Human;
using ProjectPackage;

namespace Zenject_Signals
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