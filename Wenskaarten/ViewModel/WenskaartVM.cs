using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Data;
using System.Globalization;
using System.Windows.Input;
using Wenskaarten.Model;
using System.Windows.Shapes;
using System.Windows.Controls;
using Microsoft.Win32;
using System.IO;
using System.Collections.ObjectModel;

namespace Wenskaarten.ViewModel
{
    class WenskaartVM : ViewModelBase
    {
        private Model.Wenskaart wenskaart;
        public WenskaartVM(Model.Wenskaart eenWenskaart)
        {
            wenskaart = eenWenskaart;
            wenskaart.Ballen = new ObservableCollection<Bal>();
           // SolidColorBrush b = new SolidColorBrush();
           // b.Color = new Color { B = 0, G = 0, R = 255 , A=255};

           //Ballen.Add(new Bal { Xwaarde = 350, Ywaarde = 100, Kleur = b });
        }

        public string Symbol
        {
            get { return wenskaart.Symbol; }
            set
            {
                wenskaart.Symbol = value;
                RaisePropertyChanged("Symbol");
            }
        }
        public int AantalBal
        {
            get { return wenskaart.AantalBal; }
            set
            {
                wenskaart.AantalBal = value;
                RaisePropertyChanged("AantalBal");
            }
        }
        public string Wens
        {
            get { return wenskaart.Wens; }
            set
            {
                wenskaart.Wens = value;
                RaisePropertyChanged("Wens");
            }
        }

        public int Lettergrootte
        {
            get { return wenskaart.Lettergrootte; }
            set
            {
                wenskaart.Lettergrootte = value;
                RaisePropertyChanged("Lettergrootte");
            }
        }
        public string Lettertype
        {
            get { return wenskaart.Lettertype; }
            set
            {
                wenskaart.Lettertype = value;
                RaisePropertyChanged("Lettertype");
            }
        }
        public string GridStatus
        {
            get { return wenskaart.GridStatus; }
            set
            {
                wenskaart.GridStatus = value;
                RaisePropertyChanged("GridStatus");
            }
        }
        public ObservableCollection<Bal> Ballen
        {
            get { return wenskaart.Ballen;}
            set
            {
                wenskaart.Ballen = value;
                RaisePropertyChanged("Ballen");
            }
        }
        #region Keuze kaart


        public RelayCommand KerstkaartCommand
        { get { return new RelayCommand(KerstkaartKeuze); } }

        private void KerstkaartKeuze()
        {
            Ballen.Clear();
            Lettertype = "Arial";
            Lettergrootte = 14;
            AantalBal = 0;
            Wens = "Geef hier je tekst in";
            GridStatus = "Visible";
            Symbol = (@"H:\WPF\Wenskaarten\Wenskaarten\View\Images\kerstkaart.jpg");
        }
        public RelayCommand GeboortekaartCommand
        { get { return new RelayCommand(GeboortekaartKeuze); } }
        private void GeboortekaartKeuze()
        {
            Ballen.Clear();
            Lettertype = "Arial";
            Lettergrootte = 14;
            AantalBal = 0;
            Wens = "Geef hier je tekst in";
            GridStatus = "visible";
            Symbol = (@"H:\WPF\Wenskaarten\Wenskaarten\View\Images\geboortekaart.jpg");
        }
        #endregion
        /// <summary>
        /// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>


        private Ellipse sleepbal = new Ellipse();
        public RelayCommand<MouseEventArgs> MouseMoveCommand
        {
            get { return new RelayCommand<MouseEventArgs>(MuisIn); }
        }
        private void MuisIn(MouseEventArgs e)
        {
            sleepbal = (Ellipse)e.OriginalSource;
            if (e.LeftButton == MouseButtonState.Pressed || e.RightButton == MouseButtonState.Pressed)
            {
                DataObject sleepKleur = new DataObject("deKleur", sleepbal.Fill);
                DragDrop.DoDragDrop(sleepbal, sleepKleur, DragDropEffects.Copy);
            }
        }

