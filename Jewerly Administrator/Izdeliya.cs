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
    public partial class Izdeliya : Form
    {
        MySqlOperations MySqlOperations = null;
        MySqlQueries MySqlQueries = null;
        public Izdeliya(MySqlOperations mySqlOperations, MySqlQueries mySqlQueries)
        {
            InitializeComponent();
            MySqlOperations = mySqlOperations;
            MySqlQueries = mySqlQueries;
        }

        private void Izdeliya_Load(object sender, EventArgs e) => Load_Table();

        private void Load_Table()
        {
            MySqlOperations.Select_DataGridView(MySqlQueries.Select_Izdeliya, dataGridView1);
            dataGridView1.Columns[0].Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                MySqlOperations.Insert_Update_Delete(MySqlQueries.Insert_Izdeliya, null, textBox1.Text);
                Load_Table();
                textBox1.Text = "";
            }
            else
                MessageBox.Show("Проверьте, все ли поля заполнены.", "Прежупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e) => textBox1.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                MySqlOperations.Insert_Update_Delete(MySqlQueries.Update_Izdeliya, dataGridView1.SelectedRows[0].Cells[0].Value.ToString(), textBox1.Text);
                Load_Table();
                textBox1.Text = "";
            }
            else
                MessageBox.Show("Проверьте, все ли поля заполнены.", "Прежупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void button3_Click(object sender, EventArgs e) => textBox1.Text = "";

        private void dataGridView1_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите удалить запись?", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                MySqlOperations.Insert_Update_Delete(MySqlQueries.Delete_Izdeliya, dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
        }

        private void button4_Click(object sender, EventArgs e) => MySqlOperations.Search(textBox2, dataGridView1);

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
                dataGridView1.ClearSelection();
        }
    }
}
