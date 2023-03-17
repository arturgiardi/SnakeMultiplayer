using UnityEngine;

namespace SnakeGame
{
    public class GameplayEndGameState : BaseGameplayState
    {
        public GameplayEndGameState(GameplayScene gameplayScene) : base(gameplayScene)
        {
        }

        public override void Enter()
        {
            if (GameplayScene.Winner)
                Debug.Log($"Game ended! Winner {GameplayScene.Winner.Name}");
            else
                Debug.Log($"Game over");
        }
    }
}
