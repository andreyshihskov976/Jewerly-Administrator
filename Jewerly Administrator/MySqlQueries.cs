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

        public string Select_Izdeliya_ComboBox = $@"SELECT izdeliya.Name FROM izdeliya;";

        public string Select_Izdeliya_ID = $@"SELECT izdeliya.ID_Izdeliya FROM izdeliya WHERE izdeliya.Name = @Value1;";

        public string Select_Skidki = $@"SELECT ID_Skidki, Name AS 'Наименование', Procent AS 'Значение' FROM skidki;";

        public string Select_Materialy = $@"SELECT ID_Materiala, Name AS 'Наименование', Ed_Izm AS 'Единицы измерения', Stoimost AS 'Стоимость' FROM materialy;";

        public string Select_Materialy_ComboBox = $@"SELECT materialy.Name FROM materialy;";

        public string Select_Materialy_ID = $@"SELECT materialy.ID_Materiala FROM materialy WHERE materialy.Name = @Value1;";

        public string Select_EdIzm_Materialy = $@"SELECT materialy.Ed_Izm FROM materialy WHERE materialy.Name = @Value1;";

        public string Select_Sotrudniki = $@"SELECT ID_Sotrudnika, CONCAT(Familiya, ' ', Imya, ' ', Otchestvo) AS 'Ф.И.О. Сотрудника', Doljnost AS 'Должность', Telephone AS 'Контактный телефон' FROM sotrudniki;";

        public string Select_Sotrudniki_ComboBox = $@"SELECT CONCAT(sotrudniki.Familiya, ' ',sotrudniki.Imya,' ', sotrudniki.Otchestvo) FROM sotrudniki;";

        public string Select_Sotrudniki_ID = $@"SELECT sotrudniki.ID_Sotrudnika FROM sotrudniki WHERE CONCAT(sotrudniki.Familiya, ' ',sotrudniki.Imya,' ', sotrudniki.Otchestvo) = @Value1;";

        public string Select_Clienty = $@"SELECT ID_Clienta, CONCAT(Familiya, ' ',Imya, ' ', Otchestvo) AS 'Ф.И.О. Клиента', Telephone AS 'Контактный телефон', Passport AS 'Номер паспорта' FROM clienty;";

        public string Select_Clienty_ComboBox = $@"SELECT CONCAT(clienty.Familiya, ' ',clienty.Imya,' ', clienty.Otchestvo) FROM clienty;";

        public string Select_Clienty_ID = $@"SELECT clienty.ID_Clienta FROM clienty WHERE CONCAT(clienty.Familiya, ' ',clienty.Imya,' ', clienty.Otchestvo) = @Value1;";

        public string Select_Acts = $@"SET lc_time_names = 'ru_RU';
SELECT acts.ID_Acta,CONCAT('№ ',acts.ID_Acta, ' от ',DATE_FORMAT(acts.Date,'%d %M %Y')) AS 'Договор' , CONCAT(clienty.Familiya, ' ',clienty.Imya,' ', clienty.Otchestvo) AS 'Ф.И.О. Клиента', 
CONCAT(sotrudniki.Familiya, ' ',sotrudniki.Imya,' ', sotrudniki.Otchestvo) AS 'Ф.И.О. Сотрудника', 
izdeliya.Name AS 'Наименование изделия' , Date_N AS 'Дата начала работы', Date_K AS 'Дата окончания работы' 
FROM acts INNER JOIN clienty ON acts.ID_Clienta = clienty.ID_Clienta
INNER JOIN sotrudniki ON acts.ID_Sotrudnika = sotrudniki.ID_Sotrudnika
INNER JOIN izdeliya ON acts.ID_Izdeliya = izdeliya.ID_Izdeliya;";

        public string Select_Sostav_Acta = $@"SELECT ID_Posicii, materialy.Name AS 'Наименование материала', Kolichestvo AS 'Вес' 
FROM sostav_acta INNER JOIN materialy ON sostav_acta.ID_Materiala = materialy.ID_Materiala
WHERE sostav_acta.ID_Acta = @ID;";
        //Select

        //Insert
        public string Insert_Izdeliya = $@"INSERT INTO izdeliya (Name) VALUES (@Value1);";

        public string Insert_Skidki = $@"INSERT INTO skidki (Name, Procent) VALUES (@Value1, @Value2);";

        public string Insert_Materialy = $@"INSERT INTO materialy (Name, Ed_Izm, Stoimost) VALUES (@Value1, @Value2, @Value3);";

        public string Insert_Sotrudniki = $@"INSERT INTO sotrudniki (Familiya, Imya, Otchestvo, Doljnost, Telephone) VALUES (@Value1, @Value2, @Value3, @Value4, @Value5);";

        public string Insert_Clienty = $@"INSERT INTO clienty (Familiya, Imya, Otchestvo, Telephone, Passport) VALUES (@Value1, @Value2, @Value3, @Value4, @Value5);";

        public string Insert_Acts = $@"INSERT INTO acts (Date, ID_Clienta, ID_Sotrudnika, ID_Izdeliya, Date_N, Date_K) VALUES (@Value1, @Value2, @Value3, @Value4, @Value5, @Value6);";

        public string Insert_Sostav_Acta = $@"INSERT INTO sostav_acta (ID_Acta, ID_Materiala, Kolichestvo) VALUES (@Value1, @Value2, @Value3);";
        //Insert

        //Update
        public string Update_Izdeliya = $@"UPDATE izdeliya SET Name = @Value1 WHERE ID_Izdeliya = @ID;";

        public string Update_Skidki = $@"UPDATE skidki SET Name = @Value1, Procent = @Value2 WHERE ID_Skidki = @ID;";

        public string Update_Materialy = $@"UPDATE materialy SET Name = @Value1, Ed_Izm = @Value2, Stoimost = @Value3  WHERE ID_Materiala = @ID;";

        public string Update_Sotrudniki = $@"UPDATE sotrudniki SET Familiya= @Value1, Imya = @Value2, Otchestvo = @Value3, Doljnost = @Value4, Telephone = @Value5 WHERE ID_Sotrudnika = @ID;";

        public string Update_Clienty = $@"UPDATE clienty SET Familiya = @Value1, Imya = @Value2, Otchestvo = @Value3, Telephone = @Value4, Passport = @Value5 WHERE ID_Clienta = @ID;";

        public string Update_Acts = $@"UPDATE acts SET Date = @Value1, ID_Clienta = @Value2, ID_Sotrudnika = @Value3, ID_Izdeliya = @Value4, Date_N = @Value5, Date_K = @Value6 WHERE ID_Acta = @ID;";

        public string Update_Sostav_Acta = $@"UPDATE sostav_acta SET ID_Acta = @Value1, ID_Materiala = @Value2, Kolichestvo = @Value3 WHERE ID_Posicii = @ID;";
        //Update

        //Delete
        public string Delete_Izdeliya = $@"DELETE FROM izdeliya WHERE ID_Izdeliya = @ID;";

        public string Delete_Skidki = $@"DELETE FROM skidki WHERE ID_Skidki = @ID;";

        public string Delete_Materialy = $@"DELETE FROM materialy WHERE ID_Materiala = @ID;";

        public string Delete_Sotrudniki = $@"DELETE FROM sotrudniki WHERE ID_Sotrudnika = @ID;";

        public string Delete_Clienty = $@"DELETE FROM clienty WHERE ID_Clienta = @ID;";

        public string Delete_Acts = $@"DELETE FROM acts WHERE ID_Acta = @ID;";

        public string Delete_Sostav_Acta = $@"DELETE FROM sostav_acta WHERE ID_Posicii = @ID;";
        //Delete
    }
}