namespace Base.FSM
{
    public class BaseState
    {
        protected BaseStateMachine StateMachine { get; private set; }

        protected BaseState(BaseStateMachine stateMachine)
        {
            StateMachine = stateMachine;
        }
    
        public virtual void Enter() {}
        public virtual void LogicUpdate() {}
        public virtual void PhysicsUpdate() {}
        public virtual void Exit() {}
    }
}
