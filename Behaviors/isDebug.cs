using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;

namespace BookStore.Behaviors
{

    public interface IDebuggingService
    {
        bool RunningInDebugMode();
    }

    public class isDebug : IDebuggingService
    {
        private bool debugging;

        public bool RunningInDebugMode()
        {
            //#if DEBUG
            //return true;
            //#else
            //return false;
            //#endif
            WellAreWe();
            return debugging;
        }

        [Conditional("DEBUG")]
        private void WellAreWe()
        {
            debugging = true;
        }
    }

}