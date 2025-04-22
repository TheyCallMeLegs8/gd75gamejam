using UnityEngine;

public class PlayerDeathTrigger : MonoBehaviour
{
    [SerializeField] private LayerMask _hitLayer;

    private void OnTriggerEnter(Collider other)
    {
        if ((_hitLayer & 1 << other.gameObject.layer) == 1 << other.gameObject.layer)
        {
            // add death functionality
            Debug.Log("Death");
        }
    }
}
