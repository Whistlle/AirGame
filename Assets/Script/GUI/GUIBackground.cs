using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUIBackground : GUIMenu
{
    public Material OverlayTexture;
    public Material DefaultTexture;

    void Start()
    {

    }
    public override void Quit()
    {
        Trigger("Quit");
        ShowInScene = false;
    }

    public override void Enter()
    {
        this.gameObject.SetActive(true);
        ShowInScene = true;
        Trigger("Enter");
    }

}
