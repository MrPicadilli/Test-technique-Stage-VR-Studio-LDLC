using UnityEngine;

public class PointZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Interactible"))
        {
            other.gameObject.GetComponent<Interactible>().DesactivateItem();
            GameManager.instance.AddPoint();
        }
    }
}
