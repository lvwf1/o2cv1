USE [DataMart-Stg]
GO
ALTER TABLE [dbo].[CountsUsers] DROP CONSTRAINT [FK_dbo.CountsUsers_dbo.Counts_CountModel_CountModelID]
GO
/****** Object:  Table [dbo].[CountsUsers]    Script Date: 11/5/2015 1:31:33 AM ******/
DROP TABLE [dbo].[CountsUsers]
GO
/****** Object:  Table [dbo].[CountsTemplates]    Script Date: 11/5/2015 1:31:33 AM ******/
DROP TABLE [dbo].[CountsTemplates]
GO
/****** Object:  Table [dbo].[Counts]    Script Date: 11/5/2015 1:31:33 AM ******/
DROP TABLE [dbo].[Counts]
GO
/****** Object:  Table [dbo].[Counts]    Script Date: 11/5/2015 1:31:33 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Counts](
	[CountModelID] [uniqueidentifier] NOT NULL,
	[Customername] [nvarchar](max) NULL,
	[CustomerTable] [uniqueidentifier] NOT NULL,
	[BaseTemplate] [uniqueidentifier] NOT NULL,
	[AssignTo] [uniqueidentifier] NOT NULL,
	[AccessLevel] [int] NULL,
	[CountOwner] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_dbo.Counts] PRIMARY KEY CLUSTERED 
(
	[CountModelID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CountsTemplates]    Script Date: 11/5/2015 1:31:33 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CountsTemplates](
	[CountTemplateID] [uniqueidentifier] NOT NULL,
	[Templatename] [nvarchar](max) NULL,
	[TableFileID] [uniqueidentifier] NOT NULL,
	[CountUserId] [uniqueidentifier] NOT NULL,
	[accesstype] [int] NULL,
 CONSTRAINT [PK_dbo.CountsTemplates] PRIMARY KEY CLUSTERED 
(
	[CountTemplateID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CountsUsers]    Script Date: 11/5/2015 1:31:33 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CountsUsers](
	[CountUserId] [uniqueidentifier] NOT NULL,
	[UserName] [nvarchar](max) NULL,
	[Usermail] [nvarchar](max) NOT NULL,
	[UserPassword] [nvarchar](max) NOT NULL,
	[CountModel_CountModelID] [uniqueidentifier] NULL,
 CONSTRAINT [PK_dbo.CountsUsers] PRIMARY KEY CLUSTERED 
(
	[CountUserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
INSERT [dbo].[Counts] ([CountModelID], [Customername], [CustomerTable], [BaseTemplate], [AssignTo], [AccessLevel], [CountOwner]) VALUES (N'c6e0bfae-6f4d-46bc-8b68-16682efc8c8a', N'New Test Count 123545', N'2e3270d2-be4e-4042-97c2-bdcc2ef1fb61', N'2e3270d2-be4e-4042-97c2-bdcc2ef1fb61', N'6101b14d-f565-402a-9f2f-9dd97d51912a', 0, N'dd2efd96-5d40-4188-9f59-4d751e17c64a')
INSERT [dbo].[Counts] ([CountModelID], [Customername], [CustomerTable], [BaseTemplate], [AssignTo], [AccessLevel], [CountOwner]) VALUES (N'd4a1a5a0-e94c-4f14-acb5-3bd75fcb7bd1', N'bbr', N'2e3270d2-be4e-4042-97c2-bdcc2ef1fb61', N'2e3270d2-be4e-4042-97c2-bdcc2ef1fb61', N'dd2efd96-5d40-4188-9f59-4d751e17c64a', 0, N'dd2efd96-5d40-4188-9f59-4d751e17c64a')
INSERT [dbo].[Counts] ([CountModelID], [Customername], [CustomerTable], [BaseTemplate], [AssignTo], [AccessLevel], [CountOwner]) VALUES (N'b042bef2-b962-438d-8821-8bd2aade1742', N'New Test Count', N'2e3270d2-be4e-4042-97c2-bdcc2ef1fb61', N'2e3270d2-be4e-4042-97c2-bdcc2ef1fb61', N'dd2efd96-5d40-4188-9f59-4d751e17c64a', 0, N'dd2efd96-5d40-4188-9f59-4d751e17c64a')
INSERT [dbo].[Counts] ([CountModelID], [Customername], [CustomerTable], [BaseTemplate], [AssignTo], [AccessLevel], [CountOwner]) VALUES (N'fc95bf48-2484-402e-bf7b-db630c0606eb', N'New Test Count 1', N'2e3270d2-be4e-4042-97c2-bdcc2ef1fb61', N'2e3270d2-be4e-4042-97c2-bdcc2ef1fb61', N'dd2efd96-5d40-4188-9f59-4d751e17c64a', 0, N'dd2efd96-5d40-4188-9f59-4d751e17c64a')
INSERT [dbo].[CountsTemplates] ([CountTemplateID], [Templatename], [TableFileID], [CountUserId], [accesstype]) VALUES (N'efd6d867-2845-444d-8dea-0d67aa3efe15', N'testO2Template', N'2e3270d2-be4e-4042-97c2-bdcc2ef1fb61', N'dd2efd96-5d40-4188-9f59-4d751e17c64a', 0)
INSERT [dbo].[CountsTemplates] ([CountTemplateID], [Templatename], [TableFileID], [CountUserId], [accesstype]) VALUES (N'aa987ef3-7af8-4b9f-8c62-3e1914a7cbc6', N'templates testing', N'2e3270d2-be4e-4042-97c2-bdcc2ef1fb61', N'dd2efd96-5d40-4188-9f59-4d751e17c64a', 0)
INSERT [dbo].[CountsUsers] ([CountUserId], [UserName], [Usermail], [UserPassword], [CountModel_CountModelID]) VALUES (N'dd2efd96-5d40-4188-9f59-4d751e17c64a', N'Test User', N'test', N'2s588k/0kB31nKqs2h696g==', NULL)
INSERT [dbo].[CountsUsers] ([CountUserId], [UserName], [Usermail], [UserPassword], [CountModel_CountModelID]) VALUES (N'6101b14d-f565-402a-9f2f-9dd97d51912a', N'Xiaoran Yang', N'xyang@oxyconsulting.com', N'AA7Wo1QwA8aY5Bk/ZrNP5HL7C+FYbfwDCklz/1fUQC4=', NULL)
INSERT [dbo].[CountsUsers] ([CountUserId], [UserName], [Usermail], [UserPassword], [CountModel_CountModelID]) VALUES (N'2f2ca2bd-4bd1-4a72-8e4b-e9bc7e21d553', N'Tim Olzer', N'tim@oxyconsulting.com', N'AA7Wo1QwA8aY5Bk/ZrNP5HL7C+FYbfwDCklz/1fUQC4=', NULL)
ALTER TABLE [dbo].[CountsUsers]  WITH CHECK ADD  CONSTRAINT [FK_dbo.CountsUsers_dbo.Counts_CountModel_CountModelID] FOREIGN KEY([CountModel_CountModelID])
REFERENCES [dbo].[Counts] ([CountModelID])
GO
ALTER TABLE [dbo].[CountsUsers] CHECK CONSTRAINT [FK_dbo.CountsUsers_dbo.Counts_CountModel_CountModelID]
GO
