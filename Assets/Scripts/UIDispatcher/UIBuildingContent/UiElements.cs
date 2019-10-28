using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UIPackage.UIBuildingContent
{
    internal enum AnchorType
    {
        TOP_LEFT = 0,   
        TOP_RIGHT = 1,   
        TOP = 2,   
        BOTTOM_LEFT = 3,   
        BOTTOM_RIGHT = 4,
        BOTTOM = 5,
        CENTER = 6,
        LEFT = 7,
        RIGHT = 8
    }
    internal class UiElements
    {
        internal Button CreateButton(RectTransform parent, string name, int index,AnchorType anchorType)
        {
            // __________________Button____________________
            // || || || || || || || || || || || || || || ||
            // \/ \/ \/ \/ \/ \/ \/ \/ \/ \/ \/ \/ \/ \/ \/
            var emptyObject = new GameObject(name);
            emptyObject.AddComponent<RectTransform>();
            emptyObject.AddComponent<CanvasRenderer>();
            emptyObject.AddComponent<Image>();
            var btn = emptyObject.AddComponent<Button>();
            
            
            var rectTransform = btn.GetComponent<RectTransform>();
            rectTransform.SetParent(parent.GetComponent<RectTransform>());

            var anchor = GetAnchor(anchorType);
            rectTransform.anchorMin = anchor.anchorMin;
            rectTransform.anchorMax = anchor.anchorMax;
            rectTransform.pivot = anchor.pivot;
            rectTransform.sizeDelta = new Vector2(100f, 30f);

            index *= -1;
            var x = anchor.anchorMin.x < 0.5f ? 50f : -50f;
            emptyObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, 35f * index);
            GenerateBtnLabel(rectTransform, name);
            return btn;
        }
        
        private void GenerateBtnLabel(RectTransform btnRectTransform, string name)
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

            var anchor = GetAnchor(AnchorType.CENTER);
            btnRectTransform.anchorMin = anchor.anchorMin;
            btnRectTransform.anchorMax = anchor.anchorMax;
            btnRectTransform.pivot = anchor.pivot;
            btnRectTransform.sizeDelta = new Vector2(100f, 30f);
            btnRectTransform.anchoredPosition = Vector2.zero;

            btnText.SetText(name);
            
        }
        
        internal TextMeshProUGUI GenerateCountOfEmployedWorker(RectTransform parent , int index, (int employedPlaces,int countEmployedPlaces) countOfEmployedWorker,AnchorType anchorType)
        {
            // __________________Count-Label_______________
            // || || || || || || || || || || || || || || ||
            // \/ \/ \/ \/ \/ \/ \/ \/ \/ \/ \/ \/ \/ \/ \/
            var emptyObject = new GameObject();
            var countLabel = emptyObject.AddComponent<TextMeshProUGUI>();
            emptyObject.GetComponent<RectTransform>().SetParent(parent);
            countLabel.fontSize = 12f;
            countLabel.color = Color.white;
            countLabel.alignment = TextAlignmentOptions.Midline;
            var rectTransform = countLabel.rectTransform;

            var anchor = GetAnchor(anchorType);
            rectTransform.anchorMin = anchor.anchorMin;
            rectTransform.anchorMax = anchor.anchorMax;
            rectTransform.pivot = anchor.pivot;
            rectTransform.sizeDelta = new Vector2(100f, 30f);
            index *= -1;
            rectTransform.anchoredPosition = new Vector2(rectTransform.sizeDelta.x + 50f, 35f * index);

            countLabel.SetText(countOfEmployedWorker.employedPlaces + " / "+countOfEmployedWorker.countEmployedPlaces);

            return countLabel;
        }
        
        private (Vector2 anchorMin, Vector2 anchorMax, Vector2 pivot) GetAnchor(AnchorType anchorType)
        {
            switch (anchorType)
            {
                case AnchorType.TOP:
                    return (new Vector2(0.5f, 1f), new Vector2(0.5f, 1f), new Vector2(0.5f, 1f));
                case AnchorType.TOP_LEFT:
                    return (new Vector2(0f, 1f), new Vector2(0f, 1f), new Vector2(0f, 1f));
                case AnchorType.TOP_RIGHT:
                    return (new Vector2(1f, 1f), new Vector2(1f, 1f), new Vector2(1f, 1f));
                case AnchorType.CENTER:
                    return (new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f));
                case AnchorType.LEFT:
                    return (new Vector2(0f, 0.5f), new Vector2(0f, 0.5f), new Vector2(0f, 0.5f));
                case AnchorType.RIGHT:
                    return (new Vector2(1f, 0.5f),new Vector2(1f, 0.5f),new Vector2(1f, 0.5f));
                case AnchorType.BOTTOM_LEFT:
                    return (new Vector2(0f, 0f), new Vector2(0f, 0f), new Vector2(0f, 0f));
                case AnchorType.BOTTOM_RIGHT:
                    return (new Vector2(1f, 0f), new Vector2(1f, 0f), new Vector2(1f, 0f));
                case AnchorType.BOTTOM:
                    return (new Vector2(0.5f, 0f), new Vector2(0.5f, 0f), new Vector2(0.5f, 0f));
                default:
                    return (new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f));
            }
        }
        
    }
}