using Enums;
using JetBrains.Annotations;
using Human;

namespace BuildingPackage
{

  public interface iBuilding
  {
    void Upgrade();
    void DoDamage(int damagePercent = 0);
    void SwitchWorkingState();
    void ApplyWorker([NotNull] Employee employee);
    void QuitWorker([NotNull] Employee employee);
    bool BuildingRepair();
    BuildingData BuildingData { get;}
    BuildingState buildingWorkingState { get; }
  }
}