//using UnityEngine;

//public class IPooleable : MonoBehaviour
//{
    public interface IPoolable
    {
        bool IsActive { get; }
        void Activate();
        void Deactivate();
    }
//}
