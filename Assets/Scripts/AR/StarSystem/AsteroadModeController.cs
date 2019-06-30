using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace AR
{
    namespace StarSystem
    {
        public class AsteroadModeController : MonoBehaviour
        {
            [SerializeField] private RectTransform canvasRectTransform;
            [SerializeField] private float starJudgementDistance;

            private ConstellationLineManager constellationLineManager;
            private GuideLineManager guideLineManager;
            private StarManager starManager;


            private GameObject lastNearestStar = null;          // 1フレーム前の一番近い星
            private GameObject activatableNearestStar = null;   // 現フレームでの一番近い結べる星
            private GameObject nearestStar;                     // 範囲内でマウスに一番近い星
            private GameObject[] stars;
            private Vector2 canvasSize;                     // Canvasのサイズ

            private void Start()
            {
                constellationLineManager = GetComponent<ConstellationLineManager>();
                guideLineManager = GetComponent<GuideLineManager>();
                starManager = GetComponent<StarManager>();
                canvasSize = canvasRectTransform.sizeDelta;             // キャンバスの大きさを取得
                stars = GameObject.FindGameObjectsWithTag("Star");      // Starタグのオブジェクトをすべて取得
            }

            // アステロードモード中、毎フレーム呼ばれる
            public void MyUpdate()
            {
                activatableNearestStar = getConnectableNearestStar();   // 結ぶことのできる一番カーソルから近い星を取得

                if (Input.GetMouseButtonDown(0))
                {
                    constellationLineManager.ControlLine(activatableNearestStar, nearestStar); // 星座線を引く
                }
                if (Input.GetMouseButtonDown(1))
                {
                    constellationLineManager.Cancel();
                }
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    constellationLineManager.Decide();
                }

                if(lastNearestStar != activatableNearestStar)
                {
                    guideLineManager.ControlLine(activatableNearestStar);
                }

                lastNearestStar = activatableNearestStar;
            }

            public void AllDestroy()
            {
                constellationLineManager.AllDestroy();
                guideLineManager.GuideLineDestroy();
            }

            #region
            private GameObject getConnectableNearestStar()
            {
                Dictionary<GameObject, float> activatableStarDistanceTable = GetActivatableStarTable();      // カーソルから一定範囲内の使える星をゲット
                if (activatableStarDistanceTable.Any())  // 空でなければ
                {
                    return activatableStarDistanceTable.Aggregate((x, y) => x.Value < y.Value ? x : y).Key;  // 近い星を返す
                }
                else
                {
                    return null;
                }
            }

            private Dictionary<GameObject, float> GetActivatableStarTable()
            {
                var inRangeStarDistanceTable = GetInRangeStarDistanceTable();                          // 範囲内の星取得

                List<GameObject> keyList = new List<GameObject>(inRangeStarDistanceTable.Keys);        // 辞書からkeyだけをコピーしてlist作成   

                // 繋げられない星の情報はいらないので捨てる
                foreach (GameObject key in keyList)
                {
                    if(starManager.ActiveStar != null) // 繋げられるか確認
                    {
                        if (!starManager.ActiveStar.GetComponent<StarInfo>().connectableStars.Contains(key))    // 繋げられないとき
                        {
                            inRangeStarDistanceTable.Remove(key);
                        }
                    }
                }
                return inRangeStarDistanceTable;
            }


            // 範囲内の星とカーソルとの距離を返す
            private Dictionary<GameObject, float> GetInRangeStarDistanceTable()
            {
                Vector3 mouseScreenPos = Input.mousePosition;                               // カーソル位置取得(左下原点)
                Vector3 mouseCanvasPos = Camera.main.ScreenToViewportPoint(mouseScreenPos); // canvasの大きさを(1,1)として座標を変換
                GameObject nearestInRangeStar = null;
                float minDistance = 10f;

                var StarDistanceTable = new Dictionary<GameObject, float>();                // 星と距離を格納しておくテーブル

                foreach (GameObject star in stars)
                {
                    Vector2 starAnchoredPosition = star.GetComponent<RectTransform>().anchoredPosition; // 星のUI上の座標を取得(アンカー位置が原点)
                    Vector2 starPos = starAnchoredPosition / canvasSize;                                // 右上(1,1)として変換
                    float mouseToStarDistance = Vector2.Distance(mouseCanvasPos, starPos);              // カーソルと星の距離を取得

                    if (mouseToStarDistance > starJudgementDistance) continue;                          // 判定距離外の星はリストに入れない

                    StarDistanceTable.Add(star, mouseToStarDistance);                                   // 星と距離を格納

                    if(mouseToStarDistance < minDistance)
                    {
                        nearestInRangeStar = star;
                    }
                }

                nearestStar = nearestInRangeStar;     // 一番近い星を格納しておく

                return StarDistanceTable;
            }
            #endregion
        }
    }
}