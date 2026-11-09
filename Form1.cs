/*
 Data: 28/10/2026
 Programmatore: Claudio Zucca

 Note:
      - Programma per il calcolo delle resistenze in base al colore e
        viceversa.
      - nel codice, alla dichiarazioni delle variabili vengono assegnati i seguenti prefissi:
          str --> string
          d   --> double
          ch  --> char
          fl  --> float
          bl  --> bool
          lst --> List
          frm --> Form

STRUMENTI USATI
    - Visual Studio Community 2022
    - Linguaggio C#
 */

// Commento vuoto (fare copia/incolla per nuove funzioni)
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
        //Pragma warning disable per i messaggi del compilatore non necessari
#pragma warning disable IDE0044 // Add readonly modifier
#pragma warning disable IDE0028 // Use collection initializer
#pragma warning disable IDE0059 // Unnecessary assignment of a value
#pragma warning disable IDE0079 // Unnecessary suppression
#pragma warning disable CA1822  // Mark members as static
#pragma warning disable CS0414  // The field 'Form1.strTitolo' is assigned but its value is never used

        private List<String> lstTolleranze = new();

        private List<Color> lstColoriBande_1_2_3 = new();
        private List<Color> lstColoriBandaMolt = new();
        private List<Color> lstColoriBandaToll = new();

        private List<RadioButton> lstRb1Banda;
        private List<RadioButton> lstRb2Banda;
        private List<RadioButton> lstRb3Banda;
        private List<RadioButton> lstRbBandaMolt;
        private List<RadioButton> lstRbTolleranze;

        // Dichiarazione e inizializzazione delle variabili per il calcolo
        private double dBanda1Selezione = 1;

        private double dBanda2Selezione = 0;
        private double dBanda3Selezione = 0;
        private double dBandaMolt = 0;
        private int dBandaTollSelezione = 0;

        // Booleani per verificare se le bande sono state selezionate
        private bool blBanda1Selezionata = false;

        private bool blBanda2Selezionata = false;
        private bool blBanda3Selezionata = false;
        private bool blBandaMoltSelezionata = false;

        private bool blBandaTollSelezionata = false;

        private string strIntestazionelabel = "Valore: "; // Intestazione della label del risultato
        private char chOhmSign = '\u2126'; // Simbolo Ohm

        // Inizio public
        public string strTitolo = "Colori Resistenze V 1.0"; // Titolo dell'applicazione e delle varie Form

        public Form1()
        {
            InitializeComponent();
            RiempiLista_Toll();                      // Riempie la lista con i VALORI delle tolleranze
            RienpiListeColoriBande_1_2_3();          // Riempie le liste con i COLORI per le bande 1-2-3
            RienpiListaColoriMoltiplicatore();       // Riempie le liste con i COLORI del moltiplicatore
            RienpiListeColoriToll();                 // Riempie le liste con i COLORI della tolleranza

            Lbl_Risultato.Text = strIntestazionelabel;  //Inizializza la label del risultato
            this.Text = strTitolo;                   // Imposta il titolo della finestra

            // Inizializza i colori di tutte le bande al colore "neutro" (System.Control cioe' non selezionato)
            // che sta all'indice n. 10
            PanelBanda1.BackColor = lstColoriBande_1_2_3[10];
            PanelBanda2.BackColor = lstColoriBande_1_2_3[10];
            PanelBanda3.BackColor = lstColoriBande_1_2_3[10];
            PanelBandaMolt.BackColor = lstColoriBande_1_2_3[10];
            PanelBandaToll.BackColor = lstColoriBandaToll[8];

            //List per i RadioButton

            //RadioButton della prima banda
            lstRb1Banda = new List<RadioButton>
            { RadioB1_1, RadioB1_2, RadioB1_3, RadioB1_4,
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
              RadioB_Molt_5, RadioB_Molt_6, RadioB_Molt_7,RadioB_Molt_8,RadioB_Molt_9
            };

            // RadioButton della quinta banda (tolleranza)
            lstRbTolleranze = new List<RadioButton>
            { RadioB_Toll_0, RadioB_Toll_1, RadioB_Toll_2, RadioB_Toll_3, RadioB_Toll_4,
              RadioB_Toll_5, RadioB_Toll_6, RadioB_Toll_7
            };
            Reset(); //resetta le liste con tutto deselezionato (da vagliare se va bene o no)
        }

        //***********************************************************
        // Riempimento della liste Tolleranze con i valori
        // Questa list e' dichiarata "List<string>"
        //***********************************************************
        private void RiempiLista_Toll()
        {
            //Quarta banda TOLLERANZA
            lstTolleranze.Add("+/- 1%");      // Elemento 0
            lstTolleranze.Add("+/- 2%");      // Elemento 1
            lstTolleranze.Add("+/- 0.5%");    // Elemento 2
            lstTolleranze.Add("+/- 0.25%");   // Elemento 3
            lstTolleranze.Add("+/- 0.1%");    // Elemento 4
            lstTolleranze.Add("+/- 0.05%");   // Elemento 5
            lstTolleranze.Add("+/- 5%");      // Elemento 6
            lstTolleranze.Add("+/- 10%");     // Elemento 7
        }

        //***********************************************************
        // Calcolo e aggiornamento della label del risultato in tempo
        // reale
        //***********************************************************
        private void AggiornaLabelRisultato()
        {
            string strRisultatoAbbreviato = "";
            double dRisultato;
            // string strRisultatoAbbreviato;
            if (!blBanda1Selezionata || !blBandaMoltSelezionata)
            {
                Lbl_Risultato.ForeColor = Color.Red;
                Lbl_Risultato.Text = $"{strIntestazionelabel} La banda n.1-2 e il moltiplicatore DEVONO essere selezionati.";
            }
            else if (blBanda1Selezionata && !blBanda3Selezionata && blBandaMoltSelezionata)
            {
                if (!blBanda2Selezionata)
                {
                    Lbl_Risultato.ForeColor = Color.Red;
                    Lbl_Risultato.Text = $"{strIntestazionelabel} Valore NON valido. La banda n.2 DEVE essere selezionata.";
                    return;
                }
                dRisultato = ((dBanda1Selezione * 10) + dBanda2Selezione) * dBandaMolt;
                strRisultatoAbbreviato = AbbreviazioneValore(dRisultato);
                Lbl_Risultato.ForeColor = Color.Black;
                Lbl_Risultato.Text = $"{strIntestazionelabel} {dRisultato:F2}{chOhmSign} Toll.: {lstTolleranze
                    [dBandaTollSelezione]} Abbreviato: {strRisultatoAbbreviato}{chOhmSign}";
            }
            else if (blBanda1Selezionata && blBanda3Selezionata && blBandaMoltSelezionata)
            {
                dRisultato = ((((dBanda1Selezione * 100) + (dBanda2Selezione * 10))) + dBanda3Selezione) * dBandaMolt;
                strRisultatoAbbreviato = AbbreviazioneValore(dRisultato);
                Lbl_Risultato.ForeColor = Color.Black;
                Lbl_Risultato.Text = $"{strIntestazionelabel} {dRisultato:F2}{chOhmSign} Toll.: {lstTolleranze[dBandaTollSelezione]} Abbreviato: {strRisultatoAbbreviato}{chOhmSign}";
            }
        }

        private string AbbreviazioneValore(double valore)
        {
            if (valore >= 1_000_000)
            {
                return $"{valore / 1_000_000:F2}M";
            }
            else if (valore >= 1_000)
            {
                return $"{valore / 1_000:F2}K";
            }
            else
            {
                return $"{valore:F2}";
            }
        }

        //**************************************************************************************************************
        //*****************************************************************************
        // INIZIO gestione radio button alla selezione e assegnazione valore.
        //*****************************************************************************
        // BANDA 1: questa lista parte da "0" come RadioButton ma il suo valore e' "Marrone" quindi "1".
        // L'indice della list pero' e' zero in quanto primo indice della List
        //*****************************************************************************

        //***********************************************************
        // RadioButton 1
        //***********************************************************
        private void RadioB1_1_Click(object sender, EventArgs e)
        {
            RadioButtonSelezionato_1_Banda(0);
            dBanda1Selezione = 1;
            PanelBanda1.BackColor = lstColoriBande_1_2_3[1];
            blBanda1Selezionata = true;
            AggiornaLabelRisultato();
        }

        //***********************************************************
        // RadioButton 2
        //***********************************************************
        private void RadioB1_2_Click(object sender, EventArgs e)
        {
            RadioButtonSelezionato_1_Banda(1);
            dBanda1Selezione = 2;
            PanelBanda1.BackColor = lstColoriBande_1_2_3[2];
            blBanda1Selezionata = true;
            AggiornaLabelRisultato();
        }

        //***********************************************************
        // RadioButton 3
        //***********************************************************
        private void RadioB1_3_Click(object sender, EventArgs e)
        {
            RadioButtonSelezionato_1_Banda(2);
            dBanda1Selezione = 3;
            PanelBanda1.BackColor = lstColoriBande_1_2_3[3];
            blBanda1Selezionata = true;
            AggiornaLabelRisultato();
        }

        //***********************************************************
        // RadioButton 4
        //***********************************************************
        private void RadioB1_4_Click(object sender, EventArgs e)
        {
            RadioButtonSelezionato_1_Banda(3);
            dBanda1Selezione = 4;
            PanelBanda1.BackColor = lstColoriBande_1_2_3[4];
            blBanda1Selezionata = true;
            AggiornaLabelRisultato();
        }

        //***********************************************************
        // RadioButton 5
        //***********************************************************
        private void RadioB1_5_Click(object sender, EventArgs e)
        {
            RadioButtonSelezionato_1_Banda(4);
            dBanda1Selezione = 5;
            PanelBanda1.BackColor = lstColoriBande_1_2_3[5];
            blBanda1Selezionata = true;
            AggiornaLabelRisultato();
        }

        //***********************************************************
        // RadioButton 6
        //***********************************************************
        private void RadioB1_6_Click(object sender, EventArgs e)
        {
            RadioButtonSelezionato_1_Banda(5);
            dBanda1Selezione = 6;
            PanelBanda1.BackColor = lstColoriBande_1_2_3[6];
            blBanda1Selezionata = true;
            AggiornaLabelRisultato();
        }

        //***********************************************************
        // RadioButton 7
        //***********************************************************
        private void RadioB1_7_Click(object sender, EventArgs e)
        {
            RadioButtonSelezionato_1_Banda(6);
            dBanda1Selezione = 7;
            PanelBanda1.BackColor = lstColoriBande_1_2_3[7];
            blBanda1Selezionata = true;
            AggiornaLabelRisultato();
        }

        //***********************************************************
        // RadioButton 8
        //***********************************************************
        private void RadioB1_8_Click(object sender, EventArgs e)
        {
            RadioButtonSelezionato_1_Banda(7);
            dBanda1Selezione = 8;
            PanelBanda1.BackColor = lstColoriBande_1_2_3[8];
            blBanda1Selezionata = true;
            AggiornaLabelRisultato();
        }

        //***********************************************************
        // RadioButton 9
        //***********************************************************
        private void RadioB1_9_Click(object sender, EventArgs e)
        {
            RadioButtonSelezionato_1_Banda(8);
            dBanda1Selezione = 9;
            PanelBanda1.BackColor = lstColoriBande_1_2_3[9];
            blBanda1Selezionata = true;
            AggiornaLabelRisultato();
        }

        //*****************************************************************************
        // INIZIO gestione radio button alla selezione e assegnazione valore. BANDA 2
        //*****************************************************************************

        //***********************************************************
        // RadioButton 0
        //***********************************************************
        private void RadioB2_0_Click(object sender, EventArgs e)
        {
            RadioButtonSelezionato_2_Banda(0);
            dBanda2Selezione = 0;
            PanelBanda2.BackColor = lstColoriBande_1_2_3[0];
            blBanda2Selezionata = true;
            AggiornaLabelRisultato();
        }

        //***********************************************************
        // RadioButton 1
        //***********************************************************
        private void RadioB2_1_Click(object sender, EventArgs e)
        {
            RadioButtonSelezionato_2_Banda(1);
            dBanda2Selezione = 1;
            PanelBanda2.BackColor = lstColoriBande_1_2_3[1];
            blBanda2Selezionata = true;
            AggiornaLabelRisultato();
        }

        //***********************************************************
        // RadioButton 2
        //***********************************************************
        private void RadioB2_2_Click(object sender, EventArgs e)
        {
            RadioButtonSelezionato_2_Banda(2);
            dBanda2Selezione = 2;
            PanelBanda2.BackColor = lstColoriBande_1_2_3[2];
            blBanda2Selezionata = true;
            AggiornaLabelRisultato();
        }

        //***********************************************************
        // RadioButton 3
        //***********************************************************
        private void RadioB2_3_Click(object sender, EventArgs e)
        {
            RadioButtonSelezionato_2_Banda(3);
            dBanda2Selezione = 3;
            PanelBanda2.BackColor = lstColoriBande_1_2_3[3];
            blBanda2Selezionata = true;
            AggiornaLabelRisultato();
        }

        //***********************************************************
        // RadioButton 4
        //***********************************************************
        private void RadioB2_4_Click(object sender, EventArgs e)
        {
            RadioButtonSelezionato_2_Banda(4);
            dBanda2Selezione = 4;
            PanelBanda2.BackColor = lstColoriBande_1_2_3[4];
            blBanda2Selezionata = true;
            AggiornaLabelRisultato();
        }

        //***********************************************************
        // RadioButton 5
        //***********************************************************
        private void RadioB2_5_Click(object sender, EventArgs e)
        {
            RadioButtonSelezionato_2_Banda(5);
            dBanda2Selezione = 5;
            PanelBanda2.BackColor = lstColoriBande_1_2_3[5];
            blBanda2Selezionata = true;
            AggiornaLabelRisultato();
        }

        //***********************************************************
        // RadioButton 6
        //***********************************************************
        private void RadioB2_6_Click(object sender, EventArgs e)
        {
            RadioButtonSelezionato_2_Banda(6);
            dBanda2Selezione = 6;
            PanelBanda2.BackColor = lstColoriBande_1_2_3[6];
            blBanda2Selezionata = true;
            AggiornaLabelRisultato();
        }

        //***********************************************************
        // RadioButton 7
        //***********************************************************
        private void RadioB2_7_Click(object sender, EventArgs e)
        {
            RadioButtonSelezionato_2_Banda(7);
            dBanda2Selezione = 7;
            PanelBanda2.BackColor = lstColoriBande_1_2_3[7];
            blBanda2Selezionata = true;
            AggiornaLabelRisultato();
        }

        //***********************************************************
        // RadioButton 8
        //***********************************************************
        private void RadioB2_8_Click(object sender, EventArgs e)
        {
            RadioButtonSelezionato_2_Banda(8);
            dBanda2Selezione = 8;
            PanelBanda2.BackColor = lstColoriBande_1_2_3[8];
            blBanda2Selezionata = true;
            AggiornaLabelRisultato();
        }

        //***********************************************************
        // RadioButton 9
        //***********************************************************
        private void RadioB2_9_Click(object sender, EventArgs e)
        {
            RadioButtonSelezionato_2_Banda(9);
            dBanda2Selezione = 9;
            PanelBanda2.BackColor = lstColoriBande_1_2_3[9];
            blBanda2Selezionata = true;
            AggiornaLabelRisultato();
        }

        //*****************************************************************************
        // INIZIO gestione radio button alla selezione e assegnazione valore. COLONNA 3
        //*****************************************************************************

        //***********************************************************
        // RadioButton 0
        //***********************************************************
        private void RadioB3_0_Click(object sender, EventArgs e)
        {
            RadioButtonSelezionato_3_Banda(0);
            dBanda3Selezione = 0;
            PanelBanda3.BackColor = lstColoriBande_1_2_3[0];
            blBanda3Selezionata = true;
            AggiornaLabelRisultato();
        }

        //***********************************************************
        // RadioButton 1
        //***********************************************************
        private void RadioB3_1_Click(object sender, EventArgs e)
        {
            RadioButtonSelezionato_3_Banda(1);
            dBanda3Selezione = 1;
            PanelBanda3.BackColor = lstColoriBande_1_2_3[1];
            blBanda3Selezionata = true;
            AggiornaLabelRisultato();
        }

        //***********************************************************
        // RadioButton 2
        //***********************************************************
        private void RadioB3_2_Click(object sender, EventArgs e)
        {
            RadioButtonSelezionato_3_Banda(2);
            dBanda3Selezione = 2;
            PanelBanda3.BackColor = lstColoriBande_1_2_3[2];
            blBanda3Selezionata = true;
            AggiornaLabelRisultato();
        }

        //***********************************************************
        // RadioButton 3
        //***********************************************************
        private void RadioB3_3_Click(object sender, EventArgs e)
        {
            RadioButtonSelezionato_3_Banda(3);
            dBanda3Selezione = 3;
            PanelBanda3.BackColor = lstColoriBande_1_2_3[3];
            blBanda3Selezionata = true;
            AggiornaLabelRisultato();
        }

        //***********************************************************
        // RadioButton 4
        //***********************************************************
        private void RadioB3_4_Click(object sender, EventArgs e)
        {
            RadioButtonSelezionato_3_Banda(4);
            dBanda3Selezione = 4;
            PanelBanda3.BackColor = lstColoriBande_1_2_3[4];
            blBanda3Selezionata = true;
            AggiornaLabelRisultato();
        }

        //***********************************************************
        // RadioButton 5
        //***********************************************************
        private void RadioB3_5_Click(object sender, EventArgs e)
        {
            RadioButtonSelezionato_3_Banda(5);
            dBanda3Selezione = 5;
            PanelBanda3.BackColor = lstColoriBande_1_2_3[5];
            blBanda3Selezionata = true;
            AggiornaLabelRisultato();
        }

        //***********************************************************
        // RadioButton 6
        //***********************************************************
        private void RadioB3_6_Click(object sender, EventArgs e)
        {
            RadioButtonSelezionato_3_Banda(6);
            dBanda3Selezione = 6;
            PanelBanda3.BackColor = lstColoriBande_1_2_3[6];
            blBanda3Selezionata = true;
            AggiornaLabelRisultato();
        }

        //***********************************************************
        // RadioButton 7
        //***********************************************************
        private void RadioB3_7_Click(object sender, EventArgs e)
        {
            RadioButtonSelezionato_3_Banda(7);
            dBanda3Selezione = 7;
            PanelBanda3.BackColor = lstColoriBande_1_2_3[7];
            blBanda3Selezionata = true;
            AggiornaLabelRisultato();
        }

        //***********************************************************
        // RadioButton 8
        //***********************************************************
        private void RadioB3_8_Click(object sender, EventArgs e)
        {
            RadioButtonSelezionato_3_Banda(8);
            dBanda3Selezione = 8;
            PanelBanda3.BackColor = lstColoriBande_1_2_3[8];
            blBanda3Selezionata = true;
            AggiornaLabelRisultato();
        }

        //***********************************************************
        // RadioButton 9
        //***********************************************************
        private void RadioB3_9_Click(object sender, EventArgs e)
        {
            RadioButtonSelezionato_3_Banda(9);
            dBanda3Selezione = 9;
            PanelBanda3.BackColor = lstColoriBande_1_2_3[9];
            blBanda3Selezionata = true;
            AggiornaLabelRisultato();
        }

        //*********************************************************************************
        // INIZIO gestione radio button alla selezione e assegnazione valore MOLTIPLICATORE
        //*********************************************************************************

        //***********************************************************
        // RadioButton MOLTIPLICATORE 0
        //***********************************************************
        private void RadioB_Molt_0_Click(object sender, EventArgs e)
        {
            RadioButtonSelezionato_Moltiplicatore(0);
            dBandaMolt = 1;
            PanelBandaMolt.BackColor = lstColoriBande_1_2_3[0];
            blBandaMoltSelezionata = true;
            AggiornaLabelRisultato();
        }

        //***********************************************************
        // RadioButton MOLTIPLICATORE 1
        //***********************************************************
        private void RadioB_Molt_1_Click(object sender, EventArgs e)
        {
            RadioButtonSelezionato_Moltiplicatore(1);
            dBandaMolt = 10;
            PanelBandaMolt.BackColor = lstColoriBande_1_2_3[1];
            blBandaMoltSelezionata = true;
            AggiornaLabelRisultato();
        }

        //***********************************************************
        // RadioButton MOLTIPLICATORE 2
        //***********************************************************
        private void RadioB_Molt_2_Click(object sender, EventArgs e)
        {
            RadioButtonSelezionato_Moltiplicatore(2);
            dBandaMolt = 100;
            PanelBandaMolt.BackColor = lstColoriBande_1_2_3[2];
            blBandaMoltSelezionata = true;
            AggiornaLabelRisultato();
        }

        //***********************************************************
        // RadioButton MOLTIPLICATORE 3
        //***********************************************************
        private void RadioB_Molt_3_Click(object sender, EventArgs e)
        {
            RadioButtonSelezionato_Moltiplicatore(3);
            dBandaMolt = 1000;
            PanelBandaMolt.BackColor = lstColoriBande_1_2_3[3];
            blBandaMoltSelezionata = true;
            AggiornaLabelRisultato();
        }

        //***********************************************************
        // RadioButton MOLTIPLICATORE 4
        //***********************************************************
        private void RadioB_Molt_4_Click(object sender, EventArgs e)
        {
            RadioButtonSelezionato_Moltiplicatore(4);
            dBandaMolt = 10000;
            PanelBandaMolt.BackColor = lstColoriBande_1_2_3[4];
            blBandaMoltSelezionata = true;
            AggiornaLabelRisultato();
        }

        //***********************************************************
        // RadioButton MOLTIPLICATORE 5
        //***********************************************************
        private void RadioB_Molt_5_Click(object sender, EventArgs e)
        {
            RadioButtonSelezionato_Moltiplicatore(5);
            dBandaMolt = 100000;
            PanelBandaMolt.BackColor = lstColoriBande_1_2_3[5];
            blBandaMoltSelezionata = true;
            AggiornaLabelRisultato();
        }

        //***********************************************************
        // RadioButton MOLTIPLICATORE 6
        //***********************************************************
        private void RadioB_Molt_6_Click(object sender, EventArgs e)
        {
            RadioButtonSelezionato_Moltiplicatore(6);
            dBandaMolt = 1000000;
            PanelBandaMolt.BackColor = lstColoriBande_1_2_3[6];
            blBandaMoltSelezionata = true;
            AggiornaLabelRisultato();
        }

        //***********************************************************
        // RadioButton MOLTIPLICATORE 7
        //***********************************************************
        private void RadioB_Molt_7_Click(object sender, EventArgs e)
        {
            RadioButtonSelezionato_Moltiplicatore(7);
            dBandaMolt = 100000000;
            PanelBandaMolt.BackColor = lstColoriBande_1_2_3[7];
            blBandaMoltSelezionata = true;
            AggiornaLabelRisultato();
        }

        //***********************************************************
        // RadioButton MOLTIPLICATORE 8
        //***********************************************************
        private void RadioB_Molt_8_Click(object sender, EventArgs e)
        {
            RadioButtonSelezionato_Moltiplicatore(8);
            dBandaMolt = 0.1f;
            PanelBandaMolt.BackColor = lstColoriBandaMolt[8];
            blBandaMoltSelezionata = true;
            AggiornaLabelRisultato();
        }

        //***********************************************************
        // RadioButton MOLTIPLICATORE 9
        //***********************************************************
        private void RadioB_Molt_9_Click(object sender, EventArgs e)
        {
            RadioButtonSelezionato_Moltiplicatore(9);
            dBandaMolt = 0.01f;
            PanelBandaMolt.BackColor = lstColoriBandaMolt[9];
            blBandaMoltSelezionata = true;
            AggiornaLabelRisultato();
        }

        //******************************************************************************
        // INIZIO gestione radio button alla selezione e assegnazione valore. TOLLERANZA
        //******************************************************************************

        //***********************************************************
        // RadioButton 0
        //***********************************************************
        private void RadioB_Toll_0_Click(object sender, EventArgs e)
        {
            RadioButtonSelezionato_Tolleranza(0);
            dBandaTollSelezione = 0;
            PanelBandaToll.BackColor = lstColoriBandaToll[0];
            blBandaTollSelezionata = true;
            AggiornaLabelRisultato();
        }

        //***********************************************************
        // RadioButton 1
        //***********************************************************
        private void RadioB_Toll_1_Click(object sender, EventArgs e)
        {
            RadioButtonSelezionato_Tolleranza(1);
            dBandaTollSelezione = 1;
            PanelBandaToll.BackColor = lstColoriBandaToll[1];
            blBandaTollSelezionata = true;
            AggiornaLabelRisultato();
        }

        //***********************************************************
        // RadioButton 2
        //***********************************************************
        private void RadioB_Toll_2_Click(object sender, EventArgs e)
        {
            RadioButtonSelezionato_Tolleranza(2);
            dBandaTollSelezione = 2;
            PanelBandaToll.BackColor = lstColoriBandaToll[2];
            blBandaTollSelezionata = true;
            AggiornaLabelRisultato();
        }

        //***********************************************************
        // RadioButton 3
        //***********************************************************
        private void RadioB_Toll_3_Click(object sender, EventArgs e)
        {
            RadioButtonSelezionato_Tolleranza(3);
            dBandaTollSelezione = 3;
            PanelBandaToll.BackColor = lstColoriBandaToll[3];
            blBandaTollSelezionata = true;
            AggiornaLabelRisultato();
        }

        //***********************************************************
        // RadioButton 4
        //***********************************************************
        private void RadioB_Toll_4_Click(object sender, EventArgs e)
        {
            RadioButtonSelezionato_Tolleranza(4);
            dBandaTollSelezione = 4;
            PanelBandaToll.BackColor = lstColoriBandaToll[4];
            blBandaTollSelezionata = true;
            AggiornaLabelRisultato();
        }

        //***********************************************************
        // RadioButton 5
        //***********************************************************
        private void RadioB_Toll_5_Click(object sender, EventArgs e)
        {
            RadioButtonSelezionato_Tolleranza(5);
            dBandaTollSelezione = 5;
            PanelBandaToll.BackColor = lstColoriBandaToll[5];
            blBandaTollSelezionata = true;
            AggiornaLabelRisultato();
        }

        //***********************************************************
        // RadioButton 6
        //***********************************************************
        private void RadioB_Toll_6_Click(object sender, EventArgs e)
        {
            RadioButtonSelezionato_Tolleranza(6);
            dBandaTollSelezione = 6;
            PanelBandaToll.BackColor = lstColoriBandaToll[6];
            blBandaTollSelezionata = true;
            AggiornaLabelRisultato();
        }

        //***********************************************************
        // RadioButton 7
        //***********************************************************
        private void RadioB_Toll_7_Click(object sender, EventArgs e)
        {
            RadioButtonSelezionato_Tolleranza(7);
            dBandaTollSelezione = 7;
            PanelBandaToll.BackColor = lstColoriBandaToll[7];
            blBandaTollSelezionata = true;
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

        //************************************* Riempimento liste colori bande ******************************************
        //*************************************************************
        // Riempimento della lista dei colori BANDE 1-2-3
        //*************************************************************
        private void RienpiListeColoriBande_1_2_3()
        {
            // La seguente lista viene caricata normalmente, senza ciclicità,
            // perché i colori sono sempre gli stessi e non cambiano mai.
            // I colori sono associati alle bande 1-2-3.
            // La prima banda inizia con Marrone che ha valore 1.
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
            lstColoriBande_1_2_3.Add(SystemColors.Control);   //10 Serve per banda NON utilizzata
        }

        //*************************************************************
        // Riempimento della lista dei colori banda MOLTIPLICATORE
        //*************************************************************
        private void RienpiListaColoriMoltiplicatore()
        {
            lstColoriBandaMolt.Add(Color.Black);   //1
            lstColoriBandaMolt.Add(Color.Brown);   //10
            lstColoriBandaMolt.Add(Color.Red);     //100
            lstColoriBandaMolt.Add(Color.Orange);  //1000
            lstColoriBandaMolt.Add(Color.Yellow);  //10000
            lstColoriBandaMolt.Add(Color.Green);   //100000
            lstColoriBandaMolt.Add(Color.Blue);    //1000000
            lstColoriBandaMolt.Add(Color.Purple);  //10000000
            lstColoriBandaMolt.Add(Color.Gold);    //0.1
            lstColoriBandaMolt.Add(Color.Silver);  //0.01
            lstColoriBandaMolt.Add(SystemColors.Control);// Colore per non attivo
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
            lstColoriBandaToll.Add(SystemColors.Control);  //Colore per non attivo
        }

        //***********************************************************
        // Pulsante ESCI
        //***********************************************************
        private void Btn_Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //***********************************************************
        // Pulsante ABOUT
        //***********************************************************
        private void About_Click(object sender, EventArgs e)
        {
            FormAbout frmAbout = new();
            frmAbout.ShowDialog();
        }

        //***********************************************************
        // Pulsante RESET
        //***********************************************************
        private void Btn_Reset_Click(object sender, EventArgs e)
        {
            Reset();
        }

        //***********************************************************
        // RESET di tutti i valori e le selezioni dei RadioButton
        //***********************************************************
        public void Reset()
        {
            dBanda1Selezione = 1;
            dBanda2Selezione = 0;
            dBanda3Selezione = 0;
            dBandaMolt = 0;
            dBandaTollSelezione = 0;
            blBanda1Selezionata = false;
            blBanda2Selezionata = false;
            blBanda3Selezionata = false;
            blBandaMoltSelezionata = false;
            blBandaTollSelezionata = false;
            // Inizializza i colori delle bande tutte allo Zero
            PanelBanda1.BackColor = lstColoriBande_1_2_3[10];
            PanelBanda2.BackColor = lstColoriBande_1_2_3[10];
            PanelBanda3.BackColor = lstColoriBande_1_2_3[10];
            PanelBandaMolt.BackColor = lstColoriBandaMolt[10];
            PanelBandaToll.BackColor = lstColoriBandaToll[8];
            Lbl_Risultato.ForeColor = Color.Black;
            Lbl_Risultato.Text = strIntestazionelabel; //Inizializza la label del risultato

            for (int i = 0; i < lstRb1Banda.Count; i++)
            {
                lstRb1Banda[i].Checked = false;
            }
            for (int i = 0; i < lstRb2Banda.Count; i++)
            {
                lstRb2Banda[i].Checked = false;
            }
            for (int i = 0; i < lstRb3Banda.Count; i++)
            {
                lstRb3Banda[i].Checked = false;
            }
            for (int i = 0; i < lstRbBandaMolt.Count; i++)
            {
                lstRbBandaMolt[i].Checked = false;
            }
            for (int i = 0; i < lstRbTolleranze.Count; i++)
            {
                lstRbTolleranze[i].Checked = false;
            }
            lstRbTolleranze[0].Checked = true;
        }

        //**************************************************************************************************************
    }
}
