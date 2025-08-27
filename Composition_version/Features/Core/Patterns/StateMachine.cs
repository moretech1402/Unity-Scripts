namespace Core.Patterns
{
    public interface IState
    {
        void Enter();
        void Tick();
        void Exit();
    }

    public class StateMachine
    {
        public IState CurrentState { get; private set; }

        public void Initialize(IState startingState)
        {
            CurrentState = startingState;
            CurrentState.Enter();
        }

        public void TransitionTo(IState nextState)
        {
            CurrentState?.Exit();
            CurrentState = nextState;
            CurrentState.Enter();
        }

        public void Update()
        {
            CurrentState?.Tick();
        }
    }

}

