CREATE DATABASE  IF NOT EXISTS `inventarioproyectogsi` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `inventarioproyectogsi`;
-- MySQL dump 10.13  Distrib 8.0.34, for Win64 (x86_64)
--
-- Host: localhost    Database: inventarioproyectogsi
-- ------------------------------------------------------
-- Server version	8.0.34

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `categorias`
--

DROP TABLE IF EXISTS `categorias`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `categorias` (
  `CategoriaID` int NOT NULL AUTO_INCREMENT,
  `Nombre` varchar(255) NOT NULL,
  `Descripcion` text,
  PRIMARY KEY (`CategoriaID`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `categorias`
--

LOCK TABLES `categorias` WRITE;
/*!40000 ALTER TABLE `categorias` DISABLE KEYS */;
INSERT INTO `categorias` VALUES (4,'Smartphones','Teléfonos inteligentes de última generación'),(5,'Tabletas','Dispositivos móviles con pantalla táctil de diferentes tamaños'),(6,'Accesorios','Complementos y accesorios para dispositivos electrónicos'),(7,'Monitores','Dispositivos de visualización para computadoras y otros dispositivos.'),(8,'Proyectores','Dispositivos para proyectar imágenes en una superficie grande.'),(9,'Audio','Dispositivos de salida de sonido como altavoces y auriculares.');
/*!40000 ALTER TABLE `categorias` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `productos`
--

DROP TABLE IF EXISTS `productos`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `productos` (
  `ProductoID` int NOT NULL AUTO_INCREMENT,
  `Nombre` varchar(255) NOT NULL,
  `Descripcion` text,
  `Precio` decimal(10,2) NOT NULL,
  `CategoriaID` int DEFAULT NULL,
  PRIMARY KEY (`ProductoID`),
  KEY `CategoriaID` (`CategoriaID`),
  CONSTRAINT `productos_ibfk_1` FOREIGN KEY (`CategoriaID`) REFERENCES `categorias` (`CategoriaID`)
) ENGINE=InnoDB AUTO_INCREMENT=36 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `productos`
--

LOCK TABLES `productos` WRITE;
/*!40000 ALTER TABLE `productos` DISABLE KEYS */;
INSERT INTO `productos` VALUES (4,'Galaxy S21','Smartphone con pantalla de 6.2 pulgadas y cámara de alta resolución.',799.99,4),(5,'Galaxy Tab S7','Tableta con pantalla de 11 pulgadas y batería de larga duración.',649.99,5),(6,'Cargador Inalámbrico','Cargador inalámbrico rápido con tecnología Qi.',59.99,6),(7,'Galaxy Buds Live','Auriculares inalámbricos con cancelación de ruido.',169.99,6),(8,'Galaxy Note 20','Smartphone con S-Pen para productividad y diseño.',999.99,4),(9,'Galaxy Z Fold 2','Smartphone plegable con pantalla flexible y tecnología 5G.',1999.99,4),(10,'Galaxy A52','Smartphone de gama media con cámara cuádruple.',349.99,4),(11,'Galaxy A72','Smartphone con pantalla Super AMOLED de 6.7 pulgadas.',449.99,4),(12,'Galaxy Watch Active 2','Reloj inteligente con seguimiento de actividad y salud.',249.99,6),(13,'Galaxy Fit2','Pulsera de actividad con monitorización de ejercicios y sueño.',59.99,6),(14,'Galaxy Book S','Portátil ultraligero con conectividad LTE y batería de larga duración.',999.99,5),(15,'Galaxy Tab Active 3','Tableta robusta con S Pen y diseño duradero.',489.99,5),(16,'Monitor Curvo Gaming Odyssey','Monitor curvo de 49\" con experiencia de juego inmersiva.',1499.99,6),(17,'Galaxy Buds Pro','Auriculares inalámbricos con sonido envolvente y modo ambiente.',199.99,6),(18,'Teclado Book Cover','Teclado para tabletas Galaxy Tab con trackpad integrado.',129.99,6),(19,'SmartTag','Localizador de objetos con tecnología Bluetooth para encontrar tus cosas fácilmente.',29.99,6),(20,'Monitor Gaming 144Hz','Monitor de 27 pulgadas con tasa de refresco de 144Hz para gaming.',279.99,7),(21,'Proyector 4K HDR','Proyector con resolución 4K y soporte HDR para cine en casa.',899.99,8),(22,'Barra de Sonido Wi-Fi','Barra de sonido con conectividad Wi-Fi y Bluetooth para un sonido envolvente.',199.99,9),(23,'Auriculares Noise Cancelling','Auriculares con cancelación activa de ruido para una escucha inmersiva.',299.99,9),(24,'Monitor Ultrawide 34\"','Monitor ultrawide de 34 pulgadas con QHD y HDR.',499.99,7),(25,'Monitor 4K 32\"','Monitor de 32 pulgadas con resolución 4K UHD y soporte para FreeSync.',699.99,7),(26,'Monitor Gamer Curvo 27\"','Monitor gamer curvo de 27 pulgadas con tasa de refresco de 240Hz.',299.99,7),(27,'Monitor Profesional 27\"','Monitor profesional de 27 pulgadas con calibración de color y USB-C.',599.99,7),(28,'Monitor Táctil 22\"','Monitor táctil de 22 pulgadas optimizado para puntos de venta y kioscos interactivos.',259.99,7);
/*!40000 ALTER TABLE `productos` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `stock`
--

DROP TABLE IF EXISTS `stock`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `stock` (
  `StockID` int NOT NULL AUTO_INCREMENT,
  `ProductoID` int DEFAULT NULL,
  `Cantidad` int NOT NULL,
  PRIMARY KEY (`StockID`),
  KEY `ProductoID` (`ProductoID`),
  CONSTRAINT `stock_ibfk_1` FOREIGN KEY (`ProductoID`) REFERENCES `productos` (`ProductoID`)
) ENGINE=InnoDB AUTO_INCREMENT=30 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `stock`
--

LOCK TABLES `stock` WRITE;
/*!40000 ALTER TABLE `stock` DISABLE KEYS */;
INSERT INTO `stock` VALUES (4,4,15),(5,5,50),(6,6,200),(7,7,150),(8,8,75),(9,9,5),(10,10,100),(11,11,80),(12,12,150),(13,13,200),(14,14,50),(15,15,70),(16,16,30),(17,17,120),(18,18,250),(19,19,500),(20,20,60),(21,21,30),(22,22,100),(23,23,150),(24,24,40),(25,25,25),(26,26,50),(27,27,30),(28,28,40);
/*!40000 ALTER TABLE `stock` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `transacciones`
--

DROP TABLE IF EXISTS `transacciones`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `transacciones` (
  `TransaccionID` int NOT NULL AUTO_INCREMENT,
  `ProductoID` int DEFAULT NULL,
  `Tipo` varchar(100) DEFAULT NULL,
  `Cantidad` int DEFAULT NULL,
  `Fecha` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`TransaccionID`),
  KEY `ProductoID` (`ProductoID`),
  CONSTRAINT `transacciones_ibfk_1` FOREIGN KEY (`ProductoID`) REFERENCES `productos` (`ProductoID`)
) ENGINE=InnoDB AUTO_INCREMENT=39 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `transacciones`
--

LOCK TABLES `transacciones` WRITE;
/*!40000 ALTER TABLE `transacciones` DISABLE KEYS */;
INSERT INTO `transacciones` VALUES (4,4,'entrada',100,'2023-11-21 01:24:48'),(5,5,'salida',2,'2023-11-21 01:24:48'),(6,6,'entrada',200,'2023-11-21 01:24:48'),(7,7,'salida',10,'2023-11-21 01:24:48'),(8,8,'entrada',75,'2023-11-21 01:24:48'),(9,9,'salida',1,'2023-11-21 01:24:48'),(10,10,'entrada',100,'2023-11-21 01:59:31'),(11,11,'entrada',80,'2023-11-21 01:59:31'),(12,11,'entrada',150,'2023-11-21 01:59:31'),(13,11,'entrada',200,'2023-11-21 01:59:31'),(14,11,'entrada',50,'2023-11-21 01:59:31'),(15,11,'entrada',70,'2023-11-21 01:59:31'),(16,11,'entrada',30,'2023-11-21 01:59:31'),(17,11,'entrada',120,'2023-11-21 01:59:31'),(18,11,'entrada',250,'2023-11-21 01:59:31'),(19,11,'entrada',500,'2023-11-21 01:59:31'),(20,10,'entrada',100,'2023-11-21 02:06:51'),(21,11,'entrada',80,'2023-11-21 02:06:51'),(22,12,'entrada',150,'2023-11-21 02:06:51'),(23,13,'entrada',200,'2023-11-21 02:06:51'),(24,14,'entrada',50,'2023-11-21 02:06:51'),(25,15,'entrada',70,'2023-11-21 02:06:51'),(26,16,'entrada',30,'2023-11-21 02:06:51'),(27,17,'entrada',120,'2023-11-21 02:06:51'),(28,18,'entrada',250,'2023-11-21 02:06:51'),(29,19,'entrada',500,'2023-11-21 02:06:51'),(30,20,'entrada',60,'2023-11-21 02:17:51'),(31,21,'entrada',30,'2023-11-21 02:17:51'),(32,22,'salida',10,'2023-11-21 02:17:51'),(33,23,'entrada',150,'2023-11-21 02:17:51'),(34,24,'entrada',40,'2023-11-21 02:18:36'),(35,25,'entrada',25,'2023-11-21 02:18:36'),(36,26,'entrada',40,'2023-11-21 02:18:36'),(37,27,'entrada',25,'2023-11-21 02:18:36'),(38,28,'entrada',40,'2023-11-21 02:18:36');
/*!40000 ALTER TABLE `transacciones` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping events for database 'inventarioproyectogsi'
--

--
-- Dumping routines for database 'inventarioproyectogsi'
--
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2023-11-25 21:20:02
