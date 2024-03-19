using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DriftUIHandler : MonoBehaviour
{
    DriftScoreHandler drift_handler;
    TextMeshProUGUI drift_score_tmp;
    // Start is called before the first frame update
    void Start()
    {
        drift_score_tmp = GameObject.Find("DriftScoreText").GetComponent<TextMeshProUGUI>();
        drift_handler = GameObject.Find("Player").GetComponent<DriftScoreHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if (drift_handler.in_combo)
        {
            drift_score_tmp.gameObject.SetActive(true);
            drift_score_tmp.text = drift_handler.current_combo_multiplier + " x " + Mathf.Round(drift_handler.current_drift_score) + " : " + Mathf.Round(drift_handler.current_combo.TotalScore());
        }
        else 
        {
            drift_score_tmp.gameObject.SetActive(false);
        }
    }

}
