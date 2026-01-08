using UnityEngine;

public class QuitButton : MonoBehaviour
{
    public void QuitGame()
    {
        print("Game has quit");
        Application.Quit();
    }
}
