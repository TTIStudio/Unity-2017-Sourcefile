using UnityEngine;
using System.Collections;


// Open File with Windows UI  -- Method 2

using System.IO;

// Open File with Windows UI  -- Method 2   -   END
public class Main : MonoBehaviour {

    int time = 0;   
    int MHB_size_updatetime = 0;
    int MHB_size = 0;
    int MaxFrameNumber = 4000;
    System.Random rd = new System.Random();
  

    // Use this for initialization
    void Start()
    {
        LoadData();
        Create_Plane(10);
        MainCamera_Init(10,0,0,0,10, (float)-30);
    }
	// Update is called once per frame
	void Update () {

         
        if (MaxFrameNumber > 0)
        {
            MaxFrameNumber--;  
            Task_CreatMHB();//5 MHB per time
        }
        
    }

    void Task_CreatMHB()
    {
        time++;

        if (time > 10)                      // UPDATE TIME
        {
            MHB_size_updatetime--;
            if(MHB_size_updatetime<1)       //  SIZE UPDATE TIME
            {
                // MHB_size++;
                MHB_size = rd.Next(1, 100);
                 MHB_size_updatetime = 1;
            }
            
            time = 0;

            float MHB_size_D = (float)(0.1- MHB_size*0.001);
            float MHB_size_Long = (float)(2 - MHB_size * 0.02);
            Create_MHB((float)(MHB_size_D), (float)(MHB_size_Long), 0, 0, 0, (float)0.1 * (rd.Next(1, 100)), 51, (float)0.1 *(rd.Next(1, 100)));

        }
    }

    void Create_Plane(int size)
    {
        GameObject Plane_1 = GameObject.CreatePrimitive(PrimitiveType.Plane);
        Plane_1.transform.localScale = new Vector3(size, 5, size);
    }

    void MainCamera_Init(float R_X, float R_Y, float R_Z, float P_X, float P_Y, float P_Z)
    {
        Camera.main.transform.position = new Vector3((float)P_X, (float)P_Y, (float)P_Z);
        Camera.main.transform.Rotate((float)R_X, (float)R_Y, (float)R_Z);
    }
 
    void Create_MHB(float D, float Long, float R_X, float R_Y, float R_Z, float P_X, float P_Y, float P_Z)
    {
        GameObject MHB = new GameObject();
        MHB.name = "MHB";

        GameObject cylinder_1 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        cylinder_1.transform.localScale = new Vector3((float)D, Long, (float)D);
        cylinder_1.transform.Rotate((float)54.74, (float)45, 0);
        cylinder_1.transform.parent = MHB.transform;

        GameObject cylinder_2 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        cylinder_2.transform.localScale = new Vector3((float)D, Long, (float)D);
        cylinder_2.transform.Rotate((float)54.74, (float)-45, (float)0);
        cylinder_2.transform.parent = MHB.transform;

        GameObject cylinder_3 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        cylinder_3.transform.localScale = new Vector3((float)D, Long, (float)D);
        cylinder_3.transform.Rotate((float)-54.74, (float)45, (float)0);
        cylinder_3.transform.parent = MHB.transform;

        GameObject cylinder_4 = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        cylinder_4.transform.localScale = new Vector3((float)D, Long, (float)D);
        cylinder_4.transform.Rotate((float)-54.74, (float)-45, (float)0);
        cylinder_4.transform.parent = MHB.transform;

        MHB.AddComponent<Rigidbody>();

        MHB.transform.position = new Vector3(P_X, P_Y, P_Z);
        MHB.transform.Rotate(R_X,R_Y,R_Z);        
    }

    void OnGUI()
    {
        //Create a Button with Click Event  
        if (GUI.Button(new Rect(0, 10, 100, 30), "Hello MHB"))
        {
            //System.Console.WriteLine("hello world");
            print("Hello MHB!");
            // Debug.Log("up.up");      

        }


    }


    void LoadData()
    {
        FileStream aFile = new FileStream(@"C:\Users\kongq\Desktop\Unity Project\Start 201711\New Unity Project 2\hello.txt", FileMode.OpenOrCreate);

        StreamWriter sw = new StreamWriter(aFile);

        sw.WriteLine("为今后我们之间的进一步合作，");

        sw.WriteLine("为我们之间日益增进的友谊，");

        sw.Write("为朋友们的健康幸福，");

        sw.Write("干杯！朋友！");

        sw.Close();

    }
}

