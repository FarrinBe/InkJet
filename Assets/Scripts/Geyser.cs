using System.Collections;
using UnityEngine;

public class Geyser : MonoBehaviour
{
    public ParticleSystem geyserParticleSystem; // Assign in inspector
    public float force = 1000f; // Adjust as needed
    private bool isGeyserActive = false;

    private void Start()
    {
        if (geyserParticleSystem == null)
        {
            geyserParticleSystem = GetComponent<ParticleSystem>();
        }
        StartCoroutine(GeyserRoutine());
    }

    private IEnumerator GeyserRoutine()
    {
        while (true) // Infinite loop
        {
            // Turn on the geyser
            isGeyserActive = true;
            geyserParticleSystem.Play();
            yield return new WaitForSeconds(2); // On for 2 seconds

            // Turn off the geyser
            isGeyserActive = false;
            geyserParticleSystem.Stop();
            yield return new WaitForSeconds(1); // Off for 1 second
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (isGeyserActive && other.CompareTag("Player")) // Ensure the player has a "Player" tag
        {
            Rigidbody playerRigidbody = other.GetComponent<Rigidbody>();
            if (playerRigidbody != null)
            {
                // Apply force in the direction of the geyser's up vector
                playerRigidbody.AddForce(transform.up * force);
            }
        }
    }
}
