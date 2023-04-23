using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _Game.Scripts.Core.Base;
using _Game.Scripts.Jenga.Glass;
using _Game.Scripts.Jenga.Stack.Interface;
using _Game.Scripts.Jenga.Stone;
using _Game.Scripts.Jenga.Wood;
using Lean.Pool;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using static _Game.Scripts.Logger.Logger;

namespace _Game.Scripts.Jenga.Stack.Controller
{
    public class StackController : BaseMonoController, IStackController
    {
        public event Action<List<StackBehaviour>> StacksPrepared;

        [Header("Stack Offset Informations")] 
        [SerializeField] private float _stackBetweenDistance = 3f;
        [SerializeField] private Vector3 _firstStackPosition;

        [SerializeField] private LayerMask _stackLayer;
        [SerializeField] private Transform _stacksParent;
        [SerializeField] private StackBehaviour _stackBehaviour;
        [SerializeField] private JengaGlassBehaviour _jengaGlassBehaviour;
        [SerializeField] private JengaWoodBehaviour _jengaWoodBehaviour;
        [SerializeField] private JengaStoneBehaviour _jengaStoneBehaviour;

        private List<StackBehaviour> _stackBehaviours;

        public override void Init()
        {
            _stackBehaviours = new();
            StartCoroutine(FetchJengaInformations());
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);
                var result = new RaycastHit[1];
                var hits = Physics.RaycastNonAlloc(ray, result, float.MaxValue, _stackLayer);
                if (hits > 0)
                {
                    var jengaBehaviour = result[0].transform.GetComponent<JengaBehaviour>();
                    Log($"Jenga StandardDescription:{jengaBehaviour.StandardDescription}");
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
                var jengaInformations = JsonHelper.DeserializeObject<List<JengaInformation>>(jengaInformationsJson);
                var uniqueGrades = jengaInformations.Select(x => x.grade).Distinct().ToList();
                for (var index = 0; index < uniqueGrades.Count; index++)
                {
                    var uniqueGrade = uniqueGrades[index];
                    var stackPosition = new Vector3(_firstStackPosition.x + _stackBetweenDistance * index, 0f, 0f);
                    var stackBehaviour = LeanPool.Spawn(_stackBehaviour, stackPosition, Quaternion.identity, _stacksParent);
                    stackBehaviour.Init(uniqueGrade);
                    _stackBehaviours.Add(stackBehaviour);
                }

                CreateJengas(jengaInformations);
                StacksPrepared?.Invoke(_stackBehaviours);
            }
        }

        public void CreateJengas(List<JengaInformation> jengaInformations)
        {
            jengaInformations.Sort(new JengaComparer());
            
            foreach (var jengaInformation in jengaInformations)
            {
                var parent = GetParentStack(jengaInformation.grade);
                if (jengaInformation.mastery == 0)//glass
                {
                    var jengaGlassBehaviour = LeanPool.Spawn(_jengaGlassBehaviour, parent.transform);
                    jengaGlassBehaviour.Map(jengaInformation);
                    parent.PlaceJenga(jengaGlassBehaviour);
                }
                else if (jengaInformation.mastery == 1)//wood
                {
                    var jengaWoodBehaviour = LeanPool.Spawn(_jengaWoodBehaviour, parent.transform);
                    jengaWoodBehaviour.Map(jengaInformation);
                    parent.PlaceJenga(jengaWoodBehaviour);
                }
                else if (jengaInformation.mastery == 2)//stone
                {
                    var jengaStoneBehaviour = LeanPool.Spawn(_jengaStoneBehaviour, parent.transform);
                    jengaStoneBehaviour.Map(jengaInformation);
                    parent.PlaceJenga(jengaStoneBehaviour);
                }
            }
        }

        private StackBehaviour GetParentStack(string grade) => _stackBehaviours.First(stack => stack.ID == grade);
        
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