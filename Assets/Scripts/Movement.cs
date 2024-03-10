using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Movement : MonoBehaviour
{   
    [SerializeField] float thrust = 150f;
    [SerializeField] float rotateSpeed = 200f;
    [SerializeField] AudioClip audioClip;
    [SerializeField] ParticleSystem mainBooster;
    [SerializeField] float inkCooldown = 0.3f;


    Rigidbody rb;
    AudioSource audioSource;
    private float nextInkTime = 0.0f;

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
            StartThrust();
        }
        else{
            StopThrust();
        }
    }

    void StartThrust(){
        rb.AddRelativeForce(Vector3.back * thrust * Time.deltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(audioClip);
        }
        ShootInk();
    }

    void StopThrust(){
        audioSource.Stop();
    }

    void ShootInk()
    {
        if (Time.time >= nextInkTime)
        {
            Vector3 positionOffset = new Vector3(0, 1f, .5f); // Adjust as needed
            Vector3 adjustedPosition = transform.position + transform.TransformDirection(positionOffset);
            Quaternion inkRotation = Quaternion.LookRotation(-transform.forward);

            ParticleSystem inkBurst = Instantiate(mainBooster, adjustedPosition, inkRotation);
            inkBurst.Play();
            Destroy(inkBurst.gameObject, inkBurst.main.duration + inkBurst.main.startLifetime.constantMax);

            // Update the nextInkTime to the current time plus the cooldown period
            nextInkTime = Time.time + inkCooldown;
        }
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
