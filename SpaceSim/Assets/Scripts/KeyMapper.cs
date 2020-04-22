using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyMapper : MonoBehaviour
{
    public bool useDefaultBinds = true;

    // KEYS
    public KeyCode leftKey;
    public KeyCode rightKey;
    public KeyCode backwardKey;
    public KeyCode forwardKey;
    public KeyCode descendKey;
    public KeyCode ascendKey;
    public KeyCode interactKey;
    public KeyCode menuToggleKey;
    public KeyCode rollLeftKey;
    public KeyCode rollRightKey;

    // Start is called before the first frame update
    void Start()
    {
        // Set binds
        if(useDefaultBinds)
            BindDefaultKeys();

        // TODO: Add an if statement that checks if the player doesn't have useDefaultBinds checked
        // and if he doesn't then use player binds instead and change usedefaultbinds to false
    }

    // Keybinds that are player made
    void PlayerBinds()
    {
        // TODO: Add settings for key binding and change key values here depending on what the settings are
    }

    // Default key preset, used to reset keybinds to default
    public void BindDefaultKeys()
    {
        // Movement

        // sides(wasd)
        leftKey = KeyCode.A;
        rightKey = KeyCode.D;
        forwardKey = KeyCode.W;
        backwardKey = KeyCode.S;

        // up down
        ascendKey = KeyCode.Space;
        descendKey = KeyCode.LeftControl;

        // rolling
        rollRightKey = KeyCode.E;
        rollLeftKey = KeyCode.Q;

        // Combat
        // TODO: Add combat keys

        // Other
        interactKey = KeyCode.F;
        menuToggleKey = KeyCode.G;
    }
}
