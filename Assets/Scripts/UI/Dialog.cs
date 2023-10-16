using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public abstract class Dialog : MonoBehaviour
    { 
       public void Hide() => Destroy(gameObject); 
    }
}
