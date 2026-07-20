using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRagdoll : MonoBehaviour
{
    [SerializeField] private Rigidbody[] ragdollRigidbodies;
    [SerializeField] private Animator animator;
    [SerializeField] private bool toggle; // For testing purposes

    public void SwitchRagdollState(bool isRagdoll)
    {
        foreach (Rigidbody rb in ragdollRigidbodies)
        {
            rb.isKinematic = !isRagdoll;
        }
        animator.enabled = !isRagdoll;
    }

    private void Update() // For testing
    {
        SwitchRagdollState(toggle);
    }
}
