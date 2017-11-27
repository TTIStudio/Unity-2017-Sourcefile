using UnityEngine;
using System.Collections;



public class Main : MonoBehaviour {

    int time = 0;   
    int MHB_size_updatetime = 0;
    int MHB_size = 0;

    int MaxFrameNumber = 4000;

    System.Random rd = new System.Random();
    
    // Use this for initialization
    void Start()
    {       
        Create_Plane(10);
       
    }
	// Update is called once per frame
	void Update () {

         
        if (MaxFrameNumber > 0)
        {
            Task_CreatMHB();//5 MHB per time
        }
        MaxFrameNumber--;  
    }

    void Task_CreatMHB()
    {
        time++;

        if (time > 40)
        {
            MHB_size_updatetime--;
            if(MHB_size_updatetime<1)
            {
                MHB_size++;
                MHB_size_updatetime = 1;
            }
            
            time = 0;

            float MHB_size_D = (float)(0.1- MHB_size*0.01);
            float MHB_size_Long = (float)(2 - MHB_size * 0.2);
            Create_MHB_2((float)(MHB_size_D), (float)(MHB_size_Long), 0, 0, 0, 0, 51, 0);
            Create_MHB_2((float)(MHB_size_D), (float)(MHB_size_Long), 0, 0, 0, 5, 52, 5);
            Create_MHB_2((float)(MHB_size_D), (float)(MHB_size_Long), 0, 0, 0, -5, 53, -5);
            Create_MHB_2((float)(MHB_size_D), (float)(MHB_size_Long), 0, 0, 0, 5, 54, -5);
            Create_MHB_2((float)(MHB_size_D), (float)(MHB_size_Long), 0, 0, 0, -5, 55, 5);
        }
    }


    void Create_Plane(int size)
    {
        GameObject Plane_1 = GameObject.CreatePrimitive(PrimitiveType.Plane);
        Plane_1.transform.localScale = new Vector3(size, 1, size);
    }


    void Creat_MHBs()
    {
        for (int i = 1; i < 50; i++)
        {
            Create_MHB_2((float)(0.1 + 0.01 * i), (float)(2 + 0.2 * i), 0, 0, 0, 0, 100 - 2 * i, 0);
        }
    }
 
    void Create_MHB_2(float D, float Long, float R_X, float R_Y, float R_Z, float P_X, float P_Y, float P_Z)
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
}
