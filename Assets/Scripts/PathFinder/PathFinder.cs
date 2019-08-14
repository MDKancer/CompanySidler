﻿using Constants;
using UnityEngine;
using UnityEngine.AI;

namespace PathFinderManager
{
    public static class PathFinder
    {
        /// <summary>
        /// Um ein Object zu einem bestimmten ort bewegen zu lassen,
        /// braucht man ein ZielPosition was ZielPosition.y Axe ist gleich mit der me.position.y Axe
        /// </summary>
        /// <param name="me"></param>
        /// <param name="targetPosition"> <example>(gameObject.transform.position.y == targetPosition.y)</example></param>
        public static void MoveTo(this GameObject me, Vector3 targetPosition)
        {
            var agent = me.GetComponent<NavMeshAgent>();
            if (agent)
            {
                targetPosition.y = me.transform.position.y;
                agent.destination = targetPosition;
            }
        }

        public static PathProgress GetPathStatus(this GameObject me)
        {
            var agent = me.GetComponent<NavMeshAgent>();
            if (agent)
            {
                if (agent.remainingDistance == 0)
                {
                    return PathProgress.FINISHED;
                }
                else if (agent.remainingDistance > 0)
                {
                    return PathProgress.MOVE;
                }
                else if(agent.isStopped)
                {
                    return PathProgress.STOPPED;
                }
            }

            return PathProgress.NONE;
        }
    }
}