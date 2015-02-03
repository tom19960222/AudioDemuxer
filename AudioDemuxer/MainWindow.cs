using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MediaInfoLib;
using AudioDemuxer;

namespace AudioDemuxer
{
    public partial class form_MainWindow : Form
    {
        delegate bool TabpageFocusedCallback();
        delegate ListViewItem getFirstPendingListViewItemCallback();
        delegate void ListViewItemSetTextCallback(ListViewItem LVI, string text);
        MovieFile nowFile;
        string Command = String.Empty;
        List<Thread> RunningProcesses;
        string[] SupportedFileFormats = new string[] { "MP4", "MKV", "TS", "M2TS" };
        Thread MainThread, JoinThread, RemoveThread;

        public form_MainWindow()
        {
            InitializeComponent();
            init_Form();
            tabpage_BatchDemuxAll.Enter += tabpage_BatchDemuxAll_GotFocus;
            RunningProcesses = new List<Thread>();
            MainThread = Thread.CurrentThread;
        }

        public void init_Form()
        {
            gridview_Tracks.Rows.Clear();
            txt_CommandLine.Text = String.Empty;
            lv_WaitingFileList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }

        void tabpage_BatchDemuxAll_GotFocus(object sender, EventArgs e)
        {
            JoinThread = new Thread(MonitorAndJoinRunningProcess);
            RemoveThread = new Thread(MonitorAndRemoveRunningProcess);
            JoinThread.IsBackground = true;
            RemoveThread.IsBackground = true;
            JoinThread.Start();
            RemoveThread.Start();
        }

        bool isTabpageFocused()
        {
            if (this.tabpage_BatchDemuxAll.InvokeRequired)
            {
                TabpageFocusedCallback C = new TabpageFocusedCallback(isTabpageFocused);
                return (bool)this.Invoke(C);
            }
            else
                return tabControl.SelectedTab.Equals(tabpage_BatchDemuxAll);
        }

        ListViewItem getFirstPendingListViewItem()
        {
            if (this.lv_WaitingFileList.InvokeRequired)
            {
                getFirstPendingListViewItemCallback C = new getFirstPendingListViewItemCallback(getFirstPendingListViewItem);
                return (ListViewItem)this.Invoke(C);
            }
            else
            {
                for (int i = 0; i < lv_WaitingFileList.Items.Count; i++)
                {
                    if (lv_WaitingFileList.Items[i].Text.Equals("Pending"))
                        return lv_WaitingFileList.Items[i];
                }
                return null;
            }
        }

        void setListViewItemText(ListViewItem LVI, string text)
        {
            if (this.lv_WaitingFileList.InvokeRequired)
            {
                ListViewItemSetTextCallback C = new ListViewItemSetTextCallback(setListViewItemText);
                this.Invoke(C, new object[] { LVI , text});
            }
            else
                LVI.Text = text;
        }

        private void MonitorAndJoinRunningProcess()
        {
            while (isTabpageFocused())
            {
                Thread.Sleep(500);
                lock (RunningProcesses)
                {
                    if (RunningProcesses.Count < numupdonwn_Parallel_Process.Value)
                    {
                        ListViewItem LVI = getFirstPendingListViewItem();
                        if (LVI == null) continue;
                        Thread T = new Thread(() => ProcessFile(new MovieFile(LVI.SubItems[1].Text), false));
                        RunningProcesses.Add(T);
                        setListViewItemText(LVI, "Started");
                        T.Start();
                    }
                }
            }
        }

        private void MonitorAndRemoveRunningProcess()
        {
            while (isTabpageFocused())
            {
                Thread.Sleep(500);
                lock (RunningProcesses)
                {
                    for (int i = 0; i < RunningProcesses.Count; i++)
                    {
                        if (!RunningProcesses[i].IsAlive)
                            RunningProcesses.RemoveAt(i);
                    }
                }
            }
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
                    //txt_CommandLine.Text = AudioDemuxer.Tools.MP4Box.CommandBuilder(TrackID_SourceFileName);
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
                    //txt_CommandLine.Text = AudioDemuxer.Tools.MKVExtract.CommandBuilder(TrackID_OutputFileName, File.FileName);
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
                    //txt_CommandLine.Text = AudioDemuxer.Tools.eac3to.CommandBuilder(TrackID_OutputFileName, File.FileName);
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
                    //txt_CommandLine.Text = AudioDemuxer.Tools.eac3to.CommandBuilder(TrackID_OutputFileName, File.FileName);
                    //txt_CommandLine.Text = AudioDemuxer.Tools.eac3to.AnalyseByEac3to(nowFile.FileName);
                    DP = new Tools.eac3to.TSDemuxProcess(TrackID_OutputFileName, File.FileName);
                    DP.Start();
                    DP.Dispose();
                    break;
            }
            if (Thread.CurrentThread != MainThread)
            { Thread.Sleep(3000); Thread.CurrentThread.Abort(); }
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

        private void lv_WaitingFileList_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.All;
            else
                e.Effect = DragDropEffects.None;
        }

        private void lv_WaitingFileList_DragDrop(object sender, DragEventArgs e)
        {
            string[] DragDropFiles = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            foreach (string File in DragDropFiles)
            {
                MovieFile tempFile = new MovieFile(File);
                foreach(string SupportedFileFormat in SupportedFileFormats)
                {
                    if (SupportedFileFormat.Equals(tempFile.FileExtension.ToUpper()))
                    {
                        ListViewItem LVI = new ListViewItem("Pending");
                        LVI.SubItems.Add(File);
                        lv_WaitingFileList.Items.Add(LVI);
                    }
                }
            }
            lv_WaitingFileList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }

        private void btn_Clear_Listview_Click(object sender, EventArgs e)
        {
            lv_WaitingFileList.Items.Clear();
        }

        private void btn_Clear_Finished_Click(object sender, EventArgs e)
        {
            /*for(int i = 0; i < lv_WaitingFileList.Items.Count; i++)
            {
                if (lv_WaitingFileList.Items[i].Text.Equals("Started"))
                    lv_WaitingFileList.Items.RemoveAt(i);
            }*/
            foreach(ListViewItem LVI in lv_WaitingFileList.Items)
            {
                if (LVI.Text.Equals("Started"))
                    lv_WaitingFileList.Items.Remove(LVI);
            }
        }

        private void btn_Remove_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lv_WaitingFileList.Items.Count; i++)
            {
                if (lv_WaitingFileList.Items[i].Selected)
                    lv_WaitingFileList.Items.RemoveAt(i);
            }
        }
    }
}
