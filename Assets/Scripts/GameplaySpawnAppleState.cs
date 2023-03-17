namespace SnakeGame
{
    public class GameplaySpawnAppleState : BaseGameplayState
    {
        public GameplaySpawnAppleState(GameplayScene gameplayScene) : base(gameplayScene)
        {
        }

        public override void Enter()
        {
            GameplayScene.SpawnApple();
            Manager.PushState(new GameplayInputState(GameplayScene));
        }
    }
}
