using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    public void OnBack(InputAction.CallbackContext ctx)
    {
        if (ctx.canceled)
        {
            UIManager.instance.CloseTopPanel();
        }
    }
}
