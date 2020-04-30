using Entity.Employee;
using Enums;
using JetBrains.Annotations;

namespace Buildings
{

  public interface iBuilding
  {
    
    void Upgrade();
    //void Buy();
    void DoDamage(int damagePercent = 0);
    Company Company { get; set; }
    void SwitchWorkingState();
    void ApplyWorker([NotNull] Employee employee);
    void QuitWorker([NotNull] Employee employee);
    bool BuildingRepair();
    bool IsBuying { get; }
    BuildingData BuildingData { get;}
    BuildingState buildingWorkingState { get; }
  }
}