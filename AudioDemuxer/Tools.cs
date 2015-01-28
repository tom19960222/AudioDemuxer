using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

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
                    Command += String.Format(" {0}:\"{1}\" ", (int.Parse((string)Pair.Key) - 1).ToString(), Pair.Value.ToString());
                return Command;
            }
        }
        public class eac3to
        {
            public static string ToolsBaseDirectory { get { return Environment.CurrentDirectory + "\\tools\\eac3to\\"; } }
            public static string BinaryFileName = "eac3to.exe";
            public static string CommandBuilder(ListDictionary TrackID_OutputFileName_Pair, string SourceFileName)
            {
                string Command = String.Empty;
                Command += String.Format("\"{0}{1}\" \"{2}\"", ToolsBaseDirectory, BinaryFileName, SourceFileName);
                foreach (DictionaryEntry Pair in TrackID_OutputFileName_Pair)
                    Command += String.Format(" {0}:\"{1}\" ", Pair.Key.ToString(), Pair.Value.ToString());
                return Command;
            }
            public static string AnalyseByEac3to(string FileName)
            {
                string result = String.Empty;

                Process eac3to_Analyse = new Process();
                eac3to_Analyse.StartInfo.FileName = eac3to.ToolsBaseDirectory + eac3to.BinaryFileName;
                eac3to_Analyse.StartInfo.Arguments = " " + FileName;
                eac3to_Analyse.StartInfo.RedirectStandardOutput = true;
                eac3to_Analyse.StartInfo.UseShellExecute = false;
                eac3to_Analyse.Start();
                result = eac3to_Analyse.StandardOutput.ReadToEnd();
                eac3to_Analyse.WaitForExit();
                return result;
            }
        }
    }
}
