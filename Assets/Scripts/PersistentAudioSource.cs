using UnityEngine;

public class PersistentAudioSource : MonoBehaviour
{
    private static PersistentAudioSource instance = null;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject); // Destroy this GameObject if an instance already exists.
        }
    }
}
