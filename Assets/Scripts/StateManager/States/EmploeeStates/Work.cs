using PathFinder;
using UnityEngine;
using UnityEngine.AI;

namespace StateManager.States.EmployeeStates
{
    public class Work : EmployeeState
    {
        public override void OnStateEnter()
        {
            destination = EmployeeData.GetHisOffice;
            employee.destination = destination;
            targetPosition = GenerateRandomPosition(EmployeeData.MyOfficePosition);
            navMeshAgent = employee.GetComponent<NavMeshAgent>();
            
            Navigator.MoveTo(navMeshAgent,targetPosition);

            chore = GetActivity();
            if (chore != null)
            {
                chore.ApplyTaskTaker(employee);
                        
                duration = chore.TimeDuration;
                progress = chore.ProgressBar;
                            
                time = 0f;
                part = progress.howMuchNeed / (duration / Time.smoothDeltaTime);
            }
        }

        public override void OnStateUpdate()
        {
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