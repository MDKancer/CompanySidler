using Enums;
using PathFinder;
using UnityEngine;
using UnityEngine.AI;

namespace StateManager.States.EmployeeStates
{
    public class Talk : EmployeeState
    {
        public override void OnStateEnter()
        {
            destination = EmployeeData.EntityWorkCycle[HumanState.TALK];
            employee.destination = destination;
            targetPosition = EmployeeData.OfficePosition(destination);
            targetPosition = GenerateRandomPosition(targetPosition);
            navMeshAgent = employee.GetComponent<NavMeshAgent>();
            // Debug.Log($"Enter {this} emploee {emploee.name} position {emploee.transform.position} onCompletedEvent {onCompleted}");
            Navigator.MoveTo(navMeshAgent, targetPosition);
            duration = GetActivityDuration;
        }

        public override void OnStateUpdate()
        {
            if (IsOnPosition(targetPosition))
            {
                if (time <= duration)
                {
                    time += Time.deltaTime;
                }
                else
                {
                    onCompleted.Invoke();
                }
            }
        }

        public override void OnStateExit()
        {
            time = 0f;
        }
    }
}