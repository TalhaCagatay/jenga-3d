using System;
using System.Collections.Generic;
using _Game.Scripts.Jenga.Interface;
using _Game.Scripts.Jenga.Stack.Controller;
using UnityEngine;

namespace _Game.Scripts.Jenga
{
    public abstract class JengaBehaviour : MonoBehaviour, IJenga
    {
        [SerializeField] private Rigidbody _rigidbody;

        public Transform Transform => transform;
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