using UnityEngine;

namespace io.redstart.letterboxer
{
    public class LetterBoxer : MonoBehaviour
    {
        public enum ReferenceMode { DesignedAspectRatio, OrginalResolution };

        [SerializeField] private Color matteColor = new Color(0, 0, 0, 1);
        [SerializeField] private ReferenceMode referenceMode;
        [SerializeField] private float x = 16;
        [SerializeField] private float y = 9;
        [SerializeField] private float width = 1920;
        [SerializeField] private float height = 1080;
        [SerializeField] private bool onAwake = true;
        [SerializeField] private bool onUpdate = true;

        public Camera LetterBoxerCamera => letterBoxerCamera;
        public float OverscanOffset { get; private set; }

        protected Camera cam;
        private Camera letterBoxerCamera;
        private float lastXScreenSize;
        private float lastYScreenSize;

        private void Awake()
        {
            SetMainCamera();
            AddLetterBoxingCamera();
            if (onAwake)
            {
                SetSize();
            }
        }

        public void SetOverscan(float offset) {
            OverscanOffset = Mathf.Clamp(offset, -0.2f, 0);
            SetSize();
        }

        private void Update()
        {
            if (!onUpdate)
            {
                return;
            }

            if (lastXScreenSize != Screen.width || lastYScreenSize != Screen.height)
            {
                SetSize();
                lastXScreenSize = Screen.width;
                lastYScreenSize = Screen.height;
            }
        }

        private void OnValidate()
        {
            x = Mathf.Max(1, x);
            y = Mathf.Max(1, y);
            width = Mathf.Max(1, width);
            height = Mathf.Max(1, height);
        }

		protected virtual void SetMainCamera()
		{
			cam = GetComponentInChildren<Camera>();
		}

        private void AddLetterBoxingCamera()
        {
            // create a camera to render background used for matte bars
            letterBoxerCamera = new GameObject("Letter Boxer Camera").AddComponent<Camera>();
            letterBoxerCamera.backgroundColor = matteColor;
            letterBoxerCamera.cullingMask = 0;
            letterBoxerCamera.depth = -100;
            letterBoxerCamera.farClipPlane = 1;
            letterBoxerCamera.useOcclusionCulling = false;
            letterBoxerCamera.allowHDR = false;
            letterBoxerCamera.allowMSAA = false;
            letterBoxerCamera.clearFlags = CameraClearFlags.Color;
            letterBoxerCamera.orthographic = true;
            letterBoxerCamera.transform.SetParent(transform);
        }

        public virtual Rect SetSize()
        {
            Rect rect = GetLetterboxedRect();
            cam.rect = rect;
            letterBoxerCamera.enabled = !Mathf.Approximately(rect.height, 1f) || !Mathf.Approximately(rect.width, 1f);
            return rect;
        }

        // based on logic here from http://gamedesigntheory.blogspot.com/2010/09/controlling-aspect-ratio-in-unity.html
        protected Rect GetLetterboxedRect()
        {
            // calc based on aspect ratio
            float targetRatio = x / y;

            // recalc if using resolution as reference
            if (referenceMode == LetterBoxer.ReferenceMode.OrginalResolution)
            {
                targetRatio = width / height;
            }

            // determine the game window's current aspect ratio
            float windowaspect = (float)Screen.width / (float)Screen.height;

            // current viewport height should be scaled by this amount
            float scaleheight = windowaspect / targetRatio;

            // get scale adjustment based on overscan
            float overscan = 1f + OverscanOffset;

            Rect rect = cam.rect;

            // if scaled height is less than current height, add letterbox
            if (scaleheight < 1.0f)
            {
                rect.width = 1.0f * overscan;
                rect.height = scaleheight * overscan;
                rect.x = (1.0f - rect.width) / 2.0f;
                rect.y = (1.0f - rect.height) / 2.0f;
            }
            else // add pillarbox
            {
                float scalewidth = 1.0f / scaleheight;
                rect.width = scalewidth * overscan;
                rect.height = 1.0f * overscan;
                rect.x = (1.0f - rect.width) / 2.0f;
                rect.y = (1.0f - rect.height) / 2.0f;
            }

            return rect;
        }
    }
}
