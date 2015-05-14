using UnityEngine;
using System.Collections;
using DG.Tweening;

public class GUIMenu : MonoBehaviour {
    
    //public GameObject[] MenuElements;
    public bool ShowInScene
    {
        get;
        set;
    }

    //DoTween

    public Ease CurrentAnim = Ease.Linear;

    public float InFromPos      = -1000f;
    public float InToPos        = 0f;
    public float OutToPos       = 1000f;


    float AnimDuration  = 0.5f;

	// Use this for initialization
	void Start () {
        ShowInScene = gameObject.activeInHierarchy;
        AnimDuration = GUIManager.Instance.MenuAnimationDuration;
	}
	
    public void PlayEnterAnim()
    {
        Vector3 pos = this.transform.localPosition;
        //reset position to the left
        this.transform.localPosition = new Vector3(InFromPos, pos.y, pos.z);
        this.transform.DOLocalMoveX(InToPos, AnimDuration).SetEase(CurrentAnim).SetUpdate(true);
    }

    public void PlayQuitAnim()
    {
     //   Sequence s = DOTween.Sequence();
        this.transform.DOLocalMoveX(OutToPos, AnimDuration).SetEase(CurrentAnim).SetUpdate(true);
     //   s.Append(this.transform.DOMoveX(InFromPos, 0));
    }

    public void Trigger(string trigger)
    {
        if (!this.gameObject.activeInHierarchy)
        {
            Debug.LogFormat("ERROR! Set this Menu:{0} active first", this.gameObject.name);
            this.gameObject.SetActive(true);
        }
        switch(trigger)
        {
            case "Enter":
                PlayEnterAnim();
                break;
            case "Quit":
                PlayQuitAnim();
                break;
        }
    }

    public void ActiveOn()
    {
        this.gameObject.SetActive(true);
    }

    /*
    virtual public void EnterMenu(GUIMenu enterMenu)
    {
        GUIManager.Instance.CurrentShowMenu = enterMenu;
        StartCoroutine(DelayAndEnterMenu(enterMenu, 0.5f));
        ShowInScene = true;
    }

    virtual public void EnterMenu()
    {
        //this.gameObject.SetActive(true);
        GUIManager.Instance.CurrentShowMenu = this;
        Trigger("Enter");
        ShowInScene = true;
    }

    public virtual void Quit()
    {
        Trigger("Quit");
        GUIManager.Instance.CurrentShowMenu = null;
        ShowInScene = false;
    }

    protected virtual void QuitAndEnterMenu(GUIMenu quitMenu, GUIMenu enterMenu)
    {
        quitMenu.Quit();

        enterMenu.EnterMenu();
        //enterMenu.Trigger("Enter");
    }

    

    protected IEnumerator DelayAndEnterMenu(GUIMenu menu, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        menu.Trigger("Enter");
    }*/

    public virtual void Enter()
    {
        GUIManager.Instance.CurrentShowMenu = this;
        Trigger("Enter");
        ShowInScene = true;
    }
    public virtual void Quit()
    {
        Trigger("Quit");
        GUIManager.Instance.CurrentShowMenu = null;
        GUIManager.Instance.LastShowMenu = this;
        ShowInScene = false;
    }

    


}
