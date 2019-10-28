using System.Collections.Generic;
using System.Linq;
using BootManager;
using BuildingPackage;
using Enums;
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

        public BuildingContent(Building building, GameObject buildingContent)
        {
            this.building = building;
            this.buildingContent = buildingContent;
            uiElements = new UiElements();
        }
        
        public void CreateBuildingContent(ref Dictionary<Button,TextMeshProUGUI> buildContentElements)
        {
            buildingContentBtn = buildContentElements;

            SetEmployeesButton();
            SetProjectsButton();
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
                            AnchorType.TOP_LEFT);

                        SetEventListener(btn, VARIABLE.WorkerType);
                        
                        var (employedPlaces, countEmployedPlaces) = building.BuildingData.GetCountOfEmployedWorkers(VARIABLE.WorkerType);
                        var countLabel = uiElements.GenerateCountOfEmployedWorker(
                            buildingContent.GetComponent<RectTransform>(),
                            index,
                            (employedPlaces,countEmployedPlaces),
                            AnchorType.TOP_LEFT);
                        
                        buildingContentBtn.Add(btn,countLabel);
                                                
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
                    if (!ContainsIn(buildingContentBtn.Keys, project.clientType))
                    {
                        var btn = uiElements.CreateButton(
                            buildingContent.GetComponent<RectTransform>(),
                            project.clientType.ToString(),
                            index,
                            AnchorType.TOP_RIGHT
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
        
        private void SetEventListener(Button btn, Project project)
        {
            btn.onClick.AddListener(() =>
            {
                building.ApplyProject(project);
                
                building.possibleProjects.Remove(project);
                Boot.container.Firmas[0].RemoveProject(project);                
                
                Object.Destroy(btn);
            });
        }
        
        private bool ContainsIn(Dictionary<Button, TextMeshProUGUI>.KeyCollection buttons, EntityType workerType)
        {
            return buttons.Any(VARIABLE => VARIABLE.name.Contains(workerType.ToString()));
        }
        
        private bool ContainsIn(Dictionary<Button, TextMeshProUGUI>.KeyCollection buttons, ClientType clientType)
        {
            return buttons.Any(VARIABLE => VARIABLE.name.Contains(clientType.ToString()));
        }

        private void UpdateEmployeeWorkers(TextMeshProUGUI employeeWorkersLabel, EntityType entityType)
        {
            var (employedPlaces, countEmployedPlaces) = building.BuildingData.GetCountOfEmployedWorkers(entityType);
            employeeWorkersLabel.SetText(employedPlaces + " / " + countEmployedPlaces);
        }
        
    }
}