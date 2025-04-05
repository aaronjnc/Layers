using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class LayerSwitching : MonoBehaviour
{
    private PlayerControls playerControls;

    [SerializeField]
    private LayerInfo currentLayer;

    void Awake()
    {
        playerControls = new PlayerControls();
        playerControls.LayerMap.Switch.performed += SwitchLayer;
        playerControls.LayerMap.Enable();
    }

    private void SwitchLayer(CallbackContext ctx)
    {
        int dir = ctx.ReadValue<int>();
        currentLayer = currentLayer.SwitchActiveLayer(dir);
    }

    private void OnDestroy()
    {
        playerControls.Disable();
    }
}
