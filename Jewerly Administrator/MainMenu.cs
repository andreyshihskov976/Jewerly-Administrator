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

        private void MainMenu_Load(object sender, EventArgs e)
        {
            try
            {
                MySqlOperations.OpenConnection();
                MySqlOperations.Select_DataGridView(MySqlQueries.Select_Zaversh_Acts, dataGridView1);
            }
            catch (Exception)
            {
                MessageBox.Show("Не обнаружена база данных или сервер не активен."+'\n'+"Обратитесь к системному администратору.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        private void MainMenu_FormClosed(object sender, FormClosedEventArgs e) => MySqlOperations.CloseConnection();

        private async void button3_Click(object sender, EventArgs e)
        {
            Izdeliya izdeliya = new Izdeliya(MySqlOperations, MySqlQueries);
            izdeliya.Show();
            this.Visible = false;
            await MySqlOperations.GetTaskFromEvent(izdeliya, "FormClosed");
            this.Visible = true;
            MySqlOperations.Select_DataGridView(MySqlQueries.Select_Zaversh_Acts, dataGridView1);
        }

        private async void button5_Click(object sender, EventArgs e)
        {
            Skidki skidki = new Skidki(MySqlOperations, MySqlQueries);
            skidki.Show();
            this.Visible = false;
            await MySqlOperations.GetTaskFromEvent(skidki, "FormClosed");
            this.Visible = true;
            MySqlOperations.Select_DataGridView(MySqlQueries.Select_Zaversh_Acts, dataGridView1);
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            Materialy materialy = new Materialy(MySqlOperations, MySqlQueries);
            materialy.Show();
            this.Visible = false;
            await MySqlOperations.GetTaskFromEvent(materialy, "FormClosed");
            this.Visible = true;
            MySqlOperations.Select_DataGridView(MySqlQueries.Select_Zaversh_Acts, dataGridView1);
        }

        private async void button7_Click(object sender, EventArgs e)
        {
            Sotrudniki sotrudniki = new Sotrudniki(MySqlOperations, MySqlQueries);
            sotrudniki.Show();
            this.Visible = false;
            await MySqlOperations.GetTaskFromEvent(sotrudniki, "FormClosed");
            this.Visible = true;
            MySqlOperations.Select_DataGridView(MySqlQueries.Select_Zaversh_Acts, dataGridView1);
        }

        private async void button6_Click(object sender, EventArgs e)
        {
            Clienty clienty = new Clienty(MySqlOperations, MySqlQueries);
            clienty.Show();
            this.Visible = false;
            await MySqlOperations.GetTaskFromEvent(clienty, "FormClosed");
            this.Visible = true;
            MySqlOperations.Select_DataGridView(MySqlQueries.Select_Zaversh_Acts, dataGridView1);
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            Acts acts = new Acts(MySqlOperations, MySqlQueries);
            acts.Show();
            this.Visible = false;
            await MySqlOperations.GetTaskFromEvent(acts, "FormClosed");
            this.Visible = true;
            MySqlOperations.Select_DataGridView(MySqlQueries.Select_Zaversh_Acts, dataGridView1);
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            Cheki cheki = new Cheki(MySqlOperations, MySqlQueries);
            cheki.Show();
            this.Visible = false;
            await MySqlOperations.GetTaskFromEvent(cheki, "FormClosed");
            this.Visible = true;
            MySqlOperations.Select_DataGridView(MySqlQueries.Select_Zaversh_Acts, dataGridView1);
        }

        private void MainMenu_Shown(object sender, EventArgs e)
        {
            Proverka();
            MySqlOperations.Select_DataGridView(MySqlQueries.Select_Zaversh_Acts, dataGridView1);
        }

        private void Proverka()
        {
            if (MySqlOperations.Select_Text(MySqlQueries.Select_Exists_Acts) == "1")
                MessageBox.Show("Некоторые изделия были изготовлены, необходимо сообщить клиенту и оформить чек.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("На данный момент нет готовых изделий .", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == comboBox1.Items[0])
            {
                DataTable dataTable = MySqlOperations.Select_DataTable(MySqlQueries.Select_T5_Izdeliya);
                Statistics statistics = new Statistics();
                statistics.Text = comboBox1.Text;
                statistics.chart1.Series[0].Name = "Выбрали, раз";
                statistics.chart1.Series[0].Points.Clear();
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    statistics.chart1.Series[0].Points.AddXY(dataTable.Rows[i][0].ToString(), int.Parse(dataTable.Rows[i][1].ToString()));
                }
                statistics.Show();
            }
            else if (comboBox1.SelectedItem == comboBox1.Items[1])
            {
                DataTable dataTable = MySqlOperations.Select_DataTable(MySqlQueries.Select_T5_Materialy);
                Statistics statistics = new Statistics();
                statistics.Text = comboBox1.Text;
                statistics.chart1.Series[0].Name = "Выбрали, раз";
                statistics.chart1.Series[0].Points.Clear();
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    statistics.chart1.Series[0].Points.AddXY(dataTable.Rows[i][0].ToString(), int.Parse(dataTable.Rows[i][1].ToString()));
                }
                statistics.Show();
            }
            else if (comboBox1.SelectedItem == comboBox1.Items[2])
            {
                DataTable dataTable = MySqlOperations.Select_DataTable(MySqlQueries.Select_T5_Cheki);
                Statistics statistics = new Statistics();
                statistics.Text = comboBox1.Text;
                statistics.chart1.Series[0].Name = "Сумма чека";
                statistics.chart1.Series[0].Points.Clear();
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    statistics.chart1.Series[0].Points.AddXY(dataTable.Rows[i][0].ToString(), int.Parse(dataTable.Rows[i][1].ToString()));
                }
                statistics.Show();
            }
            else if (comboBox1.SelectedItem == comboBox1.Items[3])
            {
                MessageBox.Show("Выручка за текущий месяц составляет: " + MySqlOperations.Select_Text(MySqlQueries.Select_Month_Viruchka) + " рублей.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
    }
}
