using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionController : MonoBehaviour
{
    
    public void OnBackButtonClicked()
    {
        SceneManager.LoadScene("MenuScreen");
    }

}
