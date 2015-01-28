using System;
using System.Collections.Generic;
using System.Diagnostics;
namespace AudioDemuxer
{
    interface IDemuxProcess
    {
        void Dispose();
        void Start();
    }

    public abstract class DemuxProcessUsingOneCommand : IDemuxProcess
    {
        protected Process P;
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

    public abstract class DemuxProcessUsingMutilCommand : IDemuxProcess
    {
        protected List<Process> ProcessList;
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
