using System;
using System.Diagnostics;

namespace HelloDapper.Tests
{
    internal class TimingsHelper : IDisposable
    {
        private readonly Stopwatch _stopwatch = new Stopwatch();

        public TimingsHelper()
        {
            _stopwatch.Start();
        }

        public void Dispose()
        {
            Split("Finished");
            _stopwatch.Stop();
        }

        public void Split(string label)
        {
            Debug.Print(label + ": " + _stopwatch.ElapsedMilliseconds.ToString("#,###,##0") + " ms");
        }
    } 
}