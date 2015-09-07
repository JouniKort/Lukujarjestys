using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AccessHandler = AccessSQL.AccessHandler;
using SQLRakentaja = AccessSQL.SQLRakentaja;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using Lukujärjestys.Properties;
using System.IO;

namespace Lukujärjestys
{
    public partial class Form1 : Form
    {
        private string NodeNimi;
        private TreeNode NodeValittu;
        private bool NodeParent;

        private List<string> valitut = new List<string>();
        private List<string> taulut;
        private List<string> sarakkeet;
        private List<string> lukkariSarakkeet = new List<string>();
        private List<string> data;
        private List<string> URLit = new List<string>();

        private DataTable Lukujarjestys = new DataTable();
        private DataTable lukujarjestysRAW;
        private DataTable LukujarjestysViikko = new DataTable();

        private Size frameSize;
        private Point LukuPoint;
        private Size LukuSize;
        private Size PuuSize;
        private Point PanelPoint;
        private Size EtsiSize;
        private Point OmatPoint;
        private Point SelaaPoint;
        private Point PuuPoint;
        private Point HakuPoint;
        private Point NimiPoint;

        private int Panel2Leveys;

        private ContextMenu cMenu = new ContextMenu();

        public DataTableCollection DTC { get; private set; }

        public Form1()
        {
            InitializeComponent();
            LisaaURLit();
            KootJaPisteet();
            TeePuuMenu();
            TeeLukkariSarakkeet();
        }

        private void KootJaPisteet()
        {
            frameSize = this.Size;
            LukuPoint = LukujarjestysDGV.Location;
            LukuSize = LukujarjestysDGV.Size;
            PuuSize = Puu.Size;
            PanelPoint = panel1.Location;
            EtsiSize = TextBoxEtsi.Size;
            OmatPoint = CheckBoxOmat.Location;
            SelaaPoint = ButtonSelaa.Location;
            PuuPoint = Puu.Location;
            HakuPoint = TextBoxEtsi.Location;
            Panel2Leveys = splitContainer1.Panel2.Width;
            NimiPoint = LabelNimi.Location;
        }

        private void LisaaURLit()
        {
            URLit.Add("https://uni.lut.fi/c/document_library/get_file?uuid=e21a5835-ef90-4e2e-9d3c-ce467655e157&groupId=10304");
            URLit.Add("https://uni.lut.fi/c/document_library/get_file?uuid=cd707d7c-b6ea-41fe-a983-ae21756bc15d&groupId=10304");
            URLit.Add("https://uni.lut.fi/c/document_library/get_file?uuid=becffa0b-18ad-45f3-be5f-03e336b02036&groupId=10304");
            URLit.Add("https://uni.lut.fi/c/document_library/get_file?uuid=23d23498-4545-4808-94c9-491fbb66aecb&groupId=10304");
            URLit.Add("https://uni.lut.fi/c/document_library/get_file?uuid=11ebc569-61ae-49fc-98c6-e5c1dac4a38a&groupId=10304");
            URLit.Add("https://uni.lut.fi/c/document_library/get_file?uuid=25947d3b-97c4-4389-87c9-175083479101&groupId=10304");
            URLit.Add("https://uni.lut.fi/c/document_library/get_file?uuid=b612eb81-d2cb-4363-8f8a-17c7d35fd667&groupId=10304");
            URLit.Add("https://uni.lut.fi/c/document_library/get_file?uuid=0e64af18-f810-4cbc-b249-6bfd42bfda62&groupId=10304");
            URLit.Add("https://uni.lut.fi/c/document_library/get_file?uuid=25947d3b-97c4-4389-87c9-175083479101&groupId=10304");
            URLit.Add("https://uni.lut.fi/c/document_library/get_file?uuid=ac79c67b-7b22-416f-abf9-8095caac7631&groupId=10304");
            URLit.Add("https://uni.lut.fi/c/document_library/get_file?uuid=6a68eba1-dbc5-4976-86b8-4bc6b6c943cc&groupId=10304");
            URLit.Add("https://uni.lut.fi/c/document_library/get_file?uuid=5bead19f-eae4-418b-b7c7-902bf78cef46&groupId=10304");
        }

