using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AR
{
    namespace StarSystem
    {
        public enum MODE
        {
            Constellation,
            Object
        }

        public class ModeManager : MonoBehaviour
        {
            private ObjectModeController objectModeController;
            private TransformModeController transformModeController;

            private MODE mode = MODE.Object;        // 現在のモード(変身モードかオブジェクトモードか)
            private bool isActivate = true;         // 変身かオブジェクトを使用可能か

            private void Start()
            {
                objectModeController = GetComponent<ObjectModeController>();
                transformModeController = GetComponent<TransformModeController>();
            }

            private void Update()
            {
                if (isActivate)
                {
                    switch (mode)
                    {
                        case MODE.Object:
                            objectModeController.MyUpdate();
                            break;
                        case MODE.Constellation:
                            transformModeController.MyUpdate();
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

            // オブジェクト生成、星座変身をできなくする
            public void Deactivate()
            {
                switch (mode)
                {
                    case MODE.Object:
                        objectModeController.AllDestroy();
                        break;
                    case MODE.Constellation:
                        transformModeController.AllDestroy();
                        break;
                }
                isActivate = false;
            }
        }
    }
}
