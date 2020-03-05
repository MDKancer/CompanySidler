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

    internal enum Column
    {
        FIRST = 0,
        SECOND = 1,
        THIRD = 2,
        FOURTH = 3,
        FIVETH = 4
        
    }
    public class ProceduralUiElements
    {
        public TextMeshProUGUI GetCanvas(string text)
        {
            Canvas canvas = GameObject.Find("PlayerView").GetComponent<Canvas>();
            
            var emptyObject = new GameObject();
            var countLabel = emptyObject.AddComponent<TextMeshProUGUI>();
            emptyObject.GetComponent<RectTransform>().SetParent(canvas.GetComponent<RectTransform>());
            countLabel.fontSize = 12f;
            countLabel.color = Color.white;
            countLabel.alignment = TextAlignmentOptions.Midline;
            var rectTransform = countLabel.rectTransform;

            var anchor = GetAnchor(AnchorType.CENTER);
            rectTransform.anchorMin = anchor.anchorMin;
            rectTransform.anchorMax = anchor.anchorMax;
            rectTransform.pivot = anchor.pivot;
            rectTransform.sizeDelta = new Vector2(100f, 30f);
            rectTransform.anchoredPosition = new Vector2(0, 0);

            rectTransform.localScale = Vector3.one/10f;
            countLabel.SetText(text);
            
            return countLabel;
        }

        internal Button CreateButton(RectTransform parent, string name, params Material[] material)
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

            rectTransform.sizeDelta = new Vector2(100f, 30f);
            
            if(material.Length > 0)
            {
                var imageComponent = btn.gameObject.GetComponent<Image>();
                imageComponent.material = material[0];
            }
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
        
        internal TextMeshProUGUI GenerateCountOfEmployedWorker(RectTransform parent ,(int employedPlaces,int countEmployedPlaces) countOfEmployedWorker)
        {
            // __________________Count-Label_______________
            // || || || || || || || || || || || || || || ||
            // \/ \/ \/ \/ \/ \/ \/ \/ \/ \/ \/ \/ \/ \/ \/
            var emptyObject = new GameObject();
            var countLabel = emptyObject.AddComponent<TextMeshProUGUI>();
            countLabel.rectTransform.sizeDelta = new Vector2(100f, 30f);
            emptyObject.GetComponent<RectTransform>().SetParent(parent);
            countLabel.fontSize = 12f;
            countLabel.color = Color.white;
            countLabel.alignment = TextAlignmentOptions.Midline;

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

        private float GetPosition(Vector2 size ,Column column)
        {
            switch (column)
            {
                case Column.FIRST:
                    return 10f;
                case Column.SECOND:
                    return size.x + 10f;
                case Column.THIRD:
                    return size.x * 2 + 10f;
                case Column.FOURTH:
                    return size.x * 3 + 10f*2;
                case Column.FIVETH:
                    return size.x * 4 + 10f*3;
                default:
                    return 0f;
            }
        }
        private void SetCanvas()
        {
            // __________________World-Space-Canvas______________
            // || || || || || || || || || || || || || || || || ||
            // \/ \/ \/ \/ \/ \/ \/ \/ \/ \/ \/ \/ \/ \/ \/ \/ \/
            GameObject canvasObject = new GameObject();
            Canvas canvas = canvasObject.AddComponent<Canvas>();
            CanvasScaler canvasScaler = canvasObject.AddComponent<CanvasScaler>();
            GraphicRaycaster graphicRaycaster = canvasObject.AddComponent<GraphicRaycaster>();

            canvas.renderMode = RenderMode.WorldSpace;
            canvas.targetDisplay = 1;
            canvas.worldCamera = Camera.main;
            canvas.GetComponent<RectTransform>().sizeDelta = Vector2.one;

            canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ConstantPixelSize;
            canvasScaler.scaleFactor = 1;
            canvasScaler.referencePixelsPerUnit = 100;

            graphicRaycaster.ignoreReversedGraphics = true;
            graphicRaycaster.blockingObjects = GraphicRaycaster.BlockingObjects.None;
        }
        
    }
}