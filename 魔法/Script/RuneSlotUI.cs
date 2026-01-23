using UnityEngine;
using UnityEngine.EventSystems;

public class RuneSlotUI : MonoBehaviour, IDropHandler
{
    public RuneGrammar grammar; // このページに追加

    public void OnDrop(PointerEventData eventData)
    {
        RuneUI runeUI = eventData.pointerDrag?.GetComponent<RuneUI>();
        if (runeUI != null)
        {
            grammar.runes.Add(runeUI.rune);
            Debug.Log(runeUI.rune.runeName + " を追加！");
        }
    }
}
