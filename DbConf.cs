using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movarisch1
{
    public static class DbConf
    {

        static private String createID()
        {
            using (FileStream streamID = File.Open(Utils.pathDatabase() + "id.mvrc", FileMode.CreateNew))
            using (StreamWriter w = new StreamWriter(streamID))
            {
                Random rnd = new Random();
                int num = rnd.Next();

                string protoId = DateTime.Now.ToString("dd-MM-yyyy") + num.ToString() + "CZZ1NQL0NNF4F1GL1";
                
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(protoId);
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                String id = Convert.ToHexString(hashBytes);

                w.Write(id);

                streamID.Close;
                w.Close;

                return id;
            }
        }

        static public String getID()
        {
           
            if (File.Exists(Utils.pathDatabase() + "id.mvrc"))
            {
                using (FileStream streamId = File.Open(Utils.pathDatabase() + "id.mvrc", FileMode.Open, FileAccess.Read, FileShare.Read))
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
            return 0;
        }


    }
}
