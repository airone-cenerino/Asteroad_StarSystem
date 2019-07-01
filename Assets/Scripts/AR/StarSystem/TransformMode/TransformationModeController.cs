using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * 変身モードの管理
 */

namespace AR
{
    namespace StarSystem
    {
        namespace TransformationMode
        {
            public enum ConstelltionType
            {
                Aquila,
                Lepus,
                Lyra
            }

            public class TransformationModeController : MonoBehaviour
            {
                [SerializeField] private float constellationJudgeDistance = 0.2f;
                [SerializeField] private List<GameObject> constellations;
                [SerializeField] private RectTransform canvasRectTransform;


                private Vector2 canvasSize;                     // Canvasのサイズ

                private void Start()
                {
                    canvasSize = canvasRectTransform.sizeDelta;             // キャンバスの大きさを取得
                }

                // 星座変身モード中、毎フレーム呼ばれる
                public void MyUpdate()
                {
                    GameObject nearestAvailableConstellation = GetInRangeNearestConstellation();
                    Debug.Log(nearestAvailableConstellation);
                }

                public void AllDestroy()
                {

                }

                public void Enable(ConstelltionType constelltionType)
                {
                    // 星座を探し使用可能にする
                }


                // 範囲内の一番近い使える星座を返す
                private GameObject GetInRangeNearestConstellation()
                {
                    float minDistance = 10f;
                    GameObject nearestConstellation = null;
                    Vector3 mouseScreenPos = Input.mousePosition;                               // カーソル位置取得(左下原点)
                    Vector3 mouseCanvasPos = Camera.main.ScreenToViewportPoint(mouseScreenPos); // canvasの大きさを(1,1)として座標を変換

                    var StarDistanceTable = new Dictionary<GameObject, float>();                // 星と距離を格納しておくテーブル

                    foreach (GameObject constellation in constellations)
                    {
                        Vector2 constellationAnchoredPosition = constellation.GetComponent<RectTransform>().anchoredPosition;   // 星のUI上の座標を取得(アンカー位置が原点)
                        Vector2 constellationPos = constellationAnchoredPosition / canvasSize;                                  // 右上(1,1)として変換
                        float mouseToConstellationDistance = Vector2.Distance(mouseCanvasPos, constellationPos);                // カーソルと星の距離を取得

                        if (mouseToConstellationDistance > constellationJudgeDistance) continue;                                // 判定距離外の星はリストに入れない
                        if (!constellation.GetComponent<ConstellationInfo>().IsAvailable) continue;                             // 使用可能でないとき

                        if(mouseToConstellationDistance < minDistance)
                        {
                            nearestConstellation = constellation;
                            minDistance = mouseToConstellationDistance;
                        }
                    }

                    return nearestConstellation;
                }
            }
        }
    }
}