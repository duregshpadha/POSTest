using System;
using System.Collections.Generic;
using System.Text;

namespace POS.Utilities
{
    class GenerateID : IGenerateID
    {
        /// <summary>
        /// Generate Unique Id of 42 characters
        /// </summary>
        /// <returns></returns>
        public string GenerateId()
        {
            string guid = Guid.NewGuid().ToString();
            return DateTime.UtcNow.ToString("MMddyyyy-HHmmssfff-") + guid.Substring(0, 23);
        }
    }
}
