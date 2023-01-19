using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] RectTransform fader;
    private void Start()
    {
        fader.gameObject.SetActive(true);
        LeanTween.alpha(fader, 2, 0);
        LeanTween.alpha(fader, 0, 1.0f).setOnComplete(() => {
            fader.gameObject.SetActive(false);
        });
    }
}
