using System;
using System.Collections.Generic;
using System.Text;

namespace PFE.Services
{
    interface IDialogService
    {
        void ShowMessage(string message, bool error);
    }
}