        private void ButtonPaivita_Click(object sender, EventArgs e)
        {
            var vastaus = MessageBox.Show("Oletko varma että tahdot päivittää tietokannan?", "Päivitys", MessageBoxButtons.YesNo);
            if (vastaus == DialogResult.No)
                return;
            LabelPaivitys.Text = "Odota, päivitys meneillään... 0/12";
            LabelPaivitys.ForeColor = Color.DarkRed;

            string koodi = "";
            string nimi;
            string[] rivi = {"Nimi","Periodi","Viikko","Paiva","AlkaaKlo","LoppuuKlo","Sali","Huom"};

            int paivitettyja = 0;

            foreach(string url in URLit)
            {
                HtmlWeb hw = new HtmlWeb();

                HtmlDocument doc = hw.Load(url);

                //Regex.Matches(teksti.InnerText, @"[a-zA-Z]").Count > 50 tekstin kirjaimet
                //if (row.SelectNodes("th|td")[0].InnerText == string.Empty)
                //    continue;
                //string nimi = row.SelectNodes("th|td")[0].InnerText;
                //string koodi = nimi.Split('-')[0].TrimEnd();

                foreach (HtmlNode table in doc.DocumentNode.SelectNodes("//table"))
                {
                    bool tauluTehty = false;
                    if(table.Attributes["Class"].Value.Equals("spreadsheet"))
                        if(table.SelectNodes("tr") != null)//Yhdessä listassa kurssin tietojen paikalla piste, tämä antaa nullin
                            foreach (HtmlNode row in table.SelectNodes("tr"))
                            {
                                if (row.SelectNodes("th|td")[0].InnerText == string.Empty)
                                    continue;
                                Console.WriteLine("row");
                                nimi = row.SelectNodes("th|td")[0].InnerText;
                                koodi = nimi.Split('-')[0].TrimEnd();
                                Console.WriteLine(koodi);
                                int kierrokset = 0;
                                int indeksi = 0;
                                string alaTunnus = koodi.Substring(0, 2);
                                int soluja = row.SelectNodes("th|td").Count;
                                foreach (HtmlNode cell in row.SelectNodes("th|td"))
                                {
                                    Console.WriteLine("cell: " + cell.InnerText);
                                    if (soluja == 9)
                                    {
                                        if (kierrokset != 1)//Skipataan opettaja
                                        {
                                            rivi[indeksi] = cell.InnerText;
                                            indeksi++;
                                        }
                                    }
                                    else
                                    {
                                        rivi[indeksi] = cell.InnerText;
                                        indeksi++;
                                    }
                                    kierrokset += 1;
                                    if(indeksi == 8)
                                    {
                                        uusiTaulu(koodi);
                                        if(!tauluTehty)
                                            AccessHandler.SQLkomentoTaulu(SQLRakentaja.DELETE(koodi));//Tyhjennetään taulu
                                        tauluTehty = true;
                                        annaTiedot(koodi,rivi);
                                    }
                                }
                            }
                }
                paivitettyja++;
                LabelPaivitys.Text = "Odota, päivitys meneillään... "+paivitettyja+"/12";
            }

            TeePuu();
            LabelPaivitys.Text = "Päivitys onnistui";
            LabelPaivitys.ForeColor = Color.DarkGreen;
        }

        private void uusiTaulu(string koodi)
        {
            if (!taulut.Contains(koodi))
            {
                String sql = SQLRakentaja.CREATETABLE(koodi, sarakkeet, data);
                AccessHandler.SQLkomentoTaulu(sql);
                taulut.Add(koodi);
            }
        }

        private void annaTiedot(string koodi, string[] rivi)
        {
            AccessHandler.SQLkomentoINSERT(sarakkeet,rivi,koodi);
        }

