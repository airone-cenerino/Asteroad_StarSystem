using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AR.StarSystem.TransformationMode;

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
                    modeManager.Activate(MODE.Transformation);
                }
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    modeManager.Activate(MODE.Asteroad);
                }

                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    transformationModeController.Enable(ConstelltionType.Aquila);
                    Debug.Log(1);
                }
                if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    transformationModeController.Enable(ConstelltionType.Lepus);
                }
                if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    transformationModeController.Enable(ConstelltionType.Lyra);
                }
            }
        }
    }
}