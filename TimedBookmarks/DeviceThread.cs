using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using SlimDX.DirectInput;

namespace TimedBookmarks
{
    class DeviceThread
    {
        private DeviceListener listener;
        private List<AutoResetEvent> deviceEvents;
        private List<Device> devices;
        private Thread thread;
        private volatile bool keepAlive;

        public DeviceThread(DeviceListener listener, List<Device> devices)
        {
            this.devices = devices;
            this.deviceEvents = new List<AutoResetEvent>();
            this.keepAlive = true;
            this.listener = listener;
            this.thread = null;

            foreach (Device device in this.devices)
            {
                AutoResetEvent deviceEvent = new AutoResetEvent(false);
                device.SetNotification(deviceEvent);
                this.deviceEvents.Add(deviceEvent);
            }
        }

        public void WaitForDevice()
        {
            foreach (Device deviceL in this.devices)
                deviceL.Acquire();

            Device device = null;
            int button = -1;
            while (this.keepAlive)
            {
                int deviceIndex = WaitHandle.WaitAny(this.deviceEvents.ToArray(), 500);
                if (deviceIndex == System.Threading.WaitHandle.WaitTimeout)
                    continue;

                device = this.devices.ElementAt(deviceIndex);
                if (device.Information.Type == DeviceType.Keyboard)
                {
                    KeyboardState state = ((Keyboard)device).GetCurrentState();
                    IList<Key> keys = state.PressedKeys;
                    foreach (Key key in keys)
                    {
                        if (key != Key.Escape)
                            button = (int)key;
                        else
                            device = null;
                        this.keepAlive = false;
                        break;
                    }
                }
                else
                {
                    JoystickState state = ((Joystick)device).GetCurrentState();
                    bool[] buttons = state.GetButtons();
                    for (int index = 0; index < buttons.Length; ++index)
                    {
                        if (buttons[index])
                        {
                            button = index;
                            this.keepAlive = false;
                            break;
                        }
                    }
                }
            }

            foreach (Device deviceL in this.devices)
                deviceL.Unacquire();

            if (device != null)
                listener.DevicePressed(device, button);
            else
                listener.Canceled();
        }

        public void Start()
        {
            if (this.thread == null)
            {

                this.thread = new Thread(new ThreadStart(WaitForDevice));
                this.thread.Start();
            }
        }

        public void Stop()
        {
            if (this.thread != null)
            {
                this.keepAlive = false;
                this.thread.Join(1000);

            }
        }
    }
}
