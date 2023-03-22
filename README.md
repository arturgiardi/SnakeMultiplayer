# Snake Multiplayer
Snake Multiplayer is a local multiplayer version of the famous Snake Game using FSM and the Unity Input System.

**Objective**

The players must eat apples to grow without touching the edges or the body of snakes on the board.

**Controls**

Player 1 - Use the keyboard arrows to move
Player 2 - Use WASD to move
You can also create new controls and ann then to a prefab using the PlayerInputController.

**Components**

**Gameplay Scene:** Controls the gameplay params like the number of players, the time to update the board and the apple.
**BoardController:** Determines the size of the board and the position of every tile.
**SnakeController:** Represents the snakes controlled by the players
**PlayerInputController:** Reads player inputs and changes the snake's direction. It inherits from the class BaseInputController, in case you wanna make different kinds of input like an AI input.
**AppleController:** Represents the apple.
**TileController:** Represents a tile in the board.
**SnakeBody:** Represents the body of the snake.
