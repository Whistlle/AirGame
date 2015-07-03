using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sov.AVGPart
{
    class Instances
    {
        Instances()
        {

        }

        static Instances _sharedManagerInstances;

        public static Instances Instance
        {
            get
            {
                if(_sharedManagerInstances == null)
                {
                    _sharedManagerInstances = new Instances();
                }
                return _sharedManagerInstances;
            }
        }

        UIManager _sharedUIManager = null;
        public UIManager UIManager
        {
            get
            {
                if (_sharedUIManager == null)
                {
                    _sharedUIManager = new UIManager();
                }
                return _sharedUIManager;
            }
        }

        
        public TextBoxesManager TextBoxesManager
        {
            get
            {

                return TextBoxesManager.Instance;
            }
        }
        /*
        ImageManager _sharedImageManager = null;
        public ImageManager ImageManager
        {
            get
            {
                if (_sharedImageManager == null)
                {
                    _sharedImageManager = new ImageManager();
                }
                return _sharedImageManager;
            }
        }*/

    }
}
