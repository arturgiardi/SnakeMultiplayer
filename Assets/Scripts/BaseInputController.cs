using System;
using UnityEngine;

namespace SnakeGame
{
    //To create an AI use this class as a parent
    public abstract class BaseInputController : MonoBehaviour
    {
        protected Action<Vector2Int> OnDirectionChanged { get; private set; }
        protected SnakeController Snake { get; private set; }
        protected GameplayStateMachineManager InputListener { get; private set; }

        public void Init(SnakeController snake, GameplayStateMachineManager inputListener)
        {
            Snake = snake;
            InputListener = inputListener;
            OnInit();
        }

        protected virtual void OnInit() { }

        protected void InputDirection(Vector2 newDirection)
        {
            InputListener?.InputDirection(Snake, newDirection);
        }
    }
}
