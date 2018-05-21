using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugPanel : MonoBehaviour {

    [SerializeField]
    private List<Text> textList;

    public static DebugPanel _Instance;

	// Use this for initialization
	void Start () {
        _Instance = this;
        GetComponentsInChildren(textList);

    }
	
    public void UpdateTextObject(int index, string msg)
    {
        if(index <= textList.Count)
        {
            textList[index].text = msg;
        }
    }

}
