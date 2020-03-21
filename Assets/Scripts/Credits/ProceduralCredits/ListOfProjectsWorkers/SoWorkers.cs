using System.Collections.Generic;
using Enums;
using UnityEngine;

namespace Credits.ProceduralCredits.ListOfProjectsWorkers
{
    [CreateAssetMenu(menuName = "Credits")]
    public class SoWorkers : ScriptableObject {

        public string ProjektName = "tarentSidler";
        public List<Worker_Position> Position;

        public List<string> Worker;



    }
}
