using System;
using System.Collections;
using System.Collections.Generic;
using BootManager;
using Constants;
using PathFinderManager;
using UnityEngine;
using Random = UnityEngine.Random;

public class test : MonoBehaviour
{
    List<BuildingType>  officePositions = new List<BuildingType>();

    private void Awake()
    {
        officePositions.Add(BuildingType.OFFICE);
        officePositions.Add(BuildingType.SOCIAL_RAUM);
        officePositions.Add(BuildingType.ACCOUNTING);
        officePositions.Add(BuildingType.NONE);
        /// TODO: ich packe alle Werte aus dem BuildinType enum in einer Liste,
        /// die Foreachschleife wird funktionieren wenn di Funktion GetBuildingType vollständig ist.
        /// und alle Interfaces für Gebäude implemetiert sind.

        //        foreach (BuildingType VARIABLE in Enum.GetValues(typeof(BuildingType)))
        //        {
        //            officePositions.Add(VARIABLE);            
        //        }
        
    }

// Start is called before the first frame update
    void Start()
    {
        
        StartCoroutine(Do());
    }

    private IEnumerator Do()
    {
        
        int randomPositionList = Random.Range(0, officePositions.Count);
        BuildingType randomBuildingType =  officePositions[randomPositionList];
        Vector3 positionOfRandomBuilding = Boot.container.GetPositionOffice(randomBuildingType);
        Vector3 targetPosition = GenerateRandomPosition(positionOfRandomBuilding);
        
        //         /\     /\     /\     /\     /\     /\     /\     /\     /\
        //         ||     ||     ||     ||     ||     ||     ||     ||     ||
        // Warum ich das mache?
        // um zu test ob ich kann für jeden Entity ein Büropositionziel geben, 
        // zum Bsp. :
        // Jeder Admin             ->     geht zum Server , weil da is sein Büro
        // Jeder Buchhalter        ->     geht zum Accountig Büro
        // etc......
        
        
        PathFinder.MoveTo(gameObject,targetPosition);
        
        while (true)
        {
            if (transform.position.x == targetPosition.x && transform.position.z == targetPosition.z)
            {
                yield return new WaitForSeconds(0.5f);
                
                randomPositionList = Random.Range(0, officePositions.Count);
                randomBuildingType =  officePositions[randomPositionList];
                positionOfRandomBuilding = Boot.container.GetPositionOffice(randomBuildingType);
                targetPosition = GenerateRandomPosition(positionOfRandomBuilding);

                PathFinder.MoveTo(gameObject,targetPosition);
            }
            yield return null;
        }
    }

    private Vector3 GenerateRandomPosition(Vector3 position)
    {
            return new Vector3(
                    Random.Range(position.x-9, position.x+9),
                        position.y,
                    Random.Range(position.z-9, position.z+9)
                    );
    }
}
