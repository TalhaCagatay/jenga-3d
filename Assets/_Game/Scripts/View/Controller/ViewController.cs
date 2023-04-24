using System.Collections.Generic;
using System.Linq;
using _Game.Scripts.Core.Base;
using _Game.Scripts.View.Gameplay;
using _Game.Scripts.View.Interface;
using UnityEngine;

namespace _Game.Scripts.View.Controller
{
    public class ViewController : BaseMonoController, IViewController
    {
        [Header("Views")]
        [SerializeField] private GameplayView _gameplayView;

        [SerializeField] private Canvas _worldSpaceCanvas;

        private IView _currentView;
        
        public Canvas WorldSpaceCanvas => _worldSpaceCanvas;
        public List<IView> Views { get; private set; }

        public override void Init()
        {
            Views = new()
            {
                //add all views
                _gameplayView // we only have 1 view for now 
            };

            //initialize all views
            Views.ForEach(view => view.Init());
            
            //show first view
            ChangeView(_gameplayView);
            
            base.Init();
        }
        
        public void ChangeView(IView viewToChange)
        {
            if (_currentView == viewToChange)
            {
                Logger.Logger.LogWarning("Can not change into same view");
                return;
            }
            
            _currentView?.Hide();
            _currentView = viewToChange;
            _currentView.Show();
        }
        
        public T GetView<T>() where T : class, IView => Views.First(view => view is T) as T;

        public override void Dispose()
        {
            
        }
    }
}