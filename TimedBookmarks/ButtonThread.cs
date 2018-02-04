using System.Collections.Generic;
using System.Threading;
using SlimDX.DirectInput;

namespace TimedBookmarks
{
    class ButtonThread
    {
        private AutoResetEvent deviceEvent;
        private Device device;
        private int button;
        private List<ButtonListener> listeners;
        private Thread thread;
        private volatile bool keepAlive;

        public ButtonThread(Device device, int button)
        {
            this.device = device;
            this.button = button;
            this.deviceEvent = new AutoResetEvent(false);
            this.keepAlive = true;
            this.listeners = new List<ButtonListener>();
            this.thread = null;
        }

        public void AddListener(ButtonListener listener)
        {
            if (!this.listeners.Contains(listener))
                this.listeners.Add(listener);
        }

        public void WaitForButton()
        {
            while (this.keepAlive)
            {
                if (this.deviceEvent.WaitOne(500))
                {
                    if (this.device.Information.Type == DeviceType.Keyboard)
                    {
                        KeyboardState state = ((Keyboard)this.device).GetCurrentState();
                        if (state.IsPressed((Key)this.button))
                            foreach (ButtonListener listener in this.listeners)
                                listener.ButtonPressed();
                    }
                    else
                    {
                        JoystickState state = ((Joystick)this.device).GetCurrentState();
                        bool[] buttons = state.GetButtons();
                        if (buttons[this.button])
                            foreach (ButtonListener listener in this.listeners)
                                listener.ButtonPressed();
                    }
                }
            }
        }

        public void Start()
        {
            if (this.thread == null)
            {
                this.device.SetNotification(this.deviceEvent);
                this.device.Acquire();
                this.thread = new Thread(new ThreadStart(WaitForButton));
                this.thread.Start();
            }
        }

        public void Stop()
        {
            if (this.thread != null)
            {
                this.keepAlive = false;
                this.thread.Join(1000);
                this.device.Unacquire();
            }
        }
    }
}
