using UnityEngine;

namespace SnakeGame
{
    public class GameplayStateMachineManager : BaseStateMachineManager<BaseGameplayState>
    {
        public void InputDirection(SnakeController snake, Vector2 newDirection)
        {
            CurrentState?.InputDirection(snake, newDirection);
        }
    }
}
