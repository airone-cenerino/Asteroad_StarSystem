using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using AR.StarSystem.AsteroadMode;

/*
 * 繋げる星をinspectorにアタッチする拡張関数
 */

namespace AR
{
    namespace StarSystem
    {
        public class MenuItemButton : MonoBehaviour
        {
            [MenuItem("CustomMenu/ConnectStars")]
            static void ConnectStars()
            {
                // 結ぶことのできる最長の距離はここを調整
                float connectableMaxDistance = 0.2f;
                // -------------------------------------

                GameObject[] stars = GameObject.FindGameObjectsWithTag("Star");
                Vector2 canvasSize = GameObject.Find("Canvas").GetComponent<RectTransform>().sizeDelta;

                foreach (GameObject star in stars)
                {
                    StarInfo starInfo = star.GetComponent<StarInfo>();
                    Vector2 starAnchoredPosition = star.GetComponent<RectTransform>().anchoredPosition; // 星のUI上の座標を取得(アンカー位置が原点)
                    Vector2 starPos = starAnchoredPosition / canvasSize;    // Canvas全体を(1,1)に補正

                    foreach (GameObject pairStar in stars)
                    {
                        if (star == pairStar)
                            continue;

                        Vector2 pairStarAnchoredPosition = pairStar.GetComponent<RectTransform>().anchoredPosition;         // 星のUI上の座標を取得(アンカー位置が原点)
                        Vector2 pairStarPos = pairStarAnchoredPosition / canvasSize;                // Canvas全体を(1,1)に補正
                        float distance = Vector2.Distance(starPos, pairStarPos);                    // 2つの星の距離を取得

                        if (distance < connectableMaxDistance)
                        {
                            starInfo.connectableStars.Add(pairStar);       // inspectorにアタッチ
                        }
                    }
                }
            }
        }
    }
}