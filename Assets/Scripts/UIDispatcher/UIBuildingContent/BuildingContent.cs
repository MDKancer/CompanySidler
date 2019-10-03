using System.Collections.Generic;
using BuildingPackage;
using Constants;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UIPackage.UIBuildingContent
{
    public class BuildingContent
    {
        private Building building;
        private GameObject buildingContent;

        public BuildingContent(Building building, GameObject buildingContent)
        {
            this.building = building;
            this.buildingContent = buildingContent;
        }
        
        public void CreateBuildingContent(ref List<GameObject> buildContentBtns)
        {
            int index = 0;
            foreach (var VARIABLE in building.BuildingData.accesibleWorker)
            {
                if (!ifContains(buildContentBtns,VARIABLE.WorkerType))
                {
                    GameObject objekt = new GameObject(VARIABLE.WorkerType.ToString());
                    objekt.AddComponent<RectTransform>();
                    objekt.AddComponent<CanvasRenderer>();
                    objekt.AddComponent<Image>();
                    Button btn = objekt.AddComponent<Button>();
                    
                    SetEventListener(btn, VARIABLE.WorkerType);
                    
                    var rectTransform = btn.GetComponent<RectTransform>();
                    rectTransform.SetParent(buildingContent.GetComponent<RectTransform>());

                    rectTransform.anchorMin = new Vector2(0, 0f);
                    rectTransform.anchorMax = new Vector2(0, 0f);
                    rectTransform.pivot = new Vector2(0, 0f);
                    rectTransform.sizeDelta = new Vector2(100f, 30f);

                    objekt.GetComponent<RectTransform>().anchoredPosition = new Vector2(10f, 35f * index);

                    buildContentBtns.Add(objekt);

                    objekt = new GameObject();
                    TextMeshProUGUI btnText = objekt.AddComponent<TextMeshProUGUI>();
                    objekt.GetComponent<RectTransform>().SetParent(rectTransform);
                    btnText.fontSize = 12f;
                    btnText.color = Color.black;
                    btnText.alignment = TextAlignmentOptions.Midline;
                    rectTransform = btnText.rectTransform;

                    rectTransform.anchorMin = new Vector2(0, 0f);
                    rectTransform.anchorMax = new Vector2(0, 0f);
                    rectTransform.pivot = new Vector2(0, 0f);
                    rectTransform.sizeDelta = new Vector2(100f, 30f);
                    rectTransform.anchoredPosition = Vector2.zero;

                    btnText.SetText(VARIABLE.WorkerType.ToString());
                    index++;
                }
            }
        }

        private void SetEventListener(Button btn, EntityType workerType)
        {
            btn.onClick.AddListener((() => UIDispatcher.uiDispatcher.ApplyWorker(workerType.ToString())));
        }
        private bool ifContains(List<GameObject> btns, EntityType workerType)
        {
            foreach (var VARIABLE in btns)
            {
                if (VARIABLE.name.Contains(workerType.ToString()))
                {
                    return true;
                }
            }

            return false;
        }
    }
}