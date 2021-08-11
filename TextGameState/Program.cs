using System;

namespace TextGameState
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new();
            
            game.Start();

            while (true)
            {
                game.Update();
            }
        }
    }

    public class StateMachine
    {
        public State CurrentState { get; private set; }
        public void Initialize(State startingState)
        {
            CurrentState = startingState;
            startingState.Enter();
        }

        public void ChangeState(State newState)
        {
            CurrentState.Exit();

            CurrentState = newState;
            newState.Enter();
        }
    }

    public class State
    {
        protected Game game;

        protected State(Game game)
        {
            this.game = game;
        }

        public virtual void Enter() { }
        public virtual void HandleInput() { }
        public virtual void LogicUpdate() { }
        public virtual void Exit() { }
    }

    public class Game : StateMachine
    {
        public State startState;
        public State hamState;

        public void Start()
        {
            startState = new StartState(this);
            hamState = new WindowClosedState(this);

            Initialize(startState);
            
        }

        public void Update()
        {
            CurrentState.HandleInput();
            CurrentState.LogicUpdate();
        }
    }

    public class StartState : State
    {
        ConsoleKey key;

        public StartState(Game game) : base(game) { }

        public override void Enter()
        {
            base.Enter();

            Console.WriteLine("Welcome! You're in a room with a friend. She asks, \"Are you cold?\" You see that the window is open. Do you close it? y/n");
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void HandleInput()
        {
            base.HandleInput();

            key = Console.ReadKey(false).Key;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            switch (key)
            {
                case ConsoleKey.Y:
                    game.ChangeState(game.hamState);
                    break;
                case ConsoleKey.N:
                    game.ChangeState(game.startState);
                    break;
                default:
                    break;
            }
        }
    }

    public class WindowClosedState : State
    {
        string input;

        public WindowClosedState(Game game) : base(game) { }

        public override void Enter()
        {
            base.Enter();

            Console.WriteLine("You walk up to the window. You close it. Your friend says, \"Much better!\"");
            Console.WriteLine("She asks you, \"What was the name of the dog your family had years ago?\"");
        }

        public override void Exit()
        {
            base.Exit();

            Console.WriteLine("My dog was named pizza too...");
        }

        public override void HandleInput()
        {
            base.HandleInput();

            input = Console.ReadLine();

        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (input.ToLower() == "pizza")
            {
                game.ChangeState(game.startState);
            }
        }
    }

}
