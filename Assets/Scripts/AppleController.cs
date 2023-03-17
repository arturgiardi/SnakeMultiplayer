using UnityEngine;

namespace SnakeGame
{
    public class AppleController : MonoBehaviour
    {
        public bool WasEaten { get; private set; }

        public void Hide()
        {
            WasEaten = true;
            transform.localScale = Vector3.zero;
        }
        public void Spawn(TileController tile)
        {
            transform.position = tile.transform.position;
            transform.localScale = Vector3.one;
            WasEaten = false;
            tile.SetContent(TileContent.Apple);
        }
    }
}
