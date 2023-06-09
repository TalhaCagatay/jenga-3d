﻿using System;
using System.Collections.Generic;
using _Game.Scripts.Configs.GameModes;
using _Game.Scripts.Configs.Interface;
using _Game.Scripts.Game.Controller;
using _Game.Scripts.Helpers;
using _Game.Scripts.Jenga.Interface;
using _Game.Scripts.Jenga.Stack.Controller;
using _Game.Scripts.View.Gameplay;
using _Game.Scripts.View.Helper;
using _Game.Scripts.View.Interface;
using DG.Tweening;
using UnityEngine;

namespace _Game.Scripts.Jenga
{
    public abstract class JengaBehaviour : MonoBehaviour, IJenga
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private InformationText _studyTextRight;
        [SerializeField] private InformationText _studyTextLeft;

        private Vector3 _initialPosition;
        private Quaternion _initialRotation;
        private IViewController _viewController;
        private ResetConfig _resetConfig;

        public Transform Transform => transform;
        public Rigidbody Rigidbody => _rigidbody;
        public int ID { get; private set; }
        public string Subject { get; private set; }
        public string Grade { get; private set; }
        public int Mastery { get; private set; }
        public string Domainid { get; private set; }
        public string Domain { get; private set; }
        public string Cluster { get; private set; }
        public string StandardID { get; private set; }
        public string StandardDescription { get; private set; }
        
        public void Map(JengaInformation jengaInformation)
        {
            ID = jengaInformation.id;
            Subject = jengaInformation.subject;
            Grade = jengaInformation.grade;
            Mastery = jengaInformation.mastery;
            Domainid = jengaInformation.domainid;
            Domain = jengaInformation.domain;
            Cluster = jengaInformation.cluster;
            StandardID = jengaInformation.standardid;
            StandardDescription = jengaInformation.standarddescription;
        }

        private void Awake()
        {
            _rigidbody.isKinematic = true;

            _viewController = GameController.Instance.GetController<IViewController>();
            _viewController.SubscribeToInitialize(OnInitialized);
        }

        private void OnInitialized()
        {
            _viewController.GetView<GameplayView>().ResetClicked += OnResetClicked;
            var worldCanvasTransform = _viewController.WorldSpaceCanvas.transform;
            if (_studyTextLeft.transform.parent.gameObject.activeSelf)
            {
                _studyTextLeft.SetTarget(_studyTextLeft.transform.parent);
                _studyTextLeft.SetParent(worldCanvasTransform);   
            }
            if (_studyTextRight.transform.parent.gameObject.activeSelf)
            {
                _studyTextRight.SetTarget(_studyTextRight.transform.parent);
                _studyTextRight.SetParent(worldCanvasTransform);   
            }

            _initialPosition = transform.position;
            _initialRotation = transform.rotation;

            _resetConfig = GameController.Instance.GetController<IGameConfigController>().GetConfig<ResetConfig>();
        }

        private void OnResetClicked()
        {
            if (_rigidbody.isKinematic) return;
            
            UIHelper.DisableButtons();
            _rigidbody.isKinematic = true;
            transform.DOMove(_initialPosition, _resetConfig.ResetDuration);
            transform.DORotate(_initialRotation.eulerAngles, _resetConfig.ResetDuration).OnComplete(() =>
            {
                gameObject.SetActive(true);
                UIHelper.EnableButtons();
            });
        }
    }
    
    // Define a custom comparer to compare the standards based on the given conditions
    public class JengaComparer : IComparer<JengaInformation>
    {
        public int Compare(JengaInformation x, JengaInformation y)
        {
            if (x == null) throw new ArgumentNullException(nameof(x));
            // First, compare the jengas by domain name ascending
            int result = String.Compare(x.domain, y.domain, StringComparison.Ordinal);
            if (result != 0) return result;

            // If the domain names are the same, compare the jengas by cluster name ascending
            result = String.Compare(x.cluster, y.cluster, StringComparison.Ordinal);
            if (result != 0) return result;

            // If the cluster names are the same, compare the jengas by standard ID ascending
            return String.Compare(x.standardid, y.standardid, StringComparison.Ordinal);
        }
    }
}