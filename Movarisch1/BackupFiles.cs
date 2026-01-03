using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Movarisch1
{
    class BackupFiles
    {

        /*
         * Utils.pathDatabase()pathBackup
         * */
        public List<string> listAll() {
            DirectoryInfo d = new DirectoryInfo(Utils.pathBackup());
            FileInfo[] Files = d.GetFiles("*.mvrc.bk");
            List<string> str = new List<string>();

            foreach (FileInfo file in Files)
            {
                str.Add(file.Name);
            }
            return str;
        }

        /*
        public List<string> filter() { 
            
        }
        */



        class file {
            public string name;

        }

    }




}
