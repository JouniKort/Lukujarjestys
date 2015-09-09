using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using AccessHandler = AccessSQL.AccessHandler;
using SQLRakentaja = AccessSQL.SQLRakentaja;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;
using System.Globalization;
using System.Text.RegularExpressions;
using Lukujärjestys.Properties;
using System.IO;

namespace Lukujärjestys
{
    public partial class Form1 : Form
    {
        private string NodeNimi;
        private string Paiva;

        private TreeNode NodeValittu;

        private bool NodeParent;
        private bool sovitettu = false;
        private bool Paallekkaisia = false;
        private bool Sisaiset = false;
        private bool Ulkoiset = true;

        private List<TreeNode> valitut = new List<TreeNode>();
        private List<string> taulut;
        private List<string> sarakkeet;
        private List<string> lukkariSarakkeet = new List<string>();
        private List<string> data;
        private List<string> URLit = new List<string>();
        private string[] Paivat = { "", "ma", "ti", "ke", "to", "pe" };                 //Tyhjä kelloa verten
        private string[] AlkuKello = { "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18" };
        private string[] LoppuKello = {  "9", "10", "11", "12", "13", "14", "15", "16", "17", "18","19","20" };

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
        private Point ApuPoint;
        private Point SovitaPoint;

        private int Panel2Leveys;

        private ToolTip tip1 = new ToolTip();
        private ToolTip tip2 = new ToolTip();
        private ToolTip tip3 = new ToolTip();

        private ContextMenu cMenuPuu = new ContextMenu();

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
            ApuPoint = ButtonApu.Location;
            SovitaPoint = ButtonSovita.Location;
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
            sarake.Add("Paiva");
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
                string Tieto = DT.Rows[0][0].ToString();                                //Nimen muodostus alkaa
                string KurssinNimiJaTyyppi = Tieto.Substring(Tieto.IndexOf("-")+1,Tieto.Length-Tieto.IndexOf("-")-1).TrimStart();
                string KurssinNimi = "";
                if (KurssinNimiJaTyyppi.Contains("/"))
                    KurssinNimi = KurssinNimiJaTyyppi.Substring(0, KurssinNimiJaTyyppi.IndexOf("/"));
                else
                    KurssinNimi = KurssinNimiJaTyyppi;
                Puu.Nodes[Puu.Nodes.Count - 1].ToolTipText = KurssinNimi;               //Nimen lisäys
                Puu.Nodes[indeksi].ForeColor = Color.DarkRed;
                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    TreeNode lisattava = new TreeNode(DT.Rows[i][0].ToString());
                    lisattava.ToolTipText = DT.Rows[i][2].ToString();                   //Päivän lisäys
                    Puu.Nodes[indeksi].Nodes.Add(lisattava);
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
            cMenuPuu.MenuItems.Add(it1);
            it1.Click += new EventHandler(SubMenuClickPuu);
            MenuItem it2 = new MenuItem("Poista");
            it2.Click += new EventHandler(SubMenuClickPuu);
            cMenuPuu.MenuItems.Add(it2);
            MenuItem it3 = new MenuItem("Poista tieto");
            it3.Click += new EventHandler(SubMenuClickPuu);
            cMenuPuu.MenuItems.Add(it3);
        }

