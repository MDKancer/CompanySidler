namespace StateManager.State.Template
{
    public interface IState
    {
        void OnEnter();
        void OnUpdate();
        void OnExit();
    }
}