using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class GameManager : MonoBehaviour {
    [SerializeField]
    private GameObject escMenu;
    [SerializeField]
    private SoundManager soundManager;
    [SerializeField]
    private Text titleScreen;
    [SerializeField]
    private Text controlsScreen;

    public float fadeOutTime = 5f;
    public float startFadeTime = 5f;

    public void Start()
    {
        soundManager.PlayMusic("bgm_1");
        Invoke("FadeOut", startFadeTime);
    }

    public void ToggleQuitMenu()
    {
        FirstPersonController controller = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>();
        bool status = !escMenu.gameObject.activeInHierarchy;
        escMenu.gameObject.SetActive(status);
        controller.enabled = !status;

        if (status)
        {
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void FadeOut()
    {
        StartCoroutine(FadeOutRoutine());
    }

    private IEnumerator FadeOutRoutine()
    {
        Color originalTitleColor = titleScreen.color;
        Color originalControlsColor = controlsScreen.color;
        for(float t = 0.01f; t < fadeOutTime; t += Time.deltaTime)
        {
            titleScreen.color = Color.Lerp(originalTitleColor, Color.clear, Mathf.Min(1, t / fadeOutTime));
            controlsScreen.color = Color.Lerp(originalControlsColor, Color.clear, Mathf.Min(1, t / fadeOutTime));
            yield return null;
        }
    }
}
