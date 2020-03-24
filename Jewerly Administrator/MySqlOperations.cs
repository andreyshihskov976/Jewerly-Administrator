using Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Word;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Data.Common;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Collections;
using Application = System.Windows.Forms.Application;
using Excel = Microsoft.Office.Interop.Excel;
using ExcelApplication = Microsoft.Office.Interop.Excel.Application;
using WordApplication = Microsoft.Office.Interop.Word.Application;
using System.Threading.Tasks;
using System.Reflection;
using DataTable = System.Data.DataTable;
using TextBox = System.Windows.Forms.TextBox;
using XlLineStyle = Microsoft.Office.Interop.Excel.XlLineStyle;

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

        public void Insert_Update_Delete(string query, string ID = null, string Value1 = null, string Value2 = null, string Value3 = null, string Value4 = null, string Value5 = null, string Value6 = null, string Value7 = null, string Value8 = null, string Value9 = null)
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
                sqlCommand.Parameters.AddWithValue("Value9", Value9);
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

        public void Print_Cheki(SaveFileDialog saveFileDialog, string ID)
        {
            ExcelApplication ExcelApp = null;
            Workbooks workbooks = null;
            Workbook workbook = null;
            Worksheet worksheet = null;
            string output = Select_Text(MySqlQueries.Print_Cheki, ID);
            string fileName = null;
            saveFileDialog.Title = "Сохранить чек как";
            saveFileDialog.FileName = "Товарный чек " + output.Split(';')[1];
            saveFileDialog.InitialDirectory = Application.StartupPath + "\\Чеки\\";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                fileName = saveFileDialog.FileName;
                try
                {
                    ExcelApp = new ExcelApplication();
                    workbooks = ExcelApp.Workbooks;
                    workbook = workbooks.Open(Application.StartupPath + "\\blanks\\Чек.xlsx");
                    worksheet = workbook.Worksheets.get_Item(1) as Worksheet;
                    ExcelApp.Cells[5, 1] = "Товарный чек " + output.Split(';')[1];
                    ExcelApp.Cells[7, 1] = "На основании акта: "+output.Split(';')[2];
                    ExcelApp.Cells[9, 1] = "Оформил (Ф.И.О.): "+output.Split(';')[3];
                    ExcelApp.Cells[10, 1] = "Заказчик (Ф.И.О.): " + output.Split(';')[4];
                    ExcelApp.Cells[11, 1] = "Изделие: "+output.Split(';')[5];
                    ExcelApp.Cells[12, 1] = "Размер изделия (кольцо): " + output.Split(';')[6];
                    ExcelApp.Cells[13, 1] = "Длина изделия (браслет, колье, цепь): " + output.Split(';')[7];
                    ExcelApp.Cells[14, 1] = "Стоимость модели: "+output.Split(';')[8];
                    ExcelApp.Cells[15, 1] = "Стоимость работы мастера: "+output.Split(';')[9];
                    ExcelApp.Cells[16, 1] = "Стоимость штампирования пробы: "+output.Split(';')[10];
                    DataTable data = Select_DataTable(MySqlQueries.Select_Sostav_Cheka,ID);
                    int ExCol = 1;
                    int ExRow = 19;
                    for (int i = 0; i < data.Rows.Count; i++)
                    {
                        ExCol = 1;
                        for (int j = 0; j < data.Columns.Count; j++)
                        {
                            ExcelApp.Cells[ExRow, ExCol] = data.Rows[i][j].ToString();
                            ExCol++;
                        }
                        ExRow++;
                    }
                    var cells = worksheet.get_Range("A19 ", "D" + (ExRow - 1).ToString());
                    cells.Borders[XlBordersIndex.xlInsideVertical].LineStyle = XlLineStyle.xlContinuous;
                    cells.Borders[XlBordersIndex.xlInsideHorizontal].LineStyle = XlLineStyle.xlContinuous;
                    cells.Borders[XlBordersIndex.xlEdgeTop].LineStyle = XlLineStyle.xlContinuous;
                    cells.Borders[XlBordersIndex.xlEdgeRight].LineStyle = XlLineStyle.xlContinuous;
                    cells.Borders[XlBordersIndex.xlEdgeLeft].LineStyle = XlLineStyle.xlContinuous;
                    cells.Borders[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;
                    ExRow++;
                    Excel.Range Cells = worksheet.get_Range("A"+ExRow, "D"+ExRow).Cells;
                    Cells.Merge(Type.Missing);
                    ExcelApp.Rows.RowHeight = 15;
                    ExcelApp.Cells[ExRow,1] = "Итого (без учета скидки): " + output.Split(';')[11];
                    ExRow++;
                    Cells = worksheet.get_Range("A" + ExRow, "D" + ExRow).Cells;
                    Cells.Merge(Type.Missing);
                    ExcelApp.Cells[ExRow, 1] = "Предусмотрена скидка: " + output.Split(';')[12];
                    ExRow++;
                    Cells = worksheet.get_Range("A" + ExRow, "D" + ExRow).Cells;
                    Cells.Merge(Type.Missing);
                    ExcelApp.Cells[ExRow, 1] = "Итого (с учетом скидки): " + output.Split(';')[13];
                    ExRow++;
                    Cells = worksheet.get_Range("A" + ExRow, "D" + ExRow).Cells;
                    Cells.Merge(Type.Missing);
                    ExcelApp.Cells[ExRow, 1] = "Гарантия: " + output.Split(';')[14];
                    ExRow++;
                    workbook.SaveAs(fileName);
                    ExcelApp.Visible = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Marshal.ReleaseComObject(worksheet);
                    Marshal.ReleaseComObject(workbook);
                    Marshal.ReleaseComObject(workbooks);
                    Marshal.ReleaseComObject(ExcelApp);
                }
                finally
                {
                    Marshal.ReleaseComObject(worksheet);
                    Marshal.ReleaseComObject(workbook);
                    Marshal.ReleaseComObject(workbooks);
                    Marshal.ReleaseComObject(ExcelApp);
                }
            }
        }

        public void Print_Acts(SaveFileDialog saveFileDialog, string ID)
        {
            ExcelApplication ExcelApp = null;
            Workbooks workbooks = null;
            Workbook workbook = null;
            Worksheet worksheet = null;
            string output = Select_Text(MySqlQueries.Print_Acts, ID);
            string fileName = null;
            saveFileDialog.Title = "Сохранить акт как";
            saveFileDialog.FileName = "Акт на изготовление " + output.Split(';')[0];
            saveFileDialog.InitialDirectory = Application.StartupPath + "\\Акты\\";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                fileName = saveFileDialog.FileName;
                try
                {
                    ExcelApp = new ExcelApplication();
                    workbooks = ExcelApp.Workbooks;
                    workbook = workbooks.Open(Application.StartupPath + "\\blanks\\Акт.xlsx");
                    worksheet = workbook.Worksheets.get_Item(1) as Worksheet;
                    ExcelApp.Cells[1, 2] = "Акт на изготовление " + output.Split(';')[0];
                    ExcelApp.Cells[5, 2] = "Заказчик (Ф.И.О.): " + output.Split(';')[1];
                    ExcelApp.Cells[6, 2] = "Номер паспорта: " + output.Split(';')[2];
                    ExcelApp.Cells[7, 2] = "Телефон: " + output.Split(';')[3];
                    ExcelApp.Cells[5, 8] = "Изделие: " + output.Split(';')[4];
                    ExcelApp.Cells[6, 8] = "Размер изделия (кольцо): " + output.Split(';')[5];
                    ExcelApp.Cells[7, 8] = "Длина изделия (браслет, колье, цепь): " + output.Split(';')[6];
                    ExcelApp.Cells[9, 2] = "Оформил (Ф.И.О.): " + output.Split(';')[7];
                    ExcelApp.Cells[10, 2] = "Телефон: " + output.Split(';')[8];
                    ExcelApp.Cells[11, 8] = "Дата начала срока изготовления: " + output.Split(';')[9];
                    ExcelApp.Cells[12, 8] = "Дата окончания срока изготовления: " + output.Split(';')[10];
                    DataTable data = Select_DataTable(MySqlQueries.Select_Print_Sostav_Acta, ID);
                    int ExCol = 2;
                    int ExRow = 13;
                    for (int i = 0; i < data.Rows.Count; i++)
                    {
                        ExCol = 2;
                        for (int j = 0; j < data.Columns.Count; j++)
                        {
                            ExcelApp.Cells[ExRow, ExCol] = data.Rows[i][j].ToString();
                            ExCol++;
                        }
                        ExRow++;
                    }
                    var cells = worksheet.get_Range("B13 ", "D" + (ExRow - 1).ToString());
                    cells.Borders[XlBordersIndex.xlInsideVertical].LineStyle = XlLineStyle.xlContinuous;
                    cells.Borders[XlBordersIndex.xlInsideHorizontal].LineStyle = XlLineStyle.xlContinuous;
                    cells.Borders[XlBordersIndex.xlEdgeTop].LineStyle = XlLineStyle.xlContinuous;
                    cells.Borders[XlBordersIndex.xlEdgeRight].LineStyle = XlLineStyle.xlContinuous;
                    cells.Borders[XlBordersIndex.xlEdgeLeft].LineStyle = XlLineStyle.xlContinuous;
                    cells.Borders[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;
                    ExcelApp.Cells[ExRow, 2] = "Итого вес: " + output.Split(';')[11];
                    workbook.SaveAs(fileName);
                    ExcelApp.Visible = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Marshal.ReleaseComObject(worksheet);
                    Marshal.ReleaseComObject(workbook);
                    Marshal.ReleaseComObject(workbooks);
                    Marshal.ReleaseComObject(ExcelApp);
                }
                finally
                {
                    Marshal.ReleaseComObject(worksheet);
                    Marshal.ReleaseComObject(workbook);
                    Marshal.ReleaseComObject(workbooks);
                    Marshal.ReleaseComObject(ExcelApp);
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