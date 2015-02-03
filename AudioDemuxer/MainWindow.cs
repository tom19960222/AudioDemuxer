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
        int RunningProcess = 0;

        public void init_Form()
        {
            gridview_Tracks.Rows.Clear();
            txt_CommandLine.Text = String.Empty;
            lv_WaitingFileList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }

        public void OutputAudioTracksInfoToGridview(List<AudioTrackInfo> InfoList)
        {
            gridview_Tracks.Rows.Clear();
            foreach (AudioTrackInfo Info in InfoList)
                gridview_Tracks.Rows.Add(true, Info.TrackID.ToString(), Info.Format, Info.Depth.ToString(), Info.Channels.ToString());
        }

        private void ProcessFile(MovieFile File, bool ReadSelectionFromGridview)
        {
            ListDictionary TrackID_SourceFileName = new ListDictionary();
            ListDictionary TrackID_OutputFileName = new ListDictionary();
            string OutputFormat = "flac";
            IDemuxProcess DP;
            switch (File.FileExtension.ToUpper())
            {
                case "MP4":
                    if (ReadSelectionFromGridview)
                    {
                        foreach (DataGridViewRow Row in gridview_Tracks.Rows)
                        {
                            DataGridViewCheckBoxCell CCell = Row.Cells[0] as DataGridViewCheckBoxCell;
                            if ((bool)CCell.Value)
                                TrackID_SourceFileName.Add(Row.Cells[1].Value, File.FileName);
                        }
                    }
                    else
                    {
                        foreach (AudioTrackInfo Info in File.AudioTrackInfoList)
                            TrackID_SourceFileName.Add(Info.TrackID, File.FileName);
                    }
                    txt_CommandLine.Text = AudioDemuxer.Tools.MP4Box.CommandBuilder(TrackID_SourceFileName);
                    DP = new Tools.MP4Box.MP4DemuxProcess(TrackID_SourceFileName);
                    DP.Start();
                    DP.Dispose();
                    break;
                case "MKV":
                    if (ReadSelectionFromGridview)
                    {
                        foreach (DataGridViewRow Row in gridview_Tracks.Rows)
                        {
                            DataGridViewCheckBoxCell CCell = Row.Cells[0] as DataGridViewCheckBoxCell;
                            if ((bool)CCell.Value)
                                TrackID_OutputFileName.Add(Row.Cells[1].Value, String.Format("{0}\\{1}-track{2}.{3}", File.FileFolder, File.SafeFileNameWithoutExtension, Row.Cells[1].Value.ToString(), Row.Cells[2].Value.ToString().ToLower()));
                        }
                    }
                    else
                    {
                        foreach (AudioTrackInfo Info in File.AudioTrackInfoList)
                            TrackID_OutputFileName.Add(Info.TrackID, String.Format("{0}\\{1}-track{2}.{3}", File.FileFolder, File.SafeFileNameWithoutExtension, Info.TrackID, Info.Format.ToLower()));
                    }
                    txt_CommandLine.Text = AudioDemuxer.Tools.MKVExtract.CommandBuilder(TrackID_OutputFileName, File.FileName);
                    DP = new Tools.MKVExtract.MKVDemuxProcess(TrackID_OutputFileName, File.FileName);
                    DP.Start();
                    DP.Dispose();
                    break;
                case "M2TS":
                    if (ReadSelectionFromGridview)
                    {
                        foreach (DataGridViewRow Row in gridview_Tracks.Rows)
                        {
                            DataGridViewCheckBoxCell CCell = Row.Cells[0] as DataGridViewCheckBoxCell;
                            if ((bool)CCell.Value)
                            {
                                int OutputTrackID = Row.Cells[1].RowIndex + File.VideoTracksCount + 1;
                                TrackID_OutputFileName.Add(OutputTrackID.ToString(), String.Format("{0}\\{1}-track{2}.{3}", File.FileFolder, File.SafeFileNameWithoutExtension, OutputTrackID.ToString(), OutputFormat));
                            }
                        }
                    }
                    else
                    {
                        foreach (AudioTrackInfo Info in File.AudioTrackInfoList)
                            TrackID_OutputFileName.Add(Info.Index.ToString(), String.Format("{0}\\{1}-track{2}.{3}", File.FileFolder, File.SafeFileNameWithoutExtension, Info.Index.ToString(), OutputFormat));
                    }
                    txt_CommandLine.Text = AudioDemuxer.Tools.eac3to.CommandBuilder(TrackID_OutputFileName, File.FileName);
                    //txt_CommandLine.Text = AudioDemuxer.Tools.eac3to.AnalyseByEac3to(nowFile.FileName);
                    DP = new Tools.eac3to.M2TSDemuxProcess(TrackID_OutputFileName, File.FileName);
                    DP.Start();
                    DP.Dispose();
                    break;
                case "TS":
                    if (ReadSelectionFromGridview)
                    {
                        foreach (DataGridViewRow Row in gridview_Tracks.Rows)
                        {
                            DataGridViewCheckBoxCell CCell = Row.Cells[0] as DataGridViewCheckBoxCell;
                            if ((bool)CCell.Value)
                            {
                                int OutputTrackID = Row.Cells[1].RowIndex + File.VideoTracksCount + 1;
                                TrackID_OutputFileName.Add(OutputTrackID.ToString(), String.Format("{0}\\{1}-track{2}.{3}", File.FileFolder, File.SafeFileNameWithoutExtension, OutputTrackID.ToString(), Row.Cells[2].Value.ToString().ToLower()));
                            }
                        }
                    }
                    else
                    {
                        foreach (AudioTrackInfo Info in File.AudioTrackInfoList)
                            TrackID_OutputFileName.Add(Info.Index.ToString(), String.Format("{0}\\{1}-track{2}.{3}", File.FileFolder, File.SafeFileNameWithoutExtension, Info.Index.ToString(), Info.Format.ToLower()));
                    }
                    txt_CommandLine.Text = AudioDemuxer.Tools.eac3to.CommandBuilder(TrackID_OutputFileName, File.FileName);
                    //txt_CommandLine.Text = AudioDemuxer.Tools.eac3to.AnalyseByEac3to(nowFile.FileName);
                    DP = new Tools.eac3to.TSDemuxProcess(TrackID_OutputFileName, File.FileName);
                    DP.Start();
                    DP.Dispose();
                    break;
            }
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
            OutputAudioTracksInfoToGridview(nowFile.AudioTrackInfoList);
        }

        private void btn_Start_Click(object sender, EventArgs e)
        {
            ProcessFile(nowFile, true);
        }
    }
}
