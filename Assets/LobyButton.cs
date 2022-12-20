using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LobyButton : Btn
{
    public override void Interaction()
    {
        base.Interaction();
        // SceneManager.LoadScene("UIScene", LoadSceneMode.Additive);
    }
}
