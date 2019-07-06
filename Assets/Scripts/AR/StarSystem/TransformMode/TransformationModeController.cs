using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AR.StarSystem.TransformationMode.Constellation;

/*
 * 変身モードの管理
 */

namespace AR
{
    namespace StarSystem
    {
        namespace TransformationMode
        {
            public class TransformationModeController : MonoBehaviour
            {
                private GameObject lastNearestAvailableConstellation = null;
                private ConstellationManager constellationManager;

                private void Start()
                {
                    constellationManager = GetComponent<ConstellationManager>();
                }

                // 星座変身モード中、毎フレーム呼ばれる
                public void MyUpdate()
                {
                    GameObject nearestAvailableConstellation = constellationManager.GetInRangeNearestConstellation();

                    // カーソル最寄りの星座が変わったら
                    if (lastNearestAvailableConstellation != nearestAvailableConstellation)
                    {
                        constellationManager.ChangeGoldLine(nearestAvailableConstellation, lastNearestAvailableConstellation);
                    }

                    // クリックされたら
                    if (Input.GetMouseButtonDown(0) && nearestAvailableConstellation != null)
                    {


                        // ここで変身関数を呼ぶので書いてください。
                        // nearestAvailableConstellationがUI上の星座オブジェクトです。
                        // nearestAvailableConstellation.GetComponent<ConstellationInfo>().Name　でConstelltionTypeを知ることができます。


                    }

                    lastNearestAvailableConstellation = nearestAvailableConstellation;
                }

                public void Init()
                {
                    constellationManager.Init();
                    lastNearestAvailableConstellation = null;
                }

                public void AllDestroy()
                {
                    constellationManager.AllDestroy();
                }

                public void Enable(ConstellationType targetConstelltionType)
                {
                    constellationManager.Enable(targetConstelltionType);
                }

                public void AllConstellationsDisable()
                {
                    constellationManager.AllConstelltionsDisable();
                }
            }
        }
    }
}