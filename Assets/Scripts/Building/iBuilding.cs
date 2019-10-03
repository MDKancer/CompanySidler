using Constants;
using JetBrains.Annotations;
using Life;

namespace BuildingPackage
{

  public interface iBuilding
  {
    void Upgrade();
    void DoDamage(int damagePercent = 0);
    void Work();
    void SwitchWorkingState();
    void ApplyWorker([NotNull] Worker worker);
    void QuitWorker([NotNull] Worker worker);
    bool BuildingRepair();
    BuildingData BuildingData { get;}
    BuildingState buildingWorkingState { get; }
  }
}