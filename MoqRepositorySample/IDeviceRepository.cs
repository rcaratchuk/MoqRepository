namespace MoqRepositorySample
{
    using System.Collections.Generic;

    public interface IDeviceRepository
    {
        IList<Device> FindAll();

        Device FindByName(string deviceName);

        Device FindById(int deviceId);

        bool Save(Device target);
    }
}
