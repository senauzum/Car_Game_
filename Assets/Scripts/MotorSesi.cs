using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotorSesi : MonoBehaviour
{
    public AudioClip acilis;
    public AudioClip calisma;
    public AudioClip kapanýs;

    public float du_hiz;
    public float mi_pit;
    public float pi_hiz;

    private AudioSource _source;
    bool kontak;
    float hiz;

    private void Start()
    {
        _source = GetComponent<AudioSource>();
    }
    private void Update()
    {
        kontak = GetComponent<CarControl>().kontak;
        hiz = GetComponent<CarControl>().speed;
        if(!kontak && _source.clip == calisma)
        {
            _source.clip = kapanýs;
            _source.Play();
        }
        if(kontak && (_source.clip==kapanýs || _source.clip == null))
        {
            _source.clip = acilis;
            _source.Play();
            _source.pitch = 1;
        }
        if(kontak && !_source.isPlaying)
        {
            _source.clip =calisma;
            _source.Play();
        }
        if (_source.clip == calisma)
        {
            _source.pitch = Mathf.Lerp(_source.pitch, mi_pit + Mathf.Abs(hiz) / du_hiz, pi_hiz);
        }
    }
}
