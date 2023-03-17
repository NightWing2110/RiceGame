using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamblerCat : MonoBehaviour
{
    public bool canMove;
    public List<GameObject> checkpoints;
    private int currentCheckpointIndex;
    private float setter = 50f;
    private float speed = 1;
    public void MoveToPoint(int numb)
    {
        // Set the currentCheckpointIndex to the starting checkpoint number
        StartCoroutine(MoveToNextCheckpoint(numb));
    }
    private IEnumerator MoveToNextCheckpoint(int numb)
    {
        canMove = true;
        int targetCheckpointIndex = numb - 1;
        while (currentCheckpointIndex < checkpoints.Count && canMove)
        {
            GameObject currentCheckpoint = checkpoints[currentCheckpointIndex];
            Vector3 currentCheckpointPosition = currentCheckpoint.transform.position;
            Debug.Log("Move to checkpoint " + (currentCheckpointIndex + 1) + " at " + currentCheckpointPosition);

            // Move the character towards the current checkpoint while canMove is true and the distance is greater than 0.1f
            while (canMove && Vector3.Distance(transform.position, currentCheckpointPosition) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, currentCheckpointPosition, 4f * Time.deltaTime);
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(currentCheckpointPosition - transform.position), Time.deltaTime * setter);
                Vector3 front = transform.TransformDirection(Vector3.forward);
                this.GetComponent<Rigidbody>().AddForce(front * speed, ForceMode.VelocityChange); 
                yield return null;
            }

            // Check if the current checkpoint is the target checkpoint
            if (currentCheckpointIndex == targetCheckpointIndex)
            {
                canMove = false;
            }
            else
            {
                // Move to the next checkpoint
                currentCheckpointIndex++;

                // Wait a bit before moving to the next checkpoint
                yield return new WaitForSeconds(0.3f);
            }
        }
    }
}
