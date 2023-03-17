using UnityEngine;
using UnityEngine.InputSystem;

namespace SnakeGame
{
    public class PlayerInputController : BaseInputController
    {
        [field: SerializeField] private InputActionReference ChangeDirectionAxis { get; set; }

        protected override void OnInit()
        {
            base.OnInit();
            ChangeDirectionAxis.action.Enable();
            ChangeDirectionAxis.action.performed += OnInputAxis;
        }

        private void OnInputAxis(InputAction.CallbackContext callbackContext)
        {
            InputDirection(callbackContext.ReadValue<Vector2>());
        }

        private void OnDestroy()
        {
            ChangeDirectionAxis.action.performed -= OnInputAxis;
            ChangeDirectionAxis.action.Disable();
        }
    }
}
