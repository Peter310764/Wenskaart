using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Wenskaarten.Model
{
    class Wenskaart
    {
        public string Symbol { get; set; }
        public int AantalBal { get; set; }
        public string Wens { get; set; }
        public int Lettergrootte { get; set; }
        public string Lettertype { get; set; }
        public ObservableCollection<Bal> Ballen{ get; set; }
        public string GridStatus { get; set; }

        public Wenskaart()
        {
            Symbol = (@"H:\WPF\Wenskaarten\Wenskaarten\View\Images\geboortekaart.jpg");
            AantalBal = 0;
            Wens = string.Empty;
            Lettergrootte = 14;
            
            //Ballen = null;

            //  Nog aan te passen naar hidden

            GridStatus = "Hidden";
       }
        


        //public Wenskaart(BitmapImage nSymbol,int nAantalBal, string nWens, int nLettergrootte, string nLettertype, List<Bal> nBallen)
        //{
        //    Symbol = nSymbol;
        //    AantalBal = nAantalBal;
        //    Wens = nWens;
        //    Lettergrootte = nLettergrootte;
        //    Lettertype = nLettertype;
        //    Ballen = nBallen;
        //}
        //public Wenskaart() { }
    }

}
