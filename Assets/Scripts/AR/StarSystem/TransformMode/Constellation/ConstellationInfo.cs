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
                public class ConstellationInfo : MonoBehaviour
                {
                    public bool IsAvailable { get; private set; } = false;

                    [SerializeField] private ConstellationType name;
                    public ConstellationType Name { get { return name; } }

                    [SerializeField] private GameObject dotConstellation;
                    [SerializeField] private GameObject goldConstellation;
                    [SerializeField] private GameObject whiteConstellation;


                    public void Enable()
                    {
                        IsAvailable = true;
                    }

                    public void DestroyLine()
                    {
                        dotConstellation.SetActive(false);
                        goldConstellation.SetActive(false);
                        whiteConstellation.SetActive(false);
                    }

                    public void Disable()
                    {
                        IsAvailable = false;
                        DestroyLine();
                    }

                    public void DotLineActivate()
                    {
                        dotConstellation.SetActive(true);
                    }

                    public void GoldLineActivate(bool activate)
                    {
                        if (activate)
                        {
                            goldConstellation.SetActive(true);
                            dotConstellation.SetActive(false);
                        }
                        else
                        {
                            goldConstellation.SetActive(false);
                            dotConstellation.SetActive(true);
                        }
                    }
                }
            }
        }
    }
}