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
    public partial class Cheki : Form
    {
        MySqlOperations MySqlOperations = null;
        MySqlQueries MySqlQueries = null;
        string ID_Acta = string.Empty;
        int Index = 0;
        public Cheki(MySqlOperations mySqlOperations, MySqlQueries mySqlQueries)
        {
            InitializeComponent();
            MySqlOperations = mySqlOperations;
            MySqlQueries = mySqlQueries;
        }

        private void Cheki_Load(object sender, EventArgs e)
        {
            Load_Table1();
        }

        private void Load_Table1()
        {
            MySqlOperations.Select_DataGridView(MySqlQueries.Select_Cheki, dataGridView1);
            MySqlOperations.Select_ComboBox(MySqlQueries.Select_Acts_ComboBox, comboBox1);
            MySqlOperations.Select_ComboBox(MySqlQueries.Select_Skidki_ComboBox, comboBox8);
            dateTimePicker3.Value = DateTime.Now;
            dateTimePicker3.MinDate = dateTimePicker3.Value;
            dataGridView1.Columns[0].Visible = false;
            comboBox2.SelectedItem = comboBox2.Items[0];
            comboBox5.SelectedItem = comboBox5.Items[0];
            comboBox6.SelectedItem = comboBox6.Items[0];
            comboBox8.SelectedItem = comboBox8.Items[0];
        }

        private void Load_Table2(string ID = null) => MySqlOperations.Select_DataGridView(MySqlQueries.Select_Sostav_Cheka, dataGridView2, ID);

        private void button1_Click(object sender, EventArgs e)
        {
                string date = dateTimePicker3.Value.Year.ToString() + '-' + dateTimePicker3.Value.Month.ToString() + '-' + dateTimePicker3.Value.Day.ToString();
                MySqlOperations.Insert_Update_Delete(MySqlQueries.Insert_Cheki, null,
                    MySqlOperations.Select_Text(MySqlQueries.Select_Acts_ID,null,comboBox1.Text), date,
                    comboBox5.Text.Split(' ')[1], comboBox6.Text.Split(' ')[2], textBox4.Text.Split(' ')[0],
                    MySqlOperations.Select_Text(MySqlQueries.Select_Sum_Cheka, 
                        MySqlOperations.Select_Text(MySqlQueries.Select_Acts_ID, null, comboBox1.Text),
                        comboBox5.Text.Split(' ')[1], comboBox6.Text.Split(' ')[2], textBox4.Text.Split(' ')[0]).Replace(',','.'),
                    MySqlOperations.Select_Text(MySqlQueries.Select_Skidki_ID, null, comboBox8.Text),
                    MySqlOperations.Select_Text(MySqlQueries.Select_Full_Sum_Cheka,
                        MySqlOperations.Select_Text(MySqlQueries.Select_Acts_ID, null, comboBox1.Text),
                        comboBox5.Text.Split(' ')[1], comboBox6.Text.Split(' ')[2], textBox4.Text.Split(' ')[0], 
                        MySqlOperations.Select_Text(MySqlQueries.Select_Skidki_ID, null, comboBox8.Text)).Replace(',', '.'),
                    comboBox2.Text);
                Load_Table1();
                Clear1();
                dataGridView1.ClearSelection();
                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Selected = true;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (MessageBox.Show("Хотите распечатать чек или отредактировать запись." + '\n' + "Да - распечатать. Нет - перейти к редактированию.", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                Index = dataGridView1.SelectedRows[0].Index;
                dateTimePicker3.MinDate = DateTime.Parse(MySqlOperations.Select_Text(MySqlQueries.Select_Date_Cheka, dataGridView1.SelectedRows[0].Cells[0].Value.ToString()));
                dateTimePicker3.Value = dateTimePicker3.MinDate;
                MySqlOperations.Search_In_ComboBox(dataGridView1.SelectedRows[0].Cells[2].Value.ToString(), comboBox1);
                MySqlOperations.Search_In_ComboBox(dataGridView1.SelectedRows[0].Cells[3].Value.ToString().Replace(",00", ""), comboBox5);
                MySqlOperations.Search_In_ComboBox(dataGridView1.SelectedRows[0].Cells[4].Value.ToString().Replace(",00", ""), comboBox6);
                MySqlOperations.Search_In_ComboBox(dataGridView1.SelectedRows[0].Cells[7].Value.ToString(), comboBox8);
                button1.Enabled = false;
                button2.Enabled = true;
            }
            else
            {
                MySqlOperations.Print_Cheki(saveFileDialog1, dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string date = dateTimePicker3.Value.Year.ToString() + '-' + dateTimePicker3.Value.Month.ToString() + '-' + dateTimePicker3.Value.Day.ToString();
            MySqlOperations.Insert_Update_Delete(MySqlQueries.Update_Cheki, dataGridView1.SelectedRows[0].Cells[0].Value.ToString(),
                MySqlOperations.Select_Text(MySqlQueries.Select_Acts_ID, null, comboBox1.Text), date,
                comboBox5.Text.Split(' ')[1], comboBox6.Text.Split(' ')[2], textBox4.Text.Split(' ')[0],
                MySqlOperations.Select_Text(MySqlQueries.Select_Sum_Cheka,
                    MySqlOperations.Select_Text(MySqlQueries.Select_Acts_ID, null, comboBox1.Text),
                    comboBox5.Text.Split(' ')[1], comboBox6.Text.Split(' ')[2], textBox4.Text.Split(' ')[0]).Replace(',', '.'),
                MySqlOperations.Select_Text(MySqlQueries.Select_Skidki_ID, null, comboBox8.Text),
                MySqlOperations.Select_Text(MySqlQueries.Select_Full_Sum_Cheka,
                    MySqlOperations.Select_Text(MySqlQueries.Select_Acts_ID, null, comboBox1.Text),
                    comboBox5.Text.Split(' ')[1], comboBox6.Text.Split(' ')[2], textBox4.Text.Split(' ')[0],
                    MySqlOperations.Select_Text(MySqlQueries.Select_Skidki_ID, null, comboBox8.Text)).Replace(',', '.'),
                comboBox2.Text);
            Load_Table1();
            Clear1();
            dataGridView1.ClearSelection();
            dataGridView1.Rows[Index].Selected = true;
        }

        private void button3_Click(object sender, EventArgs e) => Clear1();

        private void Clear1()
        {
            comboBox1.SelectedItem = comboBox1.Items[0];
            comboBox2.SelectedItem = comboBox2.Items[0];
            comboBox5.SelectedItem = comboBox5.Items[0];
            comboBox6.SelectedItem = comboBox6.Items[0];
            comboBox8.SelectedItem = comboBox8.Items[0];
            dateTimePicker3.MinDate = DateTime.Now;
            dateTimePicker3.Value = dateTimePicker3.MinDate;
            button2.Enabled = false;
            button1.Enabled = true;
        }

        private void dataGridView1_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите удалить запись?", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                MySqlOperations.Insert_Update_Delete(MySqlQueries.Delete_Cheki, dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                MySqlOperations.Select_ComboBox(MySqlQueries.Select_Acts_ComboBox, comboBox1);
            }

        }

        private void button4_Click(object sender, EventArgs e) 
        { 
            MySqlOperations.Search(textBox2, dataGridView1);
            MySqlOperations.Search(textBox2, dataGridView2);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                dataGridView1.ClearSelection();
                dataGridView2.ClearSelection();
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count >= 1 && dataGridView1.SelectedRows.Count == 1)
            {
                ID_Acta = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                Load_Table2(ID_Acta);
            }
        }
    }
}
