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
            private static string ToolsBaseDirectory { get { return Environment.CurrentDirectory + "\\tools\\mp4box\\"; } }
            private static string BinaryFileName = "mp4box.exe";
            public static string ProgramPath { get { return ToolsBaseDirectory + BinaryFileName; } }
            public static string ParameterBuilder(string TrackID, string SourceFileName)
            {
                string Parameter = String.Empty;
                Parameter += String.Format(" -raw {0} \"{1}\"", TrackID, SourceFileName);
                return Parameter;
            }
            public static string CommandBuilder(ListDictionary TrackID_SourceFileName_Pair)
            {
                string Command = String.Empty;
                foreach (DictionaryEntry Pair in TrackID_SourceFileName_Pair)
                    Command += String.Format("\"{0}\" {1}{2}",ProgramPath, ParameterBuilder(Pair.Key.ToString(), Pair.Value.ToString()), Environment.NewLine);
                return Command;
            }
            public class MP4DemuxProcess : IDemuxProcess
            {
                private List<Process> ProcessList;
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
                public void Start()
                {
                    foreach (Process Pro in ProcessList)
                    { Pro.Start(); Pro.WaitForExit(); }
                }
                public void Dispose()
                {
                    foreach (Process P in ProcessList)
                    {
                        P.Dispose();
                    }
                    ProcessList.Clear();
                }
            }
        }
        public class MKVExtract
        {
            private static string ToolsBaseDirectory { get { return Environment.CurrentDirectory + "\\tools\\mkvmerge\\"; } }
            private static string BinaryFileName = "mkvextract.exe";
            private static string ProgramParameter = "tracks";
            public static string ProgramPath { get { return ToolsBaseDirectory + BinaryFileName; } }
            public static string ParameterBuilder(ListDictionary TrackID_OutputFileName_Pair, string SourceFileName)
            {
                string Parameter = String.Empty;
                Parameter += String.Format(" {0} \"{1}\"", ProgramParameter, SourceFileName);
                foreach (DictionaryEntry Pair in TrackID_OutputFileName_Pair)
                    Parameter += String.Format(" {0}:\"{1}\" ", (int.Parse((string)Pair.Key) - 1).ToString(), Pair.Value.ToString());
                return Parameter;
            }
            public static string CommandBuilder(ListDictionary TrackID_OutputFileName_Pair, string SourceFileName)
            {
                return ProgramPath + ParameterBuilder(TrackID_OutputFileName_Pair, SourceFileName);
            }
            public class MKVDemuxProcess : IDemuxProcess
            {
                private Process P;
                public MKVDemuxProcess(ListDictionary TrackID_OutputFileName_Pair, string SourceFileName)
                {
                    P = new Process();
                    P.StartInfo.FileName = ProgramPath;
                    P.StartInfo.Arguments = ParameterBuilder(TrackID_OutputFileName_Pair, SourceFileName);
                }
                public void Start()
                {
                    P.Start();
                    P.WaitForExit();
                }
                public void Dispose()
                {
                    P.Dispose();
                }
            }
        }
        public class eac3to
        {
            private static string ToolsBaseDirectory { get { return Environment.CurrentDirectory + "\\tools\\eac3to\\"; } }
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
            public class M2TSDemuxProcess : IDemuxProcess
            {
                Process P;
                public M2TSDemuxProcess(ListDictionary TrackID_OutputFileName_Pair, string SourceFileName)
                {
                    P = new Process();
                    P.StartInfo.FileName = ProgramPath;
                    P.StartInfo.Arguments = ParameterBuilder(TrackID_OutputFileName_Pair, SourceFileName);
                }
                public void Start()
                {
                    P.Start();
                    P.WaitForExit();
                }
                public void Dispose()
                {
                    P.Dispose();
                }
            }
            public class TSDemuxProcess : IDemuxProcess
            {
                Process P;
                public TSDemuxProcess(ListDictionary TrackID_OutputFileName_Pair, string SourceFileName)
                {
                    P = new Process();
                    P.StartInfo.FileName = ProgramPath;
                    P.StartInfo.Arguments = ParameterBuilder(TrackID_OutputFileName_Pair, SourceFileName);
                }
                public void Start()
                {
                    P.Start();
                    P.WaitForExit();
                }
                public void Dispose()
                {
                    P.Dispose();
                }
            }

        }
    }
}
