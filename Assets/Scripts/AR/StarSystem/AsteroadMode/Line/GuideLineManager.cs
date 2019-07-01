using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AR.StarSystem.AsteroadMode.Star;

namespace AR
{
    namespace StarSystem
    {
        namespace AsteroadMode
        {
            namespace Line
            {
                public class GuideLineManager : LineManager
                {
                    private ConstellationLineManager constellationLineManager;
                    public GameObject GuideLine { get; private set; }       // ガイドライン

                    private void Start()
                    {
                        constellationLineManager = GetComponent<ConstellationLineManager>();
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

                        // すでに星座線があった時
                        List<GameObject> stars1 = new List<GameObject>() { starManager.ActiveStar, connectStar };
                        List<GameObject> stars2 = new List<GameObject>() { connectStar, starManager.ActiveStar };
                        if (constellationLineManager.constellationLinePairStars.MyContains(stars1) ||
                            constellationLineManager.constellationLinePairStars.MyContains(stars2))
                        {
                            return;
                        }

                        GuideLine = GenerateLine(connectStar);    // 点線生成
                    }

                    public void GuideLineDestroy()
                    {
                        if (GuideLine != null)
                        {
                            Destroy(GuideLine);
                        }
                    }
                }
            }
        }
    }
}