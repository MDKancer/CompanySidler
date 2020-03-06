using System;
using System.Collections;
using System.Reflection;
using BuildingPackage;
using Enums;
using GameCloud;
using Human;
using PlayerView;
using ProjectPackage;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Zenject_Signals;
using Object = UnityEngine.Object;

namespace UIPackage.UIBuildingContent
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
        private Container container;
        public BuildingContent(SignalBus signalBus, Container container, ref UIData uiData)
        {
            Init(signalBus,container);
            this.container = container;
            this.uiData = uiData;
            proceduralUiElements = new ProceduralUiElements();
        }

        private void Init(SignalBus signalBus,Container container)
        {
            this.signalBus = signalBus;
        }

        public void CreateBuildingContent(Building building)
        {
            this.building = building;
            if (building.GetType() == typeof(Bank))
            {
                var bankUIData = new BankUIData<Bank>(signalBus,ref uiData, building);
                bankUIData.SetBankInteractions();
            }
            else
            {
                var buildingUIData = new BuildingUIData<Building>(signalBus,container,ref uiData,building);
                buildingUIData.SetBuildingInteractions();
            }
        }

        ~BuildingContent()
        {
            //Deconstructor
        }
    }
}