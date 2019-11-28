using System.Collections.Generic;
using Enums;
using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu (fileName = "BeginsOffices", menuName = "ScriptableObjects/OfficesForBegin", order = 3)]
public class OfficesForBegin : ScriptableObject
{
    [ReorderableList, Header("List of all Offices")]
    public List<BuildingType> offices = new List<BuildingType>();
}
