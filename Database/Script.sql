USE [master]
GO

-- Create Database ShopBridgeDB
/****** Object:  Database [ShopBridgeDB] ******/
CREATE DATABASE [ShopBridgeDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ShopBridgeDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\ShopBridgeDB.mdf' , SIZE = 73728KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ShopBridgeDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\ShopBridgeDB_log.ldf' , SIZE = 73728KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO


-- Create Login sb
USE [master]
GO

/****** Object:  Login [sb] ******/
CREATE LOGIN [sb] WITH PASSWORD=N'thinkbridge', DEFAULT_DATABASE=[ShopBridgeDB], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
GO

USE [ShopBridgeDB]
GO
CREATE USER [sb] FOR LOGIN [sb]
GO
USE [ShopBridgeDB]
GO
ALTER ROLE [db_owner] ADD MEMBER [sb]
GO

-- Create Table itemTbl
USE [ShopBridgeDB]
GO

/****** Object:  Table [dbo].[itemTbl] ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[itemTbl](
	[itemID] [int] IDENTITY(1,1) NOT NULL,
	[itemName] [varchar](50) NOT NULL,
	[itemDescription] [varchar](500) NULL,
	[itemPrice] [int] NOT NULL,
	[itemImage] [image] NULL,
 CONSTRAINT [PK_itemTbl] PRIMARY KEY CLUSTERED 
(
	[itemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


