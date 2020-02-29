using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BootManager;
using BuildingPackage.OfficeWorker;
using Enums;
using JetBrains.Annotations;
using Human;
using ProjectPackage;
using Signals.Zenject_Test;
using SpawnManager;
using StateMachine;
using UIPackage;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace BuildingPackage
{
    public class TarentTown : Building, iTarent
    {
        private List<Dictionary<BuildingType, CustomerType>> customerCompatibleBuilding;
        [Inject]
        private SpawnController spawnController;

        
        void Awake()
        {
            #region
            customerCompatibleBuilding = new List<Dictionary<BuildingType, CustomerType>>
            {
                new Dictionary<BuildingType, CustomerType>
                {
                    {BuildingType.DEV_OPS , CustomerType.BOSH},
                },
                new Dictionary<BuildingType, CustomerType>
                {
                    {BuildingType.DEV_OPS,CustomerType.UNITY_MEDIA},
                    {BuildingType.ADMIN,CustomerType.UNITY_MEDIA},
                    {BuildingType.SERVER,CustomerType.UNITY_MEDIA},
                },
                new Dictionary<BuildingType, CustomerType>
                {
                    {BuildingType.DEV_OPS,CustomerType.DE_POST},
                    {BuildingType.ADMIN,CustomerType.DE_POST},
                    {BuildingType.SERVER,CustomerType.DE_POST},
                },
                new Dictionary<BuildingType, CustomerType>
                {
                    {BuildingType.OFFICE,CustomerType.OTHER},
                    {BuildingType.ADMIN,CustomerType.OTHER},
                    {BuildingType.SERVER,CustomerType.OTHER},
                    {BuildingType.AZUBIS,CustomerType.OTHER},
                    {BuildingType.ACCOUNTING,CustomerType.OTHER},
                    {BuildingType.MARKETING,CustomerType.OTHER},
                }
            };
            #endregion
            buildingData = new BuildingData
            {
                buildingType = BuildingType.TARENT_TOWN,
                prefab =  this.officePrefab,
                name = name,
                workers = 0,
                maxHitPoints = 2000,
                currentHitPoints = 2000,
                upgradePrice = 0,
                workPlacesLimit = 1,
                moneyPerSec = -8,
                
                AvailableWorker = new List<BuildingWorkers<Employee, EntityType>>
                {
                    new BuildingWorkers<Employee, EntityType>(EntityType.PERSONAL),
                    new BuildingWorkers<Employee, EntityType>(EntityType.ANALYST),
                    new BuildingWorkers<Employee, EntityType>(EntityType.PERSONAL),
                    new BuildingWorkers<Employee, EntityType>(EntityType.ANALYST),
                    new BuildingWorkers<Employee, EntityType>(EntityType.PERSONAL),
                    new BuildingWorkers<Employee, EntityType>(EntityType.PERSONAL),
                    new BuildingWorkers<Employee, EntityType>(EntityType.DESIGNER),
                    new BuildingWorkers<Employee, EntityType>(EntityType.PERSONAL)
                    //TODO : Die Liste Erweitern / Ändern
                }
            };
            
            stateController.CurrentState = BuildingState.EMPTY;
        }

        [Inject]
        public void Init(SignalBus signalBus)
        {
            signalBus.Subscribe<CustomerSignals>(GetSignal);
        }

        private void GetSignal(CustomerSignals customerSignals)
        {
            Debug.Log(customerSignals.name);
        }
        void Start()
        {
            stateController.CurrentState = BuildingState.WORK;
            StartCoroutine(UpdateManyGenerator());
            StartCoroutine(DistributeProjects());
        }

        public void TakeProject(ref Project newProject,CustomerType customerType)
        {
            company.AddNewProject(newProject, customerType);
            var particleSystem = spawnController.SpawnEffect(buildingData.buildingType, ParticleType.PROJECT);
            Destroy(particleSystem.gameObject, 2f);
        }
        public int ToHold()
        {
            return BuildingData.wastage; 
        }
        public override void SwitchWorkingState()
        {
            if (stateController.CurrentState == BuildingState.WORK)
            {
                stateController.CurrentState = BuildingState.PAUSE;
                StopCoroutine(UpdateManyGenerator());
            }
            else
            {
                stateController.CurrentState = BuildingState.WORK;
                StartCoroutine( UpdateManyGenerator());
            }
        }
        public override bool IsBuying
        {
            get => isBuying;
            set
            {
                Buy(buildingData.prefab, this.transform.position);
                isBuying = value;
            }
        }
        private IEnumerator UpdateManyGenerator()
        {
            if (stateController.CurrentState == BuildingState.WORK)
            {
                while (stateController.CurrentState == BuildingState.WORK)
                {
                    budget += ToHold();
                    yield return new WaitForSeconds(1f);
                }
            }
        }

        private IEnumerator DistributeProjects()
        {
            var customerValues = Enum.GetValues(typeof(CustomerType));
            var buildingValues = Enum.GetValues(typeof(BuildingType));
            
            while (true)
            {
                foreach (var customerValue in customerValues)
                {
                    foreach (var buildingValue in buildingValues)
                    {
                        DistributeRecommendedProject( (CustomerType) customerValue, (BuildingType)buildingValue);
                    }
                }
                
                yield return new WaitForSeconds(1.0f);
            }
        }

        private void DistributeRecommendedProject(CustomerType customerType,BuildingType buildingType)
        {
            // diese Abfrage muss geändert sein.
            
            if (isFor(customerType,buildingType))
            {
                Building building = Company.GetOffice(buildingType);
                building.possibleProjects = company.GetProjectsByType(customerType);
            }
        }



        private bool isFor(CustomerType customerType, BuildingType buildingType)
        {
            string customerTypeLowerCase = customerType.ToString().ToLower();
            string buildingTypeLowerCase = buildingType.ToString().ToLower();
            
            return 
                customerTypeLowerCase.Contains(buildingTypeLowerCase) 
                   || 
                customerCompatibleBuilding.Any
                    (
                        compatibleList => compatibleList.ContainsValue(customerType) 
                                        && compatibleList.ContainsKey(buildingType)
                    );
        }
    }
}