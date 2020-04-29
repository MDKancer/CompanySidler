namespace StateManager.States.GameStates.Template
{
    public interface IState
    {
        void OnStateEnter();
        void OnStateUpdate();
        void OnStateExit();
    }
}