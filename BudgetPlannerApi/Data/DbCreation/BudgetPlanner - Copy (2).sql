USE [master]
GO
/****** Object:  Database [BudgetPlanner]    Script Date: 11/2/2020 9:59:16 AM ******/
CREATE DATABASE [BudgetPlanner]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'BudgetPlanner', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\BudgetPlanner.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'BudgetPlanner_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\BudgetPlanner_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [BudgetPlanner] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [BudgetPlanner].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [BudgetPlanner] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [BudgetPlanner] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [BudgetPlanner] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [BudgetPlanner] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [BudgetPlanner] SET ARITHABORT OFF 
GO
ALTER DATABASE [BudgetPlanner] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [BudgetPlanner] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [BudgetPlanner] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [BudgetPlanner] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [BudgetPlanner] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [BudgetPlanner] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [BudgetPlanner] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [BudgetPlanner] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [BudgetPlanner] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [BudgetPlanner] SET  DISABLE_BROKER 
GO
ALTER DATABASE [BudgetPlanner] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [BudgetPlanner] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [BudgetPlanner] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [BudgetPlanner] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [BudgetPlanner] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [BudgetPlanner] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [BudgetPlanner] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [BudgetPlanner] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [BudgetPlanner] SET  MULTI_USER 
GO
ALTER DATABASE [BudgetPlanner] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [BudgetPlanner] SET DB_CHAINING OFF 
GO
ALTER DATABASE [BudgetPlanner] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [BudgetPlanner] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [BudgetPlanner] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [BudgetPlanner] SET QUERY_STORE = OFF
GO
USE [BudgetPlanner]
GO
/****** Object:  Table [dbo].[BudgetCycleItems]    Script Date: 11/2/2020 9:59:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BudgetCycleItems](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BudgetCycleId] [int] NOT NULL,
	[BudgetItemId] [int] NOT NULL,
	[Amount] [money] NOT NULL,
 CONSTRAINT [PK_BudgetCycleItems] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BudgetCycles]    Script Date: 11/2/2020 9:59:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BudgetCycles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nchar](100) NOT NULL,
	[StartDate] [date] NOT NULL,
	[EndDate] [date] NOT NULL,
	[StartingBalance] [money] NOT NULL,
 CONSTRAINT [PK_BudgetCycles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BudgetItemGroups]    Script Date: 11/2/2020 9:59:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BudgetItemGroups](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[BudgetItemTypeId] [int] NOT NULL,
 CONSTRAINT [PK_BudgetItemGroup] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BudgetItems]    Script Date: 11/2/2020 9:59:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BudgetItems](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[BudgetItemGroupId] [int] NOT NULL,
 CONSTRAINT [PK_BudgetItem] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BudgetItemTypes]    Script Date: 11/2/2020 9:59:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BudgetItemTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[IsExpense] [bit] NOT NULL,
 CONSTRAINT [PK_BudgetItemType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MemorizedTransactions]    Script Date: 11/2/2020 9:59:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MemorizedTransactions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Payee] [nvarchar](100) NOT NULL,
	[Amount] [money] NULL,
	[BudgetItemId] [int] NOT NULL,
 CONSTRAINT [PK_MemorizedTransactions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RegisterEntries]    Script Date: 11/2/2020 9:59:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RegisterEntries](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RegisterId] [int] NOT NULL,
	[BudgetCycleId] [int] NULL,
	[BudgetItemId] [int] NOT NULL,
	[EntryNumber] [int] NOT NULL,
	[TransactionDate] [date] NOT NULL,
	[CheckNumber] [int] NULL,
	[Payee] [nvarchar](100) NULL,
	[Memo] [nvarchar](100) NULL,
	[WithdrawalAmount] [money] NULL,
	[DepositAmount] [money] NULL,
	[Status] [int] NULL,
	[IsSplit] [bit] NOT NULL,
 CONSTRAINT [PK_RegistryEntries] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Registers]    Script Date: 11/2/2020 9:59:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Registers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[StartingBalance] [money] NULL,
 CONSTRAINT [PK_Registries] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RegistrySplitEntries]    Script Date: 11/2/2020 9:59:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RegistrySplitEntries](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RegistryEntryId] [int] NOT NULL,
	[BudgetItemId] [int] NULL,
	[Memo] [nvarchar](100) NULL,
	[WithdrawalAmount] [money] NULL,
	[DepositAmount] [money] NULL,
 CONSTRAINT [PK_RegistrySplitEntries] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[BudgetCycleItems] ON 

INSERT [dbo].[BudgetCycleItems] ([Id], [BudgetCycleId], [BudgetItemId], [Amount]) VALUES (1, 1, 4, 7200.0000)
SET IDENTITY_INSERT [dbo].[BudgetCycleItems] OFF
GO
SET IDENTITY_INSERT [dbo].[BudgetCycles] ON 

INSERT [dbo].[BudgetCycles] ([Id], [Name], [StartDate], [EndDate], [StartingBalance]) VALUES (1, N'Nov2020                                                                                             ', CAST(N'2020-11-01' AS Date), CAST(N'2020-11-30' AS Date), 0.0000)
INSERT [dbo].[BudgetCycles] ([Id], [Name], [StartDate], [EndDate], [StartingBalance]) VALUES (2, N'Dec2020                                                                                             ', CAST(N'2020-12-01' AS Date), CAST(N'2020-12-31' AS Date), 0.0000)
SET IDENTITY_INSERT [dbo].[BudgetCycles] OFF
GO
SET IDENTITY_INSERT [dbo].[BudgetItemGroups] ON 

INSERT [dbo].[BudgetItemGroups] ([Id], [Name], [BudgetItemTypeId]) VALUES (2, N'Salaries', 1)
INSERT [dbo].[BudgetItemGroups] ([Id], [Name], [BudgetItemTypeId]) VALUES (3, N'Reimbursements', 1)
INSERT [dbo].[BudgetItemGroups] ([Id], [Name], [BudgetItemTypeId]) VALUES (4, N'Transfer from Savings', 1)
INSERT [dbo].[BudgetItemGroups] ([Id], [Name], [BudgetItemTypeId]) VALUES (5, N'Groceries/Dining', 2)
INSERT [dbo].[BudgetItemGroups] ([Id], [Name], [BudgetItemTypeId]) VALUES (6, N'Car', 2)
INSERT [dbo].[BudgetItemGroups] ([Id], [Name], [BudgetItemTypeId]) VALUES (7, N'Clothing', 2)
INSERT [dbo].[BudgetItemGroups] ([Id], [Name], [BudgetItemTypeId]) VALUES (8, N'Grooming', 2)
INSERT [dbo].[BudgetItemGroups] ([Id], [Name], [BudgetItemTypeId]) VALUES (9, N'Entertainment', 2)
INSERT [dbo].[BudgetItemGroups] ([Id], [Name], [BudgetItemTypeId]) VALUES (10, N'Cash', 2)
INSERT [dbo].[BudgetItemGroups] ([Id], [Name], [BudgetItemTypeId]) VALUES (11, N'Gifts', 2)
INSERT [dbo].[BudgetItemGroups] ([Id], [Name], [BudgetItemTypeId]) VALUES (12, N'Charity', 2)
INSERT [dbo].[BudgetItemGroups] ([Id], [Name], [BudgetItemTypeId]) VALUES (13, N'Church', 2)
INSERT [dbo].[BudgetItemGroups] ([Id], [Name], [BudgetItemTypeId]) VALUES (14, N'Household Items', 2)
INSERT [dbo].[BudgetItemGroups] ([Id], [Name], [BudgetItemTypeId]) VALUES (15, N'Computer', 2)
INSERT [dbo].[BudgetItemGroups] ([Id], [Name], [BudgetItemTypeId]) VALUES (16, N'Misc', 2)
INSERT [dbo].[BudgetItemGroups] ([Id], [Name], [BudgetItemTypeId]) VALUES (17, N'Mortgage/Rent', 3)
INSERT [dbo].[BudgetItemGroups] ([Id], [Name], [BudgetItemTypeId]) VALUES (18, N'Credit Cards/Loans', 3)
INSERT [dbo].[BudgetItemGroups] ([Id], [Name], [BudgetItemTypeId]) VALUES (19, N'Utilities', 3)
INSERT [dbo].[BudgetItemGroups] ([Id], [Name], [BudgetItemTypeId]) VALUES (20, N'Insurance', 3)
INSERT [dbo].[BudgetItemGroups] ([Id], [Name], [BudgetItemTypeId]) VALUES (21, N'Subscriptions', 3)
INSERT [dbo].[BudgetItemGroups] ([Id], [Name], [BudgetItemTypeId]) VALUES (22, N'Transfer to Savings', 5)
INSERT [dbo].[BudgetItemGroups] ([Id], [Name], [BudgetItemTypeId]) VALUES (23, N'Car Repairs', 5)
INSERT [dbo].[BudgetItemGroups] ([Id], [Name], [BudgetItemTypeId]) VALUES (24, N'Home Repairs', 5)
INSERT [dbo].[BudgetItemGroups] ([Id], [Name], [BudgetItemTypeId]) VALUES (25, N'Registrations', 5)
INSERT [dbo].[BudgetItemGroups] ([Id], [Name], [BudgetItemTypeId]) VALUES (26, N'Renewals', 5)
INSERT [dbo].[BudgetItemGroups] ([Id], [Name], [BudgetItemTypeId]) VALUES (28, N'Other', 5)
SET IDENTITY_INSERT [dbo].[BudgetItemGroups] OFF
GO
SET IDENTITY_INSERT [dbo].[BudgetItems] ON 

INSERT [dbo].[BudgetItems] ([Id], [Name], [BudgetItemGroupId]) VALUES (2, N'Bentley', 2)
INSERT [dbo].[BudgetItems] ([Id], [Name], [BudgetItemGroupId]) VALUES (3, N'Union Home Mortgage', 2)
INSERT [dbo].[BudgetItems] ([Id], [Name], [BudgetItemGroupId]) VALUES (4, N'Transfer from Savings', 4)
INSERT [dbo].[BudgetItems] ([Id], [Name], [BudgetItemGroupId]) VALUES (5, N'Transfer from Studen Loan', 4)
INSERT [dbo].[BudgetItems] ([Id], [Name], [BudgetItemGroupId]) VALUES (6, N'Reimbursement', 3)
INSERT [dbo].[BudgetItems] ([Id], [Name], [BudgetItemGroupId]) VALUES (7, N'Groceries', 5)
INSERT [dbo].[BudgetItems] ([Id], [Name], [BudgetItemGroupId]) VALUES (8, N'Dining Out', 5)
INSERT [dbo].[BudgetItems] ([Id], [Name], [BudgetItemGroupId]) VALUES (9, N'Snacks', 5)
INSERT [dbo].[BudgetItems] ([Id], [Name], [BudgetItemGroupId]) VALUES (10, N'Fuel Pilot', 6)
INSERT [dbo].[BudgetItems] ([Id], [Name], [BudgetItemGroupId]) VALUES (11, N'Fuel Accord', 6)
INSERT [dbo].[BudgetItems] ([Id], [Name], [BudgetItemGroupId]) VALUES (12, N'Auto Maintenance', 6)
SET IDENTITY_INSERT [dbo].[BudgetItems] OFF
GO
SET IDENTITY_INSERT [dbo].[BudgetItemTypes] ON 

INSERT [dbo].[BudgetItemTypes] ([Id], [Name], [IsExpense]) VALUES (1, N'Income', 0)
INSERT [dbo].[BudgetItemTypes] ([Id], [Name], [IsExpense]) VALUES (2, N'Daily Expense', 1)
INSERT [dbo].[BudgetItemTypes] ([Id], [Name], [IsExpense]) VALUES (3, N'Monthly Expense', 1)
INSERT [dbo].[BudgetItemTypes] ([Id], [Name], [IsExpense]) VALUES (4, N'Yearly Expense', 1)
INSERT [dbo].[BudgetItemTypes] ([Id], [Name], [IsExpense]) VALUES (5, N'Occassional Expense', 1)
SET IDENTITY_INSERT [dbo].[BudgetItemTypes] OFF
GO
SET IDENTITY_INSERT [dbo].[MemorizedTransactions] ON 

INSERT [dbo].[MemorizedTransactions] ([Id], [Payee], [Amount], [BudgetItemId]) VALUES (1, N'Reeves', 0.0000, 7)
INSERT [dbo].[MemorizedTransactions] ([Id], [Payee], [Amount], [BudgetItemId]) VALUES (2, N'Ally', 5000.0000, 4)
SET IDENTITY_INSERT [dbo].[MemorizedTransactions] OFF
GO
SET IDENTITY_INSERT [dbo].[RegisterEntries] ON 

INSERT [dbo].[RegisterEntries] ([Id], [RegisterId], [BudgetCycleId], [BudgetItemId], [EntryNumber], [TransactionDate], [CheckNumber], [Payee], [Memo], [WithdrawalAmount], [DepositAmount], [Status], [IsSplit]) VALUES (1, 1, 1, 7, 1, CAST(N'2020-11-02' AS Date), 0, N'Reeves', N'groceries', 25.2000, 0.0000, 0, 1)
INSERT [dbo].[RegisterEntries] ([Id], [RegisterId], [BudgetCycleId], [BudgetItemId], [EntryNumber], [TransactionDate], [CheckNumber], [Payee], [Memo], [WithdrawalAmount], [DepositAmount], [Status], [IsSplit]) VALUES (2, 1, 1, 7, 2, CAST(N'2020-11-02' AS Date), 0, N'Albertsons', N'groceries', 125.2000, 0.0000, 0, 1)
SET IDENTITY_INSERT [dbo].[RegisterEntries] OFF
GO
SET IDENTITY_INSERT [dbo].[Registers] ON 

INSERT [dbo].[Registers] ([Id], [Name], [StartingBalance]) VALUES (1, N'My Checking Account', 20.2200)
SET IDENTITY_INSERT [dbo].[Registers] OFF
GO
ALTER TABLE [dbo].[BudgetCycleItems]  WITH CHECK ADD  CONSTRAINT [FK_BudgetCycleItems_BudgetCycles] FOREIGN KEY([BudgetCycleId])
REFERENCES [dbo].[BudgetCycles] ([Id])
GO
ALTER TABLE [dbo].[BudgetCycleItems] CHECK CONSTRAINT [FK_BudgetCycleItems_BudgetCycles]
GO
ALTER TABLE [dbo].[BudgetCycleItems]  WITH CHECK ADD  CONSTRAINT [FK_BudgetCycleItems_BudgetItems] FOREIGN KEY([BudgetItemId])
REFERENCES [dbo].[BudgetItems] ([Id])
GO
ALTER TABLE [dbo].[BudgetCycleItems] CHECK CONSTRAINT [FK_BudgetCycleItems_BudgetItems]
GO
ALTER TABLE [dbo].[BudgetItemGroups]  WITH CHECK ADD  CONSTRAINT [FK_BudgetItemGroup_BudgetItemType] FOREIGN KEY([BudgetItemTypeId])
REFERENCES [dbo].[BudgetItemTypes] ([Id])
GO
ALTER TABLE [dbo].[BudgetItemGroups] CHECK CONSTRAINT [FK_BudgetItemGroup_BudgetItemType]
GO
ALTER TABLE [dbo].[BudgetItems]  WITH CHECK ADD  CONSTRAINT [FK_BudgetItem_BudgetItemGroup] FOREIGN KEY([BudgetItemGroupId])
REFERENCES [dbo].[BudgetItemGroups] ([Id])
GO
ALTER TABLE [dbo].[BudgetItems] CHECK CONSTRAINT [FK_BudgetItem_BudgetItemGroup]
GO
ALTER TABLE [dbo].[MemorizedTransactions]  WITH CHECK ADD  CONSTRAINT [FK_MemorizedTransactions_BudgetItem] FOREIGN KEY([BudgetItemId])
REFERENCES [dbo].[BudgetItems] ([Id])
GO
ALTER TABLE [dbo].[MemorizedTransactions] CHECK CONSTRAINT [FK_MemorizedTransactions_BudgetItem]
GO
ALTER TABLE [dbo].[RegisterEntries]  WITH CHECK ADD  CONSTRAINT [FK_RegistryEntries_BudgetCycles] FOREIGN KEY([BudgetCycleId])
REFERENCES [dbo].[BudgetCycles] ([Id])
GO
ALTER TABLE [dbo].[RegisterEntries] CHECK CONSTRAINT [FK_RegistryEntries_BudgetCycles]
GO
ALTER TABLE [dbo].[RegisterEntries]  WITH CHECK ADD  CONSTRAINT [FK_RegistryEntries_BudgetItems] FOREIGN KEY([BudgetItemId])
REFERENCES [dbo].[BudgetItems] ([Id])
GO
ALTER TABLE [dbo].[RegisterEntries] CHECK CONSTRAINT [FK_RegistryEntries_BudgetItems]
GO
ALTER TABLE [dbo].[RegisterEntries]  WITH CHECK ADD  CONSTRAINT [FK_RegistryEntries_Registries] FOREIGN KEY([RegisterId])
REFERENCES [dbo].[Registers] ([Id])
GO
ALTER TABLE [dbo].[RegisterEntries] CHECK CONSTRAINT [FK_RegistryEntries_Registries]
GO
ALTER TABLE [dbo].[RegistrySplitEntries]  WITH CHECK ADD  CONSTRAINT [FK_RegistrySplitEntries_BudgetItems] FOREIGN KEY([BudgetItemId])
REFERENCES [dbo].[BudgetItems] ([Id])
GO
ALTER TABLE [dbo].[RegistrySplitEntries] CHECK CONSTRAINT [FK_RegistrySplitEntries_BudgetItems]
GO
ALTER TABLE [dbo].[RegistrySplitEntries]  WITH CHECK ADD  CONSTRAINT [FK_RegistrySplitEntries_RegistryEntries] FOREIGN KEY([RegistryEntryId])
REFERENCES [dbo].[RegisterEntries] ([Id])
GO
ALTER TABLE [dbo].[RegistrySplitEntries] CHECK CONSTRAINT [FK_RegistrySplitEntries_RegistryEntries]
GO
USE [master]
GO
ALTER DATABASE [BudgetPlanner] SET  READ_WRITE 
GO
