/* this script creates [DataMart] database to hold O2 sample data from Input_Example.csv */

USE [master]
GO
/****** Object:  Database [DataMart]    Script Date: 12/13/2014 10:09:49 PM ******/
CREATE DATABASE [DataMart]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DataMart', FILENAME = N'C:\Program Files (x86)\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA' , SIZE = 685056KB , MAXSIZE = UNLIMITED, FILEGROWTH = 100% )
 LOG ON 
( NAME = N'DataMart_log', FILENAME = N'C:\Program Files (x86)\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA' , SIZE = 1964480KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [DataMart] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DataMart].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DataMart] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DataMart] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DataMart] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DataMart] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DataMart] SET ARITHABORT OFF 
GO
ALTER DATABASE [DataMart] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [DataMart] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DataMart] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DataMart] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DataMart] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DataMart] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DataMart] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DataMart] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DataMart] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DataMart] SET  DISABLE_BROKER 
GO
ALTER DATABASE [DataMart] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DataMart] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DataMart] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DataMart] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DataMart] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DataMart] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [DataMart] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DataMart] SET RECOVERY FULL 
GO
ALTER DATABASE [DataMart] SET  MULTI_USER 
GO
ALTER DATABASE [DataMart] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DataMart] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DataMart] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DataMart] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [DataMart] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'DataMart', N'ON'
GO
USE [DataMart]
GO
/****** Object:  Schema [mgt]    Script Date: 12/13/2014 10:09:49 PM ******/
CREATE SCHEMA [mgt]
GO
/****** Object:  UserDefinedDataType [dbo].[string]    Script Date: 12/13/2014 10:09:49 PM ******/
CREATE TYPE [dbo].[string] FROM [varchar](80) NULL
GO
/****** Object:  Table [dbo].[BackBone]    Script Date: 12/13/2014 10:09:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BackBone](
	[PropertyId] [uniqueidentifier] NOT NULL,
	[PersonId] [uniqueidentifier] NOT NULL,
	[PersonTrustPosition] [int] NOT NULL,
	[MortgageId] [uniqueidentifier] NOT NULL,
	[MortgageTrustPosition] [int] NOT NULL,
	[BackBoneId] [uniqueidentifier] NOT NULL,
	[FeedId] [uniqueidentifier] NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Cities]    Script Date: 12/13/2014 10:09:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Cities](
	[City] [dbo].[string] NOT NULL,
	[County] [dbo].[string] NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Counties]    Script Date: 12/13/2014 10:09:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Counties](
	[County] [dbo].[string] NULL,
	[State] [dbo].[string] NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[HomeValueRanges]    Script Date: 12/13/2014 10:09:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[HomeValueRanges](
	[Label] [dbo].[string] NOT NULL,
	[StartValue] [bigint] NULL,
	[EndValue] [bigint] NULL,
PRIMARY KEY CLUSTERED 
(
	[Label] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[InterestRateTypes]    Script Date: 12/13/2014 10:09:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[InterestRateTypes](
	[InterestRateType] [varchar](50) NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[LoanAmountRanges]    Script Date: 12/13/2014 10:09:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[LoanAmountRanges](
	[Label] [dbo].[string] NOT NULL,
	[StartValue] [bigint] NULL,
	[EndValue] [bigint] NULL,
PRIMARY KEY CLUSTERED 
(
	[Label] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[LoanTypes]    Script Date: 12/13/2014 10:09:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[LoanTypes](
	[LoanType] [varchar](50) NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Mortgages]    Script Date: 12/13/2014 10:09:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Mortgages](
	[MortgageId] [uniqueidentifier] NOT NULL,
	[LenderName] [dbo].[string] NULL,
	[LoanAmount] [bigint] NULL,
	[LoanAmountRange] [dbo].[string] NULL,
	[OriginationDate] [date] NULL,
	[RecordingDate] [date] NULL,
	[MaturityDate] [date] NULL,
	[MortgageTerm] [int] NULL,
	[LoanType] [dbo].[string] NULL,
	[MortgageType] [dbo].[string] NULL,
	[InterestRate] [float] NULL,
	[InterestRateType] [dbo].[string] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MortgateTypes]    Script Date: 12/13/2014 10:09:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MortgateTypes](
	[MortgageType] [dbo].[string] NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Persons]    Script Date: 12/13/2014 10:09:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Persons](
	[PersonId] [uniqueidentifier] NOT NULL,
	[FullName] [dbo].[string] NULL,
	[FirstName] [dbo].[string] NULL,
	[LastName] [dbo].[string] NULL,
	[FullAddress] [dbo].[string] NULL,
	[HouseNumber] [dbo].[string] NULL,
	[StreetDirection] [dbo].[string] NULL,
	[Street] [dbo].[string] NULL,
	[Apartment] [dbo].[string] NULL,
	[City] [dbo].[string] NULL,
	[State] [dbo].[string] NULL,
	[Zip] [dbo].[string] NULL,
	[FullZip] [dbo].[string] NULL,
	[RouteCode] [dbo].[string] NULL,
	[HomeValue] [bigint] NULL,
	[HomeValueRange] [dbo].[string] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Property]    Script Date: 12/13/2014 10:09:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Property](
	[PropertyId] [uniqueidentifier] NOT NULL,
	[FederalIPSCode] [dbo].[string] NULL,
	[AcessorParcelNumber] [dbo].[string] NULL,
	[County] [dbo].[string] NULL,
	[LandUseCode] [dbo].[string] NULL,
	[OwnerOccupant] [bit] NULL,
	[FullAddress] [dbo].[string] NULL,
	[HouseNumber] [dbo].[string] NULL,
	[StreetDirection] [dbo].[string] NULL,
	[Street] [dbo].[string] NULL,
	[Apartment] [dbo].[string] NULL,
	[City] [dbo].[string] NULL,
	[State] [dbo].[string] NULL,
	[Zip] [dbo].[string] NULL,
	[FullZip] [dbo].[string] NULL,
	[CarrierRouteCode] [dbo].[string] NULL,
	[SaleDate] [date] NULL,
	[SaleRecordingDate] [date] NULL,
	[SalePrice] [bigint] NULL,
	[PropertyValue] [bigint] NULL,
	[PropertyValueRange] [dbo].[string] NULL,
	[CombinedLoanToValue] [float] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PropertyValueRanges]    Script Date: 12/13/2014 10:09:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PropertyValueRanges](
	[Label] [dbo].[string] NOT NULL,
	[StartValue] [bigint] NULL,
	[EndValue] [bigint] NULL,
PRIMARY KEY CLUSTERED 
(
	[Label] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[States]    Script Date: 12/13/2014 10:09:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[States](
	[Name] [varchar](50) NOT NULL,
	[Abbreviation] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[StreetDirections]    Script Date: 12/13/2014 10:09:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[StreetDirections](
	[Direction] [dbo].[string] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Direction] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [mgt].[Feeds]    Script Date: 12/13/2014 10:09:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [mgt].[Feeds](
	[FeedId] [uniqueidentifier] NOT NULL,
	[BackBoneId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Feeds] PRIMARY KEY CLUSTERED 
(
	[FeedId] ASC,
	[BackBoneId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [mgt].[Feeds]    Script Date: 12/13/2014 10:09:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [mgt].[FeedDetails](
	[FeedId] [uniqueidentifier] NOT NULL,
	[FeedFile] varchar(max) NOT NULL,
	[Date] DateTime NOT NULL,
 CONSTRAINT [PK_FeedDetails] PRIMARY KEY CLUSTERED 
(
	[FeedId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [mgt].[Queries]    Script Date: 12/13/2014 10:09:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [mgt].[Queries](
	[QueryId] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[Tag] [sysname] NOT NULL,
	[Text] [varchar](max) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[QueryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Index [cci_BackBone]    Script Date: 12/13/2014 10:09:49 PM ******/
CREATE CLUSTERED COLUMNSTORE INDEX [cci_BackBone] ON [dbo].[BackBone] WITH (DROP_EXISTING = OFF) ON [PRIMARY]
GO
/****** Object:  Index [cci_Mortgages]    Script Date: 12/13/2014 10:09:49 PM ******/
CREATE CLUSTERED COLUMNSTORE INDEX [cci_Mortgages] ON [dbo].[Mortgages] WITH (DROP_EXISTING = OFF) ON [PRIMARY]
GO
/****** Object:  Index [cci_Persons]    Script Date: 12/13/2014 10:09:49 PM ******/
CREATE CLUSTERED COLUMNSTORE INDEX [cci_Persons] ON [dbo].[Persons] WITH (DROP_EXISTING = OFF) ON [PRIMARY]
GO
/****** Object:  Index [cci_Property]    Script Date: 12/13/2014 10:09:49 PM ******/
CREATE CLUSTERED COLUMNSTORE INDEX [cci_Property] ON [dbo].[Property] WITH (DROP_EXISTING = OFF) ON [PRIMARY]
GO
USE [master]
GO
ALTER DATABASE [DataMart] SET  READ_WRITE 
GO
