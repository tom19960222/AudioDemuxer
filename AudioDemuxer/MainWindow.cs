using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MediaInfoLib;
using AudioDemuxer;

namespace AudioDemuxer
{
    public partial class form_MainWindow : Form
    {
        MovieFile nowFile;
        string Command = String.Empty;

        public void init_Form()
        {
            gridview_Tracks.Rows.Clear();
            txt_CommandLine.Text = String.Empty;
        }

        public void Analyse_File(string FilePath)
        {
            init_Form();
            for (int i = 0; i < nowFile.AudioTracksCount; i++)
                gridview_Tracks.Rows.Add(false, nowFile.getTrackID(StreamKind.Audio, i), nowFile.getAudioTrackFormat(i), nowFile.getAudioBitDepth(i), nowFile.getAudioChannels(i));
        }

        public form_MainWindow()
        {
            InitializeComponent();
        }

        private void btn_Browse_Click(object sender, EventArgs e)
        {
            OFD_InputFile.ShowDialog();
        }

        private void OFD_InputFile_FileOk(object sender, CancelEventArgs e)
        {
            nowFile = new MovieFile(OFD_InputFile.FileName);
            Analyse_File(OFD_InputFile.FileName);
        }

        private void btn_Start_Click(object sender, EventArgs e)
        {
            ListDictionary TrackID_SourceFileName = new ListDictionary();
            ListDictionary TrackID_OutputFileName = new ListDictionary();
            string OutputFormat = "flac";
            IDemuxProcess DP;
            switch(nowFile.FileExtension.ToUpper())
            {       
                case "MP4":
                    foreach(DataGridViewRow Row in gridview_Tracks.Rows)
                    {
                        DataGridViewCheckBoxCell CCell = Row.Cells[0] as DataGridViewCheckBoxCell;
                        if ((bool)CCell.Value)
                            TrackID_SourceFileName.Add(Row.Cells[1].Value, nowFile.FileName);
                    }
                    txt_CommandLine.Text = AudioDemuxer.Tools.MP4Box.CommandBuilder(TrackID_SourceFileName);
                    DP = new Tools.MP4Box.MP4DemuxProcess(TrackID_SourceFileName);
                    DP.Start();
                    DP.Dispose();
                break;
                case "MKV":
                    foreach(DataGridViewRow Row in gridview_Tracks.Rows)
                    {
                        DataGridViewCheckBoxCell CCell = Row.Cells[0] as DataGridViewCheckBoxCell;
                        if ((bool)CCell.Value)
                            TrackID_OutputFileName.Add(Row.Cells[1].Value, String.Format("{0}\\{1}-track{2}.{3}",nowFile.FileFolder, nowFile.SafeFileNameWithoutExtension, Row.Cells[1].Value.ToString(), Row.Cells[2].Value.ToString().ToLower()));
                    }
                    txt_CommandLine.Text = AudioDemuxer.Tools.MKVExtract.CommandBuilder(TrackID_OutputFileName, nowFile.FileName);
                    DP = new Tools.MKVExtract.MKVDemuxProcess(TrackID_OutputFileName, nowFile.FileName);
                    DP.Start();
                    DP.Dispose();
                break;
                case "M2TS":
                    foreach(DataGridViewRow Row in gridview_Tracks.Rows)
                    {
                        DataGridViewCheckBoxCell CCell = Row.Cells[0] as DataGridViewCheckBoxCell;
                        if ((bool)CCell.Value)
                        {
                            int OutputTrackID = Row.Cells[1].RowIndex + nowFile.VideoTracksCount + 1;
                            TrackID_OutputFileName.Add(OutputTrackID.ToString(), String.Format("{0}\\{1}-track{2}.{3}", nowFile.FileFolder, nowFile.SafeFileNameWithoutExtension, OutputTrackID.ToString(), OutputFormat));
                        }
                    }
                    txt_CommandLine.Text = AudioDemuxer.Tools.eac3to.CommandBuilder(TrackID_OutputFileName, nowFile.FileName);
                    //txt_CommandLine.Text = AudioDemuxer.Tools.eac3to.AnalyseByEac3to(nowFile.FileName);
                    DP = new Tools.eac3to.M2TSDemuxProcess(TrackID_OutputFileName, nowFile.FileName);
                    DP.Start();
                    DP.Dispose();
                break;
                case "TS":
                    foreach(DataGridViewRow Row in gridview_Tracks.Rows)
                    {
                        DataGridViewCheckBoxCell CCell = Row.Cells[0] as DataGridViewCheckBoxCell;
                        if ((bool)CCell.Value)
                        {
                            int OutputTrackID = Row.Cells[1].RowIndex + nowFile.VideoTracksCount + 1;
                            TrackID_OutputFileName.Add(OutputTrackID.ToString(), String.Format("{0}\\{1}-track{2}.{3}", nowFile.FileFolder, nowFile.SafeFileNameWithoutExtension, OutputTrackID.ToString(), Row.Cells[2].Value.ToString().ToLower()));
                        }
                    }
                    txt_CommandLine.Text = AudioDemuxer.Tools.eac3to.CommandBuilder(TrackID_OutputFileName, nowFile.FileName);
                    //txt_CommandLine.Text = AudioDemuxer.Tools.eac3to.AnalyseByEac3to(nowFile.FileName);
                    DP = new Tools.eac3to.TSDemuxProcess(TrackID_OutputFileName, nowFile.FileName);
                    DP.Start();
                    DP.Dispose();
                break;
            }
            
        }
    }
}
