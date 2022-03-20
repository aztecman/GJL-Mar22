using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject playerDeathPanel;
    [SerializeField] TextMesh[] missions;
    // Start is called before the first frame update
    public void ShowDeathPanel() {
        playerDeathPanel.SetActive(true);
    }

    public void UpdateMission(int missionIndex, string updateText) {
        missions[missionIndex].text = updateText;
    }

}
