using System.Collections;

namespace StateManager
{
    public interface IFSM
    {
        /// <summary>
        /// Here the process is handled when the state signal is received.
        /// </summary>
        void OnEnter();
        /// <summary>
        /// After receiving the state signal,
        /// the processes are handled during the state update.
        /// </summary>
        IEnumerator OnUpdate();
        /// <summary>
        /// Before the change of the signal state,
        /// all processes that were necessary in this state are terminated
        /// or forwarded to the next state.
        /// </summary>
        void OnExit();
    }
}