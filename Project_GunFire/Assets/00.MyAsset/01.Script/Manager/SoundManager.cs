using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : SceneObject<SoundManager>
{
    AudioSource myAudio;

    private void Start()
    {
        myAudio = GetComponent<AudioSource>();
    }

    public void WeaponSound() => myAudio.PlayOneShot(WeaponSystem.Instance.CurrWeapon.audioClip);
    public void WeaponSound(float _volume) => myAudio.PlayOneShot(WeaponSystem.Instance.CurrWeapon.audioClip, _volume);

    public void PlayOneShot(AudioClip _clip) => myAudio.PlayOneShot(_clip);
    public void PlayOneShot(AudioClip _clip, float _volume) => myAudio.PlayOneShot(_clip, _volume);
}
