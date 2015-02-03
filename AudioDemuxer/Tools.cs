using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using AudioDemuxer;

namespace AudioDemuxer
{
    public class Tools
    {
        public class MP4Box
        {
            private static string ToolsBaseDirectory = Environment.CurrentDirectory + "\\tools\\mp4box\\";
            private static string BinaryFileName = "mp4box.exe";
            public static string ProgramPath { get { return ToolsBaseDirectory + BinaryFileName; } }
            public static string ParameterBuilder(string TrackID, string SourceFileName)
            {
                return String.Format(" -raw {0} \"{1}\"", TrackID, SourceFileName);;
            }
            public static string CommandBuilder(ListDictionary TrackID_SourceFileName_Pair)
            {
                string Command = String.Empty;
                foreach (DictionaryEntry Pair in TrackID_SourceFileName_Pair)
                    Command += String.Format("\"{0}\" {1}{2}",ProgramPath, ParameterBuilder(Pair.Key.ToString(), Pair.Value.ToString()), Environment.NewLine);
                return Command;
            }
            public class MP4DemuxProcess : DemuxProcessUsingMutilCommand, IDemuxProcess
            {
                public MP4DemuxProcess(ListDictionary TrackID_SourceFileName_Pair)
                {
                    ProcessList = new List<Process>();
                    foreach (DictionaryEntry Pair in TrackID_SourceFileName_Pair)
                    {
                        Process P = new Process();
                        P.StartInfo.FileName = ProgramPath;
                        P.StartInfo.Arguments = ParameterBuilder(Pair.Key.ToString(), Pair.Value.ToString());
                        ProcessList.Add(P);
                    }
                }
            }
        }
        public class MKVExtract
        {
            private static string ToolsBaseDirectory = Environment.CurrentDirectory + "\\tools\\mkvmerge\\"; 
            private static string BinaryFileName = "mkvextract.exe";
            private static string ProgramParameter = "tracks";
            public static string ProgramPath { get { return ToolsBaseDirectory + BinaryFileName; } }
            public static string ParameterBuilder(ListDictionary TrackID_OutputFileName_Pair, string SourceFileName)
            {
                string Parameter = String.Empty;
                Parameter += String.Format(" {0} \"{1}\"", ProgramParameter, SourceFileName);
                foreach (DictionaryEntry Pair in TrackID_OutputFileName_Pair)
                    Parameter += String.Format(" {0}:\"{1}\" ", (Convert.ToInt32(Pair.Key) - 1).ToString(), Pair.Value.ToString());
                return Parameter;
            }
            public static string CommandBuilder(ListDictionary TrackID_OutputFileName_Pair, string SourceFileName)
            {
                return ProgramPath + ParameterBuilder(TrackID_OutputFileName_Pair, SourceFileName);
            }
            public class MKVDemuxProcess : DemuxProcessUsingOneCommand, IDemuxProcess
            {
                public MKVDemuxProcess(ListDictionary TrackID_OutputFileName_Pair, string SourceFileName)
                {
                    Pro = new Process();
                    Pro.StartInfo.FileName = ProgramPath;
                    Pro.StartInfo.Arguments = ParameterBuilder(TrackID_OutputFileName_Pair, SourceFileName);
                }
            }
        }
        public class eac3to
        {
            private static string ToolsBaseDirectory = Environment.CurrentDirectory + "\\tools\\eac3to\\";
            private static string BinaryFileName = "eac3to.exe";
            public static string ProgramPath { get { return ToolsBaseDirectory + BinaryFileName; } }
            public static string ParameterBuilder(ListDictionary TrackID_OutputFileName_Pair, string SourceFileName)
            {
                string Parameter = String.Empty;
                Parameter += SourceFileName;
                foreach (DictionaryEntry Pair in TrackID_OutputFileName_Pair)
                    Parameter += String.Format(" {0}:\"{1}\" ", Pair.Key.ToString(), Pair.Value.ToString());
                return Parameter;
            }
            public static string CommandBuilder(ListDictionary TrackID_OutputFileName_Pair, string SourceFileName)
            {
                return ProgramPath + ParameterBuilder(TrackID_OutputFileName_Pair, SourceFileName);
            }
            public class M2TSDemuxProcess : DemuxProcessUsingOneCommand, IDemuxProcess
            {
                public M2TSDemuxProcess(ListDictionary TrackID_OutputFileName_Pair, string SourceFileName)
                {
                    Pro = new Process();
                    Pro.StartInfo.FileName = ProgramPath;
                    Pro.StartInfo.Arguments = ParameterBuilder(TrackID_OutputFileName_Pair, SourceFileName);
                }
            }
            public class TSDemuxProcess : DemuxProcessUsingOneCommand, IDemuxProcess
            {
                public TSDemuxProcess(ListDictionary TrackID_OutputFileName_Pair, string SourceFileName)
                {
                    Pro = new Process();
                    Pro.StartInfo.FileName = ProgramPath;
                    Pro.StartInfo.Arguments = ParameterBuilder(TrackID_OutputFileName_Pair, SourceFileName);
                }
            }

        }
    }
}
