USE [master]
GO
/****** Object:  Database [PlayerInformationSystem]    Script Date: 17/04/2023 23:59:17 ******/
CREATE DATABASE [PlayerInformationSystem]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'PlayerInformationSystem', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\PlayerInformationSystem.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'PlayerInformationSystem_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\PlayerInformationSystem_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [PlayerInformationSystem] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PlayerInformationSystem].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [PlayerInformationSystem] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [PlayerInformationSystem] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [PlayerInformationSystem] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [PlayerInformationSystem] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [PlayerInformationSystem] SET ARITHABORT OFF 
GO
ALTER DATABASE [PlayerInformationSystem] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [PlayerInformationSystem] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [PlayerInformationSystem] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [PlayerInformationSystem] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [PlayerInformationSystem] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [PlayerInformationSystem] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [PlayerInformationSystem] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [PlayerInformationSystem] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [PlayerInformationSystem] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [PlayerInformationSystem] SET  ENABLE_BROKER 
GO
ALTER DATABASE [PlayerInformationSystem] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [PlayerInformationSystem] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [PlayerInformationSystem] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [PlayerInformationSystem] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [PlayerInformationSystem] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [PlayerInformationSystem] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [PlayerInformationSystem] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [PlayerInformationSystem] SET RECOVERY FULL 
GO
ALTER DATABASE [PlayerInformationSystem] SET  MULTI_USER 
GO
ALTER DATABASE [PlayerInformationSystem] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [PlayerInformationSystem] SET DB_CHAINING OFF 
GO
ALTER DATABASE [PlayerInformationSystem] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [PlayerInformationSystem] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [PlayerInformationSystem] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'PlayerInformationSystem', N'ON'
GO
ALTER DATABASE [PlayerInformationSystem] SET QUERY_STORE = OFF
GO
USE [PlayerInformationSystem]
GO
/****** Object:  Table [dbo].[Club]    Script Date: 17/04/2023 23:59:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Club](
	[ClubId] [int] IDENTITY(1,1) NOT NULL,
	[ClubName] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[ClubId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Gender]    Script Date: 17/04/2023 23:59:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Gender](
	[GenderId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
 CONSTRAINT [PK_Gender] PRIMARY KEY CLUSTERED 
(
	[GenderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Player]    Script Date: 17/04/2023 23:59:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Player](
	[PlayerId] [int] IDENTITY(1,1) NOT NULL,
	[PlayerNumber] [varchar](50) NULL,
	[Name] [nvarchar](50) NULL,
	[GenderId] [int] NULL,
	[Age] [int] NULL,
	[PositionId] [int] NULL,
	[HireDate] [datetime] NULL,
	[ExpiredDate] [datetime] NULL,
	[ClubId] [int] NULL,
	[IsActive] [bit] NULL,
	[CreatedTime] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[UpdatedTime] [datetime] NULL,
	[UpdatedBy] [nvarchar](50) NULL,
 CONSTRAINT [PK__Player__4A4E74C83C7E0340] PRIMARY KEY CLUSTERED 
(
	[PlayerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Position]    Script Date: 17/04/2023 23:59:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Position](
	[PositionId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
 CONSTRAINT [PK_Position] PRIMARY KEY CLUSTERED 
(
	[PositionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 17/04/2023 23:59:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[RoleId] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TaskApproval]    Script Date: 17/04/2023 23:59:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TaskApproval](
	[TaskApprovalId] [int] IDENTITY(1,1) NOT NULL,
	[PlayerNumber] [varchar](50) NULL,
	[Owner] [nvarchar](50) NULL,
	[Note] [nvarchar](max) NULL,
	[CreatedTime] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL,
	[UpdatedTime] [datetime] NULL,
	[UpdatedBy] [varchar](50) NULL,
	[Status] [varchar](50) NULL,
	[IsClosed] [bit] NULL,
 CONSTRAINT [PK_TaskApproval] PRIMARY KEY CLUSTERED 
(
	[TaskApprovalId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserRole]    Script Date: 17/04/2023 23:59:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRole](
	[UserRoleId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NULL,
	[RoleId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[UserRoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 17/04/2023 23:59:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](50) NULL,
	[Password] [nvarchar](50) NULL,
	[Email] [nvarchar](max) NULL,
	[PlayerId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Club] ON 

INSERT [dbo].[Club] ([ClubId], [ClubName]) VALUES (1, N'Real Madrid')
INSERT [dbo].[Club] ([ClubId], [ClubName]) VALUES (2, N'Barcelona')
INSERT [dbo].[Club] ([ClubId], [ClubName]) VALUES (3, N'Atletico Madrid')
SET IDENTITY_INSERT [dbo].[Club] OFF
SET IDENTITY_INSERT [dbo].[Gender] ON 

INSERT [dbo].[Gender] ([GenderId], [Name]) VALUES (1, N'Male')
INSERT [dbo].[Gender] ([GenderId], [Name]) VALUES (2, N'Female')
SET IDENTITY_INSERT [dbo].[Gender] OFF
SET IDENTITY_INSERT [dbo].[Player] ON 

INSERT [dbo].[Player] ([PlayerId], [PlayerNumber], [Name], [GenderId], [Age], [PositionId], [HireDate], [ExpiredDate], [ClubId], [IsActive], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (1, N'GK_20231404_0001', N'Thibaut Courtois', 1, 29, 1, CAST(N'2023-04-14T00:00:00.000' AS DateTime), CAST(N'2024-04-14T00:00:00.000' AS DateTime), 1, 1, CAST(N'2023-04-14T16:27:47.627' AS DateTime), N'admin', NULL, NULL)
INSERT [dbo].[Player] ([PlayerId], [PlayerNumber], [Name], [GenderId], [Age], [PositionId], [HireDate], [ExpiredDate], [ClubId], [IsActive], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (2, N'DF_20231404_0001', N'Dani Carvajal', 1, 30, 2, CAST(N'2023-04-14T00:00:00.000' AS DateTime), CAST(N'2023-10-14T00:00:00.000' AS DateTime), 1, 1, CAST(N'2023-04-14T18:15:42.903' AS DateTime), N'admin', NULL, NULL)
INSERT [dbo].[Player] ([PlayerId], [PlayerNumber], [Name], [GenderId], [Age], [PositionId], [HireDate], [ExpiredDate], [ClubId], [IsActive], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (3, N'DF_20231404_0002', N'Eder Militao', 1, 27, 2, CAST(N'2023-04-14T00:00:00.000' AS DateTime), CAST(N'2025-06-30T00:00:00.000' AS DateTime), 1, 1, CAST(N'2023-04-14T18:30:40.827' AS DateTime), N'admin', NULL, NULL)
INSERT [dbo].[Player] ([PlayerId], [PlayerNumber], [Name], [GenderId], [Age], [PositionId], [HireDate], [ExpiredDate], [ClubId], [IsActive], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (4, N'FW_20231504_0001', N'Karim Benzema', 1, 34, 4, CAST(N'2023-04-15T00:00:00.000' AS DateTime), CAST(N'2024-06-30T00:00:00.000' AS DateTime), 1, 1, CAST(N'2023-04-15T22:02:29.337' AS DateTime), N'kbenzema', NULL, NULL)
INSERT [dbo].[Player] ([PlayerId], [PlayerNumber], [Name], [GenderId], [Age], [PositionId], [HireDate], [ExpiredDate], [ClubId], [IsActive], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (6, N'DF_20231704_0001', N'David Alaba', 1, 31, 2, CAST(N'2023-04-14T00:00:00.000' AS DateTime), CAST(N'2026-12-31T00:00:00.000' AS DateTime), 1, 1, CAST(N'2023-04-17T10:11:01.447' AS DateTime), N'alaba04', NULL, NULL)
INSERT [dbo].[Player] ([PlayerId], [PlayerNumber], [Name], [GenderId], [Age], [PositionId], [HireDate], [ExpiredDate], [ClubId], [IsActive], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (7, N'DF_20231704_0002', N'Jesus Vallejo', 1, 25, 2, CAST(N'2023-04-17T00:00:00.000' AS DateTime), CAST(N'2027-06-30T00:00:00.000' AS DateTime), 1, 1, CAST(N'2023-04-17T11:05:12.043' AS DateTime), N'vallejo', NULL, NULL)
INSERT [dbo].[Player] ([PlayerId], [PlayerNumber], [Name], [GenderId], [Age], [PositionId], [HireDate], [ExpiredDate], [ClubId], [IsActive], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (8, N'DF_20231704_0003', N'Nacho Fernandez', 1, 34, 2, CAST(N'2023-04-17T00:00:00.000' AS DateTime), CAST(N'2024-06-30T00:00:00.000' AS DateTime), 1, 1, CAST(N'2023-04-17T16:03:18.437' AS DateTime), N'nacho06', NULL, NULL)
INSERT [dbo].[Player] ([PlayerId], [PlayerNumber], [Name], [GenderId], [Age], [PositionId], [HireDate], [ExpiredDate], [ClubId], [IsActive], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (9, N'GK_20231704_0002', N'Marc Andre Ter Stegen', 1, 32, 1, CAST(N'2023-04-17T00:00:00.000' AS DateTime), CAST(N'2023-05-17T00:00:00.000' AS DateTime), 2, 1, CAST(N'2023-04-17T16:43:25.213' AS DateTime), N'terstegen', NULL, NULL)
INSERT [dbo].[Player] ([PlayerId], [PlayerNumber], [Name], [GenderId], [Age], [PositionId], [HireDate], [ExpiredDate], [ClubId], [IsActive], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (10, N'FW_20231704_0002', N'Eden Hazard', 1, 33, 4, CAST(N'2019-01-01T00:00:00.000' AS DateTime), CAST(N'2023-01-31T00:00:00.000' AS DateTime), 1, 1, CAST(N'2023-04-17T16:56:15.130' AS DateTime), N'hazard07', NULL, NULL)
INSERT [dbo].[Player] ([PlayerId], [PlayerNumber], [Name], [GenderId], [Age], [PositionId], [HireDate], [ExpiredDate], [ClubId], [IsActive], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (11, N'MF_20231704_0001', N'Toni Kroos', 1, 32, 3, CAST(N'2023-04-01T00:00:00.000' AS DateTime), CAST(N'2024-06-30T00:00:00.000' AS DateTime), 1, 0, CAST(N'2023-04-17T18:32:05.700' AS DateTime), N'kroos08', NULL, NULL)
INSERT [dbo].[Player] ([PlayerId], [PlayerNumber], [Name], [GenderId], [Age], [PositionId], [HireDate], [ExpiredDate], [ClubId], [IsActive], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (12, N'MF_20231704_0002', N'Luka Modric', 1, 36, 3, CAST(N'2023-04-01T00:00:00.000' AS DateTime), CAST(N'2024-06-30T00:00:00.000' AS DateTime), 1, 0, CAST(N'2023-04-17T21:05:55.983' AS DateTime), N'modric10', NULL, NULL)
INSERT [dbo].[Player] ([PlayerId], [PlayerNumber], [Name], [GenderId], [Age], [PositionId], [HireDate], [ExpiredDate], [ClubId], [IsActive], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (13, N'FW_20231704_0003', N'Marco Asensio', 1, 28, 4, CAST(N'2023-04-17T00:00:00.000' AS DateTime), CAST(N'2025-06-30T00:00:00.000' AS DateTime), 1, 0, CAST(N'2023-04-17T21:12:00.117' AS DateTime), N'asensio10', NULL, NULL)
INSERT [dbo].[Player] ([PlayerId], [PlayerNumber], [Name], [GenderId], [Age], [PositionId], [HireDate], [ExpiredDate], [ClubId], [IsActive], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (14, N'MF_20231704_0003', N'Eduardo Camavinga', 1, 22, 3, CAST(N'2023-04-17T00:00:00.000' AS DateTime), CAST(N'2027-01-31T00:00:00.000' AS DateTime), 1, 0, CAST(N'2023-04-17T21:25:09.843' AS DateTime), N'camavinga12', NULL, NULL)
INSERT [dbo].[Player] ([PlayerId], [PlayerNumber], [Name], [GenderId], [Age], [PositionId], [HireDate], [ExpiredDate], [ClubId], [IsActive], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (15, N'MF_20231704_0004', N'Federico Valverde', 1, 28, 3, CAST(N'2023-04-17T00:00:00.000' AS DateTime), CAST(N'2025-06-30T00:00:00.000' AS DateTime), 1, 0, CAST(N'2023-04-17T21:44:39.893' AS DateTime), N'valverde15', NULL, NULL)
INSERT [dbo].[Player] ([PlayerId], [PlayerNumber], [Name], [GenderId], [Age], [PositionId], [HireDate], [ExpiredDate], [ClubId], [IsActive], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (16, N'MF_20231704_0005', N'Lucas Vazquez', 1, 31, 3, CAST(N'2023-04-17T00:00:00.000' AS DateTime), CAST(N'2025-01-31T00:00:00.000' AS DateTime), 1, 0, CAST(N'2023-04-17T21:56:18.440' AS DateTime), N'vazquez17', NULL, NULL)
INSERT [dbo].[Player] ([PlayerId], [PlayerNumber], [Name], [GenderId], [Age], [PositionId], [HireDate], [ExpiredDate], [ClubId], [IsActive], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (17, N'MF_20231704_0006', N'Aurelien Tchouameni', 1, 21, 3, CAST(N'2023-04-17T00:00:00.000' AS DateTime), CAST(N'2027-06-30T00:00:00.000' AS DateTime), 1, 0, CAST(N'2023-04-17T22:01:54.260' AS DateTime), N'tchouameni18', NULL, NULL)
INSERT [dbo].[Player] ([PlayerId], [PlayerNumber], [Name], [GenderId], [Age], [PositionId], [HireDate], [ExpiredDate], [ClubId], [IsActive], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (18, N'MF_20231704_0007', N'Dani Ceballos', 1, 26, 3, CAST(N'2023-04-17T00:00:00.000' AS DateTime), CAST(N'2024-01-31T00:00:00.000' AS DateTime), 1, 0, CAST(N'2023-04-17T22:05:03.990' AS DateTime), N'ceballos19', NULL, NULL)
INSERT [dbo].[Player] ([PlayerId], [PlayerNumber], [Name], [GenderId], [Age], [PositionId], [HireDate], [ExpiredDate], [ClubId], [IsActive], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (19, N'FW_20231704_0004', N'Vinicius Junior', 1, 22, 4, CAST(N'2023-04-17T00:00:00.000' AS DateTime), CAST(N'2027-06-30T00:00:00.000' AS DateTime), 1, 0, CAST(N'2023-04-17T22:08:25.560' AS DateTime), N'vinicius20', NULL, NULL)
INSERT [dbo].[Player] ([PlayerId], [PlayerNumber], [Name], [GenderId], [Age], [PositionId], [HireDate], [ExpiredDate], [ClubId], [IsActive], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (20, N'FW_20231704_0005', N'Rodrygo Goes', 1, 23, 4, CAST(N'2023-04-17T00:00:00.000' AS DateTime), CAST(N'2026-01-31T00:00:00.000' AS DateTime), 1, 0, CAST(N'2023-04-17T23:47:32.467' AS DateTime), N'rodrygo21', NULL, NULL)
SET IDENTITY_INSERT [dbo].[Player] OFF
SET IDENTITY_INSERT [dbo].[Position] ON 

INSERT [dbo].[Position] ([PositionId], [Name]) VALUES (1, N'Goalkeeper')
INSERT [dbo].[Position] ([PositionId], [Name]) VALUES (2, N'Defender')
INSERT [dbo].[Position] ([PositionId], [Name]) VALUES (3, N'Midfielder')
INSERT [dbo].[Position] ([PositionId], [Name]) VALUES (4, N'Forward')
SET IDENTITY_INSERT [dbo].[Position] OFF
SET IDENTITY_INSERT [dbo].[Roles] ON 

INSERT [dbo].[Roles] ([RoleId], [RoleName]) VALUES (1, N'Admin')
INSERT [dbo].[Roles] ([RoleId], [RoleName]) VALUES (2, N'Player')
INSERT [dbo].[Roles] ([RoleId], [RoleName]) VALUES (3, N'Committee')
INSERT [dbo].[Roles] ([RoleId], [RoleName]) VALUES (4, N'Releaser')
SET IDENTITY_INSERT [dbo].[Roles] OFF
SET IDENTITY_INSERT [dbo].[TaskApproval] ON 

INSERT [dbo].[TaskApproval] ([TaskApprovalId], [PlayerNumber], [Owner], [Note], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy], [Status], [IsClosed]) VALUES (7, N'DF_20231704_0002', N'Committee', NULL, CAST(N'2023-04-17T11:05:12.270' AS DateTime), N'vallejo', NULL, NULL, N'OnProgress', 0)
INSERT [dbo].[TaskApproval] ([TaskApprovalId], [PlayerNumber], [Owner], [Note], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy], [Status], [IsClosed]) VALUES (8, NULL, N'Releaser', N'Test approved', CAST(N'2023-04-17T15:43:00.673' AS DateTime), N'committee', NULL, NULL, N'Approved', 0)
INSERT [dbo].[TaskApproval] ([TaskApprovalId], [PlayerNumber], [Owner], [Note], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy], [Status], [IsClosed]) VALUES (9, N'DF_20231704_0003', N'Committee', N'ok, approved', CAST(N'2023-04-17T16:03:25.303' AS DateTime), N'nacho06', CAST(N'2023-04-17T16:27:57.940' AS DateTime), N'committee', N'Approved', 1)
INSERT [dbo].[TaskApproval] ([TaskApprovalId], [PlayerNumber], [Owner], [Note], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy], [Status], [IsClosed]) VALUES (10, N'GK_20231704_0002', N'Committee', N'ok', CAST(N'2023-04-17T16:43:25.630' AS DateTime), N'terstegen', CAST(N'2023-04-17T17:03:41.340' AS DateTime), N'committee', N'Approved', 1)
INSERT [dbo].[TaskApproval] ([TaskApprovalId], [PlayerNumber], [Owner], [Note], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy], [Status], [IsClosed]) VALUES (11, N'FW_20231704_0002', N'Committee', N'ok', CAST(N'2023-04-17T16:56:15.147' AS DateTime), N'hazard07', CAST(N'2023-04-17T17:03:32.493' AS DateTime), N'committee', N'Approved', 1)
INSERT [dbo].[TaskApproval] ([TaskApprovalId], [PlayerNumber], [Owner], [Note], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy], [Status], [IsClosed]) VALUES (12, N'MF_20231704_0001', N'Committee', NULL, CAST(N'2023-04-17T18:32:05.967' AS DateTime), N'kroos08', NULL, NULL, N'OnProgress', 0)
INSERT [dbo].[TaskApproval] ([TaskApprovalId], [PlayerNumber], [Owner], [Note], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy], [Status], [IsClosed]) VALUES (13, N'MF_20231704_0001', N'Committee', NULL, CAST(N'2023-04-17T21:05:56.533' AS DateTime), N'modric10', NULL, NULL, N'OnProgress', 0)
INSERT [dbo].[TaskApproval] ([TaskApprovalId], [PlayerNumber], [Owner], [Note], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy], [Status], [IsClosed]) VALUES (14, N'FW_20231704_0003', N'Committee', NULL, CAST(N'2023-04-17T21:12:00.697' AS DateTime), N'asensio10', NULL, NULL, N'OnProgress', 0)
INSERT [dbo].[TaskApproval] ([TaskApprovalId], [PlayerNumber], [Owner], [Note], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy], [Status], [IsClosed]) VALUES (15, N'MF_20231704_0001', N'Committee', NULL, CAST(N'2023-04-17T21:25:10.463' AS DateTime), N'camavinga12', NULL, NULL, N'OnProgress', 0)
INSERT [dbo].[TaskApproval] ([TaskApprovalId], [PlayerNumber], [Owner], [Note], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy], [Status], [IsClosed]) VALUES (16, N'MF_20231704_0001', N'Committee', NULL, CAST(N'2023-04-17T21:44:40.477' AS DateTime), N'valverde15', NULL, NULL, N'OnProgress', 0)
INSERT [dbo].[TaskApproval] ([TaskApprovalId], [PlayerNumber], [Owner], [Note], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy], [Status], [IsClosed]) VALUES (17, N'MF_20231704_0001', N'Committee', NULL, CAST(N'2023-04-17T21:56:19.003' AS DateTime), N'vazquez17', NULL, NULL, N'OnProgress', 0)
INSERT [dbo].[TaskApproval] ([TaskApprovalId], [PlayerNumber], [Owner], [Note], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy], [Status], [IsClosed]) VALUES (18, N'MF_20231704_0001', N'Committee', NULL, CAST(N'2023-04-17T22:01:54.833' AS DateTime), N'tchouameni18', NULL, NULL, N'OnProgress', 0)
INSERT [dbo].[TaskApproval] ([TaskApprovalId], [PlayerNumber], [Owner], [Note], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy], [Status], [IsClosed]) VALUES (19, N'MF_20231704_0001', N'Committee', NULL, CAST(N'2023-04-17T22:05:04.237' AS DateTime), N'ceballos19', NULL, NULL, N'OnProgress', 0)
INSERT [dbo].[TaskApproval] ([TaskApprovalId], [PlayerNumber], [Owner], [Note], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy], [Status], [IsClosed]) VALUES (20, N'FW_20231704_0004', N'Committee', NULL, CAST(N'2023-04-17T22:08:25.777' AS DateTime), N'vinicius20', NULL, NULL, N'OnProgress', 0)
INSERT [dbo].[TaskApproval] ([TaskApprovalId], [PlayerNumber], [Owner], [Note], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy], [Status], [IsClosed]) VALUES (21, N'FW_20231704_0005', N'Committee', NULL, CAST(N'2023-04-17T23:47:40.003' AS DateTime), N'rodrygo21', NULL, NULL, N'OnProgress', 0)
SET IDENTITY_INSERT [dbo].[TaskApproval] OFF
SET IDENTITY_INSERT [dbo].[UserRole] ON 

INSERT [dbo].[UserRole] ([UserRoleId], [UserId], [RoleId]) VALUES (1, 1, 1)
INSERT [dbo].[UserRole] ([UserRoleId], [UserId], [RoleId]) VALUES (2, 2, 3)
INSERT [dbo].[UserRole] ([UserRoleId], [UserId], [RoleId]) VALUES (3, 3, 1)
INSERT [dbo].[UserRole] ([UserRoleId], [UserId], [RoleId]) VALUES (4, 4, 1)
INSERT [dbo].[UserRole] ([UserRoleId], [UserId], [RoleId]) VALUES (5, 5, 2)
INSERT [dbo].[UserRole] ([UserRoleId], [UserId], [RoleId]) VALUES (6, 6, 2)
INSERT [dbo].[UserRole] ([UserRoleId], [UserId], [RoleId]) VALUES (7, 7, 2)
INSERT [dbo].[UserRole] ([UserRoleId], [UserId], [RoleId]) VALUES (8, 8, 2)
INSERT [dbo].[UserRole] ([UserRoleId], [UserId], [RoleId]) VALUES (9, 9, 2)
INSERT [dbo].[UserRole] ([UserRoleId], [UserId], [RoleId]) VALUES (10, 10, 3)
INSERT [dbo].[UserRole] ([UserRoleId], [UserId], [RoleId]) VALUES (11, 11, 2)
INSERT [dbo].[UserRole] ([UserRoleId], [UserId], [RoleId]) VALUES (12, 12, 2)
INSERT [dbo].[UserRole] ([UserRoleId], [UserId], [RoleId]) VALUES (13, 13, 2)
INSERT [dbo].[UserRole] ([UserRoleId], [UserId], [RoleId]) VALUES (14, 14, 2)
INSERT [dbo].[UserRole] ([UserRoleId], [UserId], [RoleId]) VALUES (15, 15, 2)
INSERT [dbo].[UserRole] ([UserRoleId], [UserId], [RoleId]) VALUES (16, 16, 2)
INSERT [dbo].[UserRole] ([UserRoleId], [UserId], [RoleId]) VALUES (17, 17, 2)
INSERT [dbo].[UserRole] ([UserRoleId], [UserId], [RoleId]) VALUES (18, 18, 2)
INSERT [dbo].[UserRole] ([UserRoleId], [UserId], [RoleId]) VALUES (19, 19, 2)
INSERT [dbo].[UserRole] ([UserRoleId], [UserId], [RoleId]) VALUES (20, 20, 2)
INSERT [dbo].[UserRole] ([UserRoleId], [UserId], [RoleId]) VALUES (21, 21, 2)
INSERT [dbo].[UserRole] ([UserRoleId], [UserId], [RoleId]) VALUES (22, 22, 2)
INSERT [dbo].[UserRole] ([UserRoleId], [UserId], [RoleId]) VALUES (23, 23, 2)
SET IDENTITY_INSERT [dbo].[UserRole] OFF
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([UserId], [Username], [Password], [Email], [PlayerId]) VALUES (1, N'admin', N'admin', N'admin@gmail.com', NULL)
INSERT [dbo].[Users] ([UserId], [Username], [Password], [Email], [PlayerId]) VALUES (2, N'committee', N'committee', NULL, NULL)
INSERT [dbo].[Users] ([UserId], [Username], [Password], [Email], [PlayerId]) VALUES (3, N'admin2', N'admin2', N'admin2@gmail.com', NULL)
INSERT [dbo].[Users] ([UserId], [Username], [Password], [Email], [PlayerId]) VALUES (4, N'admin3', N'admin3', N'admin3@gmail.com', NULL)
INSERT [dbo].[Users] ([UserId], [Username], [Password], [Email], [PlayerId]) VALUES (5, N'Karim Benzema', N'benz', N'benzema@gmail.com', NULL)
INSERT [dbo].[Users] ([UserId], [Username], [Password], [Email], [PlayerId]) VALUES (6, N'kbenzema', N'benema', N'benzema@gmail.com', 4)
INSERT [dbo].[Users] ([UserId], [Username], [Password], [Email], [PlayerId]) VALUES (7, N'kbenzema', N'benzema', N'benzema@gmail.com', 5)
INSERT [dbo].[Users] ([UserId], [Username], [Password], [Email], [PlayerId]) VALUES (8, N'alaba04', N'alaba', N'alaba@gmail.com', 6)
INSERT [dbo].[Users] ([UserId], [Username], [Password], [Email], [PlayerId]) VALUES (9, N'vallejo', N'vallejo', N'vallejo@gmail.com', 7)
INSERT [dbo].[Users] ([UserId], [Username], [Password], [Email], [PlayerId]) VALUES (10, N'committee2', N'committee2', N'committee2@gmail.com', NULL)
INSERT [dbo].[Users] ([UserId], [Username], [Password], [Email], [PlayerId]) VALUES (11, N'nacho06', N'nacho06', N'nacho@gmail.com', 8)
INSERT [dbo].[Users] ([UserId], [Username], [Password], [Email], [PlayerId]) VALUES (12, N'terstegen', N'terstegen', N'terstegen@gmail.com', 9)
INSERT [dbo].[Users] ([UserId], [Username], [Password], [Email], [PlayerId]) VALUES (13, N'hazard07', N'hazard07', N'hazard@gmail.com', 10)
INSERT [dbo].[Users] ([UserId], [Username], [Password], [Email], [PlayerId]) VALUES (14, N'kroos08', N'kroos08', N'kroos@gmail.com', 11)
INSERT [dbo].[Users] ([UserId], [Username], [Password], [Email], [PlayerId]) VALUES (15, N'modric10', N'modric10', N'ekojumaddin@gmail.com', 12)
INSERT [dbo].[Users] ([UserId], [Username], [Password], [Email], [PlayerId]) VALUES (16, N'asensio10', N'asensio10', N'ekojumaddin@gmail.com', 13)
INSERT [dbo].[Users] ([UserId], [Username], [Password], [Email], [PlayerId]) VALUES (17, N'camavinga12', N'camavinga12', N'ekojumaddin@gmail.com', 14)
INSERT [dbo].[Users] ([UserId], [Username], [Password], [Email], [PlayerId]) VALUES (18, N'valverde15', N'valverde15', N'ekojumaddin@gmail.com', 15)
INSERT [dbo].[Users] ([UserId], [Username], [Password], [Email], [PlayerId]) VALUES (19, N'vazquez17', N'vazquez17', N'ekojumaddin@gmail.com', 16)
INSERT [dbo].[Users] ([UserId], [Username], [Password], [Email], [PlayerId]) VALUES (20, N'tchouameni18', N'tchouameni18', N'ekojumaddin@gmail.com', 17)
INSERT [dbo].[Users] ([UserId], [Username], [Password], [Email], [PlayerId]) VALUES (21, N'ceballos19', N'ceballos19', N'ekojumaddin@gmail.com', 18)
INSERT [dbo].[Users] ([UserId], [Username], [Password], [Email], [PlayerId]) VALUES (22, N'vinicius20', N'vinicius20', N'ekojumaddin@gmail.com', 19)
INSERT [dbo].[Users] ([UserId], [Username], [Password], [Email], [PlayerId]) VALUES (23, N'rodrygo21', N'rodrygo21', N'ekojumaddin@gmail.com', 20)
SET IDENTITY_INSERT [dbo].[Users] OFF
ALTER TABLE [dbo].[Player]  WITH CHECK ADD  CONSTRAINT [FK__Player__ClubId__403A8C7D] FOREIGN KEY([ClubId])
REFERENCES [dbo].[Club] ([ClubId])
GO
ALTER TABLE [dbo].[Player] CHECK CONSTRAINT [FK__Player__ClubId__403A8C7D]
GO
ALTER TABLE [dbo].[Player]  WITH CHECK ADD  CONSTRAINT [FK__Player__GenderId__4BAC3F29] FOREIGN KEY([GenderId])
REFERENCES [dbo].[Gender] ([GenderId])
GO
ALTER TABLE [dbo].[Player] CHECK CONSTRAINT [FK__Player__GenderId__4BAC3F29]
GO
ALTER TABLE [dbo].[Player]  WITH CHECK ADD  CONSTRAINT [FK__Player__Position__4CA06362] FOREIGN KEY([PositionId])
REFERENCES [dbo].[Position] ([PositionId])
GO
ALTER TABLE [dbo].[Player] CHECK CONSTRAINT [FK__Player__Position__4CA06362]
GO
ALTER TABLE [dbo].[UserRole]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([RoleId])
GO
ALTER TABLE [dbo].[UserRole]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
USE [master]
GO
ALTER DATABASE [PlayerInformationSystem] SET  READ_WRITE 
GO
