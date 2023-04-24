using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _Game.Scripts.Camera.Interface;
using _Game.Scripts.Core.Base;
using _Game.Scripts.Game.Controller;
using _Game.Scripts.Jenga.Glass;
using _Game.Scripts.Jenga.Interface;
using _Game.Scripts.Jenga.Stack.Interface;
using _Game.Scripts.Jenga.Stone;
using _Game.Scripts.Jenga.Wood;
using _Game.Scripts.View.Gameplay;
using _Game.Scripts.View.Helper;
using _Game.Scripts.View.Interface;
using Lean.Pool;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;
using static _Game.Scripts.Logger.Logger;

namespace _Game.Scripts.Jenga.Stack.Controller
{
    public class StackController : BaseMonoController, IStackController
    {
        public event Action<IStack> SelectedStackChanged;

        [Header("Stack Offset Informations")] 
        [SerializeField] private float _stackBetweenDistance = 3f;
        [SerializeField] private Vector3 _firstStackPosition;

        [SerializeField] private LayerMask _stackLayer;
        [SerializeField] private Transform _stacksParent;
        [SerializeField] private StackBehaviour _stackBehaviour;
        [SerializeField] private JengaGlassBehaviour _jengaGlassBehaviour;
        [SerializeField] private JengaWoodBehaviour _jengaWoodBehaviour;
        [SerializeField] private JengaStoneBehaviour _jengaStoneBehaviour;

        public List<IStack> Stacks { get; private set; }

        private IStack _selectedStack;
        public IStack SelectedStack
        {
            get => _selectedStack;
            private set
            {
                if (value == _selectedStack)
                    return;
                
                _selectedStack = value;
                SelectedStackChanged?.Invoke(_selectedStack);
            }
        }

        private ICameraController _cameraController;

        public override void Init()
        {
            GameController.Instance.SubscribeToInitialize(OnGameControllerInitialized);
            Stacks = new();
            StartCoroutine(FetchJengaInformations());
        }

        private void OnGameControllerInitialized()
        {
            GameController.Instance.GetController<IViewController>().GetView<GameplayView>().FocusButtons.ForEach(focusButton=>focusButton.StackFocusClicked += OnStackFocusClicked);
            _cameraController = GameController.Instance.GetController<ICameraController>();
        }

        private void OnStackFocusClicked(int focusID) => SelectedStack = Stacks[focusID];

        private void Update()
        {
            HandleInformationInput();
        }

        private void HandleInformationInput()
        {
            if (Input.GetMouseButtonDown(1))
            {
                var ray = _cameraController.Camera.ScreenPointToRay(Input.mousePosition);
                var result = new RaycastHit[1];
                var hits = Physics.RaycastNonAlloc(ray, result, float.MaxValue, _stackLayer);
                if (hits > 0)
                {
                    var jenga = result[0].transform.GetComponent<IJenga>();
                    HelperCanvas.ShowInformationPopup(jenga.Transform.position,
                        $"<color=green>{jenga.Grade}</color>: {jenga.Domain}\n\n<color=green>Cluster</color=green>: {jenga.Cluster}\n\n<color=green>{jenga.StandardID}</color>: {jenga.StandardDescription}");
                    Log($"Jenga Grade:{jenga.Grade}" +
                        $"Jenga Domain:{jenga.Domain}" +
                        $"Jenga Standard ID:{jenga.StandardID}" +
                        $"Jenga Standard Description:{jenga.StandardDescription}");
                }
            }
        }

        private IEnumerator FetchJengaInformations()
        {
            using UnityWebRequest www = UnityWebRequest.Get("https://ga1vqcu3o1.execute-api.us-east-1.amazonaws.com/Assessment/stack");
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
                LogError(www.error);
            else
            {
                Log("Jenga informations fetched successfully");
                var jengaInformationsJson = www.downloadHandler.text;
                var jengaInformations = DeserializeJengaJson(jengaInformationsJson);
                jengaInformations.Remove(jengaInformations[jengaInformations.Count - 1]); // removing the last element since the last element is algebra I
                var uniqueGrades = GetUniqueGrades(jengaInformations);
                CreateStacksFromGrades(uniqueGrades);
                SetSelectedStack(Stacks[1]);
                CreateJengas(jengaInformations);
                base.Init();
            }
        }

        private void CreateStacksFromGrades(List<string> uniqueGrades)
        {
            for (var index = 0; index < uniqueGrades.Count; index++)
            {
                var uniqueGrade = uniqueGrades[index];
                var stackPosition = new Vector3(_firstStackPosition.x + _stackBetweenDistance * index, 0f, 0f);
                var stackBehaviour = LeanPool.Spawn(_stackBehaviour, stackPosition, Quaternion.identity, _stacksParent);
                stackBehaviour.Init(uniqueGrade);
                Stacks.Add(stackBehaviour);
            }
        }

        private static List<JengaInformation> DeserializeJengaJson(string jengaInformationsJson) => JsonHelper.DeserializeObject<List<JengaInformation>>(jengaInformationsJson);

        private static List<string> GetUniqueGrades(List<JengaInformation> jengaInformations) => jengaInformations.Select(x => x.grade).Distinct().ToList();

        private void SetSelectedStack(IStack selectedStack) => SelectedStack = selectedStack;

        public void CreateJengas(List<JengaInformation> jengaInformations)
        {
            var orderedJenga = jengaInformations.OrderBy(jenga => jenga.domain).ThenBy(jenga => jenga.cluster).ThenBy(jenga => jenga.standardid);

            foreach (var jengaInformation in orderedJenga)
            {
                var parent = GetParentStack(jengaInformation.grade);
                if (jengaInformation.mastery == 0)//glass
                {
                    var jengaGlassBehaviour = LeanPool.Spawn(_jengaGlassBehaviour, parent.Transform);
                    jengaGlassBehaviour.Map(jengaInformation);
                    parent.PlaceJenga(jengaGlassBehaviour);
                }
                else if (jengaInformation.mastery == 1)//wood
                {
                    var jengaWoodBehaviour = LeanPool.Spawn(_jengaWoodBehaviour, parent.Transform);
                    jengaWoodBehaviour.Map(jengaInformation);
                    parent.PlaceJenga(jengaWoodBehaviour);
                }
                else if (jengaInformation.mastery == 2)//stone
                {
                    var jengaStoneBehaviour = LeanPool.Spawn(_jengaStoneBehaviour, parent.Transform);
                    jengaStoneBehaviour.Map(jengaInformation);
                    parent.PlaceJenga(jengaStoneBehaviour);
                }
            }
        }

        private IStack GetParentStack(string grade) => Stacks.First(stack => stack.ID == grade);
        
        public override void Dispose()
        {
            
        }
    }
    
    [Serializable]
    public class JengaInformation
    {
        public int id;
        public string subject;
        public string grade;
        public int mastery;
        public string domainid;
        public string domain;
        public string cluster;
        public string standardid;
        public string standarddescription;
    }

    // JsonHelper class to deserialize JSON array
    public static class JsonHelper
    {
        public static T DeserializeObject<T>(string json)
        {
            var t = JsonConvert.DeserializeObject<T>(json);
            return t;
        }
    }
}