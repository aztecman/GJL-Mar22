using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject playerDeathPanel;
    [SerializeField] TextMesh[] missions;
    [SerializeField] GameObject playAgainBtnObj;
    [SerializeField] GameObject controlsHintObj;
    // Start is called before the first frame update
    public void ShowDeathPanel(bool show = true) {
        playerDeathPanel.SetActive(show);
    }

    public void UpdateMission(int missionIndex, string updateText) {
        missions[missionIndex].text = updateText;
    }
    public void ShowPlayAgainBtn(bool show = true) {
        playAgainBtnObj.SetActive(show);
    }
    public void ShowControlsHint(bool show = true)
    {
        controlsHintObj.SetActive(show);
    }
}
