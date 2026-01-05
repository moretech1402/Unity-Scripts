using System;

namespace MC.Core.Patterns
{
    public interface ITransitionableState
    {
        public Action<IState> TransitionCallback { get; set; }
    }

    public interface ITickableState
    {
        void Tick();
    }

    public interface IExitState
    {
        void Exit();
    }

    public interface IState
    {
        void Enter();
    }

    public class StateMachine
    {
        public IState CurrentState { get; private set; }

        public Action<IState> OnStateChanged;

        public void Initialize(IState startingState)
        {
            EnterState(startingState);
        }

        public void TransitionTo(IState nextState)
        {
            if(CurrentState != null && CurrentState is IExitState exitState)
                exitState?.Exit();

            EnterState(nextState);
        }

        public void Update()
        {
            if(CurrentState != null && CurrentState is ITickableState tickableState)
                tickableState?.Tick();
        }

        private void EnterState(IState nextState)
        {
            CurrentState = nextState;
            
            if(nextState is ITransitionableState transitionable)
                transitionable.TransitionCallback = TransitionTo;

            CurrentState.Enter();
            OnStateChanged?.Invoke(CurrentState);
        }
    }

}

