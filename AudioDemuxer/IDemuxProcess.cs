using System;
namespace AudioDemuxer
{
    interface IDemuxProcess
    {
        void Dispose();
        void Start();
    }
}
