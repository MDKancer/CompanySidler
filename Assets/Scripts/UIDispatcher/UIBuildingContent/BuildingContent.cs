using System.Collections;
using BootManager;
using BuildingPackage;
using Enums;
using Human;
using PlayerView;
using ProjectPackage;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace UIPackage.UIBuildingContent
{
    public class BuildingContent
    {
        private readonly UiElements uiElements;        
        private readonly Building building;
        private readonly GameObject buildingContent;
        private readonly RectTransform buildingContentRectTransform;
        private readonly Material projectButtonMaterial ;
        private UIData uiData;
        public BuildingContent(Building building, GameObject buildingContent)
        {
            this.building = building;
            this.buildingContent = buildingContent;
            buildingContentRectTransform = buildingContent.GetComponent<RectTransform>();
            
            uiElements = new UiElements();
            uiData = new UIData();
            
            foreach (var material in Boot.container.Materials)
            {
                if (material.name == "progressBar")
                {
                    projectButtonMaterial = material;
                }
            }
        }
        
        public void CreateBuildingContent(ref UIData uiData)
        {
            this.uiData = uiData;
            SetEmployeesButton();

            SetProjectsButton();
            
        }

        private void SetEmployeesButton()
        {
            var index = 1;
            foreach (var VARIABLE in building.BuildingData.AvailableWorker)
            {
                if (VARIABLE != null)
                {
                    if (!uiData.Contains(VARIABLE.WorkerType.ToString()))
                    {
                        var btn = uiElements.CreateButton(
                            buildingContentRectTransform,
                            VARIABLE.WorkerType.ToString(),
                            index,
                            AnchorType.TOP_LEFT,
                            Column.FIRST);

                        SetEventListener(btn, VARIABLE.WorkerType);
                        
                        var (employedPlaces, countEmployedPlaces) = building.BuildingData.GetCountOfEmployed(VARIABLE.WorkerType);
                        var countLabel = uiElements.GenerateCountOfEmployedWorker(
                            buildingContentRectTransform,
                            index,
                            (employedPlaces,countEmployedPlaces),
                            AnchorType.TOP_LEFT);
                        
                        if(employedPlaces > 0)
                        {
                            var quitBtn = uiElements.CreateButton(
                                buildingContentRectTransform,
                                "Quit",
                                index,
                                AnchorType.TOP_LEFT,
                                Column.THIRD);
                            SetEventListener(quitBtn, VARIABLE.Worker, VARIABLE.WorkerType);
                            //--------------
                            uiData.AddEmployeesQuitButton(quitBtn, quitBtn.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>());
                            
                        }
                        //----------
                        uiData.AddEmployeesApplyButton(btn,btn.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>());
                        uiData.AddEmployeesCountButton(btn,countLabel);
                                                
                        index++;
                    }
                }
            }
        }
        
        private void SetProjectsButton()
        {
            var index = 1;
            foreach (var project in building.possibleProjects)
            {
                if (project != null)
                {
                    var btnName = project.customerType.ToString() + "(" + index + ")";
                    
                    if (!uiData.Contains(btnName))
                    {
                        var materialInstance = GetCopyOfMaterial(projectButtonMaterial);
                        var btn = uiElements.CreateButton
                        (
                            buildingContentRectTransform,
                            btnName,
                            index,
                            AnchorType.TOP_LEFT,
                            Column.FOURTH,
                            materialInstance
                        );
                        SetEventListener(btn,project,materialInstance);
                        
                        //----------
                        uiData.AddProjectApplyButton(btn, btn.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>());
                        index++;
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
                    PlayerViewController.playerViewController.ApplyWorker(workerType.ToString());
                    UpdateEmployeeWorkers(uiData.GetEmployeesCountLabel(workerType.ToString()),  workerType);
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
                    building.QuitWorker(employee);
                    UpdateEmployeeWorkers(uiData.GetEmployeesCountLabel(workerType.ToString()), workerType);
                }
            });
        }
        
        private void SetEventListener(Button btn, Project project,Material material)
        {
            if(building.BuildingData.workers > 0)
            {
                Boot.monobehaviour.StartCoroutine(routine: ShowProgressbarProcess(material: material));
                Boot.monobehaviour.StartCoroutine(routine: UpdateTextLabelButton(btn: btn, project: project));
                btn.onClick.AddListener(call: () =>
                {
                    building.ApplyProject(newProject: project);
                    Boot.monobehaviour.StartCoroutine(routine: ButtonLifeTime(btn: btn, project: project));
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
            
            while (building.Project != null && step >= 0f)
            {
                step = 1 - (building.Project.percentprocessBar / 100f);
                
                material.SetFloat("_Width" , step);
                yield return null;
            }
        }

        private IEnumerator UpdateTextLabelButton(Button btn, Project project)
        {
            while (building.Project != null &&  !btn.Equals(null))
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
            SetAllProjectButtonsInteractable(false);
            while (building.Project != null)
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
                Boot.container.Companies[0].RemoveProject(project);

                var gameObject = btn.gameObject;
                Object.Destroy(btn);
                Object.Destroy(gameObject);
            }
        }

        private void SetAllProjectButtonsInteractable(bool interactable)
        {
            foreach (var btn in uiData.ProjectApplyButtons)
            {
                btn.interactable = interactable;
            }
        }
        
    }
}