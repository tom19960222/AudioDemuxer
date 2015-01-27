using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AudioDemuxer
{
    public class Tools
    {
        
        public class MP4Box 
        {
            public static string ToolsBaseDirectory { get { return Environment.CurrentDirectory + "\\tools\\mp4box\\"; } }
            public static string BinaryFileName = "mp4box.exe";
            public static string CommandBuilder(ListDictionary TrackID_SourceFileName_Pair)
            {
                string Command = String.Empty;
                foreach (DictionaryEntry Pair in TrackID_SourceFileName_Pair)
                    Command += String.Format("\"{0}{1}\" -raw {2} \"{3}\"{4}", ToolsBaseDirectory, BinaryFileName, Pair.Key.ToString(), Pair.Value.ToString(), Environment.NewLine);
                return Command;
            }
        }
        public class MKVExtract 
        {
            public static string ToolsBaseDirectory { get { return Environment.CurrentDirectory + "\\tools\\mkvmerge\\"; } }
            public static string BinaryFileName = "mkvextract.exe";
            public static string Parameter = "tracks";
            public static string CommandBuilder(ListDictionary TrackID_OutputFileName_Pair, string SourceFileName)
            {
                string Command = String.Empty;
                Command += String.Format("\"{0}{1}\" {2} \"{3}\"", ToolsBaseDirectory, BinaryFileName, Parameter, SourceFileName);
                foreach (DictionaryEntry Pair in TrackID_OutputFileName_Pair)
                    Command += String.Format(" {0}:\"{1}\" ", (int.Parse((string)Pair.Key)-1).ToString(), Pair.Value.ToString()); 
                return Command;
            }
        }
        class eac3to { }
    }
}
