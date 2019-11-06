using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UIPackage.UIBuildingContent
{
    public class UIData
    {
        private readonly List<Dictionary<Button, TextMeshProUGUI>> allUIElements;
        private readonly Dictionary<Button, TextMeshProUGUI> employeesApply;
        private readonly Dictionary<Button, TextMeshProUGUI> employeesCount;
        private readonly Dictionary<Button, TextMeshProUGUI> employeesQuit;
        private readonly Dictionary<Button, TextMeshProUGUI> projectsApply;
        //private List<Material> projectButtonMaterials;

        public UIData()
        {
            employeesApply = new Dictionary<Button, TextMeshProUGUI>();
            employeesCount = new Dictionary<Button, TextMeshProUGUI>();
            employeesQuit = new Dictionary<Button, TextMeshProUGUI>();
            projectsApply = new Dictionary<Button, TextMeshProUGUI>();
            //projectButtonMaterials = new List<Material>();
            
            allUIElements = new List<Dictionary<Button, TextMeshProUGUI>>
            {
                employeesApply,
                employeesCount,
                employeesQuit,
                projectsApply
            };

        }

//        public List<Button> AllUiElements
//        {
//            get
//            {
//                
//            }
//        };

        public IList<Button> EmployeesApplyButtons => employeesApply.Keys.ToList().AsReadOnly();
        public IList<TextMeshProUGUI> EmployeesCountLabels => employeesCount.Values.ToList().AsReadOnly();
        public IList<Button> EmployeesQuitButtons => employeesQuit.Keys.ToList().AsReadOnly();
        public IList<Button> ProjectApplyButtons => projectsApply.Keys.ToList().AsReadOnly();

        public void AddEmployeesApplyButton(Button btn, TextMeshProUGUI label)
        {
            employeesApply.Add(btn,label);
        }
        public void AddEmployeesCountButton(Button btn, TextMeshProUGUI label)
        {
            employeesCount.Add(btn,label);
        }
        public void AddEmployeesQuitButton(Button btn, TextMeshProUGUI label)
        {
            employeesQuit.Add(btn,label);
        }
        public void AddProjectApplyButton(Button btn, TextMeshProUGUI label)
        {
            projectsApply.Add(btn,label);
        }
        
        public void RemoveEmployeesApplyButton(Button btn)
        {
            employeesApply.Remove(btn);
        }
        public void RemoveEmployeesCountButton(Button btn)
        {
            employeesCount.Remove(btn);
        }
        public void RemoveEmployeesQuitButton(Button btn)
        {
            employeesQuit.Remove(btn);
        }
        public void RemoveProjectApplyButton(Button btn)
        {
            projectsApply.Remove(btn);
        }
        
        public bool Contains(string btnName)
        {
            foreach (var dictionaries in allUIElements)
            {
                foreach (var item in dictionaries)
                {
                    if (item.Key != null && item.Key.name == btnName)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public void RemoveAll()
        {
            foreach (var dictionaries in allUIElements)
            {
                for(int index = 0; index < dictionaries.Count; index++)
                //foreach (var item in dictionaries)
                {
                    var item = dictionaries.ElementAt(index);
                    if (item.Key != null)
                    {
                        var btn = item.Key;
                        var label = item.Value;
                        dictionaries.Remove(item.Key);
                        Object.Destroy(label.gameObject);
                        Object.Destroy(btn.gameObject);
                    }
                }
            }
        }
        [CanBeNull]
        public Button GetEmployeesApplyButton(string btnName)
        {
            foreach (var btn in employeesApply)
            {
                if (btn.Key != null && btn.Key.name == btnName)
                {
                    return btn.Key;
                }
            }
            return null;
        }
        [CanBeNull]
        public TextMeshProUGUI GetEmployeesApplyButtonLabel(string btnName)
        {
            foreach (var btn in employeesApply)
            {
                if (btn.Key != null && btn.Key.name == btnName)
                {
                    return btn.Value;
                }
            }
            return null;
        }
        [CanBeNull]
        public TextMeshProUGUI GetEmployeesCountLabel(string btnName)
        {
            foreach (var btn in employeesCount)
            {
                if (btn.Key != null && btn.Key.name == btnName)
                {
                    return btn.Value;
                }
            }
            return null;
        }
        [CanBeNull]
        public Button GetEmployeesQuityButton(string btnName)
        {
            foreach (var btn in employeesQuit)
            {
                if (btn.Key != null && btn.Key.name == btnName)
                {
                    return btn.Key;
                }
            }
            return null;
        }
        [CanBeNull]
        public TextMeshProUGUI GetEmployeesQuitButtonLabel(string btnName)
        {
            foreach (var btn in employeesQuit)
            {
                if (btn.Key != null && btn.Key.name == btnName)
                {
                    return btn.Value;
                }
            }
            return null;
        }
        
        [CanBeNull]
        public Button GetProjectApplyButton(string btnName)
        {
            foreach (var btn in projectsApply)
            {
                if (btn.Key != null && btn.Key.name == btnName)
                {
                    return btn.Key;
                }
            }
            return null;
        }
        [CanBeNull]
        public TextMeshProUGUI GetProjectApplyButtonLabel(string btnName)
        {
            foreach (var btn in projectsApply)
            {
                if (btn.Key != null && btn.Key.name == btnName)
                {
                    return btn.Value;
                }
            }
            return null;
        }
    }
}