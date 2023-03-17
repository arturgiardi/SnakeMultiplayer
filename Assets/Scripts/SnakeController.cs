using System;
using System.Collections.Generic;
using UnityEngine;

namespace SnakeGame
{
    public class SnakeController : MonoBehaviour
    {
        [field: SerializeField] private SnakeBody BodyPrefab { get; set; }
        public SnakeBody Head => _body.First.Value;
        public TileController TailTile => _body.Last.Value.CurrentTile;
        private LinkedList<SnakeBody> _body;
        private BoardController _boardController;
        private AppleController _apple;
        public Vector2 Direction { get; private set; }
        public int Size => _body.Count;

        public string Name { get; private set; }

        public void Init(int id, int startSize, Vector2Int startPosition, Vector2Int startDirection,
            BoardController boardController, AppleController apple)
        {
            Name = $"Player {id}";
            _boardController = boardController;
            _apple = apple;

            if (startPosition.x < 0 || startPosition.y < 0 ||
                startPosition.x >= _boardController.NumberOfRows || startPosition.y >= _boardController.NumberOfRows)
                throw new InvalidOperationException($"The snake start position doesn't exist.");

            if (startSize < 1)
                throw new InvalidOperationException("startSize < 1");

            if (!IsDirectionValid(startDirection))
                throw new InvalidOperationException($"Invalid start direction {startDirection}");

            SetDirection(startDirection);
            CreateBody(startSize, startPosition, startDirection);
        }

        public void SetDirection(Vector2 newDirection)
        {
            if (IsDirectionValid(newDirection))
                Direction = newDirection;
        }

        private bool IsDirectionValid(Vector2 direction)
        {
            if ((direction != Vector2.down && direction != Vector2.up &&
                direction != Vector2.left && direction != Vector2.right) ||
                (Mathf.Abs(direction.x) == 1 && Mathf.Abs(Direction.x) == 1) ||
                (Mathf.Abs(direction.y) == 1 && Mathf.Abs(Direction.y) == 1))
                return false;
            return true;
        }

        private void CreateBody(int startSize, Vector2Int startPosition, Vector2Int startDirection)
        {
            _body = new LinkedList<SnakeBody>();
            for (var i = 0; i < startSize; i++)
            {
                Grow();
                var tile = _boardController.GetTileByPosition(startPosition - (Direction * i));
                _body.Last.Value.SetTile(tile);
            }
        }

        private void Grow()
        {
            var bodyPart = Instantiate(BodyPrefab, transform);
            _body.AddLast(bodyPart);
        }


        public void MoveToTile(TileController tile)
        {
            if (tile.Content == TileContent.Apple)
                EatApple();

            /*If the body has more than the head we move the last item to the second position
            In this way we avoid modifying all the elements of the list*/
            if (_body.Count > 1)
            {
                var lastBodyPart = _body.Last.Value;
                var previousTile = Head.CurrentTile;

                _body.RemoveLast();
                _body.AddAfter(_body.First, lastBodyPart);

                Head.SetTile(tile);
                lastBodyPart.SetTile(previousTile);
            }
            else
            {
                Head.SetTile(tile);
            }
        }

        private void EatApple()
        {
            _apple.Hide();
            Grow();
        }

        public void Kill()
        {
            for (int i = _body.Count - 1; i >= 0; i--)
            {
                var bodyPart = _body.First.Value;
                _body.RemoveFirst();
                bodyPart.Destroy();
            }
            Destroy(gameObject);
        }
    }
}
