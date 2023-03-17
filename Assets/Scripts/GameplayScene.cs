using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SnakeGame
{
    public class GameplayScene : MonoBehaviour
    {
        [field: SerializeField] private int SnakeStartSize { get; set; }
        [field: SerializeField] private float TimeToUpdate { get; set; }
        [field: SerializeField] private SnakeController SnakePrefab { get; set; }
        [field: SerializeField] public BoardController BoardController { get; private set; }
        [field: SerializeField] public AppleController Apple { get; private set; }

        [SerializeField] private SnakeConfig[] _snakesConfig;

        public GameplayStateMachineManager StateMachineManager { get; private set; }
        private List<SnakeController> _snakes;
        public IList<SnakeController> Snakes => _snakes.AsReadOnly();
        public bool GameEnded { get; private set; }
        public SnakeController Winner { get; private set; }

        void Start()
        {
            BoardController.Init(SnakeStartSize, _snakesConfig.Length);
            StateMachineManager = new GameplayStateMachineManager();
            CreateSnakes();
            InitStateMachine();
        }

        private void CreateSnakes()
        {
            _snakes = new List<SnakeController>();
            for (var i = 0; i < _snakesConfig.Length; i++)
            {
                var playerGO = new GameObject($"Player{i + 1}").transform;
                playerGO.localScale = Vector3.one * BoardController.Scale;

                var snake = Instantiate(SnakePrefab, playerGO);
                _snakes.Add(snake);

                snake.Init(i + 1, SnakeStartSize, _snakesConfig[i].StartPosition, _snakesConfig[i].StartDirection,
                    BoardController, Apple);

                var input = Instantiate(_snakesConfig[i].InputPrefab, playerGO);
                input.Init(snake, StateMachineManager);
            }
        }

        private void InitStateMachine()
        {
            StateMachineManager.PushState(new GameplaySpawnAppleState(this));
        }

        public void SpawnApple()
        {
            if (!BoardController.HasEmptyTiles())
                return;
            var tile = BoardController.GetEmptyTile(BoardController.RandomRow, BoardController.RandomColumn);

            Apple.Spawn(tile);
        }

        public void WaitForUpdate(Action callback)
        {
            StartCoroutine(WaitForUpdateCoroutine(callback));
        }

        private IEnumerator WaitForUpdateCoroutine(Action callback)
        {
            yield return new WaitForSeconds(TimeToUpdate);
            callback.Invoke();
        }

        public void MoveSnakes()
        {
            for (var i = _snakes.Count - 1; i >= 0; i--)
            {
                //Gets the next tile considering the snake direction
                var nextTilePosition = _snakes[i].Head.CurrentTile.Position + _snakes[i].Direction;
                var tile = BoardController.GetTileByPosition(nextTilePosition);

                /*If the next tile countains a snake or is null the snake is killed
                We do not consider if it is a tail as it will move */
                if (!tile || (tile.Content == TileContent.Snake && !IsTail(tile)))
                {
                    KillSnake(_snakes[i]);
                }
                else
                {
                    _snakes[i].MoveToTile(tile);
                    if (_snakes[i].Size == BoardController.Size)
                    {
                        GameEnded = true;
                        Winner = _snakes[i];
                    }
                }
            }

            if (_snakes.Count == 0)
                GameEnded = true;
        }

        private bool IsTail(TileController tile)
        {
            foreach (var item in _snakes)
            {
                if(tile == item.TailTile)
                    return true;
            }
            return false;
        }

        private void KillSnake(SnakeController snake)
        {
            _snakes.Remove(snake);
            snake.Kill();
        }
    }

    [Serializable]
    public class SnakeConfig
    {
        [field: SerializeField] public Vector2Int StartPosition { get; private set; }
        [field: SerializeField] public Vector2Int StartDirection { get; private set; }
        [field: SerializeField] public BaseInputController InputPrefab { get; private set; }
    }
}
