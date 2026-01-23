using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Magic/RuneGrammar")]
public class RuneGrammar : ScriptableObject
{
    public List<Rune> runes = new List<Rune>();

    // この魔法用の弾プレハブ
    public GameObject projectilePrefab;
}
