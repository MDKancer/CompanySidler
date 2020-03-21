using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace UIDispatcher.UIBuildingContent
{
    /// <summary>
    ///  Die Datenbindung der Benutzeroberfläche bindet generische Elemente der Benutzeroberfläche an den PlayerViewController.
    /// </summary>
    [Serializable]
    public class UIData
    {
        /// <summary>
        /// Das UI Fenster wo alle Informationen über Das Gebäude sich befinden.
        /// </summary>
        [Required]
        public GameObject buildingInfo;
        [Required]
        public Image employeeLayout;
        [Required]
        public Image countLayout;
        [Required]
        public Image quitLayout;
        [Required]
        public Image projectsLayout;
        [Required]
        public Button upgradeBtn;
        [Required]
        public Button buyBtn;
        [Required]
        public Button stateBtn;
        [Required]
        public TextMeshProUGUI budget_Label;
        [Required]
        public TextMeshProUGUI numberOfCustomers_Label;
        [Required]
        public TextMeshProUGUI workersCount_Label;
        [Required]
        public TextMeshProUGUI buildingTitle_Label;
        [Required]
        public TextMeshProUGUI employeeCount_Label;
        [Required]
        public TextMeshProUGUI employeeLimit_Label;
        [Required]
        public TextMeshProUGUI price_Label;
        [Required]
        public TextMeshProUGUI currentBudget_Label;
        /// <summary>
        /// Akktuelles  Gebäude-Lebenspunkte.
        /// </summary>
        [Required]
        public Image currentHP;
        
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

        /// <summary>
        /// Liste aller Buttons <remarks>"neuer Mitarbeiter angagieren"</remarks>
        /// <remarks>Es ist Readonly!</remarks>
        /// </summary>
        public IList<Button> EmployeesApplyButtons => employeesApply.Keys.ToList().AsReadOnly();
        /// <summary>
        /// Liste aller Labels for Buttons <remarks>"Mitarbeiteranzahl"</remarks>
        /// <remarks>Es ist Readonly!</remarks>
        /// </summary>
        public IList<TextMeshProUGUI> EmployeesCountLabels => employeesCount.Values.ToList().AsReadOnly();
        /// <summary>
        /// Liste aller Buttons <remarks>"Mitarbeiter kündigen"</remarks>
        /// <remarks>Es ist Readonly!</remarks>
        /// </summary>
        public IList<Button> EmployeesQuitButtons => employeesQuit.Keys.ToList().AsReadOnly();
        /// <summary>
        /// Liste aller Buttons <remarks>"Projekt annehmen"</remarks>
        /// <remarks>Es ist Readonly!</remarks>
        /// </summary>
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
                    if (!item.Key.Equals(null))
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
        /// <summary>
        /// Rückgabe einer TextComponente den zu Button gehört. 
        /// </summary>
        /// <param name="btnName"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Rückgabe einer Button dem zu einer AnzahlMitarbeiter Label gehört. 
        /// </summary>
        /// <param name="btnName"></param>
        /// <returns></returns>
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
        public Button GetEmployeesQuitButton(string btnName)
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
                if (!btn.Key.Equals(null)  && btn.Key.name == btnName)
                {
                    return btn.Value;
                }
            }
            return null;
        }
    }
}