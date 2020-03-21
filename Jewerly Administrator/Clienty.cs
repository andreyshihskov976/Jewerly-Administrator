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
    public partial class Clienty : Form
    {
        MySqlOperations MySqlOperations = null;
        MySqlQueries MySqlQueries = null;
        public Clienty(MySqlOperations mySqlOperations, MySqlQueries mySqlQueries)
        {
            InitializeComponent();
            MySqlOperations = mySqlOperations;
            MySqlQueries = mySqlQueries;
        }

        private void Clienty_Load(object sender, EventArgs e) => Load_Table();

        private void Load_Table()
        {
            MySqlOperations.Select_DataGridView(MySqlQueries.Select_Clienty, dataGridView1);
            dataGridView1.Columns[0].Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox3.Text != "" && textBox4.Text != "")
            {
                MySqlOperations.Insert_Update_Delete(MySqlQueries.Insert_Clienty, null, textBox1.Text, textBox3.Text, textBox4.Text, maskedTextBox1.Text, maskedTextBox2.Text);
                Load_Table();
                Clear();
            }
            else
                MessageBox.Show("Проверьте, все ли поля заполнены.", "Прежупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString().Split(' ')[0];
            textBox3.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString().Split(' ')[1];
            textBox4.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString().Split(' ')[2];
            maskedTextBox1.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            maskedTextBox2.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox3.Text != "" && textBox4.Text != "")
            {
                MySqlOperations.Insert_Update_Delete(MySqlQueries.Update_Clienty, dataGridView1.SelectedRows[0].Cells[0].Value.ToString(), textBox1.Text, textBox3.Text, textBox4.Text, maskedTextBox1.Text, maskedTextBox2.Text);
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
            textBox3.Text = "";
            textBox4.Text = "";
            maskedTextBox1.Clear();
            maskedTextBox2.Clear();
        }

        private void dataGridView1_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите удалить запись?", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                MySqlOperations.Insert_Update_Delete(MySqlQueries.Delete_Clienty, dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
        }

        private void button4_Click(object sender, EventArgs e) => MySqlOperations.Search(textBox2, dataGridView1);

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
                dataGridView1.ClearSelection();
        }
    }
}
