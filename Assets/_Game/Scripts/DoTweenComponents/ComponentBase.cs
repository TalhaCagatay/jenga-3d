using DG.Tweening;
using UnityEngine;

namespace _Game.Scripts.DoTweenComponents
{
    public abstract class ComponentBase : MonoBehaviour
    {
        public bool PlayOnStart = true;
        public bool PlayOnEnable = true;
        public bool ResetOnDisable = true;
        [HideInInspector] public bool _useCustomStartingVector;
        [HideInInspector] public Vector3 _startingVector;
        protected Vector3 initialPosition;
        protected Quaternion initialRotation;
        protected Vector3 initialScale;

        protected Tweener tweener;

        protected virtual void Awake()
        {
            initialPosition = transform.position;
            initialRotation = transform.rotation;
            initialScale = transform.localScale;
        }

        protected virtual void OnEnable()
        {
            if (!PlayOnEnable) return;
            
            PlayTween();
        }

        private void OnDisable()
        {
            if (!ResetOnDisable) return;
            
            ResetTween();
        }

        protected virtual void Start()
        {
            if (!PlayOnStart) return;
            
            PlayTween();
        }

        public abstract void PlayTween();

        public void RePlayTween()
        {
            ResetTween();
            PlayTween();
        }
        
        public virtual void ResetTween()
        {
            tweener.Kill(true);
            transform.position = initialPosition;
            transform.rotation = initialRotation;
            transform.localScale = initialScale;
        }
    }
}