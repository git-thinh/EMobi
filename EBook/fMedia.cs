using AxWMPLib;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace EBook
{
    public partial class fMedia : Form
    {
        readonly IMain m_main;
        public fMedia() { InitializeComponent(); }

        public fMedia(IMain main) : base()
        {
            m_main = main;
        }

        private void fMedia_Load(object sender, EventArgs e)
        {
            new Thread(delegate (object o)
            {
                using (var ms = new MemoryStream(File.ReadAllBytes(@"C:\book.data\mp3\8.2.mp3")))
                {
                    ms.Position = 0;
                    using (WaveStream blockAlignedStream = new BlockAlignReductionStream(WaveFormatConversionStream.CreatePcmStream(new Mp3FileReader(ms))))
                    {
                        using (WaveOut waveOut = new WaveOut(WaveCallbackInfo.FunctionCallback()))
                        {
                            waveOut.Init(blockAlignedStream);
                            waveOut.Play();
                            while (waveOut.PlaybackState == PlaybackState.Playing)
                            {
                                System.Threading.Thread.Sleep(100);
                            }
                        }
                    }
                }
            }).Start();

            //this.Shown += (se, ev) =>
            //{                
            //    _media.URL = @"C:\book.data\mp4\shop online.mp4";
            //    //_media.URL = @"C:\git\VideoPlayer\Endszene.mp4";
            //    //axWindowsMediaPlayer1.URL = @"C:\git\VideoPlayer\testVideo.mp4";
            //    _media.uiMode = "none";
            //    _media.Ctlcontrols.currentPosition = 15;
            //};
        }

        private void _media_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {

            /**** Don't add this if you want to play it on multiple screens***** /
             * 
            if (axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsPlaying)
            {
                axWindowsMediaPlayer1.fullScreen = true;
            }
            /********************************************************************/

            if (_media.playState == WMPLib.WMPPlayState.wmppsStopped)
            {
                Application.Exit();
            }

        }

        private void _backGroundTransparent_Click(object sender, EventArgs e)
        {
            _media.URL = @"C:\git\VideoPlayer\Endszene.mp4";
        }
    }
}
