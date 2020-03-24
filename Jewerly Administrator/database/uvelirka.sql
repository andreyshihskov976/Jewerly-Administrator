-- --------------------------------------------------------
-- Хост:                         127.0.0.1
-- Версия сервера:               10.4.11-MariaDB - mariadb.org binary distribution
-- Операционная система:         Win64
-- HeidiSQL Версия:              10.3.0.5771
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;


-- Дамп структуры базы данных uvelirka
CREATE DATABASE IF NOT EXISTS `uvelirka` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `uvelirka`;

-- Дамп структуры для таблица uvelirka.acts
CREATE TABLE IF NOT EXISTS `acts` (
  `ID_Acta` int(11) NOT NULL AUTO_INCREMENT COMMENT 'ID Акта',
  `Date` date NOT NULL COMMENT 'Дата оформления',
  `ID_Clienta` int(11) NOT NULL COMMENT 'ID Клиента',
  `ID_Sotrudnika` int(11) NOT NULL COMMENT 'ID Сотрудника',
  `ID_Izdeliya` int(11) NOT NULL COMMENT 'ID Изделия',
  `Razmer` decimal(10,1) NOT NULL DEFAULT 15.0 COMMENT 'Размер изделия',
  `Dlina` decimal(10,1) NOT NULL DEFAULT 15.0 COMMENT 'Длина изделия',
  `Date_N` date NOT NULL COMMENT 'Дата начала изготовления',
  `Date_K` date NOT NULL COMMENT 'Дата завершения изготовления',
  PRIMARY KEY (`ID_Acta`),
  KEY `ID_Clienta` (`ID_Clienta`),
  KEY `ID_Sotrudnika` (`ID_Sotrudnika`),
  KEY `ID_Izdeliya` (`ID_Izdeliya`),
  CONSTRAINT `acts_ibfk_1` FOREIGN KEY (`ID_Clienta`) REFERENCES `clienty` (`ID_Clienta`),
  CONSTRAINT `acts_ibfk_2` FOREIGN KEY (`ID_Sotrudnika`) REFERENCES `sotrudniki` (`ID_Sotrudnika`),
  CONSTRAINT `acts_ibfk_3` FOREIGN KEY (`ID_Izdeliya`) REFERENCES `izdeliya` (`ID_Izdeliya`)
) ENGINE=InnoDB AUTO_INCREMENT=32 DEFAULT CHARSET=utf8 COMMENT='Журнал актов на изготовление';

-- Дамп данных таблицы uvelirka.acts: ~4 rows (приблизительно)
/*!40000 ALTER TABLE `acts` DISABLE KEYS */;
INSERT IGNORE INTO `acts` (`ID_Acta`, `Date`, `ID_Clienta`, `ID_Sotrudnika`, `ID_Izdeliya`, `Razmer`, `Dlina`, `Date_N`, `Date_K`) VALUES
	(28, '2020-03-23', 3, 2, 7, 0.0, 36.0, '2020-03-24', '2020-03-25'),
	(29, '2020-03-23', 5, 5, 11, 0.0, 17.5, '2020-03-23', '2020-03-24'),
	(32, '2020-03-24', 5, 2, 12, 15.0, 0.0, '2020-03-24', '2020-03-25'),
	(33, '2020-03-24', 3, 5, 11, 0.0, 18.0, '2020-03-24', '2020-04-04');
/*!40000 ALTER TABLE `acts` ENABLE KEYS */;

-- Дамп структуры для таблица uvelirka.cheki
CREATE TABLE IF NOT EXISTS `cheki` (
  `ID_Cheka` int(11) NOT NULL AUTO_INCREMENT COMMENT 'ID Чека',
  `ID_Acta` int(11) NOT NULL COMMENT 'ID Акта',
  `Date` date NOT NULL COMMENT 'Дата',
  `Stoimost_modeli` decimal(10,2) NOT NULL COMMENT 'Стоимость восковой или литьевой модели',
  `Stoimost_proby` decimal(10,2) NOT NULL COMMENT 'Стоимость штампирования пробы',
  `Stoimost_raboty` decimal(10,2) NOT NULL COMMENT 'Стоимость работы мастера',
  `Summa` decimal(10,2) NOT NULL DEFAULT 0.00 COMMENT 'Сумма',
  `ID_Skidki` int(11) NOT NULL COMMENT 'ID Скидки',
  `Full_Summa` decimal(10,2) NOT NULL DEFAULT 0.00 COMMENT 'Сумма со скидкой',
  `Garantiya` varchar(50) NOT NULL DEFAULT '3 месяца' COMMENT 'Гарантия',
  PRIMARY KEY (`ID_Cheka`),
  UNIQUE KEY `ID_Cheka` (`ID_Acta`),
  KEY `ID_Skidki` (`ID_Skidki`),
  KEY `ID_Acta` (`ID_Acta`),
  CONSTRAINT `cheki_ibfk_3` FOREIGN KEY (`ID_Skidki`) REFERENCES `skidki` (`ID_Skidki`),
  CONSTRAINT `cheki_ibfk_4` FOREIGN KEY (`ID_Acta`) REFERENCES `acts` (`ID_Acta`)
) ENGINE=InnoDB AUTO_INCREMENT=20 DEFAULT CHARSET=utf8 COMMENT='Журнал чеков';

-- Дамп данных таблицы uvelirka.cheki: ~0 rows (приблизительно)
/*!40000 ALTER TABLE `cheki` DISABLE KEYS */;
/*!40000 ALTER TABLE `cheki` ENABLE KEYS */;

-- Дамп структуры для таблица uvelirka.clienty
CREATE TABLE IF NOT EXISTS `clienty` (
  `ID_Clienta` int(11) NOT NULL AUTO_INCREMENT COMMENT 'ID Клиента',
  `Familiya` varchar(30) NOT NULL COMMENT 'Фамилия',
  `Imya` varchar(30) NOT NULL COMMENT 'Имя',
  `Otchestvo` varchar(30) NOT NULL COMMENT 'Отчетсво',
  `Telephone` varchar(17) NOT NULL COMMENT 'Контактный телефон',
  `Passport` varchar(9) NOT NULL DEFAULT '' COMMENT 'Паспорт',
  PRIMARY KEY (`ID_Clienta`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8 COMMENT='Справочник клиентов';

-- Дамп данных таблицы uvelirka.clienty: ~2 rows (приблизительно)
/*!40000 ALTER TABLE `clienty` DISABLE KEYS */;
INSERT IGNORE INTO `clienty` (`ID_Clienta`, `Familiya`, `Imya`, `Otchestvo`, `Telephone`, `Passport`) VALUES
	(3, 'Шишков', 'Андрей', 'Алексеевич', '+375(44)736-68-56', 'HB8645321'),
	(5, 'Юрченко', 'Анжелика', 'Николаевна', '+375(29)137-54-68', 'HB6451684');
/*!40000 ALTER TABLE `clienty` ENABLE KEYS */;

-- Дамп структуры для таблица uvelirka.izdeliya
CREATE TABLE IF NOT EXISTS `izdeliya` (
  `ID_Izdeliya` int(11) NOT NULL AUTO_INCREMENT COMMENT 'ID Изделия',
  `Name` varchar(30) NOT NULL COMMENT 'Наименования',
  PRIMARY KEY (`ID_Izdeliya`)
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=utf8 COMMENT='Справочник изделий';

-- Дамп данных таблицы uvelirka.izdeliya: ~4 rows (приблизительно)
/*!40000 ALTER TABLE `izdeliya` DISABLE KEYS */;
INSERT IGNORE INTO `izdeliya` (`ID_Izdeliya`, `Name`) VALUES
	(7, 'Цепь'),
	(10, 'Кольцо'),
	(11, 'Браслет'),
	(12, 'Серьги');
/*!40000 ALTER TABLE `izdeliya` ENABLE KEYS */;

-- Дамп структуры для таблица uvelirka.materialy
CREATE TABLE IF NOT EXISTS `materialy` (
  `ID_Materiala` int(11) NOT NULL AUTO_INCREMENT COMMENT 'ID Материала',
  `Name` varchar(30) NOT NULL COMMENT 'Наименование',
  `Ed_Izm` varchar(10) NOT NULL COMMENT 'Единицы измерения',
  `Stoimost` decimal(10,2) NOT NULL COMMENT 'Стоимость',
  PRIMARY KEY (`ID_Materiala`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8 COMMENT='Справочник материалов';

-- Дамп данных таблицы uvelirka.materialy: ~3 rows (приблизительно)
/*!40000 ALTER TABLE `materialy` DISABLE KEYS */;
INSERT IGNORE INTO `materialy` (`ID_Materiala`, `Name`, `Ed_Izm`, `Stoimost`) VALUES
	(3, 'Рубин', '1 карат', 7.40),
	(4, 'Серебро 797 ', '1 грамм', 2.00),
	(5, 'Красное золото 585', '1 грамм', 56.25);
/*!40000 ALTER TABLE `materialy` ENABLE KEYS */;

-- Дамп структуры для таблица uvelirka.skidki
CREATE TABLE IF NOT EXISTS `skidki` (
  `ID_Skidki` int(11) NOT NULL AUTO_INCREMENT COMMENT 'ID Скидки',
  `Name` varchar(30) NOT NULL DEFAULT '' COMMENT 'Наименование',
  `Procent` varchar(4) NOT NULL COMMENT 'Процент',
  PRIMARY KEY (`ID_Skidki`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;

-- Дамп данных таблицы uvelirka.skidki: ~2 rows (приблизительно)
/*!40000 ALTER TABLE `skidki` DISABLE KEYS */;
INSERT IGNORE INTO `skidki` (`ID_Skidki`, `Name`, `Procent`) VALUES
	(1, 'Пенсионная', '15%'),
	(3, 'Студенческая', '5%');
/*!40000 ALTER TABLE `skidki` ENABLE KEYS */;

-- Дамп структуры для таблица uvelirka.sostav_acta
CREATE TABLE IF NOT EXISTS `sostav_acta` (
  `ID_Posicii` int(11) NOT NULL AUTO_INCREMENT COMMENT 'ID Позиции',
  `ID_Acta` int(11) NOT NULL COMMENT 'ID Акта',
  `ID_Materiala` int(11) NOT NULL COMMENT 'ID Материала',
  `Kolichestvo` double NOT NULL COMMENT 'Количество',
  PRIMARY KEY (`ID_Posicii`),
  KEY `ID_Acta` (`ID_Acta`),
  KEY `ID_Materiala` (`ID_Materiala`),
  CONSTRAINT `sostav_acta_ibfk_1` FOREIGN KEY (`ID_Acta`) REFERENCES `acts` (`ID_Acta`),
  CONSTRAINT `sostav_acta_ibfk_2` FOREIGN KEY (`ID_Materiala`) REFERENCES `materialy` (`ID_Materiala`)
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=utf8 COMMENT='Состав акта';

-- Дамп данных таблицы uvelirka.sostav_acta: ~6 rows (приблизительно)
/*!40000 ALTER TABLE `sostav_acta` DISABLE KEYS */;
INSERT IGNORE INTO `sostav_acta` (`ID_Posicii`, `ID_Acta`, `ID_Materiala`, `Kolichestvo`) VALUES
	(11, 28, 4, 7),
	(12, 29, 4, 3.67),
	(13, 29, 3, 1.5),
	(14, 32, 4, 6.01),
	(15, 32, 3, 5.01),
	(16, 33, 4, 2.12);
/*!40000 ALTER TABLE `sostav_acta` ENABLE KEYS */;

-- Дамп структуры для таблица uvelirka.sotrudniki
CREATE TABLE IF NOT EXISTS `sotrudniki` (
  `ID_Sotrudnika` int(11) NOT NULL AUTO_INCREMENT COMMENT 'ID Сотрудника',
  `Familiya` varchar(30) NOT NULL COMMENT 'Фамилия',
  `Imya` varchar(30) NOT NULL COMMENT 'Имя',
  `Otchestvo` varchar(30) NOT NULL COMMENT 'Отчество',
  `Doljnost` varchar(30) NOT NULL COMMENT 'Должность',
  `Telephone` varchar(17) NOT NULL COMMENT 'Контактный телефон',
  PRIMARY KEY (`ID_Sotrudnika`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8 COMMENT='Справочник сотрудников';

-- Дамп данных таблицы uvelirka.sotrudniki: ~2 rows (приблизительно)
/*!40000 ALTER TABLE `sotrudniki` DISABLE KEYS */;
INSERT IGNORE INTO `sotrudniki` (`ID_Sotrudnika`, `Familiya`, `Imya`, `Otchestvo`, `Doljnost`, `Telephone`) VALUES
	(2, 'Синенок', 'Ангелина', 'Олеговна', 'Администратор', '+375(44)575-85-78'),
	(5, 'Лисовская', 'Анастасия', 'Юрьевна', 'Продавец-консультант', '+375(44)313-43-51');
/*!40000 ALTER TABLE `sotrudniki` ENABLE KEYS */;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
