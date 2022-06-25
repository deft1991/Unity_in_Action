using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSettingsPopup : MonoBehaviour
{
    /*
     * Link to sound clip
     */
    [SerializeField] private AudioClip sound;

    /*
     * Switch sount turn off/turn on
     */
    public void OnSoundToggle()
    {
        Managers.Audio.SoundMute = !Managers.Audio.SoundMute;
        /*
         * Audio effect on button click
         */
        Managers.Audio.PlaySound(sound);
    }

    /*
     * Change volume
     */
    public void OnSoundValue(float volume)
    {
        Managers.Audio.SoundVolume = volume;
    }

    public void OnPlayMusic(int selector)
    {
        switch (selector)
        {
            case 1 :
                Managers.Audio.PlayIntroMusic();
                break;
            case 2:
                Managers.Audio.PlayLevelMusic();
                break;
            default:
                Managers.Audio.StopMusic();
                break;
        }
    }

    /*
     * Turn on/off music based on MusicMute setter
     */
    public void OnMusicToggle()
    {
        Managers.Audio.MusicMute = !Managers.Audio.MusicMute;
    }

    /*
     * Set music volume with method MusicVolume
     */
    public void OnMusicValue(float volume)
    {
        Managers.Audio.MusicVolume = volume;
    }
}
