using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 星の管理
 */

namespace AR
{
    namespace StarSystem
    {
        namespace AsteroadMode
        {
            namespace Star
            {
                public class StarManager : MonoBehaviour
                {
                    public GameObject ActiveStar { get; private set; }
                    //public GameObject LastActiveStar { get; private set; }

                    // ActiveStarを更新する
                    public void ActivateStar(GameObject nextActiveStar)
                    {
                        if (ActiveStar != null)
                        {
                            //LastActiveStar = ActiveStar;
                            ActiveStar.GetComponent<StarInfo>().Deactivate();  // 今までのactiveStarをdisactivate
                        }
                        ActiveStar = nextActiveStar;
                        ActiveStar.GetComponent<StarInfo>().Activate();
                    }

                    // ActiveStarがあったらDeactivateする
                    public void Deactivate()
                    {
                        if (ActiveStar != null)
                        {
                            //LastActiveStar = ActiveStar;
                            ActiveStar.GetComponent<StarInfo>().Deactivate();
                            ActiveStar = null;
                        }
                    }
                }
            }
        }
    }
}