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
                public class PlaneManager : MonoBehaviour
                {
                    private float destroyTime = 7.0f;

                    void Update()
                    {
                        destroyTime -= Time.deltaTime;

                        if (destroyTime < 0.0f)
                        {
                            Destroy(this.gameObject);
                        }
                    }
                }
            }
        }
    }
}