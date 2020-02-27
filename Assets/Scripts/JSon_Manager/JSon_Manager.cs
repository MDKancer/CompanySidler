using System.IO;
using System.Runtime.Serialization.Json;

namespace Signals.JSon_Manager
{
    public class JSon_Manager
    {

        public JSon_Manager()
        {
            
        }

        public void JsonReader()
        {
            string path = null;
#if UNITY_EDITOR
            path = "Assets/Resources/ItemInfo.json";
#endif
#if UNITY_STANDALONE
            // You cannot add a subfolder, at least it does not work for me
            path = "ItemInfo.json";
#endif
  
            string str = "bla bla";
            using (FileStream fs = new FileStream(path, FileMode.Create)){
                using (StreamWriter writer = new StreamWriter(fs)){
                    writer.Write(str);
                }
            }
#if UNITY_EDITOR
            UnityEditor.AssetDatabase.Refresh();
#endif
        }
        
    }
}