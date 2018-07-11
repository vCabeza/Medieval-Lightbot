using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class MainMenu : MonoBehaviour {

    private string playerName;
    public Text playerName_field;
    public Text playerName_Text;
    public Text playerScore_Text;

    public void changePlayer() {
        playerName = playerName_field.text;
    }

    private void newPlayer() {
        PlayerPrefs.SetString("playerName", playerName);
        PlayerPrefs.SetInt("playerPoints", 0);
        PlayerPrefs.Save();
    }

    public void newGame() {
        if (playerName != null && !playerName.Equals("")) {
            newPlayer();
            loadLevel(1);
        }
    }

    public void repeatLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void nextLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void loadLevel(int level) {
        if (playerName != null && !playerName.Equals("")) {
            newPlayer();
            SceneManager.LoadScene(level);
        }
    }

    public void Start() {
        playerName_Text.text = PlayerPrefs.GetString("playerName");
        playerScore_Text.text = PlayerPrefs.GetInt("playerPoints").ToString();
    }
}
