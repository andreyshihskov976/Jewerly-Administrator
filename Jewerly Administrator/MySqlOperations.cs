﻿using Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Word;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Data.Common;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Collections;
using Application = System.Windows.Forms.Application;
using ExcelApplication = Microsoft.Office.Interop.Excel.Application;
using WordApplication = Microsoft.Office.Interop.Word.Application;
using System.Threading.Tasks;
using System.Reflection;
using DataTable = System.Data.DataTable;
using TextBox = System.Windows.Forms.TextBox;

namespace Jewerly_Administrator
{

    public class MySqlOperations
    {
        public MySqlConnection mySqlConnection = new MySqlConnection("server=localhost; user=root; database=uvelirka; port=3306; password=; charset=utf8;");
        public MySqlQueries MySqlQueries = null;

        MySqlDataReader sqlDataReader = null;

        MySqlDataAdapter dataAdapter = null;

        DataSet dataSet = null;

        MySqlCommand sqlCommand = null;

        public MySqlOperations(MySqlQueries sqlQueries)
        {
            this.MySqlQueries = sqlQueries;
        }
        //Подключение (Закрытие подключения) к Базе Данных
        public void OpenConnection()
        {
            mySqlConnection.Open();
        }
        public void CloseConnection()
        {
            mySqlConnection.Close();
        }
        //Подключение (Закрытие подключения) к Базе Данных

        //Универсальные методы
        public void Select_DataGridView(string query, DataGridView dataGridView, string ID = null, string Value1 = null, string Value2 = null, string Value3 = null)
        {
            try
            {
                dataGridView.DataSource = null;
                dataSet = new DataSet();
                sqlCommand = new MySqlCommand(query, mySqlConnection);
                sqlCommand.Parameters.AddWithValue("ID", ID);
                sqlCommand.Parameters.AddWithValue("Value1", Value1);
                sqlCommand.Parameters.AddWithValue("Value2", Value2);
                sqlCommand.Parameters.AddWithValue("Value3", Value3);
                dataAdapter = new MySqlDataAdapter(sqlCommand);
                dataAdapter.Fill(dataSet);
                dataGridView.DataSource = dataSet.Tables[0].DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public DataTable Select_DataTable(string query, string ID = null, string Value1 = null)
        {
            DataTable dataTable = new DataTable();
            MySqlCommand sqlCommand = new MySqlCommand(query, mySqlConnection);
            sqlCommand.Parameters.AddWithValue("ID", ID);
            sqlCommand.Parameters.AddWithValue("Value1", Value1);
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(sqlCommand);
            dataAdapter.Fill(dataTable);
            return dataTable;
        }

        public void Select_ComboBox(string query, ComboBox comboBox)
        {
            try
            {
                comboBox.Items.Clear();
                sqlCommand = new MySqlCommand(query, mySqlConnection);
                sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    comboBox.Items.Add(Convert.ToString(sqlDataReader[0]));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlDataReader != null)
                    sqlDataReader.Close();
                if (comboBox.Items.Count != 0)
                {
                    comboBox.SelectedIndex = 0;
                }
            }
        }

        public void Search_In_ComboBox(string s, ComboBox comboBox)
        {
            bool result = false;
            for (int i = 0; i < comboBox.Items.Count; i++)
            {
                if (comboBox.Items[i].ToString().Contains(s))
                {
                    comboBox.SelectedIndex = i;
                    result = true;
                    break;
                }
            }
            if (!result)
            {
                comboBox.Items.Add(s);
                comboBox.SelectedItem = s;
            }
        }

        public string Select_Text(string query, string ID = null, string Value1 = null, string Value2 = null, string Value3 = null, string Value4 = null, string Value5 = null, string Value6 = null, string Value7 = null, string Value8 = null)
        {
            string output = string.Empty;
            try
            {
                sqlCommand = new MySqlCommand(query, mySqlConnection);
                sqlCommand.Parameters.AddWithValue("Value1", Value1);
                sqlCommand.Parameters.AddWithValue("Value2", Value2);
                sqlCommand.Parameters.AddWithValue("Value3", Value3);
                sqlCommand.Parameters.AddWithValue("Value4", Value4);
                sqlCommand.Parameters.AddWithValue("Value5", Value5);
                sqlCommand.Parameters.AddWithValue("Value6", Value6);
                sqlCommand.Parameters.AddWithValue("Value7", Value7);
                sqlCommand.Parameters.AddWithValue("Value8", Value8);
                sqlCommand.Parameters.AddWithValue("ID", ID);
                sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    output = Convert.ToString(sqlDataReader[0]);
                    break;
                }
                return output;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return string.Empty;
            }
            finally
            {
                if (sqlDataReader != null)
                    sqlDataReader.Close();
            }
        }

