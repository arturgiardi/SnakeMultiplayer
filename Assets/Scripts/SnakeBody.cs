using UnityEngine;

namespace SnakeGame
{
    public class SnakeBody : MonoBehaviour
    {
        public TileController CurrentTile { get; private set; }

        public void SetTile(TileController newTile)
        {
            if (CurrentTile)
                CurrentTile.SetContent(TileContent.Empty);

            CurrentTile = newTile;
            if (CurrentTile)
            {
                CurrentTile.SetContent(TileContent.Snake);
                transform.position = CurrentTile.transform.position;
            }
        }

        public void Destroy()
        {
            SetTile(null);
            Destroy(gameObject);
        }
    }
}
