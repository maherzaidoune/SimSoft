using System;
using System.Collections.Generic;
using System.Text;

namespace PFE.Services
{
    public interface IDialogService
    {
        void ShowMessage(string message, bool error);
    }
}
