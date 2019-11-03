using System.Collections;
using System.Collections.Generic;
using BootManager;
using BuildingPackage;
using Enums;
using Human;
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
        private Dictionary<Button, TextMeshProUGUI> buildingContentBtn;
        private Material projectButtonMaterial ;
        public BuildingContent(Building building, GameObject buildingContent)
        {
            this.building = building;
            this.buildingContent = buildingContent;
            uiElements = new UiElements();
            projectButtonMaterial = Resources.Load<Material>("Materials/progressBar");
        }
        
        public void CreateBuildingContent(ref Dictionary<Button,TextMeshProUGUI> buildContentElements)
        {
            buildingContentBtn = buildContentElements;

            SetEmployeesButton();

            var materialInstance = GetCopyOfMaterial(projectButtonMaterial);
            
            SetProjectsButton(materialInstance);
            Boot.monobehaviour.StartCoroutine(ShowProgressbarProcess(materialInstance));
        }

        private void SetEmployeesButton()
        {
            var index = 1;
            foreach (var VARIABLE in building.BuildingData.AccessibleWorker)
            {
                if (VARIABLE != null)
                {
                    if (!ContainsIn(buildingContentBtn.Keys,VARIABLE.WorkerType))
                    {
                        var btn = uiElements.CreateButton(
                            buildingContent.GetComponent<RectTransform>(),
                            VARIABLE.WorkerType.ToString(),
                            index,
                            AnchorType.TOP_LEFT,
                            Column.FIRST);

                        SetEventListener(btn, VARIABLE.WorkerType);
                        
                        var (employedPlaces, countEmployedPlaces) = building.BuildingData.GetCountOfEmployedWorkers(VARIABLE.WorkerType);
                        var countLabel = uiElements.GenerateCountOfEmployedWorker(
                            buildingContent.GetComponent<RectTransform>(),
                            index,
                            (employedPlaces,countEmployedPlaces),
                            AnchorType.TOP_LEFT);
                        
                        if(employedPlaces > 0)
                        {
                            var quitBtn = uiElements.CreateButton(
                                buildingContent.GetComponent<RectTransform>(),
                                "Quit",
                                index,
                                AnchorType.TOP_LEFT,
                                Column.THIRD);
                            SetEventListener(quitBtn, VARIABLE.Worker, VARIABLE.WorkerType);
                            buildingContentBtn.Add(quitBtn, quitBtn.transform.GetChild(0).GetComponent<TextMeshProUGUI>());
                        }
                        
                        buildingContentBtn.Add(btn,countLabel);
                                                
                        index++;
                    }
                }
            }
        }
        
        private void SetProjectsButton(Material material)
        {
            var index = 1;
            foreach (var project in building.possibleProjects)
            {
                if (project != null)
                {
                    var btnName = project.customerType.ToString() + "(" + index + ")";
                    
                    if (!ContainsIn(buildingContentBtn.Keys, btnName))
                    {
                        var btn = uiElements.CreateButton
                        (
                            buildingContent.GetComponent<RectTransform>(),
                            btnName,
                            index,
                            AnchorType.TOP_LEFT,
                            Column.FOURTH,
                            material
                        );
                        SetEventListener(btn,project);
                        buildingContentBtn.Add(btn, btn.transform.GetChild(0).GetComponent<TextMeshProUGUI>());
                        index++;
                    }
                }
            }
        }

        private void SetEventListener(Button btn, EntityType workerType)
        {
            btn.onClick.AddListener(() =>
            {
                var (employedPlaces, countEmployedPlaces) = building.BuildingData.GetCountOfEmployedWorkers(workerType);
                if(employedPlaces < countEmployedPlaces)
                {
                    UIDispatcher.uiDispatcher.ApplyWorker(workerType.ToString());
                    UpdateEmployeeWorkers(buildingContentBtn[btn], workerType);
                }
            });
        }
        private void SetEventListener(Button btn, Employee employee, EntityType workerType)
        {
            btn.onClick.AddListener(() =>
            {
                var (employedPlaces, countEmployedPlaces) = building.BuildingData.GetCountOfEmployedWorkers(workerType);
                if(employedPlaces > 0)
                {
                    building.QuitWorker(employee);
                    UpdateEmployeeWorkers(buildingContentBtn[btn], workerType);
                }
            });
        }
        
        private void SetEventListener(Button btn, Project project)
        {
            btn.onClick.AddListener(() =>
            {

                building.ApplyProject(project);
                Boot.monobehaviour.StartCoroutine(ButtonLifeTime(btn, project));
            });
        }
        
        private bool ContainsIn(Dictionary<Button, TextMeshProUGUI>.KeyCollection buttons, EntityType workerType)
        {
            foreach (var btns in buttons)
            {
                if (btns != null && btns.name == workerType.ToString())
                {
                    return true;
                }
            }
            return false;
        }
        
        private bool ContainsIn(Dictionary<Button, TextMeshProUGUI>.KeyCollection buttons, string btnName)
        {
            foreach (var btns in buttons)
            {
                if (btns != null && btns.name == btnName)
                {
                    return true;
                }
            }
            return false;
        }

        private Material GetCopyOfMaterial(Material original)
        {
            Material materialCopy = new Material(original.shader);
            materialCopy.CopyPropertiesFromMaterial(original);

            return materialCopy;
        }
        private void UpdateEmployeeWorkers(TextMeshProUGUI employeeWorkersLabel, EntityType entityType)
        {
            var (employedPlaces, countEmployedPlaces) = building.BuildingData.GetCountOfEmployedWorkers(entityType);
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

        private IEnumerator ButtonLifeTime(Button btn, Project project)
        {
            while (building.Project != null)
            {
                yield return null;
            }
            building.possibleProjects.Remove(project);
            Boot.container.Companies[0].RemoveProject(project);

            var gameObject = btn.gameObject;
            Object.Destroy(btn);
            Object.Destroy(gameObject);
        }
        
    }
}