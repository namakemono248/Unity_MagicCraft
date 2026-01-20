using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageBounds Current { get; private set; }

    StageBounds[] stages;
    PlayerMove player;

    void Awake()
    {
        stages = Object.FindObjectsByType<StageBounds>(FindObjectsSortMode.None);
        player = Object.FindFirstObjectByType<PlayerMove>();
    }

    void Update()
    {
        if (player == null) return;

        Vector2 pos = player.transform.position;

        foreach (var stage in stages)
        {
            if (stage.Contains(pos))
            {
                Current = stage;
                return;
            }
        }
    }
}
