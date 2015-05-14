using UnityEngine;
using System.Collections;

public class Loading : MonoBehaviour {

    int progress = 0;
    AsyncOperation async;
    public string NextScene;
    void Start()
    {
        StartCoroutine(loadScene());
    }

 

    //注意这里返回值一定是 IEnumerator
    IEnumerator loadScene()
    {
        async = Application.LoadLevelAsync(NextScene);
        //读取完毕后返回， 系统会自动进入C场景
        yield return async;
    }
	
	// Update is called once per frame
	void Update () {

	}
}
