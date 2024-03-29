USE [master]
GO
/****** Object:  Database [RestfulBankDbo]    Script Date: 10.01.2024 05:04:53 ******/
CREATE DATABASE [RestfulBankDbo]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'RestfulBankDbo', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\RestfulBankDbo.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'RestfulBankDbo_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\RestfulBankDbo_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [RestfulBankDbo] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [RestfulBankDbo].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [RestfulBankDbo] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [RestfulBankDbo] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [RestfulBankDbo] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [RestfulBankDbo] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [RestfulBankDbo] SET ARITHABORT OFF 
GO
ALTER DATABASE [RestfulBankDbo] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [RestfulBankDbo] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [RestfulBankDbo] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [RestfulBankDbo] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [RestfulBankDbo] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [RestfulBankDbo] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [RestfulBankDbo] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [RestfulBankDbo] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [RestfulBankDbo] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [RestfulBankDbo] SET  ENABLE_BROKER 
GO
ALTER DATABASE [RestfulBankDbo] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [RestfulBankDbo] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [RestfulBankDbo] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [RestfulBankDbo] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [RestfulBankDbo] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [RestfulBankDbo] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [RestfulBankDbo] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [RestfulBankDbo] SET RECOVERY FULL 
GO
ALTER DATABASE [RestfulBankDbo] SET  MULTI_USER 
GO
ALTER DATABASE [RestfulBankDbo] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [RestfulBankDbo] SET DB_CHAINING OFF 
GO
ALTER DATABASE [RestfulBankDbo] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [RestfulBankDbo] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [RestfulBankDbo] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [RestfulBankDbo] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'RestfulBankDbo', N'ON'
GO
ALTER DATABASE [RestfulBankDbo] SET QUERY_STORE = ON
GO
ALTER DATABASE [RestfulBankDbo] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [RestfulBankDbo]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 10.01.2024 05:04:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Accounts]    Script Date: 10.01.2024 05:04:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Accounts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[IBAN] [nvarchar](max) NOT NULL,
	[Balance] [decimal](18, 2) NOT NULL,
	[CreationDate] [datetime2](7) NOT NULL,
	[Active] [bit] NOT NULL,
	[AccountType] [nvarchar](max) NULL,
	[DailyWithdrawalLimit] [decimal](18, 2) NULL,
	[DailyTransferLimit] [decimal](18, 2) NULL,
	[AutomaticPayments] [bit] NOT NULL,
	[InsuranceCoverage] [decimal](18, 2) NULL,
	[SpecialNotes] [nvarchar](max) NULL,
 CONSTRAINT [PK_Accounts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AccountTransactions]    Script Date: 10.01.2024 05:04:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AccountTransactions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AccountId] [int] NOT NULL,
	[Amount] [decimal](18, 2) NOT NULL,
	[TransactionType] [nvarchar](max) NOT NULL,
	[TransactionDate] [datetime2](7) NOT NULL,
	[Status] [nvarchar](50) NULL,
 CONSTRAINT [PK_AccountTransactions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ApplicationUserLogs]    Script Date: 10.01.2024 05:04:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ApplicationUserLogs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[LogMessage] [nvarchar](max) NOT NULL,
	[Date] [datetime2](7) NOT NULL,
	[Controller] [nvarchar](max) NOT NULL,
	[Action] [nvarchar](max) NOT NULL,
	[UserIp] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_ApplicationUserLogs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Credits]    Script Date: 10.01.2024 05:04:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Credits](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[LoanId] [int] NOT NULL,
	[Active] [bit] NOT NULL,
	[IBAN] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Credits] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Loans]    Script Date: 10.01.2024 05:04:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Loans](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OfferCode] [nvarchar](max) NULL,
	[LoanAmount] [decimal](18, 2) NULL,
	[InterestRate] [decimal](18, 2) NULL,
	[LoanTermMonths] [int] NULL,
	[Active] [bit] NULL,
 CONSTRAINT [PK_Loans] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SupportMessages]    Script Date: 10.01.2024 05:04:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SupportMessages](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[Message] [nvarchar](max) NOT NULL,
	[MessageReceivedAt] [datetime2](7) NOT NULL,
	[Category] [nvarchar](max) NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_SupportMessages] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TransferTransactions]    Script Date: 10.01.2024 05:04:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TransferTransactions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SourceAccountId] [int] NOT NULL,
	[TargetAccountId] [int] NOT NULL,
	[Amount] [decimal](18, 2) NOT NULL,
	[TransactionType] [nvarchar](max) NOT NULL,
	[TransactionDate] [datetime2](7) NOT NULL,
	[SourceIBAN] [nvarchar](max) NOT NULL,
	[TargetIBAN] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_TransferTransactions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 10.01.2024 05:04:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[PasswordHash] [nvarchar](max) NOT NULL,
	[RegistrationDate] [datetime2](7) NOT NULL,
	[Role] [nvarchar](max) NOT NULL,
	[isActive] [bit] NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Accounts] ON 

