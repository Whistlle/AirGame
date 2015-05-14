using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class GUIOverlay: GUIMenu
{
    public override void Enter()
    {
        ShowInScene = true;
        Trigger("Enter");
    }

    public override void Quit()
    {
        ShowInScene = false;
        Trigger("Quit");
    }
}

