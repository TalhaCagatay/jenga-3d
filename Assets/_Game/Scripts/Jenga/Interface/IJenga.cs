using _Game.Scripts.Jenga.Stack.Controller;
using UnityEngine;

namespace _Game.Scripts.Jenga.Interface
{
    public interface IJenga
    {
        Transform Transform { get; }
        int ID { get; }
        string Subject { get; }
        string Grade { get; }
        int Mastery { get; }
        string Domainid { get; }
        string Domain { get; }
        string Cluster { get; }
        string StandardID { get; }
        string StandardDescription { get; }

        void Map(JengaInformation jengaInformation);
    }
}