/*
 Data: 28/10/2026
 Programmatore: Claudio Zucca

 */

// Commento vuoto
//***********************************************************
//
//***********************************************************

using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ColoriResistenze
{
    public partial class Form1 : Form
    {
        private float valoreResistenza = 0; // valore della resistenza calcolato
        private char ohmSign = '\u2126'; // Simbolo Ohm dedicato
        private List<String> lstColonna1 = new List<String>(); // liste dei valori
        private List<String> lstColonna2 = new List<String>();
        private List<String> lstColonna3 = new List<String>();
        private List<String> lstMoltiplicatore = new List<String>();

        private List<String> lstTolleranze = new List<String>();
        private List<String> lstValoriImpostati = new List<String>();

        private List<Color> lstColoriBande_1_2_3 = new List<Color>();
        private List<Color> lstColoriBandaToll = new List<Color>();
        private List<RadioButton> lstRb1Banda;
        private List<RadioButton> lstRb2Banda;
        private List<RadioButton> lstRb3Banda;
        private List<RadioButton> lstRbBandaMolt;
        private List<RadioButton> lstRbTolleranze;
        private int banda1Selezione = 0;
        private int banda2Selezione = 0;
        private int banda3Selezione = 0;
        private int bandaMolt = 0;
        private int bandaTollSelezione = 0;
        private bool debug = false;

        public Form1()
        {
            InitializeComponent();
            RiempiListe();                     // Riempie le liste con i valori
            RienpiListeColoriBande();          // Riempie le liste con i colori
            RienpiListeColoriToll();         // Riempie le liste con i colori della tolleranza
            Lbl_Valore.Text = "_____________"; //Inizializza la label del risultato
            // Inizializza i colori delle bande tutte allo Zero
            PanelBanda1.BackColor = lstColoriBande_1_2_3[0];
            PanelBanda2.BackColor = lstColoriBande_1_2_3[0];
            PanelBanda3.BackColor = lstColoriBande_1_2_3[0];
            PanelBandaToll.BackColor = lstColoriBandaToll[0];
            //List per i RadioButton
            //
            //RadioButton della prima banda
            lstRb1Banda = new List<RadioButton>
            { RadioB1_0, RadioB1_1, RadioB1_2, RadioB1_3, RadioB1_4,
              RadioB1_5, RadioB1_6, RadioB1_7, RadioB1_8, RadioB1_9
            };
            // RadioButton della seconda banda
            lstRb2Banda = new List<RadioButton>
            { RadioB2_0, RadioB2_1, RadioB2_2, RadioB2_3, RadioB2_4,
              RadioB2_5, RadioB2_6, RadioB2_7, RadioB2_8, RadioB2_9
            };
            // RadioButton della terza banda
            lstRb3Banda = new List<RadioButton>
            { RadioB3_0, RadioB3_1, RadioB3_2, RadioB3_3, RadioB3_4,
              RadioB3_5, RadioB3_6, RadioB3_7, RadioB3_8, RadioB3_9
            };
            // RadioButton della quarta banda (moltiplicatore)
            lstRbBandaMolt = new List<RadioButton>
            { RadioB_Molt_0, RadioB_Molt_1, RadioB_Molt_2, RadioB_Molt_3, RadioB_Molt_4,
              RadioB_Molt_5, RadioB_Molt_6, RadioB_Molt_7
            };
            // RadioButton della quinta banda (tolleranza)
            lstRbTolleranze = new List<RadioButton>
            { RadioB_Toll_0, RadioB_Toll_1, RadioB_Toll_2, RadioB_Toll_3, RadioB_Toll_4,
              RadioB_Toll_5, RadioB_Toll_6, RadioB_Toll_7
            };
            AggiornaLabelRisultato(); // Aggiorna la label del risultato all'avvio dell'applicazione
        }

        //***********************************************************
        // Pulsante Esci
        //***********************************************************
        private void Btn_Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //***********************************************************
        // Riempimento delle liste con i valori
        //***********************************************************
        private void RiempiListe()
        {
            // Prima Banda
            lstColonna1.Add("0");
            lstColonna1.Add("1");
            lstColonna1.Add("2");
            lstColonna1.Add("3");
            lstColonna1.Add("4");
            lstColonna1.Add("5");
            lstColonna1.Add("6");
            lstColonna1.Add("7");
            lstColonna1.Add("8");
            lstColonna1.Add("9");
            //Seconda banda uguale alla prima
            lstColonna2.AddRange(lstColonna1);

            //Terza banda uguale alla prima
            lstColonna3.AddRange(lstColonna1);

            //Quarta banda MOLTIPLICATORE
            lstMoltiplicatore.Add(" ");
            lstMoltiplicatore.Add("0");
            lstMoltiplicatore.Add("00");
            lstMoltiplicatore.Add("000");      //K
            lstMoltiplicatore.Add("0000");     //K
            lstMoltiplicatore.Add("00000");    //K
            lstMoltiplicatore.Add("000000");   //M
            lstMoltiplicatore.Add("0000000");  //M
            lstMoltiplicatore.Add("00000000"); //M

            //Quarta banda TOLLERANZA
            lstTolleranze.Add("+/- 1%");
            lstTolleranze.Add("+/- 2%");
            lstTolleranze.Add("+/- 0.5%");
            lstTolleranze.Add("+/- 0.25%");
            lstTolleranze.Add("+/- 0.1%");
            lstTolleranze.Add("+/- 0.05%");
            lstTolleranze.Add("+/- 5%");
            lstTolleranze.Add("+/- 10%");
        }

        //***********************************************************
        // aggiorna la label del risultato in tempo reale
        //***********************************************************

        private void AggiornaLabelRisultato()
        {
            if (banda1Selezione == 0)
            {
                Lbl_Valore.Text = $"{lstColonna2[banda2Selezione]}{lstColonna3[banda3Selezione]}{ohmSign} - Toll. {lstTolleranze[bandaTollSelezione]}";
            }
            else
            {
                Lbl_Valore.Text = $"{lstColonna1[banda1Selezione]}{lstColonna2[banda2Selezione]}{lstColonna3[banda3Selezione]}{ohmSign} - Toll. {lstTolleranze[bandaTollSelezione]}";
            }
        }

        //***********************************************************
        // Esecuzione dei calcoli per RADIOBUTTON
        //***********************************************************
        private void ComponiValore_RadioButton()
        {
            if (debug)
            {
                MessageBox.Show($"BANDA 1: {banda1Selezione}\nBANDA 2: {banda2Selezione}\nBANDA 3: {banda3Selezione}\nTOLLERANZA: {bandaTollSelezione}");
            }
            else
            {
                if (banda1Selezione == 0)
                {
                    Lbl_Valore.Text = $"{lstColonna2[banda2Selezione]}{lstColonna3[banda3Selezione]}{ohmSign} - Toll. {lstTolleranze[bandaTollSelezione]}";
                }
                else
                {
                    Lbl_Valore.Text = $"{lstColonna1[banda1Selezione]}{lstColonna2[banda2Selezione]}{lstColonna3[banda3Selezione]}{ohmSign} - Toll. {lstTolleranze[bandaTollSelezione]}";
                }
            }
        }

        //**************************************************************************************************************
        //*****************************************************************************
        // INIZIO gestione radio button alla selezione e assegnazione valore. COLONNA 1
        //*****************************************************************************

        // RadioButton 0
        private void RadioB1_0_Click(object sender, EventArgs e)
        {
            RadioButtonSelezionato_1_Banda(0);
            banda1Selezione = 0;
            PanelBanda1.BackColor = lstColoriBande_1_2_3[0];
            AggiornaLabelRisultato();
        }

        // RadioButton 1
        private void RadioB1_1_Click(object sender, EventArgs e)
        {
            RadioButtonSelezionato_1_Banda(1);
            banda1Selezione = 1;
            PanelBanda1.BackColor = lstColoriBande_1_2_3[1];
            AggiornaLabelRisultato();
        }

        // RadioButton 2
        private void RadioB1_2_Click(object sender, EventArgs e)
        {
            RadioButtonSelezionato_1_Banda(2);
            banda1Selezione = 2;
            PanelBanda1.BackColor = lstColoriBande_1_2_3[2];
            AggiornaLabelRisultato();
        }

        // RadioButton 3
        private void RadioB1_3_Click(object sender, EventArgs e)
        {
            RadioButtonSelezionato_1_Banda(3);
            banda1Selezione = 3;
            PanelBanda1.BackColor = lstColoriBande_1_2_3[3];
            AggiornaLabelRisultato();
        }

        // RadioButton 4
        private void RadioB1_4_Click(object sender, EventArgs e)
        {
            RadioButtonSelezionato_1_Banda(4);
            banda1Selezione = 4;
            PanelBanda1.BackColor = lstColoriBande_1_2_3[4];
            AggiornaLabelRisultato();
        }

        // RadioButton 5
        private void RadioB1_5_Click(object sender, EventArgs e)
        {
            RadioButtonSelezionato_1_Banda(5);
            banda1Selezione = 5;
            PanelBanda1.BackColor = lstColoriBande_1_2_3[5];
            AggiornaLabelRisultato();
        }

        // RadioButton 6
        private void RadioB1_6_Click(object sender, EventArgs e)
        {
            RadioButtonSelezionato_1_Banda(6);
            banda1Selezione = 6;
            PanelBanda1.BackColor = lstColoriBande_1_2_3[6];
            AggiornaLabelRisultato();
        }

        // RadioButton 7
        private void RadioB1_7_Click(object sender, EventArgs e)
        {
            RadioButtonSelezionato_1_Banda(7);
            banda1Selezione = 7;
            PanelBanda1.BackColor = lstColoriBande_1_2_3[7];
            AggiornaLabelRisultato();
        }

        // RadioButton 8
        private void RadioB1_8_Click(object sender, EventArgs e)
        {
            RadioButtonSelezionato_1_Banda(8);
            banda1Selezione = 8;
            PanelBanda1.BackColor = lstColoriBande_1_2_3[8];
            AggiornaLabelRisultato();
        }

        // RadioButton 9
        private void RadioB1_9_Click(object sender, EventArgs e)
        {
            RadioButtonSelezionato_1_Banda(9);
            banda1Selezione = 9;
            PanelBanda1.BackColor = lstColoriBande_1_2_3[9];
            AggiornaLabelRisultato();
        }

        //*****************************************************************************
        // INIZIO gestione radio button alla selezione e assegnazione valore. COLONNA 2
        //*****************************************************************************

        // RadioButton 0
        private void RadioB2_0_Click(object sender, EventArgs e)
        {
            RadioButtonSelezionato_2_Banda(0);
            banda2Selezione = 0;
            PanelBanda2.BackColor = lstColoriBande_1_2_3[9];
            AggiornaLabelRisultato();
        }

        // RadioButton 1
        private void RadioB2_1_Click(object sender, EventArgs e)
        {
            RadioButtonSelezionato_2_Banda(1);
            banda2Selezione = 1;
            PanelBanda2.BackColor = lstColoriBande_1_2_3[1];
            AggiornaLabelRisultato();
        }

        // RadioButton 2
        private void RadioB2_2_Click(object sender, EventArgs e)
        {
            RadioButtonSelezionato_2_Banda(2);
            banda2Selezione = 2;
            PanelBanda2.BackColor = lstColoriBande_1_2_3[2];
            AggiornaLabelRisultato();
        }

        // RadioButton 3
        private void RadioB2_3_Click(object sender, EventArgs e)
        {
            RadioButtonSelezionato_2_Banda(3);
            banda2Selezione = 3;
            PanelBanda2.BackColor = lstColoriBande_1_2_3[3];
            AggiornaLabelRisultato();
        }

        // RadioButton 4
        private void RadioB2_4_Click(object sender, EventArgs e)
        {
            RadioButtonSelezionato_2_Banda(4);
            banda2Selezione = 4;
            PanelBanda2.BackColor = lstColoriBande_1_2_3[4];
            AggiornaLabelRisultato();
        }

        // RadioButton 5
        private void RadioB2_5_Click(object sender, EventArgs e)
        {
            RadioButtonSelezionato_2_Banda(5);
            banda2Selezione = 5;
            PanelBanda2.BackColor = lstColoriBande_1_2_3[5];
            AggiornaLabelRisultato();
        }

        // RadioButton 6
        private void RadioB2_6_Click(object sender, EventArgs e)
        {
            RadioButtonSelezionato_2_Banda(6);
            banda2Selezione = 6;
            PanelBanda2.BackColor = lstColoriBande_1_2_3[6];
            AggiornaLabelRisultato();
        }

        // RadioButton 7
        private void RadioB2_7_Click(object sender, EventArgs e)
        {
            RadioButtonSelezionato_2_Banda(7);
            banda2Selezione = 7;
            PanelBanda2.BackColor = lstColoriBande_1_2_3[7];
            AggiornaLabelRisultato();
        }

        // RadioButton 8
        private void RadioB2_8_Click(object sender, EventArgs e)
        {
            RadioButtonSelezionato_2_Banda(8);
            banda2Selezione = 8;
            PanelBanda2.BackColor = lstColoriBande_1_2_3[8];
            AggiornaLabelRisultato();
        }

        // RadioButton 9
        private void RadioB2_9_Click(object sender, EventArgs e)
        {
            RadioButtonSelezionato_2_Banda(9);
            banda2Selezione = 9;
            PanelBanda2.BackColor = lstColoriBande_1_2_3[9];
            AggiornaLabelRisultato();
        }

        //*****************************************************************************
        // INIZIO gestione radio button alla selezione e assegnazione valore. COLONNA 3
        //*****************************************************************************

        // RadioButton 0
        private void RadioB3_0_Click(object sender, EventArgs e)
        {
            RadioButtonSelezionato_3_Banda(0);
            banda3Selezione = 0;
            PanelBanda3.BackColor = lstColoriBande_1_2_3[0];
            AggiornaLabelRisultato();
        }

        // RadioButton 1
        private void RadioB3_1_Click(object sender, EventArgs e)
        {
            RadioButtonSelezionato_3_Banda(1);
            banda3Selezione = 1;
            PanelBanda3.BackColor = lstColoriBande_1_2_3[1];
            AggiornaLabelRisultato();
        }

        // RadioButton 2
        private void RadioB3_2_Click(object sender, EventArgs e)
        {
            RadioButtonSelezionato_3_Banda(2);
            banda3Selezione = 2;
            PanelBanda3.BackColor = lstColoriBande_1_2_3[2];
            AggiornaLabelRisultato();
        }

        // RadioButton 3
        private void RadioB3_3_Click(object sender, EventArgs e)
        {
            RadioButtonSelezionato_3_Banda(3);
            banda3Selezione = 3;
            PanelBanda3.BackColor = lstColoriBande_1_2_3[3];
            AggiornaLabelRisultato();
        }

        // RadioButton 4
        private void RadioB3_4_Click(object sender, EventArgs e)
        {
            RadioButtonSelezionato_3_Banda(4);
            banda3Selezione = 4;
            PanelBanda3.BackColor = lstColoriBande_1_2_3[4];
            AggiornaLabelRisultato();
        }

        // RadioButton 5
        private void RadioB3_5_Click(object sender, EventArgs e)
        {
            RadioButtonSelezionato_3_Banda(5);
            banda3Selezione = 5;
            PanelBanda3.BackColor = lstColoriBande_1_2_3[5];
            AggiornaLabelRisultato();
        }

        // RadioButton 6
        private void RadioB3_6_Click(object sender, EventArgs e)
        {
            RadioButtonSelezionato_3_Banda(6);
            banda3Selezione = 6;
            PanelBanda3.BackColor = lstColoriBande_1_2_3[6];
            AggiornaLabelRisultato();
        }

        // RadioButton 7
        private void RadioB3_7_Click(object sender, EventArgs e)
        {
            RadioButtonSelezionato_3_Banda(7);
            banda3Selezione = 7;
            PanelBanda3.BackColor = lstColoriBande_1_2_3[7];
            AggiornaLabelRisultato();
        }

        // RadioButton 8
        private void RadioB3_8_Click(object sender, EventArgs e)
        {
            RadioButtonSelezionato_3_Banda(8);
            banda3Selezione = 8;
            PanelBanda3.BackColor = lstColoriBande_1_2_3[8];
            AggiornaLabelRisultato();
        }

        // RadioButton 9
        private void RadioB3_9_Click(object sender, EventArgs e)
        {
            RadioButtonSelezionato_3_Banda(9);
            banda3Selezione = 9;
            PanelBanda3.BackColor = lstColoriBande_1_2_3[9];
            AggiornaLabelRisultato();
        }

        //*********************************************************************************
        // INIZIO gestione radio button alla selezione e assegnazione valore MOLTIPLICATORE
        //*********************************************************************************

        // RadioButton 0
        private void RadioB_Molt_0_Click(object sender, EventArgs e)
        {
            RadioButtonSelezionato_Moltiplicatore(0);
            banda3Selezione = 0;
            PanelBanda3.BackColor = lstColoriBande_1_2_3[0];
            AggiornaLabelRisultato();
        }

        //******************************************************************************
        // INIZIO gestione radio button alla selezione e assegnazione valore. TOLLERANZA
        //******************************************************************************

        // RadioButton 0
        private void RadioB_Toll_0_Click(object sender, EventArgs e)
        {
            RadioButtonSelezionato_Tolleranza(0);
            bandaTollSelezione = 0;
            PanelBandaToll.BackColor = lstColoriBandaToll[0];
            AggiornaLabelRisultato();
        }

        // RadioButton 1
        private void RadioB_Toll_1_Click(object sender, EventArgs e)
        {
            RadioButtonSelezionato_Tolleranza(1);
            bandaTollSelezione = 1;
            PanelBandaToll.BackColor = lstColoriBandaToll[1];
            AggiornaLabelRisultato();
        }

        // RadioButton 2
        private void RadioB_Toll_2_Click(object sender, EventArgs e)
        {
            RadioButtonSelezionato_Tolleranza(2);
            bandaTollSelezione = 2;
            PanelBandaToll.BackColor = lstColoriBandaToll[2];
            AggiornaLabelRisultato();
        }

        // RadioButton 3
        private void RadioB_Toll_3_Click(object sender, EventArgs e)
        {
            RadioButtonSelezionato_Tolleranza(3);
            bandaTollSelezione = 3;
            PanelBandaToll.BackColor = lstColoriBandaToll[3];
            AggiornaLabelRisultato();
        }

        // RadioButton 4
        private void RadioB_Toll_4_Click(object sender, EventArgs e)
        {
            RadioButtonSelezionato_Tolleranza(4);
            bandaTollSelezione = 4;
            PanelBandaToll.BackColor = lstColoriBandaToll[4];
            AggiornaLabelRisultato();
        }

        // RadioButton 5
        private void RadioB_Toll_5_Click(object sender, EventArgs e)
        {
            RadioButtonSelezionato_Tolleranza(5);
            bandaTollSelezione = 5;
            PanelBandaToll.BackColor = lstColoriBandaToll[5];
            AggiornaLabelRisultato();
        }

        // RadioButton 6
        private void RadioB_Toll_6_Click(object sender, EventArgs e)
        {
            RadioButtonSelezionato_Tolleranza(6);
            bandaTollSelezione = 6;
            PanelBandaToll.BackColor = lstColoriBandaToll[6];
            AggiornaLabelRisultato();
        }

        // RadioButton 7
        private void RadioB_Toll_7_Click(object sender, EventArgs e)
        {
            RadioButtonSelezionato_Tolleranza(7);
            bandaTollSelezione = 7;
            PanelBandaToll.BackColor = lstColoriBandaToll[7];
            AggiornaLabelRisultato();
        }

        //************************************* Gestione Switch dei RadioButton ****************************************
        //***********************************************************
        // Gestione delle selezioni dei RadioButton della PRIMA banda
        //***********************************************************
        private void RadioButtonSelezionato_1_Banda(int attivo)
        {
            for (int i = 0; i < lstRb1Banda.Count; i++)
            {
                if (i == attivo)
                {
                    lstRb1Banda[i].Checked = true;
                }
                else
                {
                    lstRb1Banda[i].Checked = false;
                }
            }
        }

        //*************************************************************
        // Gestione delle selezioni dei RadioButton della SECONDA banda
        //*************************************************************
        private void RadioButtonSelezionato_2_Banda(int attivo)
        {
            for (int i = 0; i < lstRb2Banda.Count; i++)
            {
                if (i == attivo)
                {
                    lstRb2Banda[i].Checked = true;
                }
                else
                {
                    lstRb2Banda[i].Checked = false;
                }
            }
        }

        //*************************************************************
        // Gestione delle selezioni dei RadioButton della TERZA banda
        //*************************************************************
        private void RadioButtonSelezionato_3_Banda(int attivo)
        {
            for (int i = 0; i < lstRb3Banda.Count; i++)
            {
                if (i == attivo)
                {
                    lstRb3Banda[i].Checked = true;
                }
                else
                {
                    lstRb3Banda[i].Checked = false;
                }
            }
        }

        //*************************************************************
        // Gestione delle selezioni dei RadioButton del MOLTIPLICATORE
        //*************************************************************
        private void RadioButtonSelezionato_Moltiplicatore(int attivo)
        {
            for (int i = 0; i < lstRbBandaMolt.Count; i++)
            {
                if (i == attivo)
                {
                    lstRbBandaMolt[i].Checked = true;
                }
                else
                {
                    lstRbBandaMolt[i].Checked = false;
                }
            }
        }

        //*************************************************************
        // Gestione delle selezioni dei RadioButton della banda TOLLERANZA
        //*************************************************************
        private void RadioButtonSelezionato_Tolleranza(int attivo)
        {
            for (int i = 0; i < lstRbTolleranze.Count; i++)
            {
                if (i == attivo)
                {
                    lstRbTolleranze[i].Checked = true;
                }
                else
                {
                    lstRbTolleranze[i].Checked = false;
                }
            }
        }

        //************************************* Riempimento liste clori bande ******************************************
        //*************************************************************
        // Riempimento della lista dei colori Bande 1-2-3
        //*************************************************************
        private void RienpiListeColoriBande()
        {
            // La seguente lista viene caricata normalmente, senza ciclicità,
            // perché i colori sono sempre gli stessi e non cambiano mai.
            // I colori sono associati alle bande 1-2-3.
            lstColoriBande_1_2_3.Add(Color.Black);   //0
            lstColoriBande_1_2_3.Add(Color.Brown);   //1
            lstColoriBande_1_2_3.Add(Color.Red);     //2
            lstColoriBande_1_2_3.Add(Color.Orange);  //3
            lstColoriBande_1_2_3.Add(Color.Yellow);  //4
            lstColoriBande_1_2_3.Add(Color.Green);   //5
            lstColoriBande_1_2_3.Add(Color.Blue);    //6
            lstColoriBande_1_2_3.Add(Color.Purple);  //7
            lstColoriBande_1_2_3.Add(Color.Gray);    //8
            lstColoriBande_1_2_3.Add(Color.White);   //9
        }

        //*************************************************************
        // Riempimento della lista dei colori banda TOLLERANZA
        //*************************************************************
        private void RienpiListeColoriToll()
        {
            // La seguente lista viene caricata normalmente, senza ciclicità,
            // perché i colori sono sempre gli stessi e non cambiano mai.
            // I colori sono associati alla banda TOLLERANZA.
            lstColoriBandaToll.Add(Color.Brown);   //1%
            lstColoriBandaToll.Add(Color.Red);     //2%
            lstColoriBandaToll.Add(Color.Green);   //0.5%
            lstColoriBandaToll.Add(Color.Blue);    //0.25%
            lstColoriBandaToll.Add(Color.Purple);  //0.1%
            lstColoriBandaToll.Add(Color.Gray);    //0.05%
            lstColoriBandaToll.Add(Color.Gold);    //5%
            lstColoriBandaToll.Add(Color.Silver);  //10%
        }

        private void About_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Colori Resistenze v1.0\n\nBy: Claudio Zucca\nData: 28/10/2026", "Informazioni", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        //**************************************************************************************************************
    }
}
