using DG.Tweening;
using UnityEngine;

namespace _Game.Scripts.DoTweenComponents
{
    public class RotateComponent : ComponentBase
    {
        [SerializeField] protected Vector3 _endValue;
        [SerializeField] protected float _duration;
        [SerializeField] protected RotateMode _rotateMode;
        [SerializeField] protected Ease _ease;
        [Tooltip("-1 for infinite loop")] [SerializeField] protected int _loops = 1;
        [SerializeField] protected LoopType _loopType;

        protected override void Awake()
        {
            initialPosition = transform.localPosition;
            initialRotation = transform.localRotation;
            initialScale = transform.localScale;
        }
        public override void PlayTween()
        {
            if (_useCustomStartingVector)
                transform.localRotation = Quaternion.Euler(_startingVector);
            tweener = transform.DOLocalRotate(_endValue, _duration, _rotateMode).SetEase(_ease).SetLoops(_loops, _loopType); 
        }
        
        public override void ResetTween()
        {
            tweener.Kill(true);
            transform.localPosition = initialPosition;
            transform.localRotation = initialRotation;
            transform.localScale = initialScale;
        }
    }
}