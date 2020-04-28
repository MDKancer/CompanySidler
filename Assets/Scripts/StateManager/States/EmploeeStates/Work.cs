using PathFinder;
using UnityEngine;
using UnityEngine.AI;

namespace StateManager.States.EmploeeStates
{
    public class Work : EmployeeState
    {
        public override void OnStateEnter()
        {
            destination = EmployeeData.GetHisOffice;
            emploee.destination = destination;
            targetPosition = GenerateRandomPosition(EmployeeData.MyOfficePosition);
            navMeshAgent = emploee.GetComponent<NavMeshAgent>();
            Navigator.MoveTo(navMeshAgent,targetPosition);
            // Debug.Log($"Enter {this} emploee {emploee.name} position {emploee.transform.position} onCompletedEvent {onCompleted}");
            chore = GetActivity();
            if (chore != null)
            {
                chore.ApplyTaskTaker(emploee);
                        
                duration = chore.TimeDuration;
                progress = chore.ProgressBar;
                            
                time = 0f;
                part = progress.howMuchNeed / (duration / Time.smoothDeltaTime);
            }
        }

        public override void OnStateUpdate()
        {
            // Debug.Log($"Update {this} emploee {emploee.name} position {emploee.transform.position}");
            if (IsOnPosition(targetPosition))
            {
                if (chore != null && time <= duration)
                {
                    time += Time.smoothDeltaTime;
                    progress.percentDoneProgress += part;

                    chore.ProgressBar = progress;
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