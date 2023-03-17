using UnityEngine;

namespace SnakeGame
{
    public class GameplayInputState : BaseGameplayState
    {
        public GameplayInputState(GameplayScene gameplayScene) : base(gameplayScene)
        {
        }

        public override void Enter()
        {
            GameplayScene.WaitForUpdate(() => Manager.PushState(new GameplayExecuteMoveState(GameplayScene)));
        }

        public override void InputDirection(SnakeController snake, Vector2 newDirection)
        {
            snake.SetDirection(newDirection);
        }
    }
}
