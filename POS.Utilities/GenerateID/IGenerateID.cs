using System;
using System.Collections.Generic;
using System.Text;

namespace POS.Utilities
{
    public interface IGenerateID
    {
        /// <summary>
        /// Generate Unique Id of 42 characters
        /// </summary>
        /// <returns></returns>
        string GenerateId();
    }
}
