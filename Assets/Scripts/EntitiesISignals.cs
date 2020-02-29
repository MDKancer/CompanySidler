using GameCloud;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Signals
{
    public class EntitiesISignals : MonoBehaviour
    {
        [Range(0f,30f)]
        public float entitiesSpeed = 3.5f;

        [Inject]
        private Container container;

        public void Update()
        {
            if(container.SpawnedGameObjects.Count > 0)
            {
                foreach (var entity in container.SpawnedGameObjects)
                {
                    if(entity != null && entity.GetComponent<NavMeshAgent>())
                    {
                        entity.GetComponent<NavMeshAgent>().speed = entitiesSpeed;
                    }
                }
            }
        }
    }
}