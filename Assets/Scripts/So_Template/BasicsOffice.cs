using System.Collections.Generic;
using Enums;
using Sirenix.OdinInspector;
using UnityEngine;

namespace So_Template
{
    [CreateAssetMenu (fileName = "BeginsOffices", menuName = "ScriptableObjects/BeginsOffices", order = 3)]
    public class BasicsOffice : ScriptableObject
    {
        [Header("List of all Offices")]
        public List<BuildingType> offices = new List<BuildingType>();
    }
}
