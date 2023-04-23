using _Game.Scripts.Jenga.Interface;

namespace _Game.Scripts.Jenga.Stack.Interface
{
    public interface IStack
    {
        public string ID { get; }
        void Init(string id);
        void PlaceJenga(IJenga jengaToPlace);
    }
}