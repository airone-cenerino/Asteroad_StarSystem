using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AR
{
    namespace StarSystem
    {
        public class StarInfo : MonoBehaviour
        {
            [SerializeField] private int starID = 1;            // 星のID
            [SerializeField] private GameObject ActiveMarker;   // アクティブ状態を表すマーカーUI

            public int StarID { get { return this.starID; } }

            public void Activate()
            {
                ActiveMarker.SetActive(true);
            }

            public void DisActivate()
            {
                ActiveMarker.SetActive(false);
            }
        }
    }
}