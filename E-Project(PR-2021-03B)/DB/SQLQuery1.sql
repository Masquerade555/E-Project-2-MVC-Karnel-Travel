USE [master]
GO
/****** Object:  Database [Karnel_Travel_Guide]    Script Date: 6/24/2022 5:58:06 PM ******/
CREATE DATABASE [Karnel_Travel_Guide]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Karnel_Travel_Guide', FILENAME = N'D:\Softwares (Installed)\SQL\Root Directory\MSSQL14.MASQUERADE\MSSQL\DATA\Karnel_Travel_Guide.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Karnel_Travel_Guide_log', FILENAME = N'D:\Softwares (Installed)\SQL\Root Directory\MSSQL14.MASQUERADE\MSSQL\DATA\Karnel_Travel_Guide_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [Karnel_Travel_Guide] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Karnel_Travel_Guide].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Karnel_Travel_Guide] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Karnel_Travel_Guide] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Karnel_Travel_Guide] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Karnel_Travel_Guide] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Karnel_Travel_Guide] SET ARITHABORT OFF 
GO
ALTER DATABASE [Karnel_Travel_Guide] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Karnel_Travel_Guide] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Karnel_Travel_Guide] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Karnel_Travel_Guide] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Karnel_Travel_Guide] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Karnel_Travel_Guide] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Karnel_Travel_Guide] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Karnel_Travel_Guide] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Karnel_Travel_Guide] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Karnel_Travel_Guide] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Karnel_Travel_Guide] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Karnel_Travel_Guide] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Karnel_Travel_Guide] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Karnel_Travel_Guide] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Karnel_Travel_Guide] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Karnel_Travel_Guide] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Karnel_Travel_Guide] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Karnel_Travel_Guide] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Karnel_Travel_Guide] SET  MULTI_USER 
GO
ALTER DATABASE [Karnel_Travel_Guide] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Karnel_Travel_Guide] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Karnel_Travel_Guide] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Karnel_Travel_Guide] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Karnel_Travel_Guide] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Karnel_Travel_Guide] SET QUERY_STORE = OFF
GO
USE [Karnel_Travel_Guide]
GO
/****** Object:  Table [dbo].[tb_admin]    Script Date: 6/24/2022 5:58:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_admin](
	[ad_id] [int] IDENTITY(1,1) NOT NULL,
	[ad_name] [varchar](50) NOT NULL,
	[ad_email] [varchar](50) NOT NULL,
	[ad_password] [varchar](50) NOT NULL,
 CONSTRAINT [PK_tb_admin] PRIMARY KEY CLUSTERED 
(
	[ad_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_category]    Script Date: 6/24/2022 5:58:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_category](
	[c_id] [int] IDENTITY(1,1) NOT NULL,
	[c_name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_tb_category] PRIMARY KEY CLUSTERED 
(
	[c_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_feedback]    Script Date: 6/24/2022 5:58:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_feedback](
	[f_id] [int] IDENTITY(1,1) NOT NULL,
	[f_name] [varchar](max) NULL,
	[f_comment] [varchar](max) NULL,
	[f_user_id_foreign_key] [int] NULL,
	[f_pro_id_foreign_key] [int] NULL,
 CONSTRAINT [PK_tb_feedback] PRIMARY KEY CLUSTERED 
(
	[f_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_hotels]    Script Date: 6/24/2022 5:58:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_hotels](
	[h_id] [int] IDENTITY(200,1) NOT NULL,
	[h_name] [varchar](100) NOT NULL,
	[h_description_1] [varchar](max) NOT NULL,
	[h_images] [varchar](max) NOT NULL,
	[h_price] [int] NOT NULL,
	[h_rating] [int] NULL,
 CONSTRAINT [PK_tb_hotels] PRIMARY KEY CLUSTERED 
(
	[h_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_orders]    Script Date: 6/24/2022 5:58:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_orders](
	[o_id] [int] IDENTITY(1,1) NOT NULL,
	[o_items] [varchar](200) NULL,
	[o_foreign_key] [int] NULL,
	[o_date] [datetime2](7) NULL,
	[o_total] [int] NULL,
	[o_status] [varchar](50) NULL,
	[o_payment] [varchar](50) NULL,
	[o_billing_city] [varchar](50) NULL,
	[o_billing_name] [varchar](50) NULL,
	[o_quantity] [varchar](200) NULL,
 CONSTRAINT [PK_tb_orders] PRIMARY KEY CLUSTERED 
(
	[o_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_packages]    Script Date: 6/24/2022 5:58:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_packages](
	[pac_id] [int] IDENTITY(400,1) NOT NULL,
	[pac_name] [varchar](100) NOT NULL,
	[pac_description_1] [varchar](max) NOT NULL,
	[pac_images] [varchar](max) NOT NULL,
	[pac_price] [int] NOT NULL,
	[pac_discount] [varchar](10) NULL,
 CONSTRAINT [PK_tb_packages] PRIMARY KEY CLUSTERED 
(
	[pac_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_rating]    Script Date: 6/24/2022 5:58:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_rating](
	[r_id] [int] IDENTITY(1,1) NOT NULL,
	[r_comment] [varchar](1000) NULL,
	[r_rating] [int] NULL,
	[r_user_id_foreign_key] [int] NOT NULL,
	[r_pro_id_foreign_key] [int] NOT NULL,
 CONSTRAINT [PK_tb_rating] PRIMARY KEY CLUSTERED 
(
	[r_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_resorts]    Script Date: 6/24/2022 5:58:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_resorts](
	[reso_id] [int] IDENTITY(600,1) NOT NULL,
	[reso_name] [varchar](100) NOT NULL,
	[reso_description_1] [varchar](max) NOT NULL,
	[reso_images] [varchar](max) NOT NULL,
	[reso_price] [int] NOT NULL,
	[reso_rating] [int] NULL,
 CONSTRAINT [PK_tb_resorts] PRIMARY KEY CLUSTERED 
(
	[reso_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_restaurant]    Script Date: 6/24/2022 5:58:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_restaurant](
	[rest_id] [int] IDENTITY(800,1) NOT NULL,
	[rest_name] [varchar](100) NOT NULL,
	[rest_description_1] [varchar](max) NOT NULL,
	[rest_images] [varchar](max) NOT NULL,
	[rest_price] [int] NOT NULL,
	[rest_rating] [int] NULL,
 CONSTRAINT [PK_tb_restaurant] PRIMARY KEY CLUSTERED 
(
	[rest_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_tour]    Script Date: 6/24/2022 5:58:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_tour](
	[to_id] [int] IDENTITY(1000,1) NOT NULL,
	[to_name] [varchar](100) NOT NULL,
	[to_description_1] [varchar](max) NOT NULL,
	[to_images] [varchar](max) NOT NULL,
	[to_price] [int] NOT NULL,
	[to_discount] [varchar](10) NULL,
 CONSTRAINT [PK_tb_tour] PRIMARY KEY CLUSTERED 
(
	[to_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_transportation]    Script Date: 6/24/2022 5:58:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_transportation](
	[tr_id] [int] IDENTITY(1200,1) NOT NULL,
	[tr_name] [varchar](100) NOT NULL,
	[tr_description] [varchar](max) NOT NULL,
	[tr_images] [varchar](max) NOT NULL,
	[tr_discount] [varchar](10) NULL,
	[tr_price] [int] NOT NULL,
 CONSTRAINT [PK_tb_transportation] PRIMARY KEY CLUSTERED 
(
	[tr_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_user]    Script Date: 6/24/2022 5:58:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_user](
	[u_tb] [int] IDENTITY(1,1) NOT NULL,
	[u_name] [varchar](50) NOT NULL,
	[u_email] [varchar](50) NOT NULL,
	[u_password] [varchar](50) NOT NULL,
	[u_contact] [varchar](50) NOT NULL,
	[u_address] [varchar](200) NOT NULL,
	[u_city] [varchar](50) NOT NULL,
 CONSTRAINT [PK_tb_user] PRIMARY KEY CLUSTERED 
(
	[u_tb] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[tb_orders] ADD  CONSTRAINT [DF_tb_orders_o_total]  DEFAULT ((1)) FOR [o_total]
GO
USE [master]
GO
ALTER DATABASE [Karnel_Travel_Guide] SET  READ_WRITE 
GO