        public void Select_List(string query, ref ArrayList list, string ID = null, string Value1 = null, string Value2 = null, string Value3 = null, string Value4 = null, string Value5 = null, string Value6 = null, string Value7 = null, string Value8 = null)
        {
            try
            {
                sqlCommand = new MySqlCommand(query, mySqlConnection);
                sqlCommand.Parameters.AddWithValue("Value1", Value1);
                sqlCommand.Parameters.AddWithValue("Value2", Value2);
                sqlCommand.Parameters.AddWithValue("Value3", Value3);
                sqlCommand.Parameters.AddWithValue("Value4", Value4);
                sqlCommand.Parameters.AddWithValue("Value5", Value5);
                sqlCommand.Parameters.AddWithValue("Value6", Value6);
                sqlCommand.Parameters.AddWithValue("Value7", Value7);
                sqlCommand.Parameters.AddWithValue("Value8", Value8);
                sqlCommand.Parameters.AddWithValue("ID", ID);
                sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    list.Add(Convert.ToString(sqlDataReader[0]));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlDataReader != null)
                    sqlDataReader.Close();
            }
        }

        public void Insert_Update_Delete(string query, string ID = null, string Value1 = null, string Value2 = null, string Value3 = null, string Value4 = null, string Value5 = null, string Value6 = null, string Value7 = null, string Value8 = null)
        {
            try
            {
                sqlCommand = new MySqlCommand(query, mySqlConnection);
                sqlCommand.Parameters.AddWithValue("Value1", Value1);
                sqlCommand.Parameters.AddWithValue("Value2", Value2);
                sqlCommand.Parameters.AddWithValue("Value3", Value3);
                sqlCommand.Parameters.AddWithValue("Value4", Value4);
                sqlCommand.Parameters.AddWithValue("Value5", Value5);
                sqlCommand.Parameters.AddWithValue("Value6", Value6);
                sqlCommand.Parameters.AddWithValue("Value7", Value7);
                sqlCommand.Parameters.AddWithValue("Value8", Value8);
                sqlCommand.Parameters.AddWithValue("ID", ID);
                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Search(TextBox textBox, DataGridView dataGridView)
        {
            if (textBox.Text != "")
            {
                for (int i = 0; i < dataGridView.RowCount; i++)
                {
                    dataGridView.Rows[i].Selected = false;
                    for (int j = 0; j < dataGridView.ColumnCount; j++)
                        if (dataGridView.Rows[i].Cells[j].Value != null)
                            if (dataGridView.Rows[i].Cells[j].Value.ToString().Contains(textBox.Text))
                            {
                                dataGridView.Rows[i].Selected = true;
                                break;
                            }
                }
            }
            else dataGridView.ClearSelection();
        }

        public void Filter(ToolStripTextBox textBox, DataGridView dataGridView)
        {
            if (textBox.Text != "")
            {
                for (int i = 0; i < dataGridView.RowCount; i++)
                {
                    dataGridView.Rows[i].Selected = false;
                    for (int j = 0; j < dataGridView.ColumnCount; j++)
                        if (dataGridView.Rows[i].Cells[j].Value != null)
                            if (dataGridView.Rows[i].Cells[j].Value.ToString().Contains(textBox.Text) == true)
                            {
                                dataGridView.CurrentCell = dataGridView.Rows[i].Cells[1];
                                dataGridView.Rows[i].Visible = true;
                                break;
                            }
                            else
                            {
                                dataGridView.Rows[i].Visible = false;
                                break;
                            }
                }
            }
            else dataGridView.ClearSelection();
        }

        private void Replace(string Identify, string Text, Document document)
        {
            var range = document.Content;
            range.Find.Execute(FindText: Identify, ReplaceWith: Text);
        }

        public void Print_Dogovor(SaveFileDialog saveFileDialog, string ID)
        {
            WordApplication WordApp = null;
            Documents Documents = null;
            Document Document = null;
            string output = null;
            string fileName = null;
            saveFileDialog.Title = "Сохранить договор как";
            //output = Select_Text(MySqlQueries.Select_Print_Dogovory, ID);
            saveFileDialog.FileName = "Договор " + output.Split(';')[0];
            saveFileDialog.InitialDirectory = Application.StartupPath + "\\Договоры\\";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    fileName = saveFileDialog.FileName;
                    WordApp = new WordApplication();
                    Documents = WordApp.Documents;
                    Document = Documents.Open(Application.StartupPath + "\\blanks\\Dogovor.docx");
                    Replace("{Договор}", output.Split(';')[0], Document);
                    Replace("{Сотрудник}", output.Split(';')[1], Document);
                    Replace("{Клиент}", output.Split(';')[2], Document);
                    Replace("{Клиент}", output.Split(';')[2], Document);
                    Replace("{Авто}", output.Split(';')[3], Document);
                    Replace("{Гос.знак}", output.Split(';')[4], Document);
                    Replace("{VIN-номер}", output.Split(';')[5], Document);
                    Replace("{Стоимость авто}", output.Split(';')[6], Document);
                    Replace("{Дата начала}", output.Split(';')[7], Document);
                    Replace("{Дата окончания}", output.Split(';')[8], Document);
                    Replace("{Дата начала}", output.Split(';')[7], Document);
                    Replace("{Дата окончания}", output.Split(';')[8], Document);
                    Replace("{Сумма}", output.Split(';')[9], Document);
                    Replace("{Паспорт}", output.Split(';')[10], Document);
                    Replace("{Ид номер}", output.Split(';')[11], Document);
                    Replace("{Телефон}", output.Split(';')[12], Document);
                    Replace("{Email}", output.Split(';')[13], Document);
                    Replace("{Дата}", output.Split(';')[14], Document);
                    Document.SaveAs(fileName);
                    WordApp.Visible = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Marshal.ReleaseComObject(Documents);
                    Marshal.ReleaseComObject(Document);
                    Marshal.ReleaseComObject(WordApp);
                }
                finally
                {
                    Marshal.ReleaseComObject(Documents);
                    Marshal.ReleaseComObject(Document);
                    Marshal.ReleaseComObject(WordApp);
                }
            }
        }

