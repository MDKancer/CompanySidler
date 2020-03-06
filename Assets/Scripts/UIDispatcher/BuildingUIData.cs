using System.Collections;
using BuildingPackage;
using Enums;
using GameCloud;
using Human;
using ProjectPackage;
using TMPro;
using UIPackage.UIBuildingContent;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Zenject_Signals;

namespace PlayerView
{
    public class BuildingUIData<T> where T : Building
    {
        private T building;
        private SignalBus signalBus;
        private UIData uiData;
        private ProceduralUiElements proceduralUiElements;
        private Container container;
        private MonoBehaviour monoBehaviour;
        private Material projectButtonMaterial;
        
        public BuildingUIData(SignalBus signalBus,Container container,ref UIData uiData, Building office)
        {
            this.signalBus = signalBus;
            this.container = container;
            this.uiData = uiData;
            this.proceduralUiElements = new ProceduralUiElements();
            this.building = office as T;
            
            container.Materials.ForEach(material =>
            {
                if (material.name == "progressBar") projectButtonMaterial = material;
            } );
            this.signalBus.Subscribe<MonoBehaviourSignal>(GetMonoBehaviour);
        }
        public void SetBuildingInteractions()
        {
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
                     signalBus.Fire(new ApplyEmployeeSignal
                     {
                         employeeType = workerType
                     });
                     UpdateEmployeeWorkers(uiData.GetEmployeesCountLabel(workerType.ToString()),  workerType);
                    
                     signalBus.Fire(new UpdateUIWindow());
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
                     signalBus.Fire(new StartProjectSignal
                     {
                         project = project
                     });
                     monoBehaviour.StartCoroutine(routine: ButtonLifeTime(btn: btn, project: project));
                     
                 });
             }
         }
         private void SetEventListener(Button btn, Employee employee, EntityType workerType)
         {
             btn.onClick.AddListener(() =>
             {
                 var (employedPlaces, countEmployedPlaces) = building.BuildingData.GetCountOfEmployed(workerType);
                 if(employedPlaces > 0)
                 {
                     signalBus.Fire(new QuitEmployeeSignal
                     {
                         employee = employee
                     });
                     UpdateEmployeeWorkers(uiData.GetEmployeesCountLabel(workerType.ToString()), workerType);
                     
                     signalBus.Fire(new UpdateUIWindow());
                 }
             });
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
             // when the startup project is finished and his button exist
             // dann remove this project and his button 
             if (!btn.Equals(null))
             {
                 signalBus.Fire(new CloseProjectSignal
                 {
                     project = project
                 });

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
         private Material GetCopyOfMaterial(Material original)
         {
             Material materialCopy = new Material(original.shader);
             materialCopy.CopyPropertiesFromMaterial(original);

             return materialCopy;
         }
        private void GetMonoBehaviour(MonoBehaviourSignal monoBehaviourSignal)
        {
            monoBehaviour = monoBehaviourSignal.monoBehaviour;
        }
        
                
        ~BuildingUIData()
        {
            this.signalBus.TryUnsubscribe<MonoBehaviourSignal>(GetMonoBehaviour);
        }
    }
}