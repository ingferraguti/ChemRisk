using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security;

namespace Movarisch1
{
    public static class DbConf
    {

        static private String createID()
        {
            using (FileStream streamID = File.Open(Utils.pathDatabase() + DbConf.file(), FileMode.CreateNew))
            using (StreamWriter w = new StreamWriter(streamID))
            {
                Random rnd = new Random();
                int num = rnd.Next();

                string protoId = DateTime.Now.ToString("dd-MM-yyyy") + num.ToString() + DbConf.wow();
                
                /*
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(protoId);

                using (var provider = System.Security.Cryptography.MD5.Create())
                {
                StringBuilder builder = new StringBuilder();  
               
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                String id = Convert.ToHexString(hashBytes);
                }
                 * */
                String id = DbConf.MD5(protoId);
                w.Write(id);

                //streamID.Close;
                //w.Close;

                return id;
            }
        }

        static public String getID()
        {

            if (File.Exists(Utils.pathDatabase() + DbConf.file()))
            {
                using (FileStream streamId = File.Open(Utils.pathDatabase() + DbConf.file(), FileMode.Open, FileAccess.Read, FileShare.Read))
                using (StreamReader r = new StreamReader(streamId))
                {
                    String Id = r.ReadLine();

                    streamId.Close();
                    r.Close();

                    return Id;

                }
            }
            else {
                return DbConf.createID();

                //USE DbConf.getID();
            }
            return "---";
        }



        public static string MD5(this string s)
        {
            using (var provider = System.Security.Cryptography.MD5.Create())       
            {
                StringBuilder builder = new StringBuilder();                           

                foreach (byte b in provider.ComputeHash(Encoding.UTF8.GetBytes(s)))
                builder.Append(b.ToString("x2").ToLower());
           
                return builder.ToString();      
            }
        }

        public static string wow() { return "CZZ1NQL0NNF4F1GL1"; }
        private static string file() { return "id.mvrc"; }
        public static bool test() { return true; }

    }
}
