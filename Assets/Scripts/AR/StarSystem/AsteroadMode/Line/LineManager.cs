﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AR.StarSystem.AsteroadMode.Star;
/*
 * UI上で線を引くクラスの親
 */

namespace AR
{
    namespace StarSystem
    {
        namespace AsteroadMode
        {
            namespace Line
            {
                public abstract class LineManager : MonoBehaviour
                {
                    [SerializeField] private Transform linesParent;         // lineオブジェクトの親
                    [SerializeField] private GameObject linePrefab;         // 線のプレハブ

                    protected StarManager starManager;

                    // 線を作成して、オブジェクトを返す
                    protected GameObject GenerateLine(GameObject connectStar)
                    {
                        if (connectStar == null) return null;

                        GameObject line = Instantiate(linePrefab, linesParent);  // 生成
                        RectTransform lineRectTransform = line.GetComponent<RectTransform>();

                        // Position
                        Vector2 toPosition = connectStar.GetComponent<RectTransform>().anchoredPosition;
                        Vector2 fromPosition = starManager.ActiveStar.GetComponent<RectTransform>().anchoredPosition;
                        Vector2 middlePoint = (toPosition + fromPosition) / 2;                  // 2つの星の中間地点の座標
                        lineRectTransform.anchoredPosition = middlePoint; // 座標変更

                        // Rotation
                        float zRotation = Mathf.Rad2Deg * Mathf.Atan2(toPosition.y - fromPosition.y, toPosition.x - fromPosition.x);    // 角度の計算
                        lineRectTransform.rotation = Quaternion.Euler(new Vector3(0, 0, zRotation));              // 角度変更

                        // Scale
                        float width = Vector2.Distance(toPosition, fromPosition);
                        lineRectTransform.sizeDelta = new Vector2(width, 100);

                        return line;
                    }
                }
            }
        }
    }
}