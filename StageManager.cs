using System;

public static class StageManager
{
    public static StageBounds Current { get; private set; }
    static StageBounds previous;

    public static event Action<StageBounds> OnStageChanged;

    public static void ForceSetStage(StageBounds next)
    {
        if (next == null) return;
        if (Current == next) return;

        // ★ 前ステージを無効化
        if (Current != null)
            Current.gameObject.SetActive(false);

        previous = Current;
        Current = next;

        // ★ 次ステージを有効化
        Current.gameObject.SetActive(true);

        OnStageChanged?.Invoke(Current);
    }
}
