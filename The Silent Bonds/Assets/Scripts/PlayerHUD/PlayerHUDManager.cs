using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHUDManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Button settingsButton;
    [SerializeField] private GameObject settingsContainer;
    [SerializeField] private GameObject controlsContainer;
    [SerializeField] private GameObject soundContainer;
    [SerializeField] private GameObject pauseContainer;
    private GameObject ring;
    private GameObject controlPanel;
    private GameObject levelPanel;


    float animationTime = 0.25f;

    bool isSettingClicked;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        settingsContainer = transform.Find("SettingsContainer").gameObject;
        settingsButton = settingsContainer.transform.Find("SettingsButton").GetComponent<Button>();
        controlsContainer = transform.Find("ControlsContainer").gameObject;
        soundContainer = transform.Find("SoundsContainer").gameObject;
        pauseContainer = transform.Find("PauseContainer").gameObject;
        ring = transform.Find("IconRing").gameObject;
        controlPanel = transform.Find("ControlPanel").gameObject;
        levelPanel = transform.Find("LevelPanel").gameObject;
    }

    void SettingsHover(bool hover)
    {
        Vector2 hoverPosition = new Vector2(825, 416);
        Vector2 normalPosition = new Vector2(830, 423);

        Vector2 targetPosition = normalPosition;

        if (hover) targetPosition = hoverPosition;

        LeanTween.moveLocal(settingsContainer, targetPosition, 0.25f).setEaseOutQuint();
    }

    public void OnSettingsClick()
    {
        isSettingClicked = !isSettingClicked;
        
        if(isSettingClicked)
            LeanTween.scale(ring, new Vector3(5, 5, 5), animationTime);
        else
            LeanTween.scale(ring, new Vector3(1,1,1), animationTime);

        PlayControllerAnimation(isSettingClicked);
        PlaySoundAnimation(isSettingClicked);
        PlayPauseAnimation(isSettingClicked);
    }

    void PlayControllerAnimation(bool open)
    {
        Vector2 targetPosition = new Vector2(596, 377);
        Vector2 normalPosition = new Vector2(830, 423);

        if (!open) targetPosition = normalPosition;

        LeanTween.moveLocal(controlsContainer, targetPosition, 0.25f).setEaseOutQuint();
    }

    void PlaySoundAnimation(bool open)
    {
        Vector2 targetPosition = new Vector2(654, 246);
        Vector2 normalPosition = new Vector2(830, 423);

        if (!open) targetPosition = normalPosition;

        LeanTween.moveLocal(soundContainer, targetPosition, 0.25f).setEaseOutQuint();

    }

    void PlayPauseAnimation(bool open)
    {
        Vector2 targetPosition = new Vector2(791, 189);
        Vector2 normalPosition = new Vector2(830, 423);

        if (!open) targetPosition = normalPosition;

        LeanTween.moveLocal(pauseContainer, targetPosition, 0.25f).setEaseOutQuint();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        SettingsHover(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        SettingsHover(false);
    }

    public void PlayKeyBindAnimaiton(bool open)
    {
        if (open) OnSettingsClick();

        float targetY = 0;
        float normalY = 841;

        if (!open) targetY = normalY;

        LeanTween.moveLocalY(controlPanel, targetY, animationTime * 3).setEaseOutQuart();
    }


    public void PlayLevelPanelAnimation(bool open)
    {
        float targetY = 0;
        float normalY = 841;

        if (!open) targetY = normalY;

        LeanTween.moveLocalY(levelPanel, targetY, animationTime * 3).setEaseOutQuart();
    }


    public void LoadScene(int number)
    {
        PlayLevelPanelAnimation(false);
        SceneManager.LoadSceneAsync("Level" + number);
    }

}
