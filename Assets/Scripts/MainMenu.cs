using UnityEngine;

public class MainMenu : MonoBehaviour
{
    // No real logic place to talk of my render mode since I'm not touching it in the 
    // code, but if you go look in the editor, my canvas is Screen Space - Overlay as 
    // I want it to be at the top of the screen, at the forefront of everything. 

    [SerializeField] private GameObject buttonPanel;
    [SerializeField] private GameObject settingPannel;

    // unity
    void Awake()
    {
        // I like for my stuff to crash when there is a gamebreaking error
        settingPannel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    // main menu buttons
    public void StartClicked()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }

    public void SettingsClicked()
    {
        buttonPanel.SetActive(false);
        settingPannel.SetActive(true);
    }

    public void QuitClicked()
    {
        Application.Quit();
    }

    // settings
    public void BackClicked()
    {
        settingPannel.SetActive(false);
        buttonPanel.SetActive(true);
    }
}
