using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScript : MonoBehaviour
{

    public static MainScript Instance;
    public WebService WebService;
    public UserInfo UserInfo;
    public Login Login;
    public GameObject PanelGestor;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        WebService = GetComponent<WebService>();
        UserInfo = GetComponent<UserInfo>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
