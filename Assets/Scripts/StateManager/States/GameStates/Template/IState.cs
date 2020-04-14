namespace StateManager.States.GameStates.Template
{
    public interface IState
    {
        void OnEnter();
        void OnUpdate();
        void OnExit();
    }
}