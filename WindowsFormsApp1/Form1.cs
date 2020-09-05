using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        //numero di righe della matrice
        private int m_altezza;
        //numero di colonne della matrice
        private int m_base;

        //matrice dei pulsanti(solo per gui e input)
        private Button[][] b_matrix;
        //matrice di bool per contenere i dati e facilitare la generazione della sequenza binaria
        private bool[][] m_bit;

        //gestisce l'input sui pulsanti della matrice di pulsanti
        void cambia(object sender, EventArgs e)
        {
            Button premuto = (Button)sender;
            //estraggo dal nome del bottone premuto gli indici di posizione
            String[] posiz = premuto.Name.Split('.');

            int i = int.Parse(posiz[0]);
            int j = int.Parse(posiz[1]);

            m_bit[i][j] = !m_bit[i][j];

            //cambio lo stato in base allo stato precedente
            if (m_bit[i][j]) //bianco(0,false) ----> nero(1,true)
                premuto.BackColor = Color.Black;
            else             //nero(1,true)    ----> bianco(0,false)
                premuto.BackColor = Color.White;
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button2.Enabled = false;
        }

        // quando premi su button 1 genera se possibile la matrice di bottoni in base alle informazioni formite
        private void Button1_Click_1(object sender, EventArgs e)
        {
            m_altezza = Int32.Parse(textBox2.Text);
            m_base = Int32.Parse(textBox1.Text);
           
                //genera un array con lunghezza pari alle righe altezza
                b_matrix = new Button[m_altezza][];
                m_bit = new bool[m_altezza][];

            //genera un array per ogni riga con lunghezza pari alla base
            for (int i = 0; i < m_altezza; i++)
            {
                 b_matrix[i] = new Button[m_base];
                 m_bit[i] = new bool[m_base];
            }

                int cont2 = 1; // contatore per ottenere la posizione della riga(altezza_pulsante * numero di riga) da 1 a n
                for (int i = 0; i < m_altezza; i++)
                {
                int cont1 = 1; // contatore per ottenere la posizione della colonna(base_pulsante * numero di colonna) da 1 a n
                for (int j = 0; j < m_base; j++)
                    {
                        b_matrix[i][j] = new Button();
                        b_matrix[i][j].Size = new Size(20, 20);
                        b_matrix[i][j].Location = new Point(20 * cont1, 20 * cont2);
                        b_matrix[i][j].Name = "" + i + "." + j;//nome dei bottoni con posizione righe.colonne per facilitare il cambio del valore(ottengo un accesso diretto)
                        b_matrix[i][j].Click += cambia;
                        b_matrix[i][j].BackColor = Color.White;
                        b_matrix[i][j].FlatStyle = FlatStyle.Flat;
                        b_matrix[i][j].FlatAppearance.BorderColor = Color.Gray;
                        panel1.Controls.Add(b_matrix[i][j]);//rendo visibile il bottone nella finestra
                        m_bit[i][j] = false;
                        cont1++;
                    }
                    cont2++;
                }
                button1.Enabled = false;
                button2.Enabled = true;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            String s = "";
            String o = "";
            for(int i = 0; i < m_altezza; i++)
            {
                //trasforma gli array di bool in una sequenza di 0 e 1 (String)
                s = "";
                foreach(bool t in m_bit[i])
                {
                    if (t)
                    {
                        s += "1";
                    }
                    else
                    {
                        s += "0";
                    }
                }
                //completamento del byte
                String op = "";
                while((s.Length + op.Length)%8 != 0)
                {
                    op += "0";
                }
                //trasforma i byte in hex
                s = op + s;
                op = "";
                for (int x = 0; s.Length - x > 0; x += 8)
                {
                    op += "x" + Convert.ToString((Convert.ToInt32(s.Substring(x,8), 2)), 16) ;
                }
                //inverti i byte
                String[] temp = op.Split('x');
                for(int x = temp.Length ;x>1; x--)
                {
                    o += "0x" + temp[x-1] +",";
                }
            }
            //mostra l'output
            MessageBox.Show(o + "\n\n" + "generato e copiato negli appunti");
            System.Windows.Forms.Clipboard.SetDataObject(o, true); //copia il risultato negli appunti
        }
    }
}


