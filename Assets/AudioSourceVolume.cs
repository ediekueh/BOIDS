using UnityEngine;

public class AudioSourceVolume : MonoBehaviour
{

    AudioSource audioSource;
    public float updateStep = 0.1f;
    public int sampleDataLength = 1024;
    public Flock flock;
    
    private float currentUpdateTime = 0.0f;

    public float clipLoudness;
    private float[] clipSampleData;

    // Use this for initialization
    void Awake()
    {

        audioSource = GetComponent<AudioSource>();
        clipSampleData = new float[sampleDataLength];
    }

    // Update is called once per frame
    void Update()
    {

        currentUpdateTime += Time.deltaTime;
        if (currentUpdateTime >= updateStep)
        {
            currentUpdateTime = 0f;
            audioSource.clip.GetData(clipSampleData, audioSource.timeSamples); //I read 1024 samples, which is about 80 ms on a 44khz stereo clip, beginning at the current sample position of the clip.
            clipLoudness = 0f;
            foreach (var sample in clipSampleData)
            {
                clipLoudness += Mathf.Abs(sample);
            }
            clipLoudness /= sampleDataLength; //clipLoudness is what you are looking for
        }
        flock.driveFactor = clipLoudness * 800;
        Debug.Log(clipLoudness);
        flock.maxSpeed = clipLoudness * 600;
    }


}