        private void TeePuu()
        {
            Puu.Nodes.Clear();

            List<string> sarake = new List<string>();
            sarake.Add("Nimi");
            sarake.Add("Huom");
            Puu.BeginUpdate();
            int indeksi = 0;

            List<string> valitut = new List<string>();
            valitut.Add("Nimi");
            DataTable table = AccessHandler.SQLkomento(SQLRakentaja.SELECT(valitut, "Lukkari"))[0];
            table.Columns[0].Namespace = "Nimi";
            List<string> list = (from row in table.AsEnumerable()
                              select row.Field<string>("Nimi")).ToList<string>();

            foreach (string s in taulut)
            {
                DataTable DT = AccessHandler.SQLkomento(SQLRakentaja.SELECT(sarake, s))[0];
                Puu.Nodes.Add(s);
                Puu.Nodes[indeksi].ForeColor = Color.DarkRed;
                for(int i = 0; i<DT.Rows.Count; i++)
                {
                    Puu.Nodes[indeksi].Nodes.Add(DT.Rows[i][0].ToString());
                    Puu.Nodes[indeksi].Nodes[i].ForeColor = Color.DarkRed;
                    foreach(string nimi in list)
                        if (nimi.Contains(DT.Rows[i][0].ToString()))
                        {
                            Puu.Nodes[indeksi].Nodes[i].ForeColor = Color.DarkGreen;
                            Puu.Nodes[indeksi].ForeColor = Color.DarkGreen;
                        }

                    if (!DT.Rows[i][1].ToString().Equals("&nbsp;"))
                        Puu.Nodes[indeksi].Nodes[i].Nodes.Add(DT.Rows[i][1].ToString());
                }
                indeksi += 1;
            }
            Puu.EndUpdate();
        }

        private void TeePuuMenu()
        {
            MenuItem it1 = new MenuItem("Näytä");
            cMenu.MenuItems.Add(it1);
            it1.Click += new EventHandler(SubMenuClick);
            MenuItem it2 = new MenuItem("Poista");
            it2.Click += new EventHandler(SubMenuClick);
            cMenu.MenuItems.Add(it2);
            MenuItem it3 = new MenuItem("Poista tieto");
            it3.Click += new EventHandler(SubMenuClick);
            cMenu.MenuItems.Add(it3);
        }

        private void SubMenuClick(object sender, EventArgs e)
        {
            valitut.Clear();
            MenuItem it = sender as MenuItem;
            System.Diagnostics.Debug.WriteLine("SubMenuClick "+it.Text+" "+NodeNimi);
            string parent = "";

            if (NodeParent)                                                             //Parentin nodejen muutto
            {
                if (NodeParent && !it.Text.Equals("Poista tieto"))                      //Jos node on Parent, muutetaan kaikkien nodejen värit
                {
                    parent = NodeValittu.Text;
                    for(int i = 0; i<NodeValittu.Nodes.Count; i++)
                    {
                        if (it.Text.Equals("Näytä"))
                            NodeValittu.Nodes[i].ForeColor = Color.DarkGreen;
                        else
                            NodeValittu.Nodes[i].ForeColor = Color.DarkRed;
                        valitut.Add(NodeValittu.Nodes[i].Text);
                    }
                }
                else if(it.Text.Equals("Poista"))
                {
                    parent = NodeValittu.Parent.Text;
                    NodeValittu.Parent.ForeColor = Color.DarkRed;
                    valitut.Add(NodeValittu.Text);
                    for(int i = 0; i< NodeValittu.Parent.Nodes.Count; i++)
                    {
                        if (NodeValittu.Parent.Nodes[i].ForeColor == Color.DarkGreen)
                        {
                            NodeValittu.Parent.ForeColor = Color.DarkGreen;
                        }
                    }
                }
            }
            else                                                                        //Yksittäisen noden muutto
            {
                if (it.Text.Equals("Näytä"))                                            //Näkyväksi asetus, asetetaan teksti vihreäksi
                {
                    NodeValittu.ForeColor = Color.DarkGreen;
                    valitut.Add(NodeValittu.Text);
                    parent = NodeValittu.Parent.Text;                                   //Myös parentin tekstin väri muutetaan
                }
                else
                {
                    NodeValittu.ForeColor = Color.DarkRed;                              //Pois näkyvistä, asetetaan teksti punaiseksi
                    valitut.Add(NodeValittu.Text);
                    parent = NodeValittu.Parent.Text;
                }

            }

            if(NodeParent && it.Text.Equals("Poista tieto"))
            {
                var vastaus = MessageBox.Show("Oletko varma että tahdot poistaa taulun tietokannasta?", "Huom", MessageBoxButtons.YesNo);
                if (vastaus == DialogResult.No)
                    return;
                AccessHandler.SQLkomentoTaulu(SQLRakentaja.DROP(NodeNimi));
                taulut.Remove(NodeNimi);
                TeePuu();
            }
            else
            {
                MessageBox.Show("Yksittäisiä tietueita ei voi poistaa.\nPoista ylin solmu!", "Huom", MessageBoxButtons.OK);
            }
            if(valitut.Count > 0)
            {
                MuutaTieto(parent, valitut, it.Text);
                DGWAsetukset();
                TeeLukujarjestysRAW();
                TeeLukujarjestysDT();
            }
        }

