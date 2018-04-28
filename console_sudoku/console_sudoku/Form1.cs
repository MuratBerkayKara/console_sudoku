using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace console_sudoku
{
    public partial class Form1 : Form
    {
        Thread th2;
        Thread th1;
        Thread th3;
        public int[,] c = new int[9, 9];
        int[,] okunan_dosya = new int[9, 9];

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            okunan_dosya = dosya_okuma();
            sudoku_cozum coz = new sudoku_cozum();
            sudoku_cozum cozer = new sudoku_cozum();
            sudoku_cozum cozdu = new sudoku_cozum();
            coz.al(okunan_dosya);
            cozer.al(okunan_dosya);
            cozdu.al(okunan_dosya);
            th1 = new Thread(new ThreadStart(coz.Solve));
            th2 = new Thread(new ThreadStart(cozer.Solve));
            th3 = new Thread(new ThreadStart(cozdu.Solve));
            th1.Start();
            th2.Start();
            th3.Start();
            //coz.Solve();
            for(;;)
            {
                if (th1.IsAlive == false)
                {
                    //th2.Abort();
                    //th3.Abort();
                    break;
                }
                else if (th2.IsAlive == false)
                {
                    //th1.Abort();
                    //th3.Abort();
                    break;
                }
                else if (th3.IsAlive == false)
                {
                    //th1.Abort();
                    //th2.Abort();
                    break;
                }
                else
                    continue;                          
            }

           for (int i=0;i<9;i++)
            {
                for(int j=0;j<9;j++)
                {
                     tableLayoutPanel1.Controls.Add(new Label() { Text = coz.table[i, j].ToString(), Anchor = AnchorStyles.None, AutoSize = true }, j, i);
                }
            }

            Form2 f2 = new Form2();
            f2.Show();
            for (int i=0;i<9;i++)
            {
                for(int j=0;j<9;j++)
                {
                     f2.tableLayoutPanel2.Controls.Add(new Label() { Text = cozer.table[i, j].ToString(), Anchor = AnchorStyles.None, AutoSize = true }, j, i);
                }
            }

            Form3 f3 = new Form3();
            f3.Show();
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    f3.tableLayoutPanel3.Controls.Add(new Label() { Text = cozdu.table[i, j].ToString(), Anchor = AnchorStyles.None, AutoSize = true }, j, i);
                }
            }


            /*for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {

                    Console.Write(cozdu.table[i, j].ToString() + " ");
                    if (j % 3 == 2)
                    {
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
                if (i % 3 == 2)
                {
                    Console.WriteLine();
                }
            }*/
        }

        int[,] dosya_okuma()
        {
            string line;

            string[] a = new String[9];
            char[,] b = new Char[9, 9];// değerleri tutan matris
            // Read the file and display it line by line.  
            System.IO.StreamReader file =
            new System.IO.StreamReader(@"D:\sudoku.txt");
            
            int k = 0;
            while ((line = file.ReadLine()) != null)
            {
                a = new String[] { line };
                for (int j = 0; j < 9; j++)
                {
                    b[k, j] = a[0][j];

                }
                k++;
            }
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if ((int)char.GetNumericValue(b[i, j]) != -1)
                    {
                        c[i, j] = (int)char.GetNumericValue(b[i, j]);
                    }
                }
            }
            return c;

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

    
    }
 }

