using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MediaInfoLib;

namespace MediaInfoLib
{
    public partial class MediaInfo
    {
        public int VideoTracksCount 
        {
            get { return this.Count_Get(StreamKind.Video); }
        }

        public int AudioTracksCount
        {
            get { return this.Count_Get(StreamKind.Audio); }
        }

        public int SubTracksCount
        {
            get { return this.Count_Get(StreamKind.Text); }
        }

        public int GeneralTracksCount
        {
            get { return this.Count_Get(StreamKind.General); }
        }

        public int OtherTracksCount
        {
            get { return this.Count_Get(StreamKind.Other); }
        }

        private string getAudioInfo (int TrackNumber, string Parameter)
        {
            if (TrackNumber >= this.AudioTracksCount)
                return "NumberOutOfRange";
            else
                return this.Get(StreamKind.Audio, TrackNumber, Parameter); 
        }

        public string getAudioTrackFormat(int TrackNumber)
        {
            return getAudioInfo(TrackNumber, "Format");
        }

        public string getAudioBitDepth(int TrackNumber)
        {
            return getAudioInfo(TrackNumber, "BitDepth/String");
        }

        public string getAudioChannels(int TrackNumber)
        {
            return getAudioInfo(TrackNumber, "Channels");
        }

        public string getVideoTrackFormat(int TrackNumber)
        {
            if (TrackNumber >= this.VideoTracksCount)
                return "NumberOutOfRange";
            else
                return this.Get(StreamKind.Video, TrackNumber, "Format"); 
        }

    }
}
