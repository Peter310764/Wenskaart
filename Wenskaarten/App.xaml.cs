using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;


namespace Wenskaarten
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Model.Wenskaart mijnWenskaart = new Model.Wenskaart();
            ViewModel.WenskaartVM vm = new ViewModel.WenskaartVM(mijnWenskaart);
            View.WenskaartWindow mijnWenskaartWindow = new View.WenskaartWindow();
            mijnWenskaartWindow.DataContext = vm;
            mijnWenskaartWindow.Show();
        }
    }
}
