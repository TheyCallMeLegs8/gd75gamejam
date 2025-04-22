using UnityEngine;

public class PlayerDeathTrigger : MonoBehaviour
{
    [SerializeField] private PlayerController _playerControls;

    [SerializeField] private LayerMask _hitLayer;

    private void OnTriggerEnter(Collider other)
    {
        if ((_hitLayer & 1 << other.gameObject.layer) == 1 << other.gameObject.layer)
        {
            // add death functionality
            Debug.Log("Death");
        }

        if (other.GetComponent<SuperPickup>())
        {
            _playerControls?.ActivateSuperMode();
        }
    }
}
