using UnityEngine;
using System.Collections;


// Open File with Windows UI  -- Method 2
using System;
using System.IO;


using System.Threading;

// Open File with Windows UI  -- Method 2   -   END
public class Main : MonoBehaviour {

    int time = 0;   
    int MHB_size_updatetime = 0;
    int MHB_size = 0;
    int MaxFrameNumber = 4000;
    System.Random rd = new System.Random();
    Thread thread;

    int MHB_Position_Sclae_for_LOADFILE = 100;
    // Use this for initialization
    void Start()
    {
        //LoadData();
        Create_Plane(300);
        MainCamera_Init(10,0,0,0,10, (float)-30);

        thread = new Thread(LoadData);
        thread.IsBackground = true;
        thread.Start();
    }
	// Update is called once per frame
	void Update () {


        //if (MaxFrameNumber > 0)
        //{
        //    MaxFrameNumber--;
        //    Task_CreatMHB();//5 MHB per time
        //}

        

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
        FileStream aFile = new FileStream(@"C:\Users\kongq\Desktop\Unity Project\Start 201711\New Unity Project 2\\putput\1.txt", FileMode.Open);

        //try
        //{
        BinaryReader Reader = new BinaryReader(aFile);
        //}
        //catch (IOException e)
        //{
        //    print(e.Message + "\n Cannot open file.");
        //    return;
        //}

        long filelength= aFile.Length;
        bool MHB_Reading = false;        
        bool MHB_Sign_Nagivate = false;

        int MHB_index = 0;  // 0-X, 1-Y, 2-Z
        int MHB_data_value_counter = 0;
        float[] MHB_XYZ= { 0,0,0};

        while ((filelength--)>0)
        {   char a= Reader.ReadChar();
            //print(a);
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
                
                //print(MHB_XYZ[MHB_index].ToString());
                MHB_XYZ[MHB_index] = MHB_XYZ[MHB_index] / (float)Math.Pow(10, MHB_data_value_counter);
                //print(MHB_XYZ[MHB_index].ToString());// wrong
                //print(MHB_data_value_counter.ToString());// work well
                if (MHB_Sign_Nagivate)
                {
                    MHB_XYZ[MHB_index] = -MHB_XYZ[MHB_index];
                    MHB_Sign_Nagivate = false;
                }
                MHB_Reading = false;//end of this MHB
                MHB_data_value_counter = 0;

                //print(MHB_XYZ[0].ToString()+','+ MHB_XYZ[1].ToString() + ','+ MHB_XYZ[2].ToString() + ',');
                //print("%f,%f,%f", MHB_XYZ[0], MHB_XYZ[1], MHB_XYZ[2]);
                Create_MHB((float)1, (float)20, (float)0, (float)0, (float)0, (float)MHB_XYZ[0], (float)MHB_XYZ[2], (float)MHB_XYZ[1]);
            }
            else if (a == ',')
            {
                //print(MHB_XYZ[MHB_index].ToString());
                MHB_XYZ[MHB_index] = MHB_XYZ[MHB_index] / (float)Math.Pow(10, MHB_data_value_counter);
                //print(MHB_XYZ[MHB_index].ToString());// wrong
                //print(MHB_data_value_counter.ToString());// work well
                if (MHB_Sign_Nagivate)
                {
                    MHB_XYZ[MHB_index] = -MHB_XYZ[MHB_index];
                    MHB_Sign_Nagivate = false;
                }


                MHB_index++;
                MHB_data_value_counter = 0;
            }
            else if (a == ' ' | a == 10 | a == 13) //10: LF, 13:CR
            {
                //Do NOTHING
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
                    //print(a+" - "+MHB_index.ToString() + " - " + MHB_XYZ[MHB_index].ToString()); //work well
                }
                //else if (MHB_Reading == false)//Creat this MHB
                //{
                //    }
            }
            else
            {
                print("AMAZON! What is this? --" + a);
            }
            

        }

        Reader.Close();
    }
}

