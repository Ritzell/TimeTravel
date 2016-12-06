using UnityEngine;
using System.Collections;
using UnityStandardAssets.Utility;

public class FanPitch : MonoBehaviour {
    private AudioSource audio;
    private AutoMoveAndRotate autoMove;
    [SerializeField]
	private AudioSource EarthquakeSound = null;
    void Start()
    {
        autoMove = FindObjectOfType<AutoMoveAndRotate>();
        audio = GetComponent<AudioSource>();
    }

    public IEnumerator SpeedUpMotor(float Max, float time)
    {
        float t = 0;
        Player player = FindObjectOfType<Player>();
        StartCoroutine(player.SwayCamera(time));
        AutoMoveAndRotate autoMove = FindObjectOfType<AutoMoveAndRotate>();
        float motorSpeed = autoMove.rotateDegreesPerSecond.value.y;
        EarthquakeSound.Play();
        while (Mathf.Abs(autoMove.rotateDegreesPerSecond.value.y) < Mathf.Abs(Max))
        {
            t = Time.deltaTime;
            autoMove.rotateDegreesPerSecond.value.y += (Max - motorSpeed) * t / time;
            audio.pitch += 20f * t / time;
            player.SwayPower += 0.5f * t / time;
            EarthquakeSound.pitch += 2f * t / time;
            yield return null;
        }
    }
}
