USE [DataMart-Stg]
GO
/****** Object:  Table [dbo].[CosUsers]    Script Date: 2015-11-19 15:03:23 ******/
DROP TABLE [dbo].[CosUsers]
GO
/****** Object:  Table [dbo].[CosUsers]    Script Date: 2015-11-19 15:03:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CosUsers](
	[CosUserId] [uniqueidentifier] NOT NULL,
	[UserName] [nvarchar](max) NULL,
	[Usermail] [nvarchar](max) NOT NULL,
	[UserPassword] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_dbo.CosUsers] PRIMARY KEY CLUSTERED 
(
	[CosUserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
INSERT [dbo].[CosUsers] ([CosUserId], [UserName], [Usermail], [UserPassword]) VALUES (N'5da1ad81-b814-49bb-8b24-12fb89c6463e', N'Test User', N'testcos1', N'2s588k/0kB31nKqs2h696g==')
INSERT [dbo].[CosUsers] ([CosUserId], [UserName], [Usermail], [UserPassword]) VALUES (N'64891e7e-6a2e-467c-9125-18d35abc6594', N'zzhang', N'zzhang', N'l1wgcfy5tZ5VEdayDKzLR9Oh2zDeQ5umu6HepzuzxP4=')
INSERT [dbo].[CosUsers] ([CosUserId], [UserName], [Usermail], [UserPassword]) VALUES (N'61d325c6-6cd8-4e7c-a7e5-1c1534849ba0', N'kdu', N'kdu', N'6Brw9AsTVcWmdwBuS8CBIA==')
INSERT [dbo].[CosUsers] ([CosUserId], [UserName], [Usermail], [UserPassword]) VALUES (N'2e066b0c-230d-4346-9bc2-24ced9ded3db', N'aPietra', N'aPietra', N'cywtM1fn5GnIo1Hfb6ugLOPNNpPvFV5sLSKoOYYYWi8=')
INSERT [dbo].[CosUsers] ([CosUserId], [UserName], [Usermail], [UserPassword]) VALUES (N'8d300bc7-654d-4ed0-940a-31c2c0723a3a', N'Test User', N'testcos2', N'2s588k/0kB31nKqs2h696g==')
INSERT [dbo].[CosUsers] ([CosUserId], [UserName], [Usermail], [UserPassword]) VALUES (N'dd2efd96-5d40-4188-9f59-4d751e17c64a', N'Test User', N'testcos', N'2s588k/0kB31nKqs2h696g==')
INSERT [dbo].[CosUsers] ([CosUserId], [UserName], [Usermail], [UserPassword]) VALUES (N'0991f032-952f-432c-a338-7359ab731378', N'test3', N'testcos3', N'2s588k/0kB31nKqs2h696g==')
INSERT [dbo].[CosUsers] ([CosUserId], [UserName], [Usermail], [UserPassword]) VALUES (N'6101b14d-f565-402a-9f2f-9dd97d51912a', N'Xiaoran Yang', N'xyang@oxyconsulting.com', N'AA7Wo1QwA8aY5Bk/ZrNP5HL7C+FYbfwDCklz/1fUQC4=')
INSERT [dbo].[CosUsers] ([CosUserId], [UserName], [Usermail], [UserPassword]) VALUES (N'7485d4b3-3e9c-40bc-b798-bbb109a2f607', N'kmartinez', N'kmartinez', N'zq1vp+0zZM83y8unELq4/MltHrwDHPHJdhF5L2KWBRY=')
INSERT [dbo].[CosUsers] ([CosUserId], [UserName], [Usermail], [UserPassword]) VALUES (N'd2d30b0f-46e2-4f22-bc37-e696cfab0436', N'kkieffer', N'kkieffer', N'rvc6vbpBgHT4k3CncU0Uf5eBLnWJ4Zd3NQbfu+O4Lj8=')
INSERT [dbo].[CosUsers] ([CosUserId], [UserName], [Usermail], [UserPassword]) VALUES (N'2f2ca2bd-4bd1-4a72-8e4b-e9bc7e21d553', N'Tim Olzer', N'tim@oxyconsulting.com', N'AA7Wo1QwA8aY5Bk/ZrNP5HL7C+FYbfwDCklz/1fUQC4=')
