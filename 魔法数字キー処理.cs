using UnityEngine;
using UnityEngine.InputSystem;

public class MagicSlotManager : MonoBehaviour
{
    public RuneGrammar[] slots = new RuneGrammar[9];
    public MagicCaster caster;

    void Update()
    {
        if (GameStateTest.isCrafting) return;

        Key[] keys =
        {
            Key.Digit1, Key.Digit2, Key.Digit3,
            Key.Digit4, Key.Digit5, Key.Digit6,
            Key.Digit7, Key.Digit8, Key.Digit9
        };

        for (int i = 0; i < slots.Length && i < keys.Length; i++)
        {
            if (Keyboard.current[keys[i]].wasPressedThisFrame)
            {
                caster.Cast(slots[i]);
            }
        }
    }
}
