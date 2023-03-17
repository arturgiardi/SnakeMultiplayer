using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SnakeGame
{
    public class BoardController : MonoBehaviour
    {
        [field: SerializeField] public int NumberOfRows { get; private set; }
        [field: SerializeField] private TileController TilePrefab { get; set; }
        [field: SerializeField] private RectTransform TileArea { get; set; }
        public float Scale => transform.localScale.x;

        private TileController[,] _board;

        public int RandomRow => Random.Range(0, _board.GetLength(0));
        public int RandomColumn => Random.Range(0, _board.GetLength(1));
        public int Size => NumberOfRows * NumberOfRows;

        public void Init(int snakeStartSize, int snakeCount)
        {
            CreateTiles();
            var boardSize = TilePrefab.Size * _board.GetLength(1);
            PositionTiles(boardSize);
            SetBoardScaleAndPosition(boardSize);
        }

        private void CreateTiles()
        {
            _board = new TileController[NumberOfRows, NumberOfRows];

            for (int row = 0; row < NumberOfRows; row++)
            {
                for (int column = 0; column < NumberOfRows; column++)
                {
                    if (_board[row, column] != null)
                        throw new InvalidOperationException($"A tile already exists at the desired position {row} {column}");
                    _board[row, column] = Instantiate(TilePrefab, transform);
                    _board[row, column].name = $"Tile{row}-{column}";
                    _board[row, column].Init(row, column);
                }
            }
        }

        private void PositionTiles(float boardSize)
        {
            var halfSize = boardSize / 2;

            for (int row = 0; row < _board.GetLength(1); row++)
            {
                for (int column = 0; column < _board.GetLength(0); column++)
                {
                    //Calculates the position of each tile considering the board center (half size of the board)
                    var xPosition = (column * _board[column, row].Size) - halfSize + (_board[column, row].Size / 2);
                    var yPosition = (row * _board[column, row].Size) - halfSize + (_board[column, row].Size / 2);
                    _board[column, row].transform.localPosition = new Vector3(xPosition, yPosition, 0);
                }
            }
        }

        private void SetBoardScaleAndPosition(float boardSize)
        {
            var boardArea = GetWorldRect();

            var scaleX = boardArea.width / boardSize;
            var scaleY = boardArea.height / boardSize;
            
            //Defines the shortest lenght to define the transform scale
            var scale = Mathf.Min(scaleX, scaleY);
            
            //Sets the scale to fit in the Tile Area
            transform.localScale = Vector3.one * scale;
            transform.position = boardArea.center;
        }

        private Rect GetWorldRect()
        {
            /*Gets the corners of the Tile Area in the world. I used a Rect transform to make sure
            where the board boundaries would be*/
            var corners = new Vector3[4];
            TileArea.GetWorldCorners(corners);
            return Rect.MinMaxRect(corners[0].x, corners[0].y, corners[2].x, corners[2].y);
        }

        public TileController GetTileByPosition(Vector2 position)
        {
            if (position.x < 0 || position.y < 0 || position.x >= _board.GetLength(0) || position.y >= _board.GetLength(1))
                return null;
            return _board[(int)position.x, (int)position.y];
        }

        public TileController GetEmptyTile(int startRow, int startColumn)
        {
            for (int row = startRow; row < _board.GetLength(0); row++)
            {
                for (int column = startRow; column < _board.GetLength(1); column++)
                {
                    if (_board[row, column].Content == TileContent.Empty)
                    {
                        return _board[row, column];
                    }
                }
            }
            return GetEmptyTile(0, 0);
        }

        public bool HasEmptyTiles()
        {
            foreach (var item in _board)
            {
                if (item.Content == TileContent.Empty)
                    return true;
            }
            return false;
        }
    }
}
