using DG.Tweening;
using UnityEngine;

namespace _Game.Scripts.DoTweenComponents
{
    public class ShakeComponent : ComponentBase
    {
        [SerializeField] protected float _duration = 1f;
        [SerializeField] protected float _strength = 90f;
        [SerializeField] protected int _vibrato = 10;
        [SerializeField] protected float _randomness = 90f;
        [SerializeField] protected bool _fadeOut = true;
        [SerializeField] protected ShakeRandomnessMode _shakeRandomnessMode;
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
                transform.rotation = Quaternion.Euler(_startingVector);
            tweener = transform.DOShakeRotation(_duration, _strength, _vibrato, _randomness, _fadeOut, _shakeRandomnessMode)
                .SetEase(_ease).SetLoops(_loops, _loopType);
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