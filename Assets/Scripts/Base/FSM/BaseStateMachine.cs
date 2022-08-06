namespace Base.FSM
{
    public class BaseStateMachine
    {
        public BaseState CurrentState { get; private set; }

        public void Initialize(BaseState initBaseState)
        {
            CurrentState = initBaseState;
            CurrentState.Enter();
        }

        public void ChangeState(BaseState newBaseState)
        {
            CurrentState.Exit();
            CurrentState = newBaseState;
            CurrentState.Enter();
        }

    }
}
