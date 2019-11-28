using BootManager;
using UnityEngine;
using UnityEngine.AI;

namespace UISignals
{
    public class UISignals : MonoBehaviour
    {
        [Range(0f,30f)]
        public float entitiesSpeed = 3.5f;


        public void Update()
        {
            var container = Boot.container;
            if(container.SpawnedGameObjects.Count > 0)
            {
                foreach (var entity in container.SpawnedGameObjects)
                {
                    if(entity != null)
                    {
                        entity.GetComponent<NavMeshAgent>().speed = entitiesSpeed;
                    }
                }
            }
        }
    }
}