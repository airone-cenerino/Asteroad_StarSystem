using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 2重ListにおいてContain()を確認する拡張メソッド
/// </summary>

public static class ListExtension
{
    public static bool MyContains(this List<List<GameObject>> self, List<GameObject> value)
    {
        bool Equals(List<GameObject> a, List<GameObject> b)
        {
            for (int i = 0; i < a.Count; i++)
            {
                if (a[i] != b[i]) return false;
            }

            return true;
        }

        foreach (var contents in self)
        {
            if (Equals(contents, value)) return true;
        }

        return false;
    }
}
