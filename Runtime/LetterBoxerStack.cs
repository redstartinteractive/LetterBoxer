using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace io.redstart.letterboxer
{
    public class LetterBoxerStack : LetterBoxer
    {
        private UniversalAdditionalCameraData cameraData;

        protected override void SetMainCamera()
        {
            base.SetMainCamera();
            cameraData = cam.GetUniversalAdditionalCameraData();
        }

        public override Rect SetSize()
        {
            Rect rect = base.SetSize();
            foreach(Camera c in cameraData.cameraStack) c.rect = rect;
            return rect;
        }
    }
}
