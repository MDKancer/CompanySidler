using System.Collections.Generic;
using Enums;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu (fileName = "BeginsOffices", menuName = "ScriptableObjects/BeginsOffices", order = 3)]
public class BasicsOffice : ScriptableObject
{
    [AssetList, Header("List of all Offices")]
    public List<BuildingType> offices = new List<BuildingType>();
}
