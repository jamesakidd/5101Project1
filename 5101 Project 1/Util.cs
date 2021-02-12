using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection.PortableExecutable;

namespace _5101_Project_1
{
    public class Util
    {
        // Path
        private const string dataPath = @"..\..\..\Data";

        // File names
        private FileInfo[] files;
        // File exts

        // Prints the title
        public String PrintTitle()
        {
            string str = "";
            // Print Title
            str += "---------------------\n";
            str += "Program functionality\n";
            str += "---------------------\n\n";
            return str;
        }

        // Prints the Instructions
        public String PrintInstructions()
        {
            string str = "";
            // Print Title
            str += "- To exit program, enter 'exit' at any prompt.\n";
            str += "- To start program from the begining enter 'restart' at any prompt.\n";
            str += "- You will be presented with a numbered list of options.\n";
            str += "  Please enter a value, when prompted, to a coresponding file name, file type or data query routine.\n";
            return str;
        }

        // Get available files
        public String GetFiles()
        {
            files = new DirectoryInfo(dataPath).GetFiles();

            string str = "";
            int count = 1;
            foreach (var f in files) {
                str += count + ") " + f.Name + "\n";
                ++count;
            }
            
            return str;
        }

    }
}
