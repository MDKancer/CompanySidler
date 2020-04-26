﻿using Enums;
using PathFinder;
using UnityEngine;

namespace StateManager.States.EmploeeStates
{
    public class Communication : EmployeeState
    {
        public override void OnStateEnter()
        {
            destination = EmployeeData.EntityWorkCycle[HumanState.COMMUNICATION];
            targetPosition = EmployeeData.OfficePosition(destination);
            targetPosition = GenerateRandomPosition(targetPosition);

            Navigator.MoveTo(navMeshAgent, targetPosition);
        }

        public override void OnStateUpdate()
        {
            if (IsOnPosition(targetPosition))
            {
                duration = GetActivityDuration;
                while (time <= duration)
                {
                    time += Time.deltaTime;
                }
                onCompleted.Invoke();
            }
        }

        public override void OnStateExit()
        {
            time = 0f;
        }
    }
}