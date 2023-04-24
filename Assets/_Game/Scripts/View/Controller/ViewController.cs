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

        public Canvas WorldSpaceCanvas => _worldSpaceCanvas;
        public List<IView> Views { get; private set; }

        public override void Init()
        {
            Views = new();
            
            //add all views
            Views.Add(_gameplayView);

            //initialize all views
            Views.ForEach(view => view.Init());
            
            //show first view
            _gameplayView.Show();
            
            base.Init();
        }
        
        public T GetView<T>() where T : class, IView => Views.First(view => view is T) as T;

        public override void Dispose()
        {
            
        }
    }
}