using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TechnoService
{
    public partial class App : Application
    {
        public int CurrentUserID {  get; set; }
        public int CurrentUserPositionID {  get; set; }

        public void SetCurrentUserID(int UserID)
        {
            CurrentUserID = UserID;
        }
    }
}
