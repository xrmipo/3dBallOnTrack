using UnityEngine;

public class CollisionCheckScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == Strings.player_Tag) GameController.TriggerAction(transform);
    }
}