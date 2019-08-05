using System;

public class StateController<T>
{
    private T currentState;
    private T lastState;
    private T temp;
    
    public T CurrentState
    {
        set { currentState = value; }
        get { return currentState; }
    }
    public T LastState
    {
        private set { currentState = value; }
        get { return currentState; }
    }

    public void SwitchToLastState()
    {
        try
        {
            temp = lastState;
            LastState = currentState;
            currentState = temp;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
    }
    
}