        private void SubMenuClickPuu(object sender, EventArgs e)
        {
            valitut.Clear();
            MenuItem it = sender as MenuItem;
            AccessHandler.Viesti("SubMenuClick "+it.Text+" "+NodeNimi);
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
                        valitut.Add(NodeValittu.Nodes[i]);
                    }
                }
                else if(it.Text.Equals("Poista"))
                {
                    parent = NodeValittu.Parent.Text;
                    NodeValittu.Parent.ForeColor = Color.DarkRed;
                    valitut.Add(NodeValittu);
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
                    valitut.Add(NodeValittu);
                    parent = NodeValittu.Parent.Text;                                   //Myös parentin tekstin väri muutetaan
                }
                else
                {
                    NodeValittu.ForeColor = Color.DarkRed;                              //Pois näkyvistä, asetetaan teksti punaiseksi
                    valitut.Add(NodeValittu);
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
            else if(!NodeParent && it.Text.Equals("Poista tieto"))
            {
                MessageBox.Show("Yksittäisiä tietueita ei voi poistaa.\nPoista ylin solmu!", "Huom", MessageBoxButtons.OK);
            }
            if(valitut.Count > 0)
            {
                MuutaTieto(parent, valitut, it.Text);
                DGVAsetukset();
                TeeLukujarjestysRAW();
                TeeLukujarjestysDT();
            }
        }

        private void SubMenuClickDGV(object sender, EventArgs e)
        {
            MenuItem it = sender as MenuItem;
            string teksti = it.Text;
            AccessHandler.Viesti("Valittiin " + teksti);
            string koodi = teksti.Substring(0, teksti.IndexOf("-")).Trim();
            string nimi = teksti.Remove(0, teksti.IndexOf("-")).Trim();

            NodeParent = false;                                                         //Node on aina Child
            NodeValittu = null;                                                         //Etsitään tietoa vastaava solmu puusta
            foreach(TreeNode node in Puu.Nodes)
                if (koodi.Contains(node.Text))
                    foreach(TreeNode child in node.Nodes)
                        if (child.Text.Contains(nimi) && child.ToolTipText.Equals(Paiva))
                        {
                            NodeValittu = child;
                            break;
                        }
            if (NodeValittu == null)
                return;
            it.Text = "Poista";                                                         //MenuItem vastaa cMenuPuu:n MenuItemiä
            SubMenuClickPuu(it, e);                                                     //Ajetaan funktio ohjelmallisesti
        }

        private void MuutaTieto(string taulu, List<TreeNode> rivi, string komento)
        {
            DataTable valinnat = new DataTable();
            valinnat.Columns.Add("Nimi");
            valinnat.Columns.Add("Operaattori");
            valinnat.Columns.Add("Arvo1");
            valinnat.Columns.Add("Arvo2");

            foreach(TreeNode node in rivi)
            {
                AccessHandler.Viesti(komento+" "+node.Text+" taulusta "+taulu);
                DataRow dr = valinnat.NewRow();
                if (node.Text.Contains("/L"))                                           //Luento /L
                {
                    dr.ItemArray = new string[] { "Nimi", "=", node.Text, "" };
                    valinnat.Rows.Add(new string[] { "Paiva", "=", node.ToolTipText }); //Luentojen kohdalla lisätään uusi rivi päivän tarkastusta varten, ilman tätä luentoa ei voi tunnistaa 
                }
                else
                    dr.ItemArray = new string[] { "Nimi", "=", node.Text ,""};
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
            AccessHandler.Viesti("Viikolla kursseja: " + LukujarjestysViikko.Rows.Count);
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
                AccessHandler.Viesti("Päivä " + paiva + " klo " + alkaa + "-" + loppuu);
                int paivaIndeksi = 0;
                int alkuIndeksi = 0;
                int loppuIndeksi = 0;

                for (int i = 1; i < Paivat.Length; i++)
                {
                    if (Paivat[i].Equals(paiva))
                    {
                        paivaIndeksi = i;
                        break;
                    }
                }

                for (int i = 0; i < AlkuKello.Length; i++)
                {
                    if (AlkuKello[i].Equals(alkaa))
                    {
                        alkuIndeksi = i;
                        break;
                    }
                }

                for (int i = 1; i < LoppuKello.Length; i++)
                {
                    if (LoppuKello[i].Equals(loppuu))
                    {
                        loppuIndeksi = i+1;
                        break;
                    }
                }

                for(int i = alkuIndeksi; i < loppuIndeksi; i++)
                {
                    DataRow dataR = Lukujarjestys.NewRow();
                    Lukujarjestys.Rows[i][paivaIndeksi] = Lukujarjestys.Rows[i][paivaIndeksi].ToString() + DR[0] + Environment.NewLine+ "Sali " + DR[5]+Environment.NewLine;
                    if (Regex.Matches(Lukujarjestys.Rows[i][paivaIndeksi].ToString(), "Sali").Count > 1)
                    {
                        LukujarjestysDGV.Rows[i].Cells[paivaIndeksi].Style.BackColor = Color.Red;
                        Paallekkaisia = true;
                    }
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

            DGVAsetukset();
        }

        private void DGVAsetukset()
        {
            Lukujarjestys.Rows.Clear();
            for (int i = 8; i < 21; i++)
            {
                DataRow dataR = Lukujarjestys.NewRow();
                dataR.ItemArray = new string[] { i + "-" + (i + 1), "", "", "", "", "" };   //"Kello" sarake
                Lukujarjestys.Rows.Add(dataR);
            }
            LukujarjestysDGV.DataSource = Lukujarjestys;

            if (!sovitettu)
                for (int i = 0; i < LukujarjestysDGV.Columns.Count; i++)
                {
                    LukujarjestysDGV.Columns[i].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                    LukujarjestysDGV.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                    if (i != 0)
                        LukujarjestysDGV.Columns[i].Width = 180;
                    else
                        LukujarjestysDGV.Columns[i].Width = 35;
                }
            LukujarjestysDGV.RowTemplate.Height = 50;
        }

        private void DGVSovita()
        {
            int sarakeLeveys = (int)(LukujarjestysDGV.Width - LukujarjestysDGV.Columns[0].Width - LukujarjestysDGV.RowHeadersWidth) / (LukujarjestysDGV.Columns.Count - 1); //leveys - kello - header
            for(int i = 0; i < LukujarjestysDGV.Columns.Count; i++)
            {
                if (i != 0)
                    LukujarjestysDGV.Columns[i].Width = sarakeLeveys-1;
            }

            if (Paallekkaisia)
            {
                Dictionary<int, int> dict = new Dictionary<int, int>();
                for (int i = 0; i < LukujarjestysDGV.Rows.Count; i++)                        //Etsitään päällekkäisiä kursseja
                {
                    for (int j = 0; j < LukujarjestysDGV.Columns.Count; j++)
                    {
                        string teksti = LukujarjestysDGV.Rows[i].Cells[j].Value.ToString();
                        Regex reg = new Regex("\n");                                        //Etsitään uudet rivit. Jokainen kurssi vie kaksi riviä
                        int kursseja = reg.Matches(teksti).Count / 2;                       //Ehden kurssin osuus muodostuu nimestä, salista sekä kahdesta uudesta rivistä
                        if (kursseja >= 2)
                        {
                            dict.Add(i,kursseja);
                        }
                    }
                }
                foreach(int key in dict.Keys)
                {
                    LukujarjestysDGV.Rows[key].Height = dict[key] * 50;
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            EtsiTietokanta();

            TextBoxEtsi.Text = "Kirjoita kurssin tunnus ja paina Enter";
            TextBoxEtsi.ForeColor = Color.Gray;
        }

        private void EtsiTietokanta()
        {
            if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Lukkari\\Lukujärjestys.mdb"))
            {
                YhdistaTietokanta(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Lukkari\\Lukujärjestys.mdb");
                MessageBox.Show(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Lukkari\\Lukujärjestys.mdb", "!", MessageBoxButtons.OK);
                return;
            }

            string polku = "";
            try
            {
                string ExeTiedosto = Environment.GetCommandLineArgs()[0];                                   //Exe:n sijainti
                for (int i = 0; i < Regex.Matches(ExeTiedosto,@"\\").Count; i++)                            //Etsitään tietostoa korkeammista kansioista
                {
                    if (Path.GetFileName(ExeTiedosto).Contains("Program Files"))
                        break;
                    int indeksi = 0;
                    indeksi = Regex.Match(ExeTiedosto, @"\\", options: RegexOptions.RightToLeft).Index;     // \:n viimeinen indeksi
                    string[] Tiedostot = Directory.GetFiles(Path.GetDirectoryName(ExeTiedosto));            //Haetaan kansion tiedosto
                    string[] Kansiot = Directory.GetDirectories(Path.GetDirectoryName(ExeTiedosto));        //Haetaan kansion kansiot
                    foreach (string kansio in Kansiot)                                                      //Käydään kansiot läpi
                    {
                        if (Path.GetFileName(kansio).Equals("Setup"))                                       //Jos kansion nimi on Setup sen sisältö tarkastetaan
                        {
                            string[] SetupTiedostot = Directory.GetFiles(kansio);                           //Setup kansion tiedostot
                            foreach (string tiedosto in SetupTiedostot)                                     //Käydään tiedostot läpi
                            {
                                if (tiedosto.Contains("Lukujärjestys.mdb"))                                 //Jos tiedoston nimi on haluttu, valitaan se poluksi
                                {
                                    polku = tiedosto;
                                    AccessHandler.Viesti(tiedosto);
                                    break;
                                }
                            }
                            break;
                        }
                    }
                    if (polku.Equals(""))                                                                   //Jos tiedostoa ei olla löydetty Setupista, käydään muut kansiot läpi
                        foreach (string tiedosto in Tiedostot)                                              //Käydään tiedostot läpi
                        {
                            if (tiedosto.Contains("Lukujärjestys.mdb"))                                     //Jos tiedoston nimi on haluttu, valitaan se poluksi
                            {
                                polku = tiedosto;
                                AccessHandler.Viesti("Tietokanta löydetty! " + tiedosto);
                                break;
                            }
                        }
                    ExeTiedosto = ExeTiedosto.Substring(0, indeksi);                                        //Leikataan polusta yksi tiedosto/kansio pois
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.InnerException.ToString(), "Virhe", MessageBoxButtons.OK);
            }
            if (File.Exists(polku))
            {
                YhdistaTietokanta(polku);
                return;
            }
            MessageBox.Show("Tietokantaa ei löydetty", "!", MessageBoxButtons.OK);
        }

        private void YhdistaTietokanta(string polku)
        {
            if (!File.Exists(polku))
            {
                MessageBox.Show("Tietokantaa ei löytynyt, käytä selausta", "Virhe", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            Settings.Default["Polku"] = polku;
            Settings.Default.Save();
            AccessHandler.Yhdista(polku);
            AccessHandler.ViestienNaytto(Ulkoiset:Ulkoiset,Sisaiset:Sisaiset);
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

            ButtonApu.Location = new Point(splitContainer1.Panel2.Width - 115, ApuPoint.Y);
            ButtonSelaa.Location = new Point(splitContainer1.Panel2.Width - 86, SelaaPoint.Y);
            ButtonSovita.Location = new Point(splitContainer1.Panel2.Width - 196, SovitaPoint.Y);

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
                    cMenuPuu.Show(Puu, e.Location);                    
                }
            }
        }

        private void ButtonSeuraava_Click(object sender, EventArgs e)
        {
            if (int.Parse(LabelViikko.Text.Substring(LabelViikko.Text.IndexOf(" "))) == 52)
                LabelViikko.Text = "Viikko 0";
            LabelViikko.Text = "Viikko " + (int.Parse(LabelViikko.Text.Substring(LabelViikko.Text.IndexOf(" "))) + 1);
            DGVAsetukset();
            TeeLukujarjestysDT();
            if (sovitettu)
                DGVSovita();
        }

        private void ButtonEdellinen_Click(object sender, EventArgs e)
        {
            if (int.Parse(LabelViikko.Text.Substring(LabelViikko.Text.IndexOf(" "))) == 1)
                LabelViikko.Text = "Viikko 53";
            LabelViikko.Text = "Viikko " + (int.Parse(LabelViikko.Text.Substring(LabelViikko.Text.IndexOf(" "))) - 1);
            DGVAsetukset();
            TeeLukujarjestysDT();
            if (sovitettu)
                DGVSovita();
        }

        private void TextBoxEtsi_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                Puu.Select();
                foreach (TreeNode node in Puu.Nodes)
                {

                    AccessHandler.Viesti(node.Text);
                    if (node.Text.ToLower().Equals(TextBoxEtsi.Text.ToLower()))
                    {
                        Puu.SelectedNode = node;
                        return;
                    }else if (node.ToolTipText.ToLower().Contains(TextBoxEtsi.Text.ToLower()))
                    {
                        Puu.SelectedNode = node;
                        return;
                    }
                }
                Puu.SelectedNode = null;
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
            dialogi.Title = "Valitse tietokanta";
            dialogi.Filter = ".mdb|*.mdb";
            dialogi.Multiselect = false;
            var res = dialogi.ShowDialog();
            if(res == DialogResult.OK)
            {
                YhdistaTietokanta(dialogi.FileName);
            }
        }

        private void LukujarjestysDGV_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right)
            {
                ContextMenu cMenuDGV = new ContextMenu();
                Paiva = Paivat[e.ColumnIndex];
                string teksti = LukujarjestysDGV.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                if (teksti.Length < 1)                                                  //Solussa ei ole tekstiä => poistutaan
                    return;
                Regex reg = new Regex("\n");
                int edellinen = 0;
                int kierrokset = 0;
                ContextMenu CM = new ContextMenu();
                foreach(Match osuma in reg.Matches(teksti))                             //Tehdään jokaiselle solun kurssille oma MenuItem ja kasataan ContextMenu
                {
                    if(kierrokset % 2 == 0)
                    {
                        string nimi = teksti.Substring(edellinen, osuma.Index - edellinen).Trim();
                        MenuItem it = new MenuItem("Poista " + nimi);
                        it.Click += new EventHandler(SubMenuClickDGV);
                        CM.MenuItems.Add(it);
                        AccessHandler.Viesti(nimi);
                    }
                    edellinen = osuma.Index;
                    kierrokset++;
                }
                CM.Show(this, this.PointToClient(Cursor.Position));
            }
        }

        private void LukujarjestysDGV_SelectionChanged(object sender, EventArgs e)
        {
            LukujarjestysDGV.ClearSelection();
        }

        private void TextBoxEtsi_Enter(object sender, EventArgs e)
        {
            if(TextBoxEtsi.ForeColor == Color.Gray)
            {
                TextBoxEtsi.Text = "";
                TextBoxEtsi.ForeColor = Color.Black;
            }
        }

        private void ButtonApu_Click(object sender, EventArgs e)
        {
            Point DGVLoc = LukujarjestysDGV.FindForm().PointToClient(LukujarjestysDGV.Parent.PointToScreen(LukujarjestysDGV.Location));
            Point MidDGVLoc = new Point(DGVLoc.X+(int)LukujarjestysDGV.Width/2, (int)LukujarjestysDGV.Height / 2);
            //verbatim literal @ stringiä voi jatkaa usealle riville
            tip1.Show(@"Lukujärjestys, tässä näkyy valittujen kurssien aikataulut.
Kursseja voi poistaa joko luettelosta tai painamalla postettavan kurssin solua hiiren oikealla näppäimellä
Päällekkäin olevien kurssien solut tulevat näkyviin punaisina.",
                this, MidDGVLoc);

            tip2.Show(@"Tässä näkyy kaikki Lappeenrannan teknillisen yliopiston sisältämät kurssit.
Kursseja voi hakea niiden nimellä tai tunnuksella yllä olevalla tekstikentällä.
Kursseja saa valittua painamalla niiden nimeä hiiren oikealla ja valitsemalla valikosta näytä.
Kursseja voi poistaa valitsemalla poista. Poista tieto poistaa kurssin tietokannasta.",
            this, Puu.FindForm().PointToClient(Puu.Parent.PointToScreen(Puu.Location)));

            tip3.Show("Tämä valittuna näet listalla vain valitsemasi kurssit", this, 
                CheckBoxOmat.FindForm().PointToClient(CheckBoxOmat.Parent.PointToScreen(CheckBoxOmat.Location)));
        }

        private void ButtonApu_Leave(object sender, EventArgs e)
        {
            tip1.Hide(this);
            tip2.Hide(this);
            tip3.Hide(this);
        }

        private void ButtonSovita_Click(object sender, EventArgs e)
        {
            if (!sovitettu)
            {
                sovitettu = true;
                DGVSovita();
            }
            else
                sovitettu = false;
        }
    }
}
