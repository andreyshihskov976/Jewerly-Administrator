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
    public partial class Acts : Form
    {
        MySqlOperations MySqlOperations = null;
        MySqlQueries MySqlQueries = null;
        string ID_Acta = string.Empty;
        int Index = 0;
        public Acts(MySqlOperations mySqlOperations, MySqlQueries mySqlQueries)
        {
            InitializeComponent();
            MySqlOperations = mySqlOperations;
            MySqlQueries = mySqlQueries;
        }

        private void Acts_Load(object sender, EventArgs e) 
        { 
            Load_Table1(); 
        }

        private void Load_Table1()
        {
            MySqlOperations.Select_DataGridView(MySqlQueries.Select_Acts, dataGridView1);
            MySqlOperations.Select_ComboBox(MySqlQueries.Select_Clienty_ComboBox,comboBox2);
            MySqlOperations.Select_ComboBox(MySqlQueries.Select_Sotrudniki_ComboBox, comboBox3);
            MySqlOperations.Select_ComboBox(MySqlQueries.Select_Izdeliya_ComboBox, comboBox4);
            dateTimePicker3.Value = DateTime.Now;
            dateTimePicker3.MinDate = dateTimePicker3.Value;
            dateTimePicker1.Value = dateTimePicker3.Value;
            dateTimePicker1.MinDate = dateTimePicker3.MinDate;
            dataGridView1.Columns[0].Visible = false;
        }

        private void Load_Table2(string ID = null)
        {
            MySqlOperations.Select_DataGridView(MySqlQueries.Select_Sostav_Acta, dataGridView2, ID);
            MySqlOperations.Select_ComboBox(MySqlQueries.Select_Materialy_ComboBox, comboBox1);
            dataGridView2.Columns[0].Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dateTimePicker1.Value < dateTimePicker2.Value)
            {
                string date = dateTimePicker3.Value.Year.ToString() + '-' + dateTimePicker3.Value.Month.ToString() + '-' + dateTimePicker3.Value.Day.ToString();
                string date1 = dateTimePicker1.Value.Year.ToString() + '-' + dateTimePicker1.Value.Month.ToString() + '-' + dateTimePicker1.Value.Day.ToString();
                string date2 = dateTimePicker2.Value.Year.ToString() + '-' + dateTimePicker2.Value.Month.ToString() + '-' + dateTimePicker2.Value.Day.ToString();
                MySqlOperations.Insert_Update_Delete(MySqlQueries.Insert_Acts, null, date,
                    MySqlOperations.Select_Text(MySqlQueries.Select_Clienty_ID,null,comboBox2.Text),
                    MySqlOperations.Select_Text(MySqlQueries.Select_Sotrudniki_ID, null, comboBox3.Text),
                    MySqlOperations.Select_Text(MySqlQueries.Select_Izdeliya_ID, null, comboBox4.Text),
                    date1, date2);
                Load_Table1();
                Clear1();
                dataGridView1.ClearSelection();
                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Selected = true;
            }
            else
                MessageBox.Show("Проверьте, все ли поля заполнены.", "Прежупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Index = dataGridView1.SelectedRows[0].Index;
            MySqlOperations.Search_In_ComboBox(dataGridView1.SelectedRows[0].Cells[2].Value.ToString(), comboBox2);
            MySqlOperations.Search_In_ComboBox(dataGridView1.SelectedRows[0].Cells[3].Value.ToString(), comboBox3);
            MySqlOperations.Search_In_ComboBox(dataGridView1.SelectedRows[0].Cells[4].Value.ToString(), comboBox4);
            dateTimePicker1.MinDate = DateTime.Parse(dataGridView1.SelectedRows[0].Cells[5].Value.ToString());
            dateTimePicker1.Value = dateTimePicker1.MinDate;
            dateTimePicker2.MinDate = DateTime.Parse(dataGridView1.SelectedRows[0].Cells[5].Value.ToString());
            button1.Enabled = false;
            button2.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dateTimePicker1.Value < dateTimePicker2.Value)
            {
                string date = dateTimePicker3.Value.Year.ToString() + '-' + dateTimePicker3.Value.Month.ToString() + '-' + dateTimePicker3.Value.Day.ToString();
                string date1 = dateTimePicker1.Value.Year.ToString() + '-' + dateTimePicker1.Value.Month.ToString() + '-' + dateTimePicker1.Value.Day.ToString();
                string date2 = dateTimePicker2.Value.Year.ToString() + '-' + dateTimePicker2.Value.Month.ToString() + '-' + dateTimePicker2.Value.Day.ToString();
                MySqlOperations.Insert_Update_Delete(MySqlQueries.Update_Acts, dataGridView1.SelectedRows[0].Cells[0].Value.ToString(), date,
                    MySqlOperations.Select_Text(MySqlQueries.Select_Clienty_ID, null, comboBox2.Text),
                    MySqlOperations.Select_Text(MySqlQueries.Select_Sotrudniki_ID, null, comboBox3.Text),
                    MySqlOperations.Select_Text(MySqlQueries.Select_Izdeliya_ID, null, comboBox4.Text),
                    date1, date2);
                Load_Table1();
                Clear1();
                dataGridView1.ClearSelection();
                dataGridView1.Rows[Index].Selected = true;
            }
            else
                MessageBox.Show("Проверьте, все ли поля заполнены.", "Прежупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void button3_Click(object sender, EventArgs e) => Clear1();

        private void Clear1()
        {
            comboBox2.SelectedItem = comboBox2.Items[0];
            comboBox3.SelectedItem = comboBox3.Items[0];
            comboBox4.SelectedItem = comboBox4.Items[0];
            dateTimePicker1.MinDate = DateTime.Now;
            dateTimePicker1.Value = dateTimePicker1.MinDate;
            button2.Enabled = false;
            button1.Enabled = true;
        }

        private void dataGridView1_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите удалить запись?", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                MySqlOperations.Insert_Update_Delete(MySqlQueries.Delete_Acts, dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
        }

        private void button4_Click(object sender, EventArgs e) => MySqlOperations.Search(textBox2, dataGridView1);

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
                dataGridView1.ClearSelection();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker2.MinDate = dateTimePicker1.Value;
            dateTimePicker2.Value = dateTimePicker1.Value.AddDays(1);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            MySqlOperations.Insert_Update_Delete(MySqlQueries.Insert_Sostav_Acta, null,ID_Acta,
                MySqlOperations.Select_Text(MySqlQueries.Select_Materialy_ID, null, comboBox1.Text), numericUpDown1.Value.ToString().Replace(',','.'));
            Load_Table2(ID_Acta);
            Clear2();

        }

        private void button7_Click(object sender, EventArgs e)
        {
            MySqlOperations.Insert_Update_Delete(MySqlQueries.Update_Sostav_Acta, 
                dataGridView2.SelectedRows[0].Cells[0].Value.ToString(), ID_Acta,
                MySqlOperations.Select_Text(MySqlQueries.Select_Materialy_ID, null, comboBox1.Text), numericUpDown1.Value.ToString().Replace(',', '.'));
            Load_Table2(ID_Acta);
            Clear2();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Clear2();
        }

        private void Clear2()
        {
            numericUpDown1.Value = numericUpDown1.Minimum;
            comboBox1.SelectedItem = comboBox1.Items[0];
            button7.Enabled = false;
            button8.Enabled = true;
        }

        private void button5_Click(object sender, EventArgs e) => MySqlOperations.Search(textBox1, dataGridView2);

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
                dataGridView2.ClearSelection();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            label10.Text = MySqlOperations.Select_Text(MySqlQueries.Select_EdIzm_Materialy, null, comboBox1.Text);
        }

        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            MySqlOperations.Search_In_ComboBox(dataGridView2.SelectedRows[0].Cells[1].Value.ToString(), comboBox1);
            numericUpDown1.Value = decimal.Parse(dataGridView2.SelectedRows[0].Cells[2].Value.ToString());
            button8.Enabled = false;
            button7.Enabled = true;
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count >= 1 && dataGridView1.SelectedRows.Count == 1)
            {
                ID_Acta = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                Load_Table2(ID_Acta);
            }
        }

        private void dateTimePicker3_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                dateTimePicker1.Value = dateTimePicker3.Value;
            }
            catch {}
        }

        private void dataGridView2_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите удалить запись?", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                MySqlOperations.Insert_Update_Delete(MySqlQueries.Delete_Sostav_Acta, dataGridView2.SelectedRows[0].Cells[0].Value.ToString());
        }
    }
}
