using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MediaInfoLib;

namespace AudioDemuxer
{
    public partial class form_MainWindow : Form
    {
        MediaInfo MI;
        public form_MainWindow()
        {
            InitializeComponent();
            MI = new MediaInfo();
        }

        private void btn_Browse_Click(object sender, EventArgs e)
        {
            OFD_InputFile.ShowDialog();
        }

        private void OFD_InputFile_FileOk(object sender, CancelEventArgs e)
        {
            gridview_Tracks.Rows.Clear();
            MI.Open(OFD_InputFile.FileName);
            for (int i = 0; i < MI.AudioTracksCount; i++)
            {
                gridview_Tracks.Rows.Add(false, i + 1, MI.getAudioTrackFormat(i), MI.getAudioBitDepth(i), MI.getAudioChannels(i));
            }
        }
    }
}
