using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AR.StarSystem.TransformationMode;
using AR.StarSystem.TransformationMode.Constellation;

namespace AR
{
    namespace StarSystem
    {
        public class TestScript : MonoBehaviour
        {
            private ModeManager modeManager;
            private TransformationModeController transformationModeController;

            // Start is called before the first frame update
            void Start()
            {
                modeManager = GetComponent<ModeManager>();
                transformationModeController = GetComponent<TransformationModeController>();
            }

            // Update is called once per frame
            void Update()
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    modeManager.Activate(MODE.Transformation);      // 変身モードにする。
                }
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    modeManager.Activate(MODE.Asteroad);            // アステロードモードにする。
                }

                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    transformationModeController.Enable(ConstellationType.Aquila);   // わし座の解放
                }
                if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    transformationModeController.Enable(ConstellationType.Lepus);    // うさぎ座の解放
                }
                if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    transformationModeController.Enable(ConstellationType.Lyra);     // こと座の解放
                }
                if (Input.GetKeyDown(KeyCode.Alpha4))
                {
                    modeManager.Deactivate();       // 変身、アステロードモードの強制終了
                }

                if (Input.GetKeyDown(KeyCode.Alpha5))
                {
                    transformationModeController.AllConstellationsDisable();     // 星座解放リセットボタン
                }
            }
        }
    }
}