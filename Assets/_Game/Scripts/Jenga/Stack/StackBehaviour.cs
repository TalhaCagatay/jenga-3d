using System.Collections.Generic;
using _Game.Scripts.Game.Controller;
using _Game.Scripts.Jenga.Glass;
using _Game.Scripts.Jenga.Interface;
using _Game.Scripts.Jenga.Stack.Interface;
using _Game.Scripts.View.Controller;
using _Game.Scripts.View.Views.Gameplay;
using TMPro;
using UnityEngine;

namespace _Game.Scripts.Jenga.Stack
{
    public class StackBehaviour : MonoBehaviour, IStack
    {
        [SerializeField] private TMP_Text _stackTitleText;
        [Header("Jenga Offset Informations")]
        [SerializeField, Tooltip("Offset between each jenga in same row")] private float _rowOffset = 0.85f;
        [SerializeField, Tooltip("Height difference between each jenga row")] private float _heightOffset = 0.5f;
        [SerializeField, Tooltip("Base height that will be added to each jenga")] private float _baseHeightOffset = 0.25f;
        public string ID { get; private set; }
        public Transform Transform => transform;

        public List<IJenga> Jengas { get; private set; }

        public void Init(string id)
        {
            Jengas = new();
            ID = id;
            _stackTitleText.text = ID;
        }

        public void PlaceJenga(IJenga jengaToPlace)
        {
            jengaToPlace.Transform.localPosition = CalculateNextJengaPosition();
            jengaToPlace.Transform.rotation = CalculateNextJengaRotation();
            Jengas.Add(jengaToPlace);
        }

        private Vector3 CalculateNextJengaPosition()
        {
            var y = Mathf.Floor(Jengas.Count / 3f) * _heightOffset + _baseHeightOffset;
            if (Jengas.Count / 3 % 2 == 0)
            {
                var z = Jengas.Count % 3 == 0 ? -_rowOffset : Jengas.Count % 3 == 1 ? 0f : _rowOffset;
                var x = 0f;   
                return new Vector3(x, y, z);
            }
            else
            {
                var x = Jengas.Count % 3 == 0 ? -_rowOffset : Jengas.Count % 3 == 1 ? 0f : _rowOffset;
                var z = 0f;
                return new Vector3(x, y, z);
            }
        }
        
        private Quaternion CalculateNextJengaRotation()
        {
            var yRotation = Jengas.Count / 3 % 2 == 0 ? 90f : 0f;
            return Quaternion.Euler(0f, yRotation, 0f);
        }
    }
}