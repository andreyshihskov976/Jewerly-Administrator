﻿using System;
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
    public partial class Materialy : Form
    {
        MySqlOperations MySqlOperations = null;
        MySqlQueries MySqlQueries = null;
        public Materialy(MySqlOperations mySqlOperations, MySqlQueries mySqlQueries)
        {
            InitializeComponent();
            MySqlOperations = mySqlOperations;
            MySqlQueries = mySqlQueries;
        }

        private void Materialy_Load(object sender, EventArgs e) => Load_Table();

        private void Load_Table()
        {
            MySqlOperations.Select_DataGridView(MySqlQueries.Select_Materialy, dataGridView1);
            dataGridView1.Columns[0].Visible = false;
            comboBox1.SelectedItem = comboBox1.Items[0];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                MySqlOperations.Insert_Update_Delete(MySqlQueries.Insert_Materialy, null, textBox1.Text, comboBox1.Text, numericUpDown1.Value.ToString().Replace(',', '.'));
                Load_Table();
                Clear();
            }
            else
                MessageBox.Show("Проверьте, все ли поля заполнены.", "Прежупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            MySqlOperations.Search_In_ComboBox(dataGridView1.SelectedRows[0].Cells[2].Value.ToString(), comboBox1);
            numericUpDown1.Value = decimal.Parse(dataGridView1.SelectedRows[0].Cells[3].Value.ToString());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                MySqlOperations.Insert_Update_Delete(MySqlQueries.Update_Materialy, dataGridView1.SelectedRows[0].Cells[0].Value.ToString(), textBox1.Text, comboBox1.Text, numericUpDown1.Value.ToString().Replace(',','.'));
                Load_Table();
                Clear();
            }
            else
                MessageBox.Show("Проверьте, все ли поля заполнены.", "Прежупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void button3_Click(object sender, EventArgs e) => Clear();

        private void Clear()
        {
            textBox1.Text = "";
            comboBox1.SelectedItem = comboBox1.Items[0];
            numericUpDown1.Value = numericUpDown1.Minimum;
        }

        private void dataGridView1_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите удалить запись?", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                MySqlOperations.Insert_Update_Delete(MySqlQueries.Delete_Materialy, dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
        }

        private void button4_Click(object sender, EventArgs e) => MySqlOperations.Search(textBox2, dataGridView1);

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
                dataGridView1.ClearSelection();
        }
    }
}
