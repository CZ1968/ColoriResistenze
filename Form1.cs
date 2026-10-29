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
        private List<String> lstTolleranze = new List<String>();
        private List<String> lstValoriImpostati = new List<String>();
        private List<RadioButton> Rb1Banda;
        private List<RadioButton> Rb2Banda;
        private List<RadioButton> Rb3Banda;
        private List<RadioButton> RbTolleranze;

        public Form1()
        {
            InitializeComponent();
            RiempiListe();                     // Riempie le liste con i valori
            Lbl_Valore.Text = "_____________"; //Inizializza la label del risultato
            Rb1Banda = new List<RadioButton> //RadioButton della prima banda
            { RadioB1_0, RadioB1_1, RadioB1_2, RadioB1_3, RadioB1_4,
              RadioB1_5, RadioB1_6, RadioB1_7, RadioB1_8, RadioB1_9
            };
            Rb2Banda = new List<RadioButton> // RadioButton della seconda banda
            { RadioB2_0, RadioB2_1, RadioB2_2, RadioB2_3, RadioB2_4,
              RadioB2_5, RadioB2_6, RadioB2_7, RadioB2_8, RadioB2_9
            };
            Rb3Banda = new List<RadioButton> // RadioButton della terza banda
            { RadioB3_0, RadioB3_1, RadioB3_2, RadioB3_3, RadioB3_4,
              RadioB3_5, RadioB3_6
            };
            RbTolleranze = new List<RadioButton> // RadioButton della quarta banda
            { RadioB_Toll_0, RadioB_Toll_1, RadioB_Toll_2, RadioB_Toll_3, RadioB_Toll_4,
              RadioB_Toll_5, RadioB_Toll_6, RadioB_Toll_7
            };
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
            //Terza banda MOLTIPLICATORE
            lstColonna3.Add(" ");
            lstColonna3.Add("0");
            lstColonna3.Add("00");
            lstColonna3.Add("000");      //K
            lstColonna3.Add("0000");     //K
            lstColonna3.Add("00000");    //K
            lstColonna3.Add("000000");   //M
            lstColonna3.Add("0000000");  //M
            lstColonna3.Add("00000000"); //M

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

        private void Btn_Calcola_Click(object sender, EventArgs e)
        {
            Lbl_Valore.Text = Componi_Valore_ComboBox() ?? "_____________";
        }

        private string? Componi_Valore_ComboBox()
        {
            int? selectedValue1;
            int? selectedValue2;
            int? selectedValue3;
            int? selectedValue4;

            if (ComboBox_1_Banda.SelectedItem != null)
            {
                selectedValue1 = ComboBox_1_Banda.SelectedIndex;
            }
            else
            {
                MessageBox.Show("Seleziona un valore per la prima banda!");
                return null;
            }
            if (ComboBox_2_Banda.SelectedItem != null)
            {
                selectedValue2 = ComboBox_2_Banda.SelectedIndex;
            }
            else
            {
                MessageBox.Show("Seleziona un valore per la seconda banda!");
                return null;
            }
            if (ComboBox_3_Banda.SelectedItem != null)
            {
                selectedValue3 = ComboBox_3_Banda.SelectedIndex;
            }
            else
            {
                MessageBox.Show("Seleziona un valore per la terza banda!");
                return null;
            }
            if (ComboBox_Tolleranza.SelectedItem != null)
            {
                selectedValue4 = ComboBox_Tolleranza.SelectedIndex;
            }
            else
            {
                MessageBox.Show("Seleziona un valore per la Tolleranza!");
                return null;
            }
            if (selectedValue1 == 0)
            {
                return $"{lstColonna2[selectedValue2.Value]}{lstColonna3[selectedValue3.Value]}{ohmSign} - Toll. {lstTolleranze[selectedValue4.Value]}";
            }
            else
            {
                return $"{lstColonna1[selectedValue1.Value]}{lstColonna2[selectedValue2.Value]}{lstColonna3[selectedValue3.Value]}{ohmSign} - Toll. {lstTolleranze[selectedValue4.Value]}";
            }
        }

        private void CheckedChange(object sender, EventArgs e)
        {
        }
    }
}
