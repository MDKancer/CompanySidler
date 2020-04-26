using PathFinder;
using UnityEngine;

namespace StateManager.States.EmploeeStates
{
    public class Work : EmployeeState
    {
        public override void OnStateEnter()
        {
            destination = EmployeeData.GetHisOffice;
            targetPosition = GenerateRandomPosition(EmployeeData.MyOfficePosition);

            
            Navigator.MoveTo(navMeshAgent,targetPosition);
            
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
            if (IsOnPosition(targetPosition))
            {
                if (time <= duration)
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