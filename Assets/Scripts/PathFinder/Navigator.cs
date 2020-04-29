using Enums;
using UnityEngine;
using UnityEngine.AI;

namespace PathFinder
{
    public class Navigator
    {
        /// <summary>
        /// Um ein Object nach einem bestimmten ort bewegen zu lassen,
        /// braucht man ein ZielPosition was ZielPosition.y Axe ist gleich mit der me.position.y Axe
        /// </summary>
        /// <param name="targetPosition"> <example>(gameObject.transform.position.y == targetPosition.y)</example></param>
        public static void MoveTo(NavMeshAgent agent, Vector3 targetPosition)
        {
            if (agent)
            {
                targetPosition.y = agent.transform.position.y;
                agent.destination = targetPosition;
            }
        }

        public static PathProgress MyPathStatus(NavMeshAgent agent)
        {
            if (agent)
            {
                if (Mathf.Approximately(agent.remainingDistance,0f)) // agent.remainingDistance == 0f;
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