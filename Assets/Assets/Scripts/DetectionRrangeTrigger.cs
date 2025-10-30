using UnityEngine;

public class DetectionRrangeTrigger : MonoBehaviour
{
    
    public Monster_Controller monsterController;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            monsterController.followPlayer = true;
            Debug.Log("Player detected — chasing!");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            monsterController.followPlayer = false;
            Debug.Log("Player left range — roaming again.");
        }
    }
    
}
