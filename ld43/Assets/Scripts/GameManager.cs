using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class GameManager : MonoBehaviour {

    [SerializeField]
    private GameObject escMenu;
    [SerializeField]
    private SoundManager soundManager;

    public void Start()
    {
        soundManager.PlayMusic("bgm_1");
    }

    public void ToggleQuitMenu()
    {
        FirstPersonController controller = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>();
        bool status = !escMenu.gameObject.activeInHierarchy;
        escMenu.gameObject.SetActive(status);
        controller.enabled = !status;

        if (status)
        {
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
}
