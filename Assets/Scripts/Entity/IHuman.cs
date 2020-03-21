using Enums;
using StateManager;

namespace Entity
{
    public interface IHuman
    {
        StateController<HumanState> SelfState { get; }
    }
}