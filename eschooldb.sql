-- phpMyAdmin SQL Dump
-- version 5.1.2
-- https://www.phpmyadmin.net/
--
-- Anamakine: localhost:3306
-- Üretim Zamanı: 14 Kas 2024, 15:39:10
-- Sunucu sürümü: 5.7.24
-- PHP Sürümü: 8.0.1

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Veritabanı: `eschooldb`
--

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı `course_table`
--

CREATE TABLE `course_table` (
  `Id` int(11) NOT NULL,
  `CourseCode` varchar(5) COLLATE utf8_turkish_ci NOT NULL,
  `CourseName` varchar(45) COLLATE utf8_turkish_ci NOT NULL,
  `ClassHour` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci;

--
-- Tablo döküm verisi `course_table`
--

INSERT INTO `course_table` (`Id`, `CourseCode`, `CourseName`, `ClassHour`) VALUES
(1, 'MAT10', 'Math', 5),
(2, 'CHE01', 'Chemistry', 5);

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı `student_note_table`
--

CREATE TABLE `student_note_table` (
  `Id` int(11) NOT NULL,
  `StudentID` int(11) DEFAULT NULL,
  `CourseID` int(11) DEFAULT NULL,
  `Exam1` int(11) DEFAULT NULL,
  `Exam2` int(11) DEFAULT NULL,
  `Performance1` int(11) DEFAULT NULL,
  `Performance2` int(11) DEFAULT NULL,
  `Average` double DEFAULT NULL,
  `Situation` tinyint(1) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci;

--
-- Tablo döküm verisi `student_note_table`
--

INSERT INTO `student_note_table` (`Id`, `StudentID`, `CourseID`, `Exam1`, `Exam2`, `Performance1`, `Performance2`, `Average`, `Situation`) VALUES
(2, 2, 1, 61, 95, 85, 90, 82.75, 1),
(3, 2, 2, 95, 90, 90, 90, 91.25, 1),
(4, 1, 1, 50, 60, 70, 70, 62.5, 1),
(5, 1, 2, 70, 60, 80, 85, 73.75, 1);

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı `student_table`
--

CREATE TABLE `student_table` (
  `Id` int(11) NOT NULL,
  `StudentName` varchar(50) COLLATE utf8_turkish_ci NOT NULL,
  `StudentSurname` varchar(50) COLLATE utf8_turkish_ci NOT NULL,
  `SchoolNumber` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci;

--
-- Tablo döküm verisi `student_table`
--

INSERT INTO `student_table` (`Id`, `StudentName`, `StudentSurname`, `SchoolNumber`) VALUES
(1, 'Ahmet', 'Veli', 57),
(2, 'Fatih', 'Tekke', 61);

--
-- Dökümü yapılmış tablolar için indeksler
--

--
-- Tablo için indeksler `course_table`
--
ALTER TABLE `course_table`
  ADD PRIMARY KEY (`Id`);

--
-- Tablo için indeksler `student_note_table`
--
ALTER TABLE `student_note_table`
  ADD PRIMARY KEY (`Id`);

--
-- Tablo için indeksler `student_table`
--
ALTER TABLE `student_table`
  ADD PRIMARY KEY (`Id`),
  ADD UNIQUE KEY `SchoolNumber` (`SchoolNumber`);

--
-- Dökümü yapılmış tablolar için AUTO_INCREMENT değeri
--

--
-- Tablo için AUTO_INCREMENT değeri `course_table`
--
ALTER TABLE `course_table`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- Tablo için AUTO_INCREMENT değeri `student_note_table`
--
ALTER TABLE `student_note_table`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- Tablo için AUTO_INCREMENT değeri `student_table`
--
ALTER TABLE `student_table`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
