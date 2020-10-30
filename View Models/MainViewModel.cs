using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MVVM_Base.View_Models;
using System.Threading.Tasks;
using DevExpress.Mvvm;
using System.Windows.Input;
using System.Windows.Forms;
using System.IO;
using System.Collections.ObjectModel;

namespace MVVM_Base.View_Models
{
    public class MainViewModel : ViewModelBase
    {
        public int TestVariable { get; set; }
        public MainViewModel()
        {

            Task.Run(() =>
            {
                while (true)
                {
                    Task.Delay(3000);
                }
            });
        }



        public ICommand Delete
        {
            get
            {
                return new DelegateCommand<string>((obj) =>
                {
                    
                });

            }
        }
    }
}
