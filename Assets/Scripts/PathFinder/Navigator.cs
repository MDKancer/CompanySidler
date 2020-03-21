using Enums;
using UnityEngine;
using UnityEngine.AI;

namespace PathFinder
{
    public static class Navigator
    {
        /// <summary>
        /// Um ein Object zu einem bestimmten ort bewegen zu lassen,
        /// braucht man ein ZielPosition was ZielPosition.y Axe ist gleich mit der me.position.y Axe
        /// </summary>
        /// <param name="targetPosition"> <example>(gameObject.transform.position.y == targetPosition.y)</example></param>
        public static void MoveTo(GameObject me, Vector3 targetPosition)
        {
            var agent = me.GetComponent<NavMeshAgent>();
            if (agent)
            {
                targetPosition.y = me.transform.position.y;
                agent.destination = targetPosition;
            }
        }

        public static PathProgress MyPathStatus(GameObject me)
        {
            var agent = me.GetComponent<NavMeshAgent>();
            if (agent)
            {
                if (agent.remainingDistance == 0f)
                {
                    return PathProgress.FINISHED;
                }
                else if (agent.remainingDistance > 0f)
                {
                    return PathProgress.MOVE;
                }
                else if(agent.isStopped)
                {
                    return PathProgress.STOPPED;
                }
                else if(agent.isStopped && agent.remainingDistance > 0f)
                {
                    return PathProgress.NONE;
                }
            }
            return PathProgress.NONE;
        }
    }
}