        private void MuutaTieto(string taulu, List<string> rivi, string komento)
        {
            DataTable valinnat = new DataTable();
            valinnat.Columns.Add("Nimi");
            valinnat.Columns.Add("Operaattori");
            valinnat.Columns.Add("Arvo1");
            valinnat.Columns.Add("Arvo2");

            foreach(string s in rivi)
            {
                System.Diagnostics.Debug.WriteLine(komento+" "+s+" taulusta "+taulu);
                DataRow dr = valinnat.NewRow();
                dr.ItemArray = new string[] { "Nimi", "=", s ,""};
                valinnat.Rows.Add(dr);
            }
            if (komento.Equals("Näytä"))
            {
                DataTable DT = AccessHandler.SQLkomento(SQLRakentaja.WHERE_AND_OR(valinnat, lukkariSarakkeet, taulu))[0];
                for(int i = 0; i<DT.Rows.Count; i++)
                    AccessHandler.SQLkomentoINSERT(lukkariSarakkeet, new string[] { DT.Rows[i][0].ToString(), DT.Rows[i][1].ToString(), DT.Rows[i][2].ToString(), DT.Rows[i][3].ToString(), DT.Rows[i][4].ToString(), DT.Rows[i][5].ToString() }, "Lukkari");
            }else if (komento.Equals("Poista"))
            {
                AccessHandler.SQLkomentoTaulu(SQLRakentaja.WHERE_AND_OR(valinnat, lukkariSarakkeet, taulu,"DELETE FROM Lukkari WHERE "));
            }
        }

        private void TeeLukujarjestysRAW()
        {
            List<string> order = new List<string>();
            order.Add("Alkaa");
            order.Add("Nouseva");
            lukujarjestysRAW = AccessHandler.SQLkomento(SQLRakentaja.ORDERBY(lukkariSarakkeet, order, "Lukkari"))[0];
        }

        private void TeeLukujarjestysDT()
        {
            if (lukujarjestysRAW == null)
                TeeLukujarjestysRAW();

            TarkastaViikko();
            System.Diagnostics.Debug.WriteLine("Viikolla kursseja: " + LukujarjestysViikko.Rows.Count);
            if(LukujarjestysViikko.Rows.Count > 0)
            {
                PaivaJaAika();
            }
            LukujarjestysDGV.DataSource = Lukujarjestys;
        }

        private void TarkastaViikko()
        {
            LukujarjestysViikko.Clear();
            foreach (DataRow DR in lukujarjestysRAW.Rows)
            {
                string viikko = DR[1].ToString();
                if (viikko.Equals("&nbsp;"))
                {
                    continue;
                }

                if (viikko.Equals("" + int.Parse(LabelViikko.Text.Substring(LabelViikko.Text.IndexOf(" ")))))
                {
                    DataRow uusi = LukujarjestysViikko.NewRow();
                    uusi.ItemArray = DR.ItemArray;
                    LukujarjestysViikko.Rows.Add(uusi);
                    continue;
                }

                string[] viikot = null;
                if (viikko.Contains(",") && viikko.Contains("-"))
                {
                    viikot = viikko.Split(new string[] { "," }, StringSplitOptions.None);
                }
                else if (!viikko.Contains(",") && viikko.Contains("-"))
                {
                    viikot = viikko.Split(new string[] { "-" }, StringSplitOptions.None);
                    if (Valilta(viikot[0], viikot[1]))
                    {
                        DataRow uusi = LukujarjestysViikko.NewRow();
                        uusi.ItemArray = DR.ItemArray;
                        LukujarjestysViikko.Rows.Add(uusi);
                        continue;
                    }
                }
                if (viikot != null)
                {
                    foreach (string s in viikot)
                    {
                        if (s.Contains("-"))
                        {
                            string[] vali = s.Split(new string[] { "-" }, StringSplitOptions.None);
                            if (Valilta(vali[0], vali[1]))
                            {
                                DataRow uusi = LukujarjestysViikko.NewRow();
                                uusi.ItemArray = DR.ItemArray;
                                LukujarjestysViikko.Rows.Add(uusi);
                                continue;
                            }
                        }
                    }
                }
            }

        }

        private bool Valilta(string alku, string loppu)
        {
            int viikko = int.Parse(LabelViikko.Text.Substring(LabelViikko.Text.IndexOf(" ")));
            for(int i = int.Parse(alku); i<= int.Parse(loppu); i++)
            {
                if (i == viikko)
                    return true;
            }
            return false;
        }

        private void PaivaJaAika()
        {
            foreach(DataRow DR in LukujarjestysViikko.Rows)
            {
                string paiva = DR[2].ToString();
                string alkaa = DR[3].ToString();
                string loppuu = DR[4].ToString();
                System.Diagnostics.Debug.WriteLine("Päivä " + paiva + " klo " + alkaa + "-" + loppuu);
                int paivaIndeksi = 0;
                int alkuIndeksi = 0;
                int loppuIndeksi = 0;
                switch (paiva)
                {
                    case "ma":
                        paivaIndeksi = 1;
                        break;
                    case "ti":
                        paivaIndeksi = 2;
                        break;
                    case "ke":
                        paivaIndeksi = 3;
                        break;
                    case "to":
                        paivaIndeksi = 4;
                        break;
                    case "pe":
                        paivaIndeksi = 5;
                        break;                   
                }
                switch (alkaa)
                {
                    case "8":
                        alkuIndeksi = 0;
                        break;
                    case "9":
                        alkuIndeksi = 1;
                        break;
                    case "10":
                        alkuIndeksi = 2;
                        break;
                    case "11":
                        alkuIndeksi = 3;
                        break;
                    case "12":
                        alkuIndeksi = 4;
                        break;
                    case "13":
                        alkuIndeksi = 5;
                        break;
                    case "14":
                        alkuIndeksi = 6;
                        break;
                    case "15":
                        alkuIndeksi = 7;
                        break;
                    case "16":
                        alkuIndeksi = 8;
                        break;
                    case "17":
                        alkuIndeksi = 9;
                        break;
                    case "18":
                        alkuIndeksi = 10;
                        break;
                }
                switch (loppuu)
                {
                    case "9":
                        loppuIndeksi = 1;
                        break;
                    case "10":
                        loppuIndeksi = 2;
                        break;
                    case "11":
                        loppuIndeksi = 3;
                        break;
                    case "12":
                        loppuIndeksi = 4;
                        break;
                    case "13":
                        loppuIndeksi = 5;
                        break;
                    case "14":
                        loppuIndeksi = 6;
                        break;
                    case "15":
                        loppuIndeksi = 7;
                        break;
                    case "16":
                        loppuIndeksi = 8;
                        break;
                    case "17":
                        loppuIndeksi = 9;
                        break;
                    case "18":
                        loppuIndeksi = 10;
                        break;
                    case "19":
                        loppuIndeksi = 11;
                        break;
                    case "20":
                        loppuIndeksi = 12;
                        break;
                }
                for(int i = alkuIndeksi; i < loppuIndeksi; i++)
                {
                    DataRow dataR = Lukujarjestys.NewRow();
                    Lukujarjestys.Rows[i][paivaIndeksi] = Lukujarjestys.Rows[i][paivaIndeksi].ToString() + DR[0] + Environment.NewLine + "Sali " + DR[5]+Environment.NewLine;
                    if (Regex.Matches(Lukujarjestys.Rows[i][paivaIndeksi].ToString(), "Sali").Count > 1)
                        LukujarjestysDGV.Rows[i].Cells[paivaIndeksi].Style.BackColor = Color.Red;
                    else
                        LukujarjestysDGV.Rows[i].Cells[paivaIndeksi].Style.BackColor = Color.White;
                }
            }
        }

        private void TeeLukkariSarakkeet()
        {
            lukkariSarakkeet.Add("Nimi");
            lukkariSarakkeet.Add("Viikko");
            lukkariSarakkeet.Add("Paiva");
            lukkariSarakkeet.Add("Alkaa");
            lukkariSarakkeet.Add("Loppuu");
            lukkariSarakkeet.Add("Sali");

            Lukujarjestys.Columns.Add("Kello");
            Lukujarjestys.Columns.Add("Maanantai");
            Lukujarjestys.Columns.Add("Tiistai");
            Lukujarjestys.Columns.Add("Keskiviikko");
            Lukujarjestys.Columns.Add("Torstai");
            Lukujarjestys.Columns.Add("Perjantai");

            LukujarjestysViikko.Columns.Add("Kello");
            LukujarjestysViikko.Columns.Add("Maanantai");
            LukujarjestysViikko.Columns.Add("Tiistai");
            LukujarjestysViikko.Columns.Add("Keskiviikko");
            LukujarjestysViikko.Columns.Add("Torstai");
            LukujarjestysViikko.Columns.Add("Perjantai");

            DGWAsetukset();
        }

        private void DGWAsetukset()
        {
            Lukujarjestys.Rows.Clear();
            for (int i = 8; i < 21; i++)
            {
                DataRow dataR = Lukujarjestys.NewRow();
                dataR.ItemArray = new string[] { i + "-" + (i + 1), "", "", "", "", "" };
                Lukujarjestys.Rows.Add(dataR);
            }
            LukujarjestysDGV.DataSource = Lukujarjestys;
            for (int i = 0; i < LukujarjestysDGV.Columns.Count; i++)
            {
                LukujarjestysDGV.Columns[i].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                if (i != 0)
                    LukujarjestysDGV.Columns[i].Width = 180;
                else
                    LukujarjestysDGV.Columns[i].Width = 35;
            }
            LukujarjestysDGV.RowTemplate.Height = 50;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (Settings.Default["Polku"].ToString().Length > 5)
                YhdistaTietokanta(Settings.Default["Polku"].ToString());
            else
                ButtonSelaa_Click(sender, e);
        }

        private void YhdistaTietokanta(string polku)
        {
            if (!File.Exists(polku))
            {
                MessageBox.Show("Tiedostoa ei löytynyt, käytä selausta", "Virhe", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            Settings.Default["Polku"] = polku;
            Settings.Default.Save();
            AccessHandler.Yhdista(polku);
            AccessHandler.ViestienNaytto(true);
            taulut = AccessHandler.EtsiTaulut();
            taulut.Remove("Lukkari");
            DTC = AccessHandler.HaeTaulut(taulut);
            AccessHandler.TaulunTiedot(DTC);
            SQLRakentaja.DTC = DTC;

            sarakkeet = new List<string>();
            sarakkeet.Add("Nimi");
            sarakkeet.Add("Periodi");
            sarakkeet.Add("Viikko");
            sarakkeet.Add("Paiva");
            sarakkeet.Add("Alkaa");
            sarakkeet.Add("Loppuu");
            sarakkeet.Add("Sali");
            sarakkeet.Add("Huom");

            data = new List<string>();
            data.Add("VARCHAR(255)");
            data.Add("VARCHAR(255)");
            data.Add("VARCHAR(255)");
            data.Add("VARCHAR(255)");
            data.Add("VARCHAR(255)");
            data.Add("VARCHAR(255)");
            data.Add("VARCHAR(255)");
            data.Add("VARCHAR(255)");

            if (taulut.Count > 0)
                TeePuu();
            var cult = CultureInfo.CurrentCulture;
            var weekNo = cult.Calendar.GetWeekOfYear(
                System.DateTime.Now,
                cult.DateTimeFormat.CalendarWeekRule,
                cult.DateTimeFormat.FirstDayOfWeek);
            LabelViikko.Text = "Viikko " + weekNo;
            TeeLukujarjestysDT();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            AccessHandler.SuljeYhteys();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            int xMuutos = this.Width - frameSize.Width;
            int yMuutos = this.Height - frameSize.Height;

            panel1.Location = new Point(PanelPoint.X, PanelPoint.Y);
            CheckBoxOmat.Location = new Point(OmatPoint.X, OmatPoint.Y + yMuutos);

            Puu.Height = PuuSize.Height + yMuutos;
            Puu.Location = new Point(PuuPoint.X ,PuuPoint.Y);

            TextBoxEtsi.Location = new Point(HakuPoint.X, HakuPoint.Y);

            LabelNimi.Location = new Point(splitContainer1.Panel2.Width-88, NimiPoint.Y + yMuutos);
        }

        private void Puu_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                NodeNimi = e.Node.Text;
                if(e.Node.Level != 2)
                {
                    if (e.Node.Level == 0)
                        NodeParent = true;
                    else
                        NodeParent = false;
                    NodeValittu = e.Node;
                    cMenu.Show(Puu, e.Location);                    
                }
            }
        }

        private void ButtonSeuraava_Click(object sender, EventArgs e)
        {
            if (int.Parse(LabelViikko.Text.Substring(LabelViikko.Text.IndexOf(" "))) == 52)
                LabelViikko.Text = "Viikko 0";
            LabelViikko.Text = "Viikko " + (int.Parse(LabelViikko.Text.Substring(LabelViikko.Text.IndexOf(" "))) + 1);
            DGWAsetukset();
            TeeLukujarjestysDT();
        }

        private void ButtonEdellinen_Click(object sender, EventArgs e)
        {
            if (int.Parse(LabelViikko.Text.Substring(LabelViikko.Text.IndexOf(" "))) == 1)
                LabelViikko.Text = "Viikko 53";
            LabelViikko.Text = "Viikko " + (int.Parse(LabelViikko.Text.Substring(LabelViikko.Text.IndexOf(" "))) - 1);
            DGWAsetukset();
            TeeLukujarjestysDT();
        }

        private void TextBoxEtsi_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                foreach (TreeNode node in Puu.Nodes)
                {
                    Puu.Select();
                    System.Diagnostics.Debug.WriteLine(node.Text);
                    if (node.Text.Equals(TextBoxEtsi.Text))
                        Puu.SelectedNode = node;
                }
            }

        }

        private void RadioOmat_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox rd = sender as CheckBox;
            if (rd.Checked)
            {
                List<TreeNode> poistettavat = new List<TreeNode>();
                foreach(TreeNode node in Puu.Nodes)
                {
                    if(node.ForeColor != Color.DarkGreen)
                    {
                        poistettavat.Add(node);
                        taulut.Remove(node.Text);
                    }
                }
                Puu.BeginUpdate();
                foreach (TreeNode i in poistettavat)
                {
                    Puu.Nodes.Remove(i);
                }
                Puu.EndUpdate();
            }
            else
            {
                taulut.Clear();
                taulut = AccessHandler.EtsiTaulut();
                taulut.Remove("Lukkari");
                TeePuu();
            }
        }

        private void ButtonSelaa_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialogi = new OpenFileDialog();
            dialogi.Filter = ".mdb|*.mdb";
            dialogi.Multiselect = false;
            var res = dialogi.ShowDialog();
            if(res == DialogResult.OK)
            {
                YhdistaTietokanta(dialogi.FileName);
            }
        }
    }
}
