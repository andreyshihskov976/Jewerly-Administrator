using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Jewerly_Administrator
{
    public partial class MainMenu : Form
    {
        MySqlOperations MySqlOperations = null;
        MySqlQueries MySqlQueries = null;
        public MainMenu()
        {
            InitializeComponent();
            MySqlQueries = new MySqlQueries();
            MySqlOperations = new MySqlOperations(MySqlQueries);
        }

        private void MainMenu_Load(object sender, EventArgs e) => MySqlOperations.OpenConnection();

        private void MainMenu_FormClosed(object sender, FormClosedEventArgs e) => MySqlOperations.CloseConnection();

        private void button3_Click(object sender, EventArgs e)
        {
            Izdeliya izdeliya = new Izdeliya(MySqlOperations, MySqlQueries);
            izdeliya.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Skidki skidki = new Skidki(MySqlOperations, MySqlQueries);
            skidki.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Materialy materialy = new Materialy(MySqlOperations, MySqlQueries);
            materialy.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Sotrudniki sotrudniki = new Sotrudniki(MySqlOperations, MySqlQueries);
            sotrudniki.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Clienty clienty = new Clienty(MySqlOperations, MySqlQueries);
            clienty.Show();
        }
    }
}
