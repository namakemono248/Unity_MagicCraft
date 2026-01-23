using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using System.Collections.Generic;

/// <summary>
/// ルーンの制作
/// </summary>
public class RuneTestSelector : MonoBehaviour
{
    [Header("ドロップダウン（上から順）")]
    public TMP_Dropdown ribbon1;
    public TMP_Dropdown ribbon2;
    public TMP_Dropdown ribbon3;
    public TMP_Dropdown ribbon4;

    [Header("選択可能なルーン")]
    public Rune[] selectableRunes;

    [Header("テスト用 RuneGrammar")]
    public RuneGrammar tempGrammar;

    [Header("スロット管理")]
    public MagicSlotManager slotManager;

    [Header("UI")]
    public GameObject craftPanel; // 制作画面パネル（←追加）

    void Start()
    {
        SetupDropdown(ribbon1);
        SetupDropdown(ribbon2);
        SetupDropdown(ribbon3);
        SetupDropdown(ribbon4);

        craftPanel.SetActive(false);
    }

    void Update()
    {
        // 制作画面中のみ Space を有効にする
        if (GameStateTest.isCrafting &&
            Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            CommitToSlot1();
        }

        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            OpenCraft();
        }
    }

    void SetupDropdown(TMP_Dropdown dropdown)
    {
        dropdown.ClearOptions();

        List<string> options = new List<string>();
        options.Add("None");

        foreach (var rune in selectableRunes)
            options.Add(rune.name);

        dropdown.AddOptions(options);
        dropdown.value = 0;
    }

    // =========================
    // 制作開始
    // =========================
    public void OpenCraft()
    {
        GameStateTest.isCrafting = true;

        craftPanel.SetActive(true);

        Debug.Log("制作モード開始");
    }

    // =========================
    // スロット1に確定
    // =========================
    void CommitToSlot1()
    {
        tempGrammar.runes.Clear();

        AddRuneFromDropdown(ribbon1);
        AddRuneFromDropdown(ribbon2);
        AddRuneFromDropdown(ribbon3);
        AddRuneFromDropdown(ribbon4);

        slotManager.slots[0] = tempGrammar;

        CloseCraft();

        Debug.Log("ドロップダウン魔法を 1キーにセット！");
    }

    void AddRuneFromDropdown(TMP_Dropdown dropdown)
    {
        if (dropdown.value == 0) return;

        int runeIndex = dropdown.value - 1;
        tempGrammar.runes.Add(selectableRunes[runeIndex]);
    }

    // =========================
    // 制作終了
    // =========================
    public void CloseCraft()
    {
        GameStateTest.isCrafting = false;

        craftPanel.SetActive(false);

        Debug.Log("制作モード終了");
    }
}
