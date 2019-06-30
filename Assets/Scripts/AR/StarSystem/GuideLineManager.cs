using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AR
{
    namespace StarSystem
    {
        public class GuideLineManager : LineManager
        {
            public GameObject GuideLine { get; private set; }       // ガイドライン

            private void Start()
            {
                starManager = GetComponent<StarManager>();
            }

            public void ControlLine(GameObject connectStar)
            {
                // まず既存のガイドラインを削除する
                if (GuideLine != null)
                {
                    Destroy(GuideLine);
                }


                if (starManager.ActiveStar == null || connectStar == null) return;

                //// すでに星座線があった時
                //List<GameObject> stars1 = new List<GameObject>() { starManager.ActiveStar, availableNearestStar };
                //List<GameObject> stars2 = new List<GameObject>() { availableNearestStar, starManager.ActiveStar };
                //if (constellationLineManager.constellationLinePairStars.MyContains(stars1) ||
                //    constellationLineManager.constellationLinePairStars.MyContains(stars2))
                //{
                //    return;
                //}

                GuideLine = GenerateLine(connectStar);    // 点線生成
            }
        }
    }
}