using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SlimDX.DirectInput;

namespace TimedBookmarks
{
    public partial class MainForm : Form, ButtonListener, DeviceListener
    {
        private Bookmarks bookmarks;
        private ButtonThread buttonThread;
        private DeviceThread deviceThread;


        public MainForm()
        {
            this.bookmarks = new Bookmarks();
            InitializeComponent();

            // Tries to load saved device.
            if (Inputs.hasDevice())
            {
                try
                {
                    this.DevicePressed(Inputs.getDevice(), Inputs.GetButton());
                }
                catch (Exception ex)
                {
                    // Case somtehing happens, reset the config!
                    Inputs.ResetConfig();
                }
            }
        }

        private void AbortButtonThread()
        {
            if (this.buttonThread != null)
            {
                this.buttonThread.Stop();
                this.buttonThread = null;
            }
        }

        private void CaptureEvent(object sender, EventArgs e)
        {
            btReset.Enabled = !cbCapture.Checked;
            if (cbCapture.Checked)
            {
                this.AbortButtonThread();

                this.cbCapture.Text = "Press any controller button to capture...";
                this.deviceThread = new DeviceThread(this, Inputs.GetConnected(this));
                this.deviceThread.Start();
            }
            else
            {
                this.cbCapture.Text = "Configure capture button!";
                this.deviceThread.Stop();
                this.deviceThread = null;
            }
        }

        delegate void DelegatedResetEvent(object sender, EventArgs e);
        private void ResetEvent(object sender, EventArgs e)
        {
            if (this.lvBookmarks.InvokeRequired)
                this.Invoke(new DelegatedResetEvent(ResetEvent), new Object[] { null, null });
            else
            {
                this.AbortButtonThread();

                this.bookmarks.Reset(true);
                this.lvBookmarks.Items.Clear();
                this.lvBookmarks.Columns[0].Text = "Start Time: " + this.bookmarks.GetStartTime();

                this.StartButtonThread();
            }
        }

        private void SaveEvent(object sender, EventArgs e)
        {
            // TODO: try to save bookmarks.
            this.AbortButtonThread();
        }

        private void StartButtonThread()
        {
            if (this.buttonThread == null && Inputs.hasDevice())
            {
                this.buttonThread = new ButtonThread(Inputs.getDevice(), Inputs.GetButton());
                this.buttonThread.AddListener(this);
                this.buttonThread.Start();
            }
        }

        private void BeforeCloseEvent(object sender, FormClosingEventArgs e)
        {
            if (!this.bookmarks.IsEmpty())
                this.SaveEvent(sender, e);
            else
                this.AbortButtonThread();
        }

        delegate void DelegatedButtonPressed();
        public void ButtonPressed()
        {
            if (this.lvBookmarks.InvokeRequired)
                this.Invoke(new DelegatedButtonPressed(ButtonPressed));
            else
            {
                TimeSpan bookmark = this.bookmarks.Bookmark();
                string youtube = this.tbYoutube.Text;
                if (youtube.Length > 0)
                    this.lvBookmarks.Items.Add(youtube + (youtube.Contains("?") ? "&t=" : "?t=") + bookmark.TotalSeconds.ToString("F0"));
                else
                    this.lvBookmarks.Items.Add(bookmark.ToString());
                this.lvBookmarks.Columns[0].Width = this.lvBookmarks.Width - 30;
            }
        }

        delegate void DelegatedCanceled();
        public void Canceled()
        {
            if (this.cbCapture.InvokeRequired)
                this.Invoke(new DelegatedCanceled(Canceled));
            else
            {
                Inputs.ResetConfig();
                this.cbCapture.Checked = false;
                this.cbCapture.Text = "Configure capture button!";
                this.lvBookmarks.Items.Clear();
            }
        }

        delegate void DelegatedDevicePressed(Device device, int buttonIndex);
        public void DevicePressed(Device device, int buttonIndex)
        {
            if (this.cbCapture.InvokeRequired)
                this.Invoke(new DelegatedDevicePressed(DevicePressed), new Object[] { device, buttonIndex });
            else
            {
                Inputs.SetConfig(device, buttonIndex);
                this.btReset.Enabled = true;
                this.cbCapture.Checked = false;
                this.cbCapture.Text = device.Information.InstanceName + " [" + (buttonIndex + 1) + "]" + " to capture bookmark.";
                this.ResetEvent(null, null);
            }
        }

        private void UpdateLink(object sender, EventArgs e)
        {
            string youtube = this.tbYoutube.Text;
            youtube += youtube.Contains("?") ? "&t=" : "?t=";
            ListView.ListViewItemCollection items = this.lvBookmarks.Items;
            for (int index = 0; index < items.Count; index++)
            {
                items[index].Text = youtube + this.bookmarks.GetBookmark(index).TotalSeconds.ToString("F0");
            }
        }

        private void LinkSelected(object sender, EventArgs e)
        {
            var selected = this.lvBookmarks.SelectedIndices;
            if (selected.Count > 0 && this.tbYoutube.Text.Length > 0)
            {
                string link = this.lvBookmarks.Items[selected[0]].Text;
                try
                {
                    System.Diagnostics.Process.Start(link.StartsWith("http") ? link : ("http://" + link));
                }
                catch (Exception ex) { }
            }
        }
    }
}
