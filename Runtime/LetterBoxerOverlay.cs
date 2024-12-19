using UnityEngine;

namespace io.redstart.letterboxer
{
    public class LetterBoxerOverlay : LetterBoxer
    {
        [SerializeField] private RectTransform targetRectTransform;
        public RectTransform Target
        {
            get => targetRectTransform;
            set => targetRectTransform = value;
        }

        public override Rect SetSize()
        {
            Rect rect = base.SetSize();
            Target.localScale = Vector3.one + (Vector3.one * OverscanOffset);
            return rect;
        }
    }
}
