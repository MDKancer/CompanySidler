 using Constants;
 using UnityEngine;
using UnityEngine.UI;

public sealed class LabelCreator
{

    private GameObject Label;
    private Text txt_Label;
    private Worker_Position old_Position= Worker_Position.NONE;
    public LabelCreator()
    {

    }

    public GameObject CreateLabel( Worker_Position position, string name)
    {
       
        Label = new GameObject();
        Label.AddComponent<Text>();
        txt_Label = Label.GetComponent<Text>();
        txt_Label.font = Resources.GetBuiltinResource<Font>("Arial.ttf");

        RectTransform rt= txt_Label.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(120,100);

        if(position != old_Position)
        {  
            txt_Label.text = position.ToString() + " \n" + name;
            old_Position = position;
        }
        else
        {
            txt_Label.text = name;
        }
        
        return Label;
    }

    public GameObject CreateTitle(string name, GameObject Parent)
    {

        Label = new GameObject();
        Label.AddComponent<Text>();
        txt_Label = Label.GetComponent<Text>();

        txt_Label.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        txt_Label.alignment = TextAnchor.MiddleCenter;
        RectTransform rt = Label.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(250, 100);
        Label.transform.SetParent(Parent.transform);
        rt.localPosition = new Vector3(0,200f,0);
        
        txt_Label.text = name;
            
      

        return Label;
    }


}
