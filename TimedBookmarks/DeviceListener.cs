using SlimDX.DirectInput;

namespace TimedBookmarks
{
    interface ButtonListener
    {
        void ButtonPressed();
    }

    interface DeviceListener
    {
        void Canceled();
        void DevicePressed(Device device, int button);
    }
}
