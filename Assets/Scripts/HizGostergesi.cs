using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class HizGostergesi : MonoBehaviour
{
    public CarControl wd;   

    private const float MIN_SPEED_ANG = 188.0f;
    private const float MAX_SPEED_ANG = -82.0f;

    private Transform ibreTransform;
    public float speed;
    public float topSpeed;
    public GameObject ibre;

    private void Awake()
    {
        ibreTransform = ibre.transform;
    }
    private void FixedUpdate()
    {
        speed = wd.speed;
        topSpeed = wd.topSpeed;
        if (speed > topSpeed) speed = topSpeed;
        ibreTransform.eulerAngles = new Vector3(0, 0, GetSpeedRotation());
    }
    private float GetSpeedRotation()
    {
        float toplamDonusAcýsý = MIN_SPEED_ANG - MAX_SPEED_ANG;
        float speedNormalized = speed / topSpeed;
        return MIN_SPEED_ANG - speedNormalized * toplamDonusAcýsý;
    }
}
