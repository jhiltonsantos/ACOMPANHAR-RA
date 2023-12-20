using UnityEngine;

public class MenuAudioManager : MonoBehaviour
{
    public AudioClip yourMenuMusicClip;

    private void Awake()
    {
        AudioManager.Instance.sceneMusics.Add(new AudioManager.SceneMusic { sceneName = "MainMenuScene", music = yourMenuMusicClip });
        AudioManager.Instance.sceneMusics.Add(new AudioManager.SceneMusic { sceneName = "SelectorNivelScene", music = yourMenuMusicClip });
        AudioManager.Instance.sceneMusics.Add(new AudioManager.SceneMusic { sceneName = "SelectorNivel1LevelScene", music = yourMenuMusicClip });
        AudioManager.Instance.sceneMusics.Add(new AudioManager.SceneMusic { sceneName = "SelectorNivel2LevelScene", music = yourMenuMusicClip });
        AudioManager.Instance.sceneMusics.Add(new AudioManager.SceneMusic { sceneName = "SelectorNivel3LevelScene", music = yourMenuMusicClip });
        AudioManager.Instance.sceneMusics.Add(new AudioManager.SceneMusic { sceneName = "SelectorNivel4LevelScene", music = yourMenuMusicClip });
    }
}