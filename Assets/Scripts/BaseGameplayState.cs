using UnityEngine;

namespace SnakeGame
{
    public class BaseGameplayState : BaseState
    {
        protected GameplayScene GameplayScene { get; private set; }
        protected GameplayStateMachineManager Manager { get; private set; }
        public BaseGameplayState(GameplayScene gameplayScene)
        {
            GameplayScene = gameplayScene;
            Manager = GameplayScene.StateMachineManager;
        }

        public virtual void InputDirection(SnakeController snake, Vector2 newDirection) { }
    }
}
