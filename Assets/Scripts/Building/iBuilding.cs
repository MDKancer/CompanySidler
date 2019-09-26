using System;
using Constants;
using JetBrains.Annotations;
using UnityEngine;

namespace BuildingPackage
{

  public interface iBuilding
  {
    void Upgrade();
    void DoDamage(int damagePercent = 0);
    void Work();
    void SwitchWorkingState();
    void ApplyWorker([NotNull] Life.Worker worker);
    void QuitWorker([NotNull] Life.Worker worker);
    bool BuildingRepair();
    BuildingData BuildingData { get;}
    BuildingState buildingWorkingState { get; }
  }
}