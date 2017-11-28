using UnityEngine;
using System.Collections;


// Open File with Windows UI  -- Method 1
// for OpenFileName Class
using System;
using System.Runtime.InteropServices;
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
// Open File with Windows UI  -- Method 1   -   END
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



    // from : https://www.cnblogs.com/qingjoin/p/3630505.html
    void OnGUI()
    {
        //Create a Button with Click Event  
        if (GUI.Button(new Rect(0, 10, 100, 30), "Open File"))
        {
            //System.Console.WriteLine("hello world");
            print("Hello MHB!");
            // Debug.Log("up.up");



            // Open File with Windows UI  -- Method 1

            OpenFileName ofn = new OpenFileName();


            //ArgumentException: Type OpenFileName cannot be marshaled as an unmanaged structure.
            //Parameter name: t
            ofn.structSize = Marshal.SizeOf(ofn);
            

            ofn.filter = "All Files\0*.*\0\0";

            ofn.file = new string(new char[256]);

            ofn.maxFile = ofn.file.Length;

            ofn.fileTitle = new string(new char[64]);

            ofn.maxFileTitle = ofn.fileTitle.Length;
            string path = Application.streamingAssetsPath;
            path = path.Replace('/', '\\');
            //默认路径  
            ofn.initialDir = path;
            //ofn.initialDir = "D:\\MyProject\\UnityOpenCV\\Assets\\StreamingAssets";  
            ofn.title = "Open Project";

            ofn.defExt = "JPG";//显示文件的类型  
            //注意 一下项目不一定要全选 但是0x00000008项不要缺少  
            ofn.flags = 0x00080000 | 0x00001000 | 0x00000800 | 0x00000200 | 0x00000008;//OFN_EXPLORER|OFN_FILEMUSTEXIST|OFN_PATHMUSTEXIST| OFN_ALLOWMULTISELECT|OFN_NOCHANGEDIR  

            if (WindowDll.GetOpenFileName(ofn))
            {
                Debug.Log("Selected file with full path: {0}" + ofn.file);
            }
            // Open File with Windows UI  -- Method 1   -   END

        }

    }

}




// Open File with Windows UI  -- Method 1
// Ref: http://www.cnblogs.com/U-tansuo/archive/2012/07/10/GetOpenFileName.html
public class OpenFileName
{
    public int structSize = 0;
    public IntPtr dlgOwner = IntPtr.Zero;
    public IntPtr instance = IntPtr.Zero;
    public String filter = null;
    public String customFilter = null;
    public int maxCustFilter = 0;
    public int filterIndex = 0;
    public String file = null;
    public int maxFile = 0;
    public String fileTitle = null;
    public int maxFileTitle = 0;
    public String initialDir = null;
    public String title = null;
    public int flags = 0;
    public short fileOffset = 0;
    public short fileExtension = 0;
    public String defExt = null;
    public IntPtr custData = IntPtr.Zero;
    public IntPtr hook = IntPtr.Zero;
    public String templateName = null;
    public IntPtr reservedPtr = IntPtr.Zero;
    public int reservedInt = 0;
    public int flagsEx = 0;
}

public class WindowDll
{
    [DllImport("Comdlg32.dll", SetLastError = true, ThrowOnUnmappableChar = true, CharSet = CharSet.Auto)]
    public static extern bool GetOpenFileName([In, Out] OpenFileName ofn);
    public static bool GetOpenFileName1([In, Out] OpenFileName ofn)
    {
        return GetOpenFileName(ofn);
    }
}
// Open File with Windows UI  -- Method 1   -   END
