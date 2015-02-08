using UnityEngine;

namespace UnityConsole.Examples
{
    [AddComponentMenu("UnityConsole/Examples/Keyboard Controller")]
    [RequireComponent(typeof(CharacterController))]
    public class KeyboardController : MonoBehaviour
    {
        public CharacterController characterController;

        void Update()
        {
            Vector3 moveDirection = Vector3.zero;
            bool moveForward = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
            bool moveLeft = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
            bool moveRight = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);
            bool moveBack = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);

            if (moveForward)
                moveDirection += transform.forward;
            if (moveBack)
                moveDirection -= transform.forward;
            if (moveRight)
                moveDirection += transform.right;
            if (moveLeft)
                moveDirection -= transform.right;

            characterController.SimpleMove(moveDirection);
        }
    }
}