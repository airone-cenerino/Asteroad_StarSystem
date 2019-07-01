using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using AR.StarSystem.AsteroadMode.Plane;
using AR.StarSystem.AsteroadMode.Star;

/*
 * 星座線管理
 */

namespace AR
{
    namespace StarSystem
    {
        namespace AsteroadMode
        {
            namespace Line
            {
                public class ConstellationLineManager : LineManager
                {
                    private GuideLineManager guideLineManager;
                    private PlaneGenerateManager planeGenerateManager;
                    private AudioSource audioSource;

                    [SerializeField] private AudioClip lineConnectSound;
                    [SerializeField] private AudioClip makePlaneSound;


                    private List<GameObject> constellationLines = new List<GameObject>();                       // 星座線オブジェクトの格納
                    private List<GameObject> connectedStars = new List<GameObject>();                           // 星座線で結ばれている星を格納
                    public List<List<GameObject>> constellationLinePairStars = new List<List<GameObject>>();    // 星座線で結ばれている星をペアでそれぞれ格納

                    private const int maxLineNum = 10;

                    private void Start()
                    {
                        audioSource = GetComponent<AudioSource>();
                        guideLineManager = GetComponent<GuideLineManager>();
                        planeGenerateManager = GetComponent<PlaneGenerateManager>();
                        starManager = GetComponent<StarManager>();
                    }

                    public void ControlLine(GameObject connectStar, GameObject nearestStar)
                    {
                        if (maxLineNum == constellationLines.Count()) return;

                        if (guideLineManager.GuideLine != null)    // ガイドライン有
                        {
                            guideLineManager.GuideLineDestroy();        // ガイドライン削除
                            GenerateConstellationLine(connectStar);     // 星座線生成

                            if (maxLineNum == constellationLines.Count())   // 本数がmaxになったら
                            {
                                starManager.Deactivate();
                            }
                            else
                            {
                                starManager.ActivateStar(connectStar);      // activeStar切り替え
                            }
                        }
                        else if (!constellationLines.Any() || connectedStars.Contains(nearestStar)) // 星座線無しまたはその星がすでに結ばれている
                        {
                            starManager.ActivateStar(nearestStar);  // ActiveStar変更
                        }
                    }

                    public void Cancel()
                    {
                        if (constellationLines.Any())   // 星座線有
                        {
                            // 星座線を一本消す
                            Destroy(constellationLines[constellationLines.Count() - 1]);
                            constellationLines.RemoveAt(constellationLines.Count() - 1);

                            // 使用した星リストの変更
                            int starAOccurrenceNum = 0;
                            int starBOccurrenceNum = 0;
                            foreach (List<GameObject> pair in constellationLinePairStars)
                            {
                                foreach (GameObject star in pair)    // 全てのペアの星を確認
                                {
                                    // ここで消したい2つの星が2回登場していたらリストから除く必要はない
                                    if (star == constellationLinePairStars[constellationLinePairStars.Count() - 1][0]) starAOccurrenceNum++;
                                    if (star == constellationLinePairStars[constellationLinePairStars.Count() - 1][1]) starBOccurrenceNum++;
                                }
                            }
                            // 登場回数が1回ならもう結ばれていないので削除
                            if (starAOccurrenceNum == 1) connectedStars.Remove(constellationLinePairStars[constellationLinePairStars.Count() - 1][0]);
                            if (starBOccurrenceNum == 1) connectedStars.Remove(constellationLinePairStars[constellationLinePairStars.Count() - 1][1]);

                            // ペアを保存しているリストの末尾削除
                            constellationLinePairStars.RemoveAt(constellationLinePairStars.Count() - 1);
                        }

                        starManager.Deactivate();               // activeStarを消す
                        guideLineManager.GuideLineDestroy();    // ガイドライン削除
                    }

                    public void Decide()
                    {
                        if (connectedStars != null && constellationLines.Count == 0) return;

                        planeGenerateManager.GeneratePlane(constellationLines);
                        audioSource.PlayOneShot(makePlaneSound);
                        AllDestroy();
                    }

                    public void AllDestroy()
                    {
                        // 星座線削除
                        foreach (GameObject constellationLine in constellationLines)
                        {
                            Destroy(constellationLine);
                        }

                        // リスト初期化
                        constellationLines.Clear();
                        constellationLinePairStars.Clear();
                        connectedStars.Clear();

                        starManager.Deactivate();
                        guideLineManager.GuideLineDestroy();
                    }

                    private void GenerateConstellationLine(GameObject connectStar)
                    {
                        GameObject line = GenerateLine(connectStar);     // 星座線生成
                        audioSource.PlayOneShot(lineConnectSound);

                        // リストに格納
                        // line格納
                        constellationLines.Add(line);

                        // ペア格納
                        List<GameObject> stars = new List<GameObject>() { starManager.ActiveStar, connectStar };
                        constellationLinePairStars.Add(stars);

                        // 結ばれている星格納
                        if (!connectedStars.Contains(connectStar)) connectedStars.Add(connectStar);
                        if (!connectedStars.Contains(starManager.ActiveStar)) connectedStars.Add(starManager.ActiveStar);
                    }
                }
            }
        }
    }
}