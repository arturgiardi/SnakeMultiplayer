namespace SnakeGame
{
    public class GameplayExecuteMoveState : BaseGameplayState
    {
        public GameplayExecuteMoveState(GameplayScene gameplayScene) : base(gameplayScene)
        {
        }

        public override void Enter()
        {
            GameplayScene.MoveSnakes();
            if (GameplayScene.GameEnded)
                Manager.PushState(new GameplayEndGameState(GameplayScene));
            else if (GameplayScene.Apple.WasEaten)
                Manager.PopTo<GameplaySpawnAppleState>();
            else
                Manager.PopState();
        }
    }
}
