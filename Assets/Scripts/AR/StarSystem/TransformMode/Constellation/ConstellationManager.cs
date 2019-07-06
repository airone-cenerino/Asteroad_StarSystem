using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AR
{
    namespace StarSystem
    {
        namespace TransformationMode
        {
            namespace Constellation
            {
                public enum ConstellationType
                {
                    Aquila,
                    Lepus,
                    Lyra
                }

                public class ConstellationManager : MonoBehaviour
                {
                    [SerializeField] private float constellationJudgeDistance = 0.2f;
                    [SerializeField] private List<GameObject> constellations;
                    [SerializeField] private RectTransform canvasRectTransform;

                    private Vector2 canvasSize;     // Canvasのサイズ
                    private void Start()
                    {
                        canvasSize = canvasRectTransform.sizeDelta;     // キャンバスの大きさを取得
                    }

                    public void ChangeGoldLine(GameObject activeConstellation, GameObject deactivateConstellation)
                    {
                        if (activeConstellation != null)
                        {
                            activeConstellation.GetComponent<ConstellationInfo>().GoldLineActivate(true);
                        }

                        if (deactivateConstellation != null)
                        {
                            deactivateConstellation.GetComponent<ConstellationInfo>().GoldLineActivate(false);
                        }
                    }

                    public void Init()
                    {
                        foreach (GameObject constellation in constellations)
                        {
                            ConstellationInfo info = constellation.GetComponent<ConstellationInfo>();
                            if (info.IsAvailable)
                            {
                               info.DotLineActivate();
                            }
                        }

                    }

                    // 星座の解放。
                    public void Enable(ConstellationType targetConstelltionType)
                    {
                        // 星座を探し使用可能にする
                        foreach (GameObject constellation in constellations)
                        {
                            ConstellationInfo info = constellation.GetComponent<ConstellationInfo>();
                            if (info.Name == targetConstelltionType)
                            {
                                info.Enable();
                            }
                        }
                    }

                    // 解放状況をリセット。
                    public void AllConstelltionsDisable()
                    {
                        foreach (GameObject constellation in constellations)
                        {
                            constellation.GetComponent<ConstellationInfo>().Disable();
                        }
                    }

                    // 全ての星座を消す。ただし、解放状況はリセットされない。
                    public void AllDestroy()
                    {
                        foreach (GameObject constellation in constellations)
                        {
                            constellation.GetComponent<ConstellationInfo>().DestroyLine();
                        }
                    }

                    // 範囲内の一番近い使える星座を返す。
                    public GameObject GetInRangeNearestConstellation()
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

                            if (mouseToConstellationDistance < minDistance)
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
}
