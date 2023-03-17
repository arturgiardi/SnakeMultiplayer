using UnityEngine;

namespace SnakeGame
{
    public class TileController : MonoBehaviour
    {
        [field: SerializeField] private SpriteRenderer SpriteRenderer { get; set; }
        public float Size => SpriteRenderer.sprite.bounds.max.x -SpriteRenderer.sprite.bounds.min.x;
        public TileContent Content { get; private set; }
        public Vector2Int Position { get; private set; }

        public void Init(int row, int column)
        {
            Position = new Vector2Int(row, column);
            SetContent(TileContent.Empty);
        }

        public void SetContent(TileContent content)
        {
            Content = content;
        }
    }

    public enum TileContent
    {
        Empty = 0,
        Snake = 1,
        Apple = 2
    }
}
