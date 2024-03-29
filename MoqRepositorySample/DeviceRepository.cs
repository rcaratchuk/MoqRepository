﻿namespace MoqRepositorySample
{
    using System.Collections.Generic;

    public class DeviceRepository : IDeviceRepository
    {
        #region IDeviceRepository Members

        public IList<Device> FindAll()
        {
            // Your database code here, whether it is linq, or ADO.Net, or something else
            // That actually fetches all the Products from a database and creates a list
            throw new System.NotImplementedException();
        }

        public Device FindByName(string deviceName)
        {
            // Your database code here, whether it is linq, or ADO.Net, or something else
            // That actually fetches a Product from a database, using the supplied parameter
            throw new System.NotImplementedException();
        }

        public Device FindById(int deviceId)
        {
            // Your database code here, whether it is linq, or ADO.Net, or something else
            // That actually fetches a Product from a database, using the supplied parameter
            throw new System.NotImplementedException();
        }

        public bool Save(Device target)
        {
            // Your database code here, whether it is linq, or ADO.Net, or something else
            // That actually saves a Product to a database (insert or update), using the supplied parameter
            throw new System.NotImplementedException();
        }

        #endregion
    }
}
