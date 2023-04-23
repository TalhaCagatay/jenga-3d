using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

namespace _Game.Scripts.Initializations
{
    [CreateAssetMenu(fileName = "GameInitModule", menuName = "Jenga/InitModules/GameInitModule", order = 0)]
    public abstract class InitStep : ScriptableObject
    {
        public event Action Initialized;

        [SerializeField] private List<InitStep> _dependencies;

        [NonSerialized] private bool _initRequested;
        [NonSerialized] private bool _isInitialized;
        [NonSerialized] private Dictionary<InitStep, bool> _dependencyInitStatus = new Dictionary<InitStep, bool>();
        
        private string _name;

        public bool IsInitialized => _isInitialized;

        public void Run()
        {
            _name = name;
            
            if (_initRequested) return;
            
            for (var i = 0; i < _dependencies.Count; i++)
                _dependencyInitStatus[_dependencies[i]] = false;
            _initRequested = true;
            
            Debug.Log($"[INITIALIZER] {_name} started initialization at {Thread.CurrentThread.ManagedThreadId}.");
            
            if (_dependencies.TrueForAll(x => x.IsInitialized))
            {
                Debug.Log($"[INITIALIZER] {_name} already initialized");
                InternalStep();
            }
            else
            {
            
                foreach (var dep in _dependencies)
                {
                    _dependencyInitStatus[dep] = dep.IsInitialized;
            
                    if (!dep.IsInitialized)
                    {
                        var temp = dep;
                        dep.Initialized += AfterInit;
                        dep.Run();
                        void AfterInit()
                        {
                            Debug.Log($"[INITIALIZER] {Thread.CurrentThread.ManagedThreadId} {_name} afterInit");
                            temp.Initialized -= AfterInit;
                            _dependencyInitStatus[temp] = temp.IsInitialized;
                            if (_dependencyInitStatus.All(x => x.Value))
                                InternalStep();
                        }
                    }
                }
            }
        }

        protected virtual void InternalStep()
        {
            Debug.Log($"[INITIALIZER] {Thread.CurrentThread.ManagedThreadId} {name} internal step");
            FinalizeStep();
        }

        protected void FinalizeStep()
        {
            Debug.Log($"<color=green>[INITIALIZER] {Thread.CurrentThread.ManagedThreadId} {_name} finalized initialization.</color>");
            _isInitialized = true;
            Initialized?.Invoke();
        }
    }
}