using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * モード管理
 * StarSystemの一番の大元
 */

namespace AR
{
    namespace StarSystem
    {
        public enum MODE
        {
            Transformation,
            Asteroad
        }

        public class ModeManager : MonoBehaviour
        {
            private AsteroadModeController asteroadModeController;
            private TransformationModeController transformationModeController;

            private MODE mode = MODE.Asteroad;      // 現在のモード(変身モードかアステロードモードか)
            private bool isActivate = true;         // StarSystemを使用可能か

            private void Start()
            {
                asteroadModeController = GetComponent<AsteroadModeController>();
                transformationModeController = GetComponent<TransformationModeController>();
            }

            private void Update()
            {
                if (isActivate)
                {
                    switch (mode)
                    {
                        case MODE.Asteroad:
                            asteroadModeController.MyUpdate();
                            break;
                        case MODE.Transformation:
                            transformationModeController.MyUpdate();
                            break;
                    }
                }
            }

            // 引数のモードに変更
            public void Activate(MODE mode)
            {
                isActivate = true;
                this.mode = mode;
            }

            // StarSystemの無効化
            public void Deactivate()
            {
                switch (mode)
                {
                    case MODE.Asteroad:
                        asteroadModeController.AllDestroy();
                        break;
                    case MODE.Transformation:
                        transformationModeController.AllDestroy();
                        break;
                }
                isActivate = false;
            }
        }
    }
}