        //private Bal bal = new Bal();
        private RelayCommand<DragEventArgs> dropCommand;
        public RelayCommand<DragEventArgs> DropBalCommand
        {
          get
            {
                if (dropCommand == null)
                    dropCommand = new RelayCommand<DragEventArgs>(DropBal);
                return dropCommand;
            }
        }
        private void DropBal(DragEventArgs e)
        {
            Point point = e.GetPosition((IInputElement)e.OriginalSource);
            Brush gesleepteKleur = (Brush)e.Data.GetData("deKleur");
            //bal.Kleur = gesleepteKleur;
            //bal.Xwaarde = (int)point.X - 20;
            //bal.Ywaarde = (int)point.Y - 20;
            Ballen.Add(new Bal { Kleur = gesleepteKleur, Xwaarde = (int)point.X - 20, Ywaarde = (int)point.Y});

        }

        #region + en - buttons


        public RelayCommand MeerCommand
        { get { return new RelayCommand(GroterLetter); } }
        private void GroterLetter()
        {
            if (Lettergrootte < 40)
            {
                Lettergrootte++;
            }
        }
        public RelayCommand MinderCommand
        { get { return new RelayCommand(KleinerLetter); } }
        private void KleinerLetter()
        {
            if (Lettergrootte > 10)
            {
                Lettergrootte--;
            }
        }
        #endregion
        #region Nieuwe Wenskaart
    
        public RelayCommand NieuwCommand
        {   get { return new RelayCommand(NieuwWenskaart); } }

        private void NieuwWenskaart()
        {
            //Lettertype = "Arial";
            //Lettergrootte = 14;
            //AantalBal = 0;
            //Wens = "Geef hier je tekst in";
            GridStatus = "Hidden";

            wenskaart.Ballen = new ObservableCollection<Bal>();
        }
        #endregion
        #region Opslaan Wenskaart


        public RelayCommand OpslaanCommand
        {
            get { return new RelayCommand(OpslaanBestand); }
        }
        private void OpslaanBestand()
        {
            try
            {
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.FileName = "Wenskaart";
                dlg.DefaultExt = ".wns";
                dlg.Filter = "Textbox documents |*.wns";

                if (dlg.ShowDialog()==true)
                {
                    using (StreamWriter bestand = new StreamWriter(dlg.FileName))
                    {
                        bestand.WriteLine(Symbol);
                        bestand.WriteLine(";");
                        bestand.WriteLine(AantalBal.ToString());
                        bestand.WriteLine(";");
                        List<Bal> Ballen = new List<Bal>();
                        //foreach (var child in canvas)
                        //{
                        //    Bal.Kleur = child.kleur;
                        //    Bal.Xwaarde = child.Xwaarde;
                        //    Bal.Ywaarde = child.Ywarde;
                        //}
                        bestand.WriteLine(Ballen.ToString());
                        bestand.WriteLine(Wens);
                        bestand.WriteLine(";");
                        bestand.WriteLine(Lettertype.ToString());
                        bestand.WriteLine(";");
                        bestand.WriteLine(Lettergrootte.ToString());
                    }
                }
              }
            catch (Exception ex)
            {
                MessageBox.Show("Opslaan mislukt : " + ex.Message);
            }
        }
        #endregion
        #region Programma sluiten


        public RelayCommand<CancelEventArgs> ClosingCommand
            { get { return new RelayCommand<CancelEventArgs>(OnClosing); } }

        public void OnClosing(CancelEventArgs e)
        {
            if (MessageBox.Show("Wilt u het programma sluiten ?", "Afsluiten",
            MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) ==
            MessageBoxResult.No)
                e.Cancel = true;
        }
        public RelayCommand AfsluitenCommand
        {
            get { return new RelayCommand(AfsluitenApp); }
        }
        private void AfsluitenApp()
        {
            Application.Current.MainWindow.Close();
        }
        #endregion
    }
}
