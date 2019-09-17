using System;

namespace BuildingPackage
{

// sese
  public interface iBuilding
  {
    void Upgrade();
    void GetDamage();
    void Work();
    void SwitchState();
    BuildingData BuildingData { get; set; }
  }
}