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
using ExcelApplication = Microsoft.Office.Interop.Excel.Application;
using WordApplication = Microsoft.Office.Interop.Word.Application;
using System.Threading.Tasks;
using System.Reflection;
using DataTable = System.Data.DataTable;

namespace Jewerly_Administrator
{

    public class MySqlQueries
    {
        //Select
        public string Select_Izdeliya = $@"SELECT izdeliya.ID_Izdeliya, izdeliya.Name AS 'Наименование' FROM izdeliya;";

        public string Select_Skidki = $@"SELECT ID_Skidki, Name AS 'Наименование', Procent AS 'Значение' FROM skidki;";

        public string Select_Materialy = $@"SELECT ID_Materiala, Name AS 'Наименование', Ed_Izm AS 'Единицы измерения', Stoimost AS 'Стоимость' FROM materialy;";

        public string Select_Sotrudniki = $@"SELECT ID_Sotrudnika, CONCAT(Familiya, ' ', Imya, ' ', Otchestvo) AS 'Ф.И.О. Сотрудника', Doljnost AS 'Должность', Telephone AS 'Контактный телефон' FROM sotrudniki;";

        public string Select_Clienty = $@"SELECT ID_Clienta, CONCAT(Familiya, ' ',Imya, ' ', Otchestvo) AS 'Ф.И.О. Клиента', Telephone AS 'Контактный телефон', Passport AS 'Номер паспорта' FROM clienty;";
        //Select

        //Insert
        public string Insert_Izdeliya = $@"INSERT INTO izdeliya (Name) VALUES (@Value1);";

        public string Insert_Skidki = $@"INSERT INTO skidki (Name, Procent) VALUES (@Value1, @Value2);";

        public string Insert_Materialy = $@"INSERT INTO materialy (Name, Ed_Izm, Stoimost) VALUES (@Value1, @Value2, @Value3);";

        public string Insert_Sotrudniki = $@"INSERT INTO sotrudniki (Familiya, Imya, Otchestvo, Doljnost, Telephone) VALUES (@Value1, @Value2, @Value3, @Value4, @Value5);";

        public string Insert_Clienty = $@"INSERT INTO clienty (Familiya, Imya, Otchestvo, Telephone, Passport) VALUES (@Value1, @Value2, @Value3, @Value4, @Value5);";
        //Insert

        //Update
        public string Update_Izdeliya = $@"UPDATE izdeliya SET Name = @Value1 WHERE ID_Izdeliya = @ID;";

        public string Update_Skidki = $@"UPDATE skidki SET Name = @Value1, Procent = @Value2 WHERE ID_Skidki = @ID;";

        public string Update_Materialy = $@"UPDATE materialy SET Name = @Value1, Ed_Izm = @Value2, Stoimost = @Value3  WHERE ID_Materiala = @ID;";

        public string Update_Sotrudniki = $@"UPDATE sotrudniki SET Familiya= @Value1, Imya = @Value2, Otchestvo = @Value3, Doljnost = @Value4, Telephone = @Value5 WHERE ID_Sotrudnika = @ID;";

        public string Update_Clienty = $@"UPDATE clienty SET Familiya = @Value1, Imya = @Value2, Otchestvo = @Value3, Telephone = @Value4, Passport = @Value5 WHERE ID_Clienta = @ID;";
        //Update

        //Delete
        public string Delete_Izdeliya = $@"DELETE FROM izdeliya WHERE ID_Izdeliya = @ID;";

        public string Delete_Skidki = $@"DELETE FROM skidki WHERE ID_Skidki = @ID;";

        public string Delete_Materialy = $@"DELETE FROM materialy WHERE ID_Materiala = @ID;";

        public string Delete_Sotrudniki = $@"DELETE FROM sotrudniki WHERE ID_Sotrudnika = @ID;";

        public string Delete_Clienty = $@"DELETE FROM clienty WHERE ID_Clienta = @ID;";
        //Delete
    }
}