        public void Print_Acts(SaveFileDialog saveFileDialog, string ID)
        {
            WordApplication WordApp = null;
            Documents Documents = null;
            Document Document = null;
            string output = null;
            string fileName = null;
            saveFileDialog.Title = "Сохранить акт как";
            //output = Select_Text(MySqlQueries.Select_Print_Acts, ID);
            saveFileDialog.FileName = output.Split(';')[0];
            saveFileDialog.InitialDirectory = Application.StartupPath + "\\Акты\\";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    fileName = saveFileDialog.FileName;
                    WordApp = new WordApplication();
                    Documents = WordApp.Documents;
                    Document = Documents.Open(Application.StartupPath + "\\blanks\\Act.docx");
                    Replace("{Наименование}", output.Split(';')[0], Document);
                    Replace("{Договор}", output.Split(';')[1], Document);
                    Replace("{Сотрудник}", output.Split(';')[2], Document);
                    Replace("{Сотрудник}", output.Split(';')[2], Document);
                    Replace("{Автомобиль}", output.Split(';')[3], Document);
                    Replace("{Клиент}", output.Split(';')[4], Document);
                    Replace("{Клиент}", output.Split(';')[4], Document);
                    Replace("{Комментарий}", output.Split(';')[5], Document);
                    Document.SaveAs(fileName);
                    WordApp.Visible = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Marshal.ReleaseComObject(Documents);
                    Marshal.ReleaseComObject(Document);
                    Marshal.ReleaseComObject(WordApp);
                }
                finally
                {
                    Marshal.ReleaseComObject(Documents);
                    Marshal.ReleaseComObject(Document);
                    Marshal.ReleaseComObject(WordApp);
                }
            }
        }

        public static Task<object> GetTaskFromEvent(object @object, string @event)
        {
            if (@object == null || @event == null) throw new ArgumentNullException("Arguments cannot be null");

            EventInfo EventInfo = @object.GetType().GetEvent(@event);
            if (EventInfo == null)
            {
                throw new ArgumentException(String.Format("*{0}* has no *{1}* event", @object, @event));
            }

            TaskCompletionSource<object> TaskComleteSource = new TaskCompletionSource<object>();
            MethodInfo MethodInfo = null;
            Delegate Delegate = null;
            EventHandler Handler = null;

            Handler = (s, e) =>
            {
                MethodInfo = Handler.Method;
                Delegate = Delegate.CreateDelegate(EventInfo.EventHandlerType, Handler.Target, MethodInfo);
                EventInfo.RemoveEventHandler(s, Delegate);
                TaskComleteSource.TrySetResult(null);
            };

            MethodInfo = Handler.Method;
            Delegate = Delegate.CreateDelegate(EventInfo.EventHandlerType, Handler.Target, MethodInfo);
            EventInfo.AddEventHandler(@object, Delegate);
            return TaskComleteSource.Task;
        }
    }
}