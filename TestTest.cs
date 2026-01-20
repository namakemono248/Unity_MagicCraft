using UnityEngine;
using TMPro;

public class TestTest : MonoBehaviour
{
    [Header("Player")]
    public PlayerMove playerMove;

    [Header("UI")]
    public TMP_Dropdown stageDropdown;

    [Header("Stages")]
    public GameObject[] stages;
    public Vector2[] spawnPositions;

    void Start()
    {
        if (stages.Length != spawnPositions.Length)
        {
            Debug.LogError("Stages と SpawnPositions の数が一致していません");
            return;
        }

        stageDropdown.onValueChanged.AddListener(OnStageChanged);
    }

    void OnDestroy()
    {
        stageDropdown.onValueChanged.RemoveListener(OnStageChanged);
    }

    void OnStageChanged(int index)
    {
        if (index < 0 || index >= stages.Length) return;

        foreach (var stage in stages)
            stage.SetActive(false);

        stages[index].SetActive(true);

        playerMove.WarpTo(spawnPositions[index]);
    }
}
