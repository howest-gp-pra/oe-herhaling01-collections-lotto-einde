using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Pra.OefeningCollections.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Random rnd;
        List<int> MijnNummers;
        List<int> WinnendeNummers;
        public MainWindow()
        {
            InitializeComponent();
            Initialiseer();
            MaakAllesLeeg();
            MaakAllesWit();
        }
        private void Initialiseer()
        {
            rnd = new Random();
            MijnNummers = new List<int>();
            WinnendeNummers = new List<int>();
            cmbBallen.Items.Clear();
            for (int r = 40; r <= 50; r++)
            {
                cmbBallen.Items.Add(r);
            }
            cmbBallen.SelectedIndex = 5;




        }
        private void MaakAllesLeeg()
        {
            txtGetal1.Text = "";
            txtGetal2.Text = "";
            txtGetal3.Text = "";
            txtGetal4.Text = "";
            txtGetal5.Text = "";
            txtGetal6.Text = "";

            txtWinGetal1.Text = "";
            txtWinGetal2.Text = "";
            txtWinGetal3.Text = "";
            txtWinGetal4.Text = "";
            txtWinGetal5.Text = "";
            txtWinGetal6.Text = "";
            lblJuisteCijfers.Content = "-";
            lblPrijzenGeld.Content = "-";
        }
        private void MaakAllesWit()
        {
            txtGetal1.Background = Brushes.White;
            txtGetal2.Background = Brushes.White;
            txtGetal3.Background = Brushes.White;
            txtGetal4.Background = Brushes.White;
            txtGetal5.Background = Brushes.White;
            txtGetal6.Background = Brushes.White;

            txtWinGetal1.Background = Brushes.White;
            txtWinGetal2.Background = Brushes.White;
            txtWinGetal3.Background = Brushes.White;
            txtWinGetal4.Background = Brushes.White;
            txtWinGetal5.Background = Brushes.White;
            txtWinGetal6.Background = Brushes.White;
        }
        private void CmbBallen_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MaakAllesLeeg();
            MaakAllesWit();
        }
        private void RdbJa_Checked(object sender, RoutedEventArgs e)
        {
            if (!this.IsLoaded) return;
            MaakAllesLeeg();
            MaakAllesWit();
        }
        private void RdbNeen_Checked(object sender, RoutedEventArgs e)
        {
            if (!this.IsLoaded) return;
            MaakAllesLeeg();
            MaakAllesWit();
        }
        private void BtnEigenNummers_Click(object sender, RoutedEventArgs e)
        {
            MaakAllesLeeg();
            MaakAllesWit();
            WinnendeNummers.Clear();
            MijnNummers.Clear();

            int aantalballen = int.Parse(cmbBallen.SelectedItem.ToString());

            int getal;
            for (int r = 1; r <= 6; r++)
            {
                getal = rnd.Next(1, aantalballen + 1);
                while (ReedsInDeLijst(MijnNummers, getal))
                {
                    getal = rnd.Next(1, aantalballen + 1);
                }
                MijnNummers.Add(getal);
            }
            for (int r = 1; r <= 6; r++)
            {
                TextBox txt = (TextBox)this.FindName("txtGetal" + r.ToString());
                txt.Text = MijnNummers[r - 1].ToString();
            }

        }
        private void BtnWinnendeNummers_Click(object sender, RoutedEventArgs e)
        {
            MaakAllesWit();

            WinnendeNummers.Clear();
            int aantalballen = int.Parse(cmbBallen.SelectedItem.ToString());

            int getal;
            for (int r = 1; r <= 6; r++)
            {
                getal = rnd.Next(1, aantalballen + 1);
                while (ReedsInDeLijst(WinnendeNummers, getal))
                {
                    getal = rnd.Next(1, aantalballen + 1);
                }
                WinnendeNummers.Add(getal);
            }
            for (int r = 1; r <= 6; r++)
            {
                TextBox txt = (TextBox)this.FindName("txtWinGetal" + r.ToString());
                txt.Text = WinnendeNummers[r - 1].ToString();
            }
            DoeWinstBerekening();

        }
        private bool ReedsInDeLijst(List<int> zoeklijst, int getal)
        {
            foreach (int zoek in zoeklijst)
            {
                if (zoek == getal) return true;
            }
            return false;
        }

        private void DoeWinstBerekening()
        {
            int aantalJuist = 0;
            int prijzenGeld = 0;
            if (rdbJa.IsChecked == true)
            {
                for (int r = 0; r < 6; r++)
                {
                    if (MijnNummers[r] == WinnendeNummers[r])
                    {
                        aantalJuist++;
                        TextBox txt = (TextBox)this.FindName("txtGetal" + (r + 1).ToString());
                        txt.Background = Brushes.Gold;
                        txt = (TextBox)this.FindName("txtWinGetal" + (r + 1).ToString());
                        txt.Background = Brushes.Gold;
                    }
                }
            }
            else
            {
                for (int x = 0; x < 6; x++)
                {
                    for (int y = 0; y < 6; y++)
                    {
                        if (MijnNummers[x] == WinnendeNummers[y])
                        {
                            aantalJuist++;
                            TextBox txt = (TextBox)this.FindName("txtGetal" + (x + 1).ToString());
                            txt.Background = Brushes.Gold;
                            txt = (TextBox)this.FindName("txtWinGetal" + (y + 1).ToString());
                            txt.Background = Brushes.Gold;

                        }
                    }
                }
            }
            switch (aantalJuist)
            {
                case 1:
                    prijzenGeld = 1;
                    break;
                case 2:
                    prijzenGeld = 5;
                    break;
                case 3:
                    prijzenGeld = 50;
                    break;
                case 4:
                    prijzenGeld = 450;
                    break;
                case 5:
                    prijzenGeld = 5000;
                    break;
                case 6:
                    prijzenGeld = 200000;
                    break;
                default:
                    prijzenGeld = 0;
                    break;

            }
            lblJuisteCijfers.Content = aantalJuist.ToString();
            lblPrijzenGeld.Content = prijzenGeld.ToString();

        }



    }
}
