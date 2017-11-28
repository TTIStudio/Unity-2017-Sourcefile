using UnityEngine;
using System.Collections;

using System;
using System.IO;

// Open File with Windows UI  -- Method 2   -   END
public class Main : MonoBehaviour {
    int MHB_Position_Sclae_for_LOADFILE = 100;
    float[,] MHB_Position = new float[1000,3];
    int MHB_number = 0;
    int MHB_number_count = 0;

    int frame_count=0;
    // Use this for initialization
    void Start()
    {
        LoadData();
        Create_Plane(300);
        MainCamera_Init(20,-50,0,600,750, (float)-700);

    }
	// Update is called once per frame
	void Update () {

        frame_count++;
        if (frame_count > 1)
        {
            PUT_MHB_Task();
            frame_count = 0;
        }
        
    }

    void PUT_MHB_Task()
    {
        if (MHB_number_count < MHB_number)
        {
            Create_MHB((float)1, (float)20, (float)0, (float)0, (float)0, (float)MHB_Position[MHB_number_count,0], (float)MHB_Position[MHB_number_count,2], (float)MHB_Position[MHB_number_count,1]);

            MHB_number_count++;
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

        //MHB.AddComponent<Rigidbody>();

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
        FileStream aFile = new FileStream(@"C:\Users\kongq\Desktop\Unity Project\Start 201711\New Unity Project 2\\putput\all.txt", FileMode.Open);
        BinaryReader Reader = new BinaryReader(aFile);

        long filelength= aFile.Length;
        bool MHB_Reading = false;        
        bool MHB_Sign_Nagivate = false;

        int MHB_index = 0;  // 0-X, 1-Y, 2-Z
        int MHB_data_value_counter = 0;
        float[] MHB_XYZ= { 0,0,0};

        while ((filelength--)>0)
        {   char a= Reader.ReadChar();
            if (a == '{')// start a new MHB
            {   //intilize all flags
                MHB_Reading = true;
                MHB_data_value_counter = 0;
                MHB_index = 0;
                MHB_Sign_Nagivate = false;
                MHB_XYZ[0] = 0;
                MHB_XYZ[1] = 0;
                MHB_XYZ[2] = 0;

                
            }
            else if (a == '}')
            {
                MHB_XYZ[MHB_index] = MHB_XYZ[MHB_index] / (float)Math.Pow(10, MHB_data_value_counter);
                if (MHB_Sign_Nagivate)
                {
                    MHB_XYZ[MHB_index] = -MHB_XYZ[MHB_index];
                    MHB_Sign_Nagivate = false;
                }
                MHB_Reading = false;//end of this MHB
                MHB_data_value_counter = 0;
                //Create_MHB((float)1, (float)20, (float)0, (float)0, (float)0, (float)MHB_XYZ[0], (float)MHB_XYZ[2], (float)MHB_XYZ[1]);
                MHB_Position[MHB_number,0] = MHB_XYZ[0];
                MHB_Position[MHB_number,1] = MHB_XYZ[1];
                MHB_Position[MHB_number,2] = MHB_XYZ[2];
                MHB_number++;
            }
            else if (a == ',')
            {
                MHB_XYZ[MHB_index] = MHB_XYZ[MHB_index] / (float)Math.Pow(10, MHB_data_value_counter);
                if (MHB_Sign_Nagivate)
                {
                    MHB_XYZ[MHB_index] = -MHB_XYZ[MHB_index];
                    MHB_Sign_Nagivate = false;
                }
                MHB_index++;
                MHB_data_value_counter = 0;
            }
            else if (a == ' ' | a == 10 | a == 13) //10: LF, 13:CR
            {//Do NOTHING
            }
            else if (a == '-')
            {
                MHB_Sign_Nagivate = true;
            }
            else if (a == '.')
            {
                MHB_data_value_counter = 0;
            }
            else if (a == '0' | a == '1' | a == '2' | a == '3' | a == '4' | a == '5' | a == '6' | a == '7' | a == '8' | a == '9')//deal the number
            {
                
                if (MHB_Reading == true)//deal this MHB
                {   MHB_data_value_counter++;
                    MHB_XYZ[MHB_index] = MHB_XYZ[MHB_index] * 10 + (a - '0');
                }
            }
            else
            {
                print("AMAZON! What is this? --" + a);
            }
        }

        Reader.Close();
    }
}