INSERT [dbo].[Accounts] ([Id], [UserId], [IBAN], [Balance], [CreationDate], [Active], [AccountType], [DailyWithdrawalLimit], [DailyTransferLimit], [AutomaticPayments], [InsuranceCoverage], [SpecialNotes]) VALUES (1, 1, N'TR481846516471270938', CAST(96766.00 AS Decimal(18, 2)), CAST(N'2024-01-07T22:47:07.6194061' AS DateTime2), 1, N'Savings', CAST(190000.00 AS Decimal(18, 2)), CAST(50000.00 AS Decimal(18, 2)), 1, CAST(100000.00 AS Decimal(18, 2)), NULL)
INSERT [dbo].[Accounts] ([Id], [UserId], [IBAN], [Balance], [CreationDate], [Active], [AccountType], [DailyWithdrawalLimit], [DailyTransferLimit], [AutomaticPayments], [InsuranceCoverage], [SpecialNotes]) VALUES (2, 1, N'TR230891141217319427', CAST(461000.00 AS Decimal(18, 2)), CAST(N'2024-01-07T22:48:05.2778908' AS DateTime2), 1, N'Savings', CAST(0.00 AS Decimal(18, 2)), CAST(50000.00 AS Decimal(18, 2)), 1, CAST(100000.00 AS Decimal(18, 2)), NULL)
INSERT [dbo].[Accounts] ([Id], [UserId], [IBAN], [Balance], [CreationDate], [Active], [AccountType], [DailyWithdrawalLimit], [DailyTransferLimit], [AutomaticPayments], [InsuranceCoverage], [SpecialNotes]) VALUES (3, 4, N'TR973040117206439843', CAST(104000.00 AS Decimal(18, 2)), CAST(N'2024-01-07T23:36:41.3511638' AS DateTime2), 0, N'Savings', CAST(20000.00 AS Decimal(18, 2)), CAST(50000.00 AS Decimal(18, 2)), 1, CAST(100000.00 AS Decimal(18, 2)), NULL)
INSERT [dbo].[Accounts] ([Id], [UserId], [IBAN], [Balance], [CreationDate], [Active], [AccountType], [DailyWithdrawalLimit], [DailyTransferLimit], [AutomaticPayments], [InsuranceCoverage], [SpecialNotes]) VALUES (4, 4, N'TR895739095067157991', CAST(98000.00 AS Decimal(18, 2)), CAST(N'2024-01-07T23:38:22.4553676' AS DateTime2), 0, N'Savings', CAST(20000.00 AS Decimal(18, 2)), CAST(50000.00 AS Decimal(18, 2)), 1, CAST(100000.00 AS Decimal(18, 2)), NULL)
INSERT [dbo].[Accounts] ([Id], [UserId], [IBAN], [Balance], [CreationDate], [Active], [AccountType], [DailyWithdrawalLimit], [DailyTransferLimit], [AutomaticPayments], [InsuranceCoverage], [SpecialNotes]) VALUES (5, 6, N'TR085895820074454270', CAST(81234.00 AS Decimal(18, 2)), CAST(N'2024-01-09T21:22:12.9503022' AS DateTime2), 0, N'Savings', CAST(20000.00 AS Decimal(18, 2)), CAST(50000.00 AS Decimal(18, 2)), 1, CAST(180000.00 AS Decimal(18, 2)), NULL)
INSERT [dbo].[Accounts] ([Id], [UserId], [IBAN], [Balance], [CreationDate], [Active], [AccountType], [DailyWithdrawalLimit], [DailyTransferLimit], [AutomaticPayments], [InsuranceCoverage], [SpecialNotes]) VALUES (6, 9, N'TR575202433939709203', CAST(300000.00 AS Decimal(18, 2)), CAST(N'2024-01-10T01:49:38.4742138' AS DateTime2), 0, N'Savings', CAST(20000.00 AS Decimal(18, 2)), CAST(50000.00 AS Decimal(18, 2)), 1, CAST(800000.00 AS Decimal(18, 2)), NULL)
INSERT [dbo].[Accounts] ([Id], [UserId], [IBAN], [Balance], [CreationDate], [Active], [AccountType], [DailyWithdrawalLimit], [DailyTransferLimit], [AutomaticPayments], [InsuranceCoverage], [SpecialNotes]) VALUES (7, 14, N'TR040851331193590146', CAST(95000.00 AS Decimal(18, 2)), CAST(N'2024-01-10T03:52:22.8838418' AS DateTime2), 0, N'Savings', CAST(20000.00 AS Decimal(18, 2)), CAST(50000.00 AS Decimal(18, 2)), 1, CAST(100000.00 AS Decimal(18, 2)), NULL)
INSERT [dbo].[Accounts] ([Id], [UserId], [IBAN], [Balance], [CreationDate], [Active], [AccountType], [DailyWithdrawalLimit], [DailyTransferLimit], [AutomaticPayments], [InsuranceCoverage], [SpecialNotes]) VALUES (8, 14, N'TR346456052461731591', CAST(101000.00 AS Decimal(18, 2)), CAST(N'2024-01-10T03:52:34.5111933' AS DateTime2), 1, N'Savings', CAST(19000.00 AS Decimal(18, 2)), CAST(50000.00 AS Decimal(18, 2)), 1, CAST(100000.00 AS Decimal(18, 2)), NULL)
SET IDENTITY_INSERT [dbo].[Accounts] OFF
GO
SET IDENTITY_INSERT [dbo].[AccountTransactions] ON 

INSERT [dbo].[AccountTransactions] ([Id], [AccountId], [Amount], [TransactionType], [TransactionDate], [Status]) VALUES (1, 1, CAST(30000.00 AS Decimal(18, 2)), N'Yatırım', CAST(N'2024-01-07T22:48:52.9595592' AS DateTime2), NULL)
INSERT [dbo].[AccountTransactions] ([Id], [AccountId], [Amount], [TransactionType], [TransactionDate], [Status]) VALUES (2, 1, CAST(30000.00 AS Decimal(18, 2)), N'Çekim', CAST(N'2024-01-07T22:49:16.5864648' AS DateTime2), NULL)
INSERT [dbo].[AccountTransactions] ([Id], [AccountId], [Amount], [TransactionType], [TransactionDate], [Status]) VALUES (3, 3, CAST(2000.00 AS Decimal(18, 2)), N'Yatırım', CAST(N'2024-01-07T23:38:52.6436603' AS DateTime2), NULL)
INSERT [dbo].[AccountTransactions] ([Id], [AccountId], [Amount], [TransactionType], [TransactionDate], [Status]) VALUES (4, 1, CAST(2000.00 AS Decimal(18, 2)), N'Deposit', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), NULL)
INSERT [dbo].[AccountTransactions] ([Id], [AccountId], [Amount], [TransactionType], [TransactionDate], [Status]) VALUES (5, 1, CAST(50000.00 AS Decimal(18, 2)), N'Withdrawal', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), N'Daily Limit Exceeded')
INSERT [dbo].[AccountTransactions] ([Id], [AccountId], [Amount], [TransactionType], [TransactionDate], [Status]) VALUES (6, 1, CAST(2000.00 AS Decimal(18, 2)), N'Withdrawal', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), N'Done')
INSERT [dbo].[AccountTransactions] ([Id], [AccountId], [Amount], [TransactionType], [TransactionDate], [Status]) VALUES (7, 1, CAST(2000.00 AS Decimal(18, 2)), N'Withdrawal', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), N'Done')
INSERT [dbo].[AccountTransactions] ([Id], [AccountId], [Amount], [TransactionType], [TransactionDate], [Status]) VALUES (8, 1, CAST(2000.00 AS Decimal(18, 2)), N'Withdrawal', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), N'Done')
INSERT [dbo].[AccountTransactions] ([Id], [AccountId], [Amount], [TransactionType], [TransactionDate], [Status]) VALUES (9, 1, CAST(14000.00 AS Decimal(18, 2)), N'Withdrawal', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), N'Done')
INSERT [dbo].[AccountTransactions] ([Id], [AccountId], [Amount], [TransactionType], [TransactionDate], [Status]) VALUES (10, 1, CAST(14000.00 AS Decimal(18, 2)), N'Withdrawal', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), N'Daily Limit Exceeded')
INSERT [dbo].[AccountTransactions] ([Id], [AccountId], [Amount], [TransactionType], [TransactionDate], [Status]) VALUES (11, 1, CAST(80000.00 AS Decimal(18, 2)), N'Withdrawal', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), N'Pending Approval')
INSERT [dbo].[AccountTransactions] ([Id], [AccountId], [Amount], [TransactionType], [TransactionDate], [Status]) VALUES (12, 1, CAST(5000.00 AS Decimal(18, 2)), N'Withdrawal', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), N'Done')
INSERT [dbo].[AccountTransactions] ([Id], [AccountId], [Amount], [TransactionType], [TransactionDate], [Status]) VALUES (13, 1, CAST(5000.00 AS Decimal(18, 2)), N'Withdrawal', CAST(N'2024-01-09T17:20:17.0778117' AS DateTime2), N'Done')
INSERT [dbo].[AccountTransactions] ([Id], [AccountId], [Amount], [TransactionType], [TransactionDate], [Status]) VALUES (14, 2, CAST(2000.00 AS Decimal(18, 2)), N'Deposit', CAST(N'2024-01-09T23:59:39.7193399' AS DateTime2), N'Processed')
INSERT [dbo].[AccountTransactions] ([Id], [AccountId], [Amount], [TransactionType], [TransactionDate], [Status]) VALUES (15, 2, CAST(200000.00 AS Decimal(18, 2)), N'Deposit', CAST(N'2024-01-09T23:59:53.0919650' AS DateTime2), N'Processed')
INSERT [dbo].[AccountTransactions] ([Id], [AccountId], [Amount], [TransactionType], [TransactionDate], [Status]) VALUES (16, 2, CAST(200000.00 AS Decimal(18, 2)), N'Deposit', CAST(N'2024-01-10T00:00:11.0554137' AS DateTime2), N'Processed')
INSERT [dbo].[AccountTransactions] ([Id], [AccountId], [Amount], [TransactionType], [TransactionDate], [Status]) VALUES (18, 2, CAST(2000.00 AS Decimal(18, 2)), N'Withdrawal', CAST(N'2024-01-10T00:01:27.8875183' AS DateTime2), N'Processed')
INSERT [dbo].[AccountTransactions] ([Id], [AccountId], [Amount], [TransactionType], [TransactionDate], [Status]) VALUES (19, 2, CAST(2000.00 AS Decimal(18, 2)), N'Withdrawal', CAST(N'2024-01-10T00:01:49.5494684' AS DateTime2), N'Processed')
INSERT [dbo].[AccountTransactions] ([Id], [AccountId], [Amount], [TransactionType], [TransactionDate], [Status]) VALUES (20, 2, CAST(2000.00 AS Decimal(18, 2)), N'Withdrawal', CAST(N'2024-01-10T00:01:57.9088058' AS DateTime2), N'Processed')
INSERT [dbo].[AccountTransactions] ([Id], [AccountId], [Amount], [TransactionType], [TransactionDate], [Status]) VALUES (21, 2, CAST(2000.00 AS Decimal(18, 2)), N'Withdrawal', CAST(N'2024-01-10T00:02:04.3940050' AS DateTime2), N'Processed')
INSERT [dbo].[AccountTransactions] ([Id], [AccountId], [Amount], [TransactionType], [TransactionDate], [Status]) VALUES (23, 2, CAST(8000.00 AS Decimal(18, 2)), N'Withdrawal', CAST(N'2024-01-10T00:07:55.9663361' AS DateTime2), N'Processed')
INSERT [dbo].[AccountTransactions] ([Id], [AccountId], [Amount], [TransactionType], [TransactionDate], [Status]) VALUES (27, 2, CAST(300.00 AS Decimal(18, 2)), N'Withdrawal', CAST(N'2024-01-10T00:24:11.2637226' AS DateTime2), N'Processed')
INSERT [dbo].[AccountTransactions] ([Id], [AccountId], [Amount], [TransactionType], [TransactionDate], [Status]) VALUES (29, 2, CAST(3700.00 AS Decimal(18, 2)), N'Withdrawal', CAST(N'2024-01-10T00:25:49.0984345' AS DateTime2), N'Processed')
INSERT [dbo].[AccountTransactions] ([Id], [AccountId], [Amount], [TransactionType], [TransactionDate], [Status]) VALUES (34, 2, CAST(3500.00 AS Decimal(18, 2)), N'Withdrawal', CAST(N'2024-01-10T00:41:29.4363958' AS DateTime2), N'Daily Limit Exceeded')
INSERT [dbo].[AccountTransactions] ([Id], [AccountId], [Amount], [TransactionType], [TransactionDate], [Status]) VALUES (35, 2, CAST(85500.00 AS Decimal(18, 2)), N'Withdrawal', CAST(N'2024-01-10T00:41:57.7398636' AS DateTime2), N'Pending Approval')
INSERT [dbo].[AccountTransactions] ([Id], [AccountId], [Amount], [TransactionType], [TransactionDate], [Status]) VALUES (36, 6, CAST(80000.00 AS Decimal(18, 2)), N'Withdrawal', CAST(N'2024-01-10T01:50:29.0549864' AS DateTime2), N'Approved')
INSERT [dbo].[AccountTransactions] ([Id], [AccountId], [Amount], [TransactionType], [TransactionDate], [Status]) VALUES (37, 8, CAST(1000.00 AS Decimal(18, 2)), N'Withdrawal', CAST(N'2024-01-10T03:53:04.5594998' AS DateTime2), N'Processed')
INSERT [dbo].[AccountTransactions] ([Id], [AccountId], [Amount], [TransactionType], [TransactionDate], [Status]) VALUES (38, 8, CAST(2000.00 AS Decimal(18, 2)), N'Deposit', CAST(N'2024-01-10T03:53:37.9594386' AS DateTime2), N'Processed')
SET IDENTITY_INSERT [dbo].[AccountTransactions] OFF
GO
SET IDENTITY_INSERT [dbo].[Credits] ON 

INSERT [dbo].[Credits] ([Id], [UserId], [LoanId], [Active], [IBAN]) VALUES (2, 14, 1, 0, N'TR346456052461731591')
SET IDENTITY_INSERT [dbo].[Credits] OFF
GO
SET IDENTITY_INSERT [dbo].[Loans] ON 

INSERT [dbo].[Loans] ([Id], [OfferCode], [LoanAmount], [InterestRate], [LoanTermMonths], [Active]) VALUES (1, N'IHTIYAC200', CAST(200000.00 AS Decimal(18, 2)), CAST(2.79 AS Decimal(18, 2)), 6, 1)
INSERT [dbo].[Loans] ([Id], [OfferCode], [LoanAmount], [InterestRate], [LoanTermMonths], [Active]) VALUES (2, N'YATIRIM20', CAST(20000.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 6, 1)
INSERT [dbo].[Loans] ([Id], [OfferCode], [LoanAmount], [InterestRate], [LoanTermMonths], [Active]) VALUES (3, N'TASIT50', CAST(50000.00 AS Decimal(18, 2)), CAST(1.50 AS Decimal(18, 2)), 12, 1)
INSERT [dbo].[Loans] ([Id], [OfferCode], [LoanAmount], [InterestRate], [LoanTermMonths], [Active]) VALUES (4, N'EV300', CAST(300000.00 AS Decimal(18, 2)), CAST(3.25 AS Decimal(18, 2)), 24, 1)
INSERT [dbo].[Loans] ([Id], [OfferCode], [LoanAmount], [InterestRate], [LoanTermMonths], [Active]) VALUES (5, N'TATIL5000', CAST(5000.00 AS Decimal(18, 2)), CAST(5.00 AS Decimal(18, 2)), 3, 1)
INSERT [dbo].[Loans] ([Id], [OfferCode], [LoanAmount], [InterestRate], [LoanTermMonths], [Active]) VALUES (6, N'SAGLIK10000', CAST(10000.00 AS Decimal(18, 2)), CAST(2.00 AS Decimal(18, 2)), 6, 1)
INSERT [dbo].[Loans] ([Id], [OfferCode], [LoanAmount], [InterestRate], [LoanTermMonths], [Active]) VALUES (7, N'KIS600', CAST(60000.00 AS Decimal(18, 2)), CAST(4.75 AS Decimal(18, 2)), 18, 1)
INSERT [dbo].[Loans] ([Id], [OfferCode], [LoanAmount], [InterestRate], [LoanTermMonths], [Active]) VALUES (8, N'EGITIM15000', CAST(15000.00 AS Decimal(18, 2)), CAST(1.75 AS Decimal(18, 2)), 12, 1)
INSERT [dbo].[Loans] ([Id], [OfferCode], [LoanAmount], [InterestRate], [LoanTermMonths], [Active]) VALUES (9, N'ISYERI100000', CAST(100000.00 AS Decimal(18, 2)), CAST(3.50 AS Decimal(18, 2)), 36, 1)
INSERT [dbo].[Loans] ([Id], [OfferCode], [LoanAmount], [InterestRate], [LoanTermMonths], [Active]) VALUES (10, N'ESNAF45000', CAST(45000.00 AS Decimal(18, 2)), CAST(2.25 AS Decimal(18, 2)), 24, 1)
SET IDENTITY_INSERT [dbo].[Loans] OFF
GO
SET IDENTITY_INSERT [dbo].[SupportMessages] ON 

INSERT [dbo].[SupportMessages] ([Id], [Email], [Message], [MessageReceivedAt], [Category], [Active]) VALUES (1, N'aticibrk@gmail.com', N'Hesabıma erişemiyorum', CAST(N'2024-01-07T19:50:34.8290000' AS DateTime2), N'Hesap Sorunları', 0)
SET IDENTITY_INSERT [dbo].[SupportMessages] OFF
GO
SET IDENTITY_INSERT [dbo].[TransferTransactions] ON 

INSERT [dbo].[TransferTransactions] ([Id], [SourceAccountId], [TargetAccountId], [Amount], [TransactionType], [TransactionDate], [SourceIBAN], [TargetIBAN]) VALUES (1, 0, 0, CAST(1000.00 AS Decimal(18, 2)), N'HAVALE', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), N'TR230891141217319427', N'TR481846516471270938')
INSERT [dbo].[TransferTransactions] ([Id], [SourceAccountId], [TargetAccountId], [Amount], [TransactionType], [TransactionDate], [SourceIBAN], [TargetIBAN]) VALUES (2, 0, 0, CAST(2000.00 AS Decimal(18, 2)), N'HAVALE', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), N'TR895739095067157991', N'TR973040117206439843')
INSERT [dbo].[TransferTransactions] ([Id], [SourceAccountId], [TargetAccountId], [Amount], [TransactionType], [TransactionDate], [SourceIBAN], [TargetIBAN]) VALUES (3, 0, 0, CAST(20000.00 AS Decimal(18, 2)), N'HAVALE', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), N'TR230891141217319427', N'TR481846516471270938')
INSERT [dbo].[TransferTransactions] ([Id], [SourceAccountId], [TargetAccountId], [Amount], [TransactionType], [TransactionDate], [SourceIBAN], [TargetIBAN]) VALUES (4, 0, 0, CAST(1234.00 AS Decimal(18, 2)), N'HAVALE', CAST(N'2024-01-09T21:24:50.0581632' AS DateTime2), N'TR481846516471270938', N'TR085895820074454270')
INSERT [dbo].[TransferTransactions] ([Id], [SourceAccountId], [TargetAccountId], [Amount], [TransactionType], [TransactionDate], [SourceIBAN], [TargetIBAN]) VALUES (5, 0, 0, CAST(5000.00 AS Decimal(18, 2)), N'HAVALE', CAST(N'2024-01-10T03:54:53.9780890' AS DateTime2), N'TR040851331193590146', N'TR346456052461731591')
INSERT [dbo].[TransferTransactions] ([Id], [SourceAccountId], [TargetAccountId], [Amount], [TransactionType], [TransactionDate], [SourceIBAN], [TargetIBAN]) VALUES (6, 0, 0, CAST(5000.00 AS Decimal(18, 2)), N'HAVALE', CAST(N'2024-01-10T03:55:40.3084314' AS DateTime2), N'TR346456052461731591', N'TR481846516471270938')
SET IDENTITY_INSERT [dbo].[TransferTransactions] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([UserId], [UserName], [Email], [PasswordHash], [RegistrationDate], [Role], [isActive]) VALUES (1, N'Burak', N'aticibrk@gmail.com', N'$2a$11$zHeDRSOzKLjPAirahf5lc.Sbh78cEU/iEkbWpKoBMvCI4Mi9g1MAK', CAST(N'2024-01-07T22:45:58.3510553' AS DateTime2), N'User', 1)
INSERT [dbo].[Users] ([UserId], [UserName], [Email], [PasswordHash], [RegistrationDate], [Role], [isActive]) VALUES (2, N'Burak', N'aticibrk123@gmail.com', N'$2a$11$8aFVMCiy.NSEztWu4twyP.5tC/Qz0MZpZBRkMrw3LrcPHpM7wDxRG', CAST(N'2024-01-07T23:30:43.5926024' AS DateTime2), N'User', 1)
INSERT [dbo].[Users] ([UserId], [UserName], [Email], [PasswordHash], [RegistrationDate], [Role], [isActive]) VALUES (3, N'deneme', N'deneme@gmail.com', N'$2a$11$nr6OXiPnhlSRc/ByON44iuseOIod.zd90Ag6MuWShRxXXhVfiKTJm', CAST(N'2024-01-07T23:32:57.3411973' AS DateTime2), N'User', 1)
INSERT [dbo].[Users] ([UserId], [UserName], [Email], [PasswordHash], [RegistrationDate], [Role], [isActive]) VALUES (4, N'deneme1', N'deneme1@gmail.com', N'$2a$11$eFlUxfqBeKRblPFByy4up.OEZpyZs1TM8A87oc0mkOESFKc/S5IUe', CAST(N'2024-01-07T23:34:46.4885387' AS DateTime2), N'User', 1)
INSERT [dbo].[Users] ([UserId], [UserName], [Email], [PasswordHash], [RegistrationDate], [Role], [isActive]) VALUES (5, N'keko', N'keko@gmail.com', N'$2a$11$7A5Gd7tT2ADSswP0HoCyj.qpGF9g3INbvicPIeiWxy99WV9pUW3JS', CAST(N'2024-01-09T17:39:17.9665866' AS DateTime2), N'User', 1)
INSERT [dbo].[Users] ([UserId], [UserName], [Email], [PasswordHash], [RegistrationDate], [Role], [isActive]) VALUES (6, N'Modaa', N'moda@gmail.com', N'$2a$11$lW8S8Wy6NkpZCo9vnRVI.ebRF5nPuEHRrM4.hJw7cv1JFBy4pm3zK', CAST(N'2024-01-09T21:21:33.6052015' AS DateTime2), N'User', 1)
INSERT [dbo].[Users] ([UserId], [UserName], [Email], [PasswordHash], [RegistrationDate], [Role], [isActive]) VALUES (7, N'Admin', N'info@bankapi.com', N'$2a$11$WUnpNRrPk33736mjMsRcsexjN3nACe4hwUGdcQepUsnZH7RZzhHnK', CAST(N'2024-01-10T00:58:28.4111296' AS DateTime2), N'Admin', 1)
INSERT [dbo].[Users] ([UserId], [UserName], [Email], [PasswordHash], [RegistrationDate], [Role], [isActive]) VALUES (8, N'Auditor', N'auditor@bankapi.com', N'$2a$11$m6Ih/vR4MIpFkpFcBDBtW.qebCTaO/gPus87fW5UyqmtvnYbzQCj6', CAST(N'2024-01-10T01:14:40.6081319' AS DateTime2), N'Auditor', 1)
INSERT [dbo].[Users] ([UserId], [UserName], [Email], [PasswordHash], [RegistrationDate], [Role], [isActive]) VALUES (9, N'Customer', N'customer1@gmail.com', N'$2a$11$FHc.1oVK/hVCL7QgOcs07ehpd8m8hYeLoPU.gLvOLnPws92t8m1MS', CAST(N'2024-01-10T01:48:49.5015816' AS DateTime2), N'Auditor', 0)
INSERT [dbo].[Users] ([UserId], [UserName], [Email], [PasswordHash], [RegistrationDate], [Role], [isActive]) VALUES (10, N'Deneme1', N'deneme@gmail.com', N'$2a$11$ClsThS74Hs4sobdld.mg.urnt0Wc9p3jxq6mfi3NlXGFFr8wyxJly', CAST(N'2024-01-10T03:35:14.8822300' AS DateTime2), N'User', 0)
INSERT [dbo].[Users] ([UserId], [UserName], [Email], [PasswordHash], [RegistrationDate], [Role], [isActive]) VALUES (11, N'Deneme2', N'deneme2@gmail.com', N'$2a$11$99C6rlq9kNpTS.y3XzS7CuEHrsGoRCxnBZGpSQaBfGd9wLIL3j3v2', CAST(N'2024-01-10T03:47:00.7762581' AS DateTime2), N'User', 0)
INSERT [dbo].[Users] ([UserId], [UserName], [Email], [PasswordHash], [RegistrationDate], [Role], [isActive]) VALUES (12, N'Merhaba', N'string', N'$2a$11$JOXfcQFLk.XTUgCwF2Owkegkq8jLeAlkKyfPigCM6AtxcMLlwuF6a', CAST(N'2024-01-10T03:48:08.7250603' AS DateTime2), N'User', 0)
INSERT [dbo].[Users] ([UserId], [UserName], [Email], [PasswordHash], [RegistrationDate], [Role], [isActive]) VALUES (13, N'lol', N'lol@gmail.com', N'$2a$11$3q1UiT4P5iXdjzDMuCY6W.XNh2k45ge8aSeHJwoIr6eNCCXxNyvfm', CAST(N'2024-01-10T03:50:04.5914489' AS DateTime2), N'User', 1)
INSERT [dbo].[Users] ([UserId], [UserName], [Email], [PasswordHash], [RegistrationDate], [Role], [isActive]) VALUES (14, N'deneme3', N'deneme3@gmail.com', N'$2a$11$fH6NGY1wVnPQcFHRFVBI0.crBCMFZdnOQmjTsULuQp4x.9Rm/Rqh.', CAST(N'2024-01-10T03:51:24.9516459' AS DateTime2), N'User', 1)
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
/****** Object:  Index [IX_Accounts_UserId]    Script Date: 10.01.2024 05:04:54 ******/
CREATE NONCLUSTERED INDEX [IX_Accounts_UserId] ON [dbo].[Accounts]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Accounts] ADD  CONSTRAINT [DF__Accounts__UserId__5535A963]  DEFAULT ((0)) FOR [UserId]
GO
ALTER TABLE [dbo].[Accounts] ADD  CONSTRAINT [DF__Accounts__IBAN__59063A47]  DEFAULT (N'') FOR [IBAN]
GO
ALTER TABLE [dbo].[Credits] ADD  DEFAULT (N'') FOR [IBAN]
GO
ALTER TABLE [dbo].[TransferTransactions] ADD  DEFAULT (N'') FOR [SourceIBAN]
GO
ALTER TABLE [dbo].[TransferTransactions] ADD  DEFAULT (N'') FOR [TargetIBAN]
GO
ALTER TABLE [dbo].[Accounts]  WITH CHECK ADD  CONSTRAINT [FK_Accounts_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Accounts] CHECK CONSTRAINT [FK_Accounts_Users_UserId]
GO
USE [master]
GO
ALTER DATABASE [RestfulBankDbo] SET  READ_WRITE 
GO
