using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _sfxSource;

    // Custom Volume
    private const float _buttonClickedVolume = 1;
    private const float _timeSkipVolume = 1;
    private const float _pauseGameVolume = 1;
    private const float _resumeGameVolume = 1;
    private const float _openCharacterMenuVolume = 1;
    private const float _closeCharacterMenuVolume = 1;
    private const float _openLocationPinVolume = 1;
    private const float _closeLocationPinVolume = 1;
    private const float _statsProgressionVolume = 1;

    private const float _startMenuMusicVolume = 0.25f;
    private const float _daySongVolume = 0.25f;
    private const float _nightSongVolume = 0.5f;
    private const float _reviewSongVolume = 1;

    [Header("Clips References")]
    public AudioClip ButtonClicked;
    public AudioClip TimeSkip;
    public AudioClip PauseGame;
    public AudioClip ResumeGame;
    public AudioClip OpenCharacterMenu;
    public AudioClip CloseCharacterMenu;
    public AudioClip OpenLocationPin;
    public AudioClip CloseLocationPin;
    public AudioClip StatsProgression;

    [Header("Music References")]
    public AudioClip StartMenuMusic;
    public AudioClip DaySong;
    public AudioClip NightSong;
    public AudioClip ReviewSong;


    public void PlaySFX(AudioClip audioClip)
    {
        float volume = 0;
        switch (audioClip.name)
        {
            case "Button Click":
                volume = _buttonClickedVolume;
                break;

            case "Pause":
                volume = _pauseGameVolume;
                break;

            case "Resume":
                volume = _resumeGameVolume;
                break;

            case "Time Skip":
                volume = _timeSkipVolume;
                break;

            case "Open Location Pin":
                volume = _openLocationPinVolume;
                break;

            case "Close Location Pin":
                volume = _closeLocationPinVolume;
                break;

            case "Open Character Menu":
                volume = _openCharacterMenuVolume;
                break;

            case "Close Character Menu":
                volume = _closeCharacterMenuVolume;
                break;

            case "Stats Progression":
                volume = _statsProgressionVolume;
                break;

            default:
                print($"Error in finding audioclip name: {audioClip.name}");
                break;
        }

        _sfxSource.PlayOneShot(audioClip, volume);
    }

    public void PlayMusic(AudioClip audioClip)
    {
        float volume = 0;
        switch (audioClip.name)
        {
            case "Start Menu Song":
                volume = _startMenuMusicVolume;
                break;

            case "Day Song":
                volume = _daySongVolume;
                break;


            case "Night Song":
                volume = _nightSongVolume;
                break;

            case "Review Song":
                volume = _reviewSongVolume;
                break;

            default:
                print($"Error in finding music name: {audioClip.name}");
                break;
        }
       
        _musicSource.clip = audioClip;
        _musicSource.volume = volume;
        _musicSource.Play();
    }
}
