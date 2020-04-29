using Buildings;
using Buildings.Bank;
using UnityEngine;
using Zenject;

namespace UIDispatcher.GameComponents.UIBuildingContent
{
    /// <summary>
    /// Ein einfacher Content Generator, hier werden alle UI Elemente und Funktionen was ein Büro braucht.
    /// Sehr Complex und nicht schön.
    /// </summary>
    public class BuildingContent
    {
        private readonly ProceduralUiElements proceduralUiElements;        
        private Building building;
        private UIData uiData;
        private SignalBus signalBus;
        private Container.Cloud cloud;
        private MonoBehaviour monoBehaviour;
        public BuildingContent(SignalBus signalBus,MonoBehaviour monoBehaviour, Container.Cloud cloud, ref UIData uiData)
        {
            this.signalBus = signalBus;
            this.cloud = cloud;
            this.monoBehaviour = monoBehaviour;
            this.uiData = uiData;
            proceduralUiElements = new ProceduralUiElements();
        }


        public void CreateBuildingContent(Building building)
        {
            this.building = building;
            if (building.GetType() == typeof(Bank))
            {
                var bankUIData = new BankUIData<Bank>(signalBus,monoBehaviour,ref uiData, building);
                bankUIData.SetBankInteractions();
            }
            else
            {
                var buildingUIData = new BuildingUIData<Building>(signalBus,monoBehaviour,cloud,ref uiData,building);
                buildingUIData.SetBuildingInteractions();
            }
        }

        ~BuildingContent()
        {
            //Deconstructor
        }
    }
}