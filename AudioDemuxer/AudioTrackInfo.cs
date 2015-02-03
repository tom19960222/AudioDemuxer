using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioDemuxer
{
    public class AudioTrackInfo
    {
        private int _TrackID;
        private string _Format;
        private int _Depth;
        private int _Channels;
        private int _Index;

        public int TrackID { get { return _TrackID; } }
        public string Format { get { return _Format; } }
        public int Depth { get { return _Depth; } }
        public int Channels { get { return _Channels; } }
        public int Index { get { return _Index; } }


        public AudioTrackInfo(string TrackID, string Format, string Depth, string Channels, int Index)
        {
            if (!int.TryParse(TrackID, out this._TrackID))
                _TrackID = 1;
            if (!int.TryParse(Depth, out this._Depth))
                _Depth = 0;
            if (!int.TryParse(Channels, out this._Channels))
                _Channels = 0;
            _Format = Format;
        }
    }
}
