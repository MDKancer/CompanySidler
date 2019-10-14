using System.Collections.Generic;
using System.Linq;
using BuildingPackage;
using Constants;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UIPackage.UIBuildingContent
{
    public class BuildingContent
    {
        private readonly Building building;
        private readonly GameObject buildingContent;
        private Dictionary<Button, TextMeshProUGUI> buildingContentBtn;

        public BuildingContent(Building building, GameObject buildingContent)
        {
            this.building = building;
            this.buildingContent = buildingContent;
        }
        
        public void CreateBuildingContent(ref Dictionary<Button,TextMeshProUGUI> buildContentElements)
        {
            buildingContentBtn = buildContentElements;
            var index = -1;
            foreach (var VARIABLE in building.BuildingData.AccessibleWorker)
            {
                if (!ContainsIn(buildContentElements.Keys,VARIABLE.WorkerType))
                {
                    GenerateButton(VARIABLE.WorkerType,index);
                    index--;
                }
            }
        }

        private void GenerateButton(EntityType workerType, int index)
        {
            // __________________Button____________________
            // || || || || || || || || || || || || || || ||
            // \/ \/ \/ \/ \/ \/ \/ \/ \/ \/ \/ \/ \/ \/ \/
            var emptyObject = new GameObject(workerType.ToString());
            emptyObject.AddComponent<RectTransform>();
            emptyObject.AddComponent<CanvasRenderer>();
            emptyObject.AddComponent<Image>();
            var btn = emptyObject.AddComponent<Button>();
                    
            SetEventListener(btn, workerType);
                    
            var rectTransform = btn.GetComponent<RectTransform>();
            rectTransform.SetParent(buildingContent.GetComponent<RectTransform>());

            rectTransform.anchorMin = new Vector2(0, 1f);
            rectTransform.anchorMax = new Vector2(0, 1f);
            rectTransform.pivot = new Vector2(0, 1f);
            rectTransform.sizeDelta = new Vector2(100f, 30f);
                    
            emptyObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(10f, 35f * index);
            
            GenerateBtnLabel(rectTransform,workerType);
            GenerateCountOfEmployedWorker(btn, index, workerType);
        }

        private void GenerateBtnLabel(RectTransform btnRectTransform, EntityType workerType)
        {
            // __________________Label_____________________
            // || || || || || || || || || || || || || || ||
            // \/ \/ \/ \/ \/ \/ \/ \/ \/ \/ \/ \/ \/ \/ \/
            var emptyObject = new GameObject();
                    
            var btnText = emptyObject.AddComponent<TextMeshProUGUI>();
            emptyObject.GetComponent<RectTransform>().SetParent(btnRectTransform);
            btnText.fontSize = 12f;
            btnText.color = Color.black;
            btnText.alignment = TextAlignmentOptions.Midline;
            btnRectTransform = btnText.rectTransform;

            btnRectTransform.anchorMin = new Vector2(0, 0f);
            btnRectTransform.anchorMax = new Vector2(0, 0f);
            btnRectTransform.pivot = new Vector2(0, 0f);
            btnRectTransform.sizeDelta = new Vector2(100f, 30f);
            btnRectTransform.anchoredPosition = Vector2.zero;

            btnText.SetText(workerType.ToString());
        }

        private void GenerateCountOfEmployedWorker(Button referenceBtn, int index, EntityType workerType)
        {
            // __________________Count-Label_______________
            // || || || || || || || || || || || || || || ||
            // \/ \/ \/ \/ \/ \/ \/ \/ \/ \/ \/ \/ \/ \/ \/
            var emptyObject = new GameObject();
            var countLabel = emptyObject.AddComponent<TextMeshProUGUI>();
            emptyObject.GetComponent<RectTransform>().SetParent(buildingContent.GetComponent<RectTransform>());
            countLabel.fontSize = 12f;
            countLabel.color = Color.white;
            countLabel.alignment = TextAlignmentOptions.Midline;
            var rectTransform = countLabel.rectTransform;

            rectTransform.anchorMin = new Vector2(0, 1f);
            rectTransform.anchorMax = new Vector2(0, 1f);
            rectTransform.pivot = new Vector2(0, 1f);
            rectTransform.sizeDelta = new Vector2(100f, 30f);
            rectTransform.anchoredPosition = new Vector2(rectTransform.sizeDelta.x + 10f, 35f * index);

            var (employedPlaces, countEmployedPlaces) = building.BuildingData.GetCountOfEmployedWorkers(workerType);
            countLabel.SetText(employedPlaces + " / "+countEmployedPlaces);
                    
            buildingContentBtn.Add(referenceBtn,countLabel);
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
        private bool ContainsIn(Dictionary<Button, TextMeshProUGUI>.KeyCollection buttons, EntityType workerType)
        {
            return buttons.Any(VARIABLE => VARIABLE.name.Contains(workerType.ToString()));
        }

        private void UpdateEmployeeWorkers(TextMeshProUGUI employeeWorkersLabel, EntityType entityType)
        {
            var (employedPlaces, countEmployedPlaces) = building.BuildingData.GetCountOfEmployedWorkers(entityType);
            employeeWorkersLabel.SetText(employedPlaces + " / " + countEmployedPlaces);
        }
        
    }
}