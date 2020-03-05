using System;
using System.Collections;
using BuildingPackage;
using Enums;
using GameCloud;
using Human;
using ModestTree;
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
#region Delegates and Events

        public delegate void UpdateWindow();
        public delegate void UpgradeBuilding();
        public delegate void BuyBuilding();
        public delegate void ChangeStateBuilding(Transform transform);
        public delegate void ApplyEmployee(String employeeType);
        public delegate void QuitEmployee(Employee employee);
        public delegate void StartProject(Project project);
        public event UpdateWindow windowUpdateEvent;
        public UpgradeBuilding buildingUpgradeEvent;
        public BuyBuilding buyBuildingEvent;
        public ChangeStateBuilding changeStateBuilding;
        public ApplyEmployee applyEmployeeEvent;
        public event QuitEmployee quitEmployeeEvent;
  
#endregion
        public event StartProject startProject;
        private readonly ProceduralUiElements proceduralUiElements;        
        private Material projectButtonMaterial;
        private Building building;
        private UIData uiData;
        private MonoBehaviour monoBehaviour;
        private Container container;
        private SignalBus signalBus;
        public BuildingContent(ref UIData uiData)
        {
            
            this.uiData = uiData;
            proceduralUiElements = new ProceduralUiElements();
            
        }
        [Inject]
        private void Init(SignalBus signalBus,Container container)
        {
            this.signalBus = signalBus;
            this.container = container;
            
            foreach (var material in container.Materials)
            {
                if (material.name == "progressBar")
                {
                    projectButtonMaterial = material;
                }
            }
            
            this.signalBus.Subscribe<MonoBehaviourSignal>(GetMonoBehaviour);
        }

        private void GetMonoBehaviour(MonoBehaviourSignal monoBehaviourSignal)
        {
            monoBehaviour = monoBehaviourSignal.monoBehaviour;
        }
        public void CreateBuildingContent(Building building)
        {
            this.building = building;
            SetEmployeesButton();

            SetProjectsButton();
            
        }

        private void SetEmployeesButton()
        {
            foreach (var buildingWorker in building.BuildingData.AvailableWorker)
            {
                if (buildingWorker != null)
                {
                    if (!uiData.Contains(buildingWorker.WorkerType.ToString()))
                    {

                        var btn = proceduralUiElements.CreateButton(
                            uiData.employeeLayout.rectTransform,
                            buildingWorker.WorkerType.ToString());

                        SetEventListener(btn, buildingWorker.WorkerType);
                        
                        var (employedPlaces, countEmployedPlaces) = building.BuildingData.GetCountOfEmployed(buildingWorker.WorkerType);
                       
                        var countLabel = proceduralUiElements.GenerateCountOfEmployedWorker(
                            uiData.countLayout.rectTransform,
                            (employedPlaces,countEmployedPlaces)
                            );
                        
                        if(employedPlaces > 0)
                        {
                            var signsoBtn = proceduralUiElements.CreateButton(
                                parent: uiData.quitLayout.rectTransform,
                                "Quit"
                                );
                            SetEventListener(signsoBtn, buildingWorker.Worker, buildingWorker.WorkerType);
                            //--------------
                            uiData.AddEmployeesQuitButton(signsoBtn, signsoBtn.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>());
                            
                        }
                        //----------
                        uiData.AddEmployeesApplyButton(btn,btn.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>());
                        uiData.AddEmployeesCountButton(btn,countLabel);
                    }
                }
            }
        }
        
        private void SetProjectsButton()
        {
            if(building.GetType() != typeof(TarentTown))
            {
                foreach (var project in building.possibleProjects)
                {
                    if (project != null)
                    {
                        var btnName = project.customerType.ToString() + "(" + project.GetHashCode() + ")";
                        
                        if (!uiData.Contains(btnName))
                        {
                            var materialInstance = GetCopyOfMaterial(projectButtonMaterial);
                            var btn = proceduralUiElements.CreateButton
                            (
                                uiData.projectsLayout.rectTransform,
                                btnName,
                                materialInstance
                            );
                            SetEventListener(btn,project,materialInstance);
                            
                            uiData.AddProjectApplyButton(btn, btn.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>());
                        }
                    }
                }
            }
            else
            {
                var allProjects = building.Company.GetAllProjects;
                for (int i = 0; i < allProjects.Count; i++)
                {
                    if (allProjects[i] != null)
                    {
                        var btnName = allProjects[i].customerType.ToString() + "(" + allProjects[i].GetHashCode() + ")";
                        
                        if (!uiData.Contains(btnName))
                        {
                            var btn = proceduralUiElements.CreateButton
                            (
                                uiData.projectsLayout.rectTransform,
                                btnName
                            );
                            btn.interactable = false;
                            //----------
                            uiData.AddProjectApplyButton(btn, btn.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>());
                        }
                    }
                }
            }
        }

        private void SetEventListener(Button btn, EntityType workerType)
        {
            btn.onClick.AddListener(() =>
            {
                var (employedPlaces, countEmployedPlaces) = building.BuildingData.GetCountOfEmployed(workerType);
                if(employedPlaces < countEmployedPlaces)
                {
                    this.applyEmployeeEvent(workerType.ToString());
                    UpdateEmployeeWorkers(uiData.GetEmployeesCountLabel(workerType.ToString()),  workerType);
                    
                    windowUpdateEvent();

                }
            });
        }
        private void SetEventListener(Button btn, Employee employee, EntityType workerType)
        {
            btn.onClick.AddListener(() =>
            {
                var (employedPlaces, countEmployedPlaces) = building.BuildingData.GetCountOfEmployed(workerType);
                if(employedPlaces > 0)
                {
                    quitEmployeeEvent(employee);
                    UpdateEmployeeWorkers(uiData.GetEmployeesCountLabel(workerType.ToString()), workerType);
                   
                    windowUpdateEvent();
                }
            });
        }
        
        private void SetEventListener(Button btn, Project project,Material material)
        {
            if(building.BuildingData.workers > 0)
            {
                monoBehaviour.StartCoroutine(routine: ShowProgressbarProcess(material: material));
                monoBehaviour.StartCoroutine(routine: UpdateTextLabelButton(btn: btn, project: project));
                btn.onClick.AddListener(call: () =>
                {
                    startProject(project);
                    monoBehaviour.StartCoroutine(routine: ButtonLifeTime(btn: btn, project: project));
                });
            }
        }
        private Material GetCopyOfMaterial(Material original)
        {
            Material materialCopy = new Material(original.shader);
            materialCopy.CopyPropertiesFromMaterial(original);

            return materialCopy;
        }
        private void UpdateEmployeeWorkers(TextMeshProUGUI employeeWorkersLabel, EntityType entityType)
        {
            var (employedPlaces, countEmployedPlaces) = building.BuildingData.GetCountOfEmployed(entityType);
            employeeWorkersLabel.SetText(employedPlaces + " / " + countEmployedPlaces);
        }

        private IEnumerator ShowProgressbarProcess(Material material)
        {
            material.SetFloat("_Width" , 1f);
            var step = 1f;
            
            while (building.StartupProject != null && step >= 0f)
            {
                step = 1 - (building.StartupProject.percentprocessBar / 100f);
                
                material.SetFloat("_Width" , step);
                yield return null;
            }
        }

        private IEnumerator UpdateTextLabelButton(Button btn, Project project)
        {
            while (building.StartupProject != null &&  !btn.Equals(null))
            {
                if (project.percentprocessBar > 0)
                {
                    uiData.GetProjectApplyButtonLabel(btn.name)?.SetText(project.percentprocessBar.ToString());
                }
                yield return null;
            }
        }

        private IEnumerator ButtonLifeTime(Button btn, Project project)
        {
            MakeButtonsProjectInactive(false);
            while (building.StartupProject != null)
            {
                if(!btn.Equals(null))
                {
                    //TODO : if Project was Started than Button should be not interactable !!!!
                    btn.interactable = false;
                }
                yield return null;
            }
            if (!btn.Equals(null))
            {
                building.possibleProjects.Remove(project);
                container.Companies[0].RemoveProject(project);

                var gameObject = btn.gameObject;
                Object.Destroy(btn);
                Object.Destroy(gameObject);
            }
        }

        private void MakeButtonsProjectInactive(bool interactable)
        {
            foreach (var btn in uiData.ProjectApplyButtons)
            {
                btn.interactable = interactable;
            }
        }

        ~BuildingContent()
        {
            signalBus.TryUnsubscribe<MonoBehaviourSignal>(GetMonoBehaviour);
        }
    }
}