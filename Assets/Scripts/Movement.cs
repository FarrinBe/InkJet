using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Movement : MonoBehaviour
{   
    [SerializeField] float thrust = 110f;
    [SerializeField] float rotateSpeed = 15f;
    [SerializeField] AudioClip audioClip;
    [SerializeField] ParticleSystem mainBooster;


    Rigidbody rb;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update() {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(Vector3.back * thrust * Time.deltaTime);
            if (!audioSource.isPlaying){
                audioSource.PlayOneShot(audioClip);
            }
            ShootInk();
        }
        else{
            audioSource.Stop();
        }
    }
    void ShootInk()
    {
        // Assuming the player's forward direction aligns with the Z-axis due to the rotation (90, 270, 0),
        // and considering you want the ink to appear a bit forward (.25 in Z) and above (.3 in Y) the player.
        Vector3 adjustedPosition = transform.position + new Vector3(0, -1f, -1f);

        // Use the player's rotation directly without further adjustments,
        // assuming the prefab itself is oriented correctly for the effect you want.
        Quaternion inkRotation = Quaternion.LookRotation(-transform.forward);

        // Instantiate the ink prefab with the adjusted position and the player's rotation
        ParticleSystem inkBurst = Instantiate(mainBooster, adjustedPosition, inkRotation);
        inkBurst.Play();

        // Destroy the Particle System after it's finished
        Destroy(inkBurst.gameObject, inkBurst.main.duration + inkBurst.main.startLifetime.constantMax);
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotateSpeed);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotateSpeed);
        }
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; // freezing rotation so we can manually rotate
        transform.Rotate(Vector3.right * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; // unfreezing rotation so the physics system can take over
    }
}
