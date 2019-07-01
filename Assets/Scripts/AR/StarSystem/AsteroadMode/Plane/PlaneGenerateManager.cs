using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AR
{
    namespace StarSystem
    {
        namespace AsteroadMode
        {
            namespace Plane
            {
                public class PlaneGenerateManager : MonoBehaviour
                {
                    [SerializeField] private Canvas canvas;
                    [SerializeField] private GameObject plane;


                    public void GeneratePlane(List<GameObject> constellationLines)
                    {
                        GameObject parent = new GameObject();                   // 全てのパネルの親
                        Rigidbody rigidbody = parent.AddComponent<Rigidbody>(); // 重力
                        rigidbody.useGravity = true;
                        parent.AddComponent<PlaneManager>();                    // 一定時間で消えるためのスクリプトを実装する

                        foreach (GameObject line in constellationLines)
                        {
                            if (line == null) continue;

                            Vector3 lineWorldPosition = GetWorldPositionFromRect(line); // 星座線のワールド座標を取得
                            float cameraZ0Distance = Camera.main.transform.position.z;
                            float reductionRatio = Mathf.Abs(cameraZ0Distance / canvas.planeDistance);

                            Vector3 lineRelativCoordinate = lineWorldPosition - Camera.main.transform.position;    // 星座線のカメラからの相対座標

                            Vector3 instantiatePosition = new Vector3(Camera.main.transform.position.x + lineRelativCoordinate.x * reductionRatio,
                                                                      Camera.main.transform.position.y + lineRelativCoordinate.y * reductionRatio, 0);  // 生成ポイントの計算

                            GameObject lineObject = Instantiate(plane, instantiatePosition, line.transform.rotation);
                            lineObject.transform.parent = parent.transform;
                            lineObject.transform.localScale = new Vector3(line.GetComponent<RectTransform>().sizeDelta.x * 0.07f, lineObject.transform.localScale.y, lineObject.transform.localScale.z);
                        }
                    }

                    // RectTransformを持つGameObjectを受けとり、ワールド座標を返す
                    private Vector3 GetWorldPositionFromRect(GameObject rectObject)
                    {
                        Vector3 worldPosition = Vector3.zero;                               // オブジェクトのWorld座標
                        RectTransform rect = rectObject.GetComponent<RectTransform>();      // rectTransformの取得 
                        Vector2 screenPos = RectTransformUtility.WorldToScreenPoint(canvas.worldCamera, rect.position);
                        RectTransformUtility.ScreenPointToWorldPointInRectangle(rect, screenPos, canvas.worldCamera, out worldPosition);    // World座標取得

                        return worldPosition;
                    }
                }
            }
        }
    }
}