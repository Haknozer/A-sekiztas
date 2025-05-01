using Microsoft.VisualBasic.Devices;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;
using System.Threading;

namespace sekiz_tas_a_star
{
    public partial class Form1 : Form
    {
        class tahta_durum
        {
            public int[,] tahta { get; private set; }
            public tahta_durum eski_tahta { get; private set; }
            public string hareket { get; private set; }
            public int toplam_hareket { get; private set; }
            public int h {  get; private set; }

            public int hareket_sayý { get; private set; }
            public int f => 6 + h;


            public tahta_durum(int[,] board, tahta_durum eski_tahta = null, string hareket = "", int toplam_hareket = 0)
            {
                tahta = (int[,])board.Clone();
                this.eski_tahta = eski_tahta;
                this.hareket = hareket;
                this.toplam_hareket = toplam_hareket; 
                var hesap = h_hesapla(eski_tahta);
                h = hesap.Item1;
                this.hareket_sayý = hesap.Item2;
            }

            (int,int) h_hesapla(tahta_durum eski_tahta)
            {
                if (eski_tahta == null) return (6,0); 

                int hareket_eden = 0, eskiX = 0, eskiY = 0, yeniX = 0, yeniY = 0;

                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (tahta[i, j] == 0) { yeniX = i; yeniY = j; }
                        if (eski_tahta.tahta[i, j] == 0) { eskiX = i; eskiY = j; }
                    }
                }

                hareket_eden = eski_tahta.tahta[yeniX, yeniY];

                int dogruX = (hareket_eden - 1) / 3;
                int dogruY = (hareket_eden - 1) % 3;

                if (tahta[dogruX,dogruY] == hareket_eden)
                    return (0, hareket_eden);
                
                if (eski_tahta.tahta[dogruX, dogruY] != hareket_eden)
                    return (1, hareket_eden);

                return (2, hareket_eden);
            }


            public List<tahta_durum> ihtimal_hesapla()
            {
                List<tahta_durum> tahta_Durums = new List<tahta_durum>();

                int x = 0, y = 0;

                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (tahta[i,j] == 0)
                        {
                            x = i;
                            y = j;
                            break;
                        }
                    }
                }


                int[,] haraketler = { { -1, 0 }, { 1, 0 }, { 0, -1 }, { 0, 1 } };
                string[] hareketYonler = { "Yukarý", "Asaðý", "Sol", "Sað" };

                for (int i = 0; i < haraketler.GetLength(0); i++)
                {
                    int X = x + haraketler[i, 0];
                    int Y = y + haraketler[i, 1];

                    if (X >= 0 && X < 3 && Y >= 0 && Y < 3)
                    {
                        int[,] kopya_tahta = (int[,])tahta.Clone();
                        kopya_tahta[x, y] = kopya_tahta[X, Y];
                        kopya_tahta[X, Y] = 0;
                        tahta_Durums.Add(new tahta_durum(kopya_tahta, this, hareketYonler[i], toplam_hareket + 1));
                    }
                }


                return tahta_Durums;
            }

            public int kod()
            {
                string sayi = "";

                foreach (var item in tahta)
                {
                    sayi += item.ToString();   
                }
                return Convert.ToInt32(sayi);
            }
        }


        int sayi = 1;
        int[,] tahta_baslangic = {
            { 1, 2, 3 },
            { 4, 5, 6 },
            { 7, 8, 0 }
        };


        Dictionary<string, Tuple<int, int>> button_yerleri = new Dictionary<string, Tuple<int, int>>()
        {
            { "button1", Tuple.Create(0, 0) },
            { "button2", Tuple.Create(0, 1) },
            { "button3", Tuple.Create(0, 2) },
            { "button4", Tuple.Create(1, 0) },
            { "button5", Tuple.Create(1, 1) },
            { "button6", Tuple.Create(1, 2) },
            { "button7", Tuple.Create(2, 0) },
            { "button8", Tuple.Create(2, 1) },
            { "button9", Tuple.Create(2, 2) }
        };

        Dictionary<int, Button> button_sayilari= new Dictionary<int, Button>(){};



        public Form1()
        {
            InitializeComponent();

            foreach (Control control in this.Controls)
            {
                if (control is Button)
                {
                    control.Click += sayi_yerlestir;
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label1.Text = "1. Sayýyý yerleþtiriniz";
        }




        void sayi_yerlestir(object sender, EventArgs e)
        {
            Button týklananButton = sender as Button;
            if (týklananButton.Text == "")
            {
                týklananButton.Text = sayi.ToString();
                var yer = button_yerleri[týklananButton.Name];
                tahta_baslangic[yer.Item1, yer.Item2] = sayi;
                button_sayilari.Add(sayi,týklananButton);
                sayi += 1;
                label1.Text = sayi.ToString() + ". Sayýyý yerleþtiriniz";

            }
            else
            {
                MessageBox.Show("Boþ bir yer seçiniz");
            }


            if (sayi > 8)
            {
                label1.Text = "";
                foreach (Control control in this.Controls)
                {
                    if (control is Button)
                    {
                        control.Enabled = false;

                        if (control.Text == "")
                        {
                            var yer = button_yerleri[control.Name];
                            tahta_baslangic[yer.Item1, yer.Item2] = 0;
                            control.Visible = false;
                        }
                    }
                }

                List<string> cevap = bulmaca_coz(tahta_baslangic);


                cozum(cevap);
               
            }
        }

        void cozum(List<string> cevap)
        {
            if (cevap != null)
            {
                foreach (var islem in cevap)
                {
                    label1.Text = $"{cevap.Count()} adýmda, Çözülüyor";
                    string[] bolme = islem.Split(',');
                    listBox1.Items.Add(islem);
                    switch (bolme[0]) {
                        case "Yukarý": yukarý(Convert.ToInt32(bolme[1])); break;
                        case "Aþaðý": asagi(Convert.ToInt32(bolme[1])); break;
                        case "Sol": sol(Convert.ToInt32(bolme[1])); break;
                        case "Sað": sag(Convert.ToInt32(bolme[1])); break;

                    }
                }
                label1.Text = $"Çözüm {listBox1.Items.Count} Adýmda Bitti";
            }
            else
            {
                MessageBox.Show("Çözüm Yok");
            }
        }
        void yukarý(int buton_sayi)
        {
            Button button = button_sayilari[buton_sayi];

            for (int i = 0; i <= 80; i+=10)
            {
                button.Location = new Point(button.Location.X, button.Location.Y - 10);
                Application.DoEvents();
                Thread.Sleep(200);

            }
        }


        void asagi(int buton_sayi)
        {
            Button button = button_sayilari[buton_sayi];

            for (int i = 0; i <= 80; i += 10)
            {
                button.Location = new Point(button.Location.X, button.Location.Y + 10);
                Application.DoEvents();
                Thread.Sleep(200);
            }
        }


        void sol(int buton_sayi)
        {
            Button button = button_sayilari[buton_sayi];

            for (int i = 0; i <= 90; i += 10)
            {
                button.Location = new Point(button.Location.X - 10, button.Location.Y);
                Application.DoEvents();
                Thread.Sleep(200);
            }
        }


        void sag(int buton_sayi)
        {
            Button button = button_sayilari[buton_sayi];

            for (int i = 0; i <= 90; i += 10)
            {
                button.Location = new Point(button.Location.X + 10, button.Location.Y);
                Application.DoEvents();
                Thread.Sleep(200);
            }
        }



        List<string> bulmaca_coz(int[,] tahtas)
        {
            var ilk = new tahta_durum(tahta_baslangic);
            var acik = new SortedSet<tahta_durum>(Comparer<tahta_durum>.Create((a, b) => a.f == b.f ? a.toplam_hareket.CompareTo(b.toplam_hareket) : a.f.CompareTo(b.f)));
            var kapali = new List<int>();

            acik.Add(ilk);

            while (acik.Count > 0)
            {
                tahta_durum current = acik.Min;
                acik.Remove(current);

                if (current.kod() == 123456780)
                {
                    List<string> path = new List<string>();
                    while (current.eski_tahta != null)
                    {

                        if (current.hareket == "Yukarý")
                        {
                            path.Add($"Aþaðý,{current.hareket_sayý}");
                        }
                        else if (current.hareket == "Asaðý")
                        {
                            path.Add($"Yukarý,{current.hareket_sayý}");
                        }
                        else if (current.hareket == "Sol")
                        {
                            path.Add($"Sað,{current.hareket_sayý}");
                        }
                        else if (current.hareket == "Sað")
                        {
                            path.Add($"Sol,{current.hareket_sayý}");
                        }
                        current = current.eski_tahta;
                    }
                    path.Reverse();
                    return path;
                }

                kapali.Add(current.kod());

                foreach (var neighbor in current.ihtimal_hesapla())
                {
                    if (kapali.Contains(neighbor.kod()))
                        continue;

                    if (!acik.Contains(neighbor))
                        acik.Add(neighbor);
                }
            }

            return null;
        }
    }
}
