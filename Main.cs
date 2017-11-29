using UnityEngine;
using System.Collections;

using System;
using System.IO;

// Open File with Windows UI  -- Method 2   -   END
public class Main : MonoBehaviour {
    int MHB_Sclae_for_LOADFILE = 100;
    float[,] MHB_Position = new float[1000,3];      // support MAX 1000 MHB
    float[,] MHB_Config_table = new float[100, 3];  // Support 100 kinds of MHB - - table : diameter, length
    int Config_Number = 0;
    int MHB_number = 0;
    int MHB_number_count = 0;

    int frame_count=0;

    System.String MappingFilePosition = @"C:\Users\kongq\Desktop\Unity Project\Start 201711\Unity-2017-Sourcefile\putput\";
    // Use this for initialization
    void Start()
    {
        print("Loading Config File");
        LoadMHB_ConfigF(MappingFilePosition + "Config.txt");
        Create_Plane(300,-10);
        MainCamera_Init(20,-50,0,600,750, (float)-700);

        //LOAD_MHB_ALL();
    }
    // Update is called once per frame
    void Update () {

        //frame_count++;
        //if (frame_count > 1)
        //{
        //    PUT_MHB_Task();
        //    frame_count = 0;
        //}

    }

    void LOAD_MHB_ALL() //Load MHBs in one Time According to Config File
    {
        print("Loading Config File");
        for (int i=1;i< Config_Number;i++)
        {
            LoadMHBs(MappingFilePosition+i.ToString()+".txt", MHB_Config_table[i,0], MHB_Config_table[i, 1], MHB_Sclae_for_LOADFILE);
        }
    }


    void PUT_MHB_Task()  // used in load MHB one by one
    {
        if (MHB_number_count < MHB_number)
        {
            Create_MHB((float)1, (float)20, (float)0, (float)0, (float)0, (float)MHB_Position[MHB_number_count,0], (float)MHB_Position[MHB_number_count,2], (float)MHB_Position[MHB_number_count,1]);

            MHB_number_count++;
        }
    }
   
    void Create_Plane(int size,float Hight)
    {
        GameObject Plane_1 = GameObject.CreatePrimitive(PrimitiveType.Plane);
        Plane_1.transform.localScale = new Vector3(size, 5, size);
        Plane_1.transform.position = new Vector3(0,(float)Hight,0);
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
    
    //Load MHBs in one time according to a Mapping File
    void LoadMHBs(System.String FilePosition,float D,float Long,int Scale)
    {
        FileStream aFile = new FileStream(FilePosition, FileMode.Open);
        BinaryReader Reader = new BinaryReader(aFile);

        long filelength= aFile.Length;
        bool MHB_Reading = false;        
        bool MHB_Sign_Nagivate = false;
        bool MHB_is_floating = false;

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
                MHB_is_floating = false;
                MHB_XYZ[0] = 0;
                MHB_XYZ[1] = 0;
                MHB_XYZ[2] = 0;

                
            }
            else if (a == '}')
            {
                if (MHB_is_floating)
                {
                    MHB_XYZ[MHB_index] = MHB_XYZ[MHB_index] / (float)Math.Pow(10, MHB_data_value_counter);
                    MHB_is_floating = false;

                }
                if (MHB_Sign_Nagivate)
                {
                    MHB_XYZ[MHB_index] = -MHB_XYZ[MHB_index];
                    MHB_Sign_Nagivate = false;
                }
                MHB_Reading = false;//end of this MHB
                MHB_data_value_counter = 0;


                Create_MHB((float)D/ Scale, (float)Long/ Scale, (float)0, (float)0, (float)0, (float)MHB_XYZ[0]/ Scale, (float)MHB_XYZ[2]/ Scale, (float)MHB_XYZ[1]/ Scale);
                //MHB_Position[MHB_number,0] = MHB_XYZ[0];
                //MHB_Position[MHB_number,1] = MHB_XYZ[1];
                //MHB_Position[MHB_number,2] = MHB_XYZ[2];
                MHB_number++;
            }
            else if (a == ',')
            {
                if (MHB_is_floating)
                {
                    MHB_XYZ[MHB_index] = MHB_XYZ[MHB_index] / (float)Math.Pow(10, MHB_data_value_counter);
                    MHB_is_floating = false;
                }
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
                MHB_is_floating = true;
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


    void LoadMHB_ConfigF(System.String FilePosition)
    {
        FileStream aFile = new FileStream(FilePosition, FileMode.Open);
        BinaryReader Reader = new BinaryReader(aFile);
        long filelength = aFile.Length;

        bool FLAG_IN_Comment = true;
        bool FLAG_IN_Config = false;
        bool FLAG_IS_NUMBER = false;
        bool FLAG_IS_FLOATING = false;
        int Count_comma = 0;
        int Count_conf = 0;
        int Count_filename = 0;
        
        while ((filelength--) > 0)
        {
            char a = Reader.ReadChar();

            FLAG_IS_NUMBER = false;
            switch (a)
            {
                case '/': FLAG_IN_Comment = true; break;
                case (char)10:
                case (char)13:
                    if (FLAG_IN_Comment == true)
                    {
                        FLAG_IN_Comment = false;
                    } break;
                case '{'://start a new configure  -
                    FLAG_IN_Config = true;
                    Count_conf =0;
                    Count_comma = 0;
                    FLAG_IS_NUMBER = false;
                    FLAG_IS_FLOATING = false;
                    break;
                case '}':
                    if (FLAG_IS_FLOATING)
                    {
                        MHB_Config_table[Count_filename, Count_conf - 1] = MHB_Config_table[Count_filename, Count_conf - 1] / (float)(Math.Pow(10, Count_comma));
                        Count_comma = 0;
                        FLAG_IS_FLOATING = false;
                    }
                    FLAG_IN_Config = false;
                    if(!FLAG_IN_Comment) Config_Number++;
                    //Count_comma = 0;
                    break;//end of this configure

                case ',':
                    if (FLAG_IS_FLOATING)
                    {
                        MHB_Config_table[Count_filename, Count_conf - 1] = MHB_Config_table[Count_filename, Count_conf - 1] / (float)(Math.Pow(10, Count_comma));
                        Count_comma = 0;
                        FLAG_IS_FLOATING = false;
                    }
                    Count_conf ++;
                    
                    break;
                case ':': Count_conf=1; break;
                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                    Count_comma++;
                    FLAG_IS_NUMBER = true;
                    break;
                case '.': FLAG_IS_FLOATING = true; Count_comma = 0; break;
                default:break;//Do nothing
            }

            //if ((FLAG_IN_Comment & FLAG_IN_Config) == true) { print("ERROR: something went wrong!"); }
            if(FLAG_IN_Config&(!FLAG_IN_Comment)&(FLAG_IS_NUMBER))
            {
                switch (Count_conf)
                {
                    case 0: Count_filename = a - '0'; break;
                    case 1: 
                    case 2:
                        MHB_Config_table[Count_filename, Count_conf - 1] = MHB_Config_table[Count_filename, Count_conf - 1] * 10 + a - '0';
                        break;
                    case 3:
                        MHB_Config_table[Count_filename, Count_conf-1] = a - '0';
                        break;
                    default:print("ERROR: something went wrong!"); break;
                }

            }

        }

        //report
        print("find "+Config_Number.ToString()+ " different kinds MHBs");


        for (int i = 0; i < Config_Number+1; i++)
        {
            print(MHB_Config_table[i, 0].ToString() + ',' + MHB_Config_table[i, 1].ToString() + ',' + MHB_Config_table[i, 2].ToString());
        }
        Reader.Close();
    }
}

