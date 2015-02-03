using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaInfoLib;
using AudioDemuxer;

namespace AudioDemuxer
{
    public class MovieFile
    {
        private MediaInfo MI;
        public List<AudioTrackInfo> AudioTrackInfoList;
        public MovieFile(string FilePath)
        {
            MI = new MediaInfo();
            MI.Open(FilePath);
            _FileName = FilePath;
            AudioTrackInfoList = AnalyzeFile(this);
        }
        private string _FileName;
        public string FileName
        {
            get { return _FileName; }
            set { _FileName = value; MI.Close(); MI.Open(_FileName); }
        }
        public string FileExtension
        {
            get { return MI.Get(StreamKind.General, 0, "FileExtension"); }
        }

        public string SafeFileNameWithoutExtension 
        {
            get { return MI.Get(StreamKind.General, 0, "FileName"); }
        }

        public string FileFolder
        {
            get { return MI.Get(StreamKind.General, 0, "FolderName"); }
        }

        public int AudioTracksCount
        {
            get { return MI.AudioTracksCount; }
        }
        public int VideoTracksCount
        {
            get { return MI.VideoTracksCount; }
        }
        public string getAudioTrackFormat(int TrackNumber)
        {
            return MI.getAudioInfo(TrackNumber, "Format");
        }

        public string getAudioBitDepth(int TrackNumber)
        {
            return MI.getAudioInfo(TrackNumber, "BitDepth/String");
        }

        public string getAudioChannels(int TrackNumber)
        {
            return MI.getAudioInfo(TrackNumber, "Channels");
        }

        public string getVideoTrackFormat(int TrackNumber)
        {
            return MI.getVideoTrackFormat(TrackNumber);
        }

        public string getTrackID(StreamKind Format, int Nth)
        {
            return MI.getTrackID(Format, Nth);
        }

        public List<AudioTrackInfo> AnalyzeFile(MovieFile File)
        {
            List<AudioTrackInfo> TrackInfoList = new List<AudioTrackInfo>();

            for (int i = 0; i < File.AudioTracksCount; i++)
            {
                AudioTrackInfo Info = new AudioTrackInfo(File.getTrackID(StreamKind.Audio, i), File.getAudioTrackFormat(i), File.getAudioBitDepth(i), File.getAudioChannels(i), i + File.VideoTracksCount + 1);
                TrackInfoList.Add(Info);
            }

            return TrackInfoList;
        }
    }
}
