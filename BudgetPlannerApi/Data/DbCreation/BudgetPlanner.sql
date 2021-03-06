USE [master]
GO
/****** Object:  Database [BudgetPlanner]    Script Date: 11/7/2020 3:31:38 PM ******/
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
/****** Object:  Table [dbo].[BudgetCategories]    Script Date: 11/7/2020 3:31:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BudgetCategories](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[BudgetGroupId] [int] NOT NULL,
 CONSTRAINT [PK_BudgetItemGroup] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BudgetCycleItems]    Script Date: 11/7/2020 3:31:38 PM ******/
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
/****** Object:  Table [dbo].[BudgetCycles]    Script Date: 11/7/2020 3:31:38 PM ******/
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
/****** Object:  Table [dbo].[BudgetGroups]    Script Date: 11/7/2020 3:31:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BudgetGroups](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_BudgetItemType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BudgetItems]    Script Date: 11/7/2020 3:31:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BudgetItems](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[BudgetCategoryId] [int] NOT NULL,
	[IsIncome] [bit] NULL,
 CONSTRAINT [PK_BudgetItem] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MemorizedTransactions]    Script Date: 11/7/2020 3:31:38 PM ******/
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
/****** Object:  Table [dbo].[RegisterEntries]    Script Date: 11/7/2020 3:31:38 PM ******/
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
 CONSTRAINT [PK_RegisterEntries] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Registers]    Script Date: 11/7/2020 3:31:38 PM ******/
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
/****** Object:  Table [dbo].[RegisterSplitEntries]    Script Date: 11/7/2020 3:31:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RegisterSplitEntries](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RegisterEntryId] [int] NOT NULL,
	[BudgetItemId] [int] NULL,
	[Payee] [nvarchar](100) NOT NULL,
	[Memo] [nvarchar](100) NULL,
	[WithdrawalAmount] [money] NULL,
	[DepositAmount] [money] NULL,
 CONSTRAINT [PK_RegisterSplitEntries] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[BudgetCategories] ON 

INSERT [dbo].[BudgetCategories] ([Id], [Name], [BudgetGroupId]) VALUES (2, N'Salaries', 1)
INSERT [dbo].[BudgetCategories] ([Id], [Name], [BudgetGroupId]) VALUES (3, N'Reimbursements', 1)
INSERT [dbo].[BudgetCategories] ([Id], [Name], [BudgetGroupId]) VALUES (4, N'Transfer from Savings', 1)
INSERT [dbo].[BudgetCategories] ([Id], [Name], [BudgetGroupId]) VALUES (5, N'Groceries/Dining', 2)
INSERT [dbo].[BudgetCategories] ([Id], [Name], [BudgetGroupId]) VALUES (6, N'Car', 2)
INSERT [dbo].[BudgetCategories] ([Id], [Name], [BudgetGroupId]) VALUES (7, N'Clothing', 2)
INSERT [dbo].[BudgetCategories] ([Id], [Name], [BudgetGroupId]) VALUES (8, N'Grooming', 2)
INSERT [dbo].[BudgetCategories] ([Id], [Name], [BudgetGroupId]) VALUES (9, N'Entertainment', 2)
INSERT [dbo].[BudgetCategories] ([Id], [Name], [BudgetGroupId]) VALUES (10, N'Cash', 2)
INSERT [dbo].[BudgetCategories] ([Id], [Name], [BudgetGroupId]) VALUES (11, N'Gifts/Giving', 2)
INSERT [dbo].[BudgetCategories] ([Id], [Name], [BudgetGroupId]) VALUES (13, N'Church', 2)
INSERT [dbo].[BudgetCategories] ([Id], [Name], [BudgetGroupId]) VALUES (14, N'Household Items', 2)
INSERT [dbo].[BudgetCategories] ([Id], [Name], [BudgetGroupId]) VALUES (15, N'Computer', 2)
INSERT [dbo].[BudgetCategories] ([Id], [Name], [BudgetGroupId]) VALUES (16, N'Misc', 2)
INSERT [dbo].[BudgetCategories] ([Id], [Name], [BudgetGroupId]) VALUES (17, N'Mortgage/Rent', 3)
INSERT [dbo].[BudgetCategories] ([Id], [Name], [BudgetGroupId]) VALUES (18, N'Credit Cards/Loans', 3)
INSERT [dbo].[BudgetCategories] ([Id], [Name], [BudgetGroupId]) VALUES (19, N'Utilities', 3)
INSERT [dbo].[BudgetCategories] ([Id], [Name], [BudgetGroupId]) VALUES (20, N'Insurance', 3)
INSERT [dbo].[BudgetCategories] ([Id], [Name], [BudgetGroupId]) VALUES (21, N'Subscriptions', 3)
INSERT [dbo].[BudgetCategories] ([Id], [Name], [BudgetGroupId]) VALUES (22, N'Transfer to Savings', 5)
INSERT [dbo].[BudgetCategories] ([Id], [Name], [BudgetGroupId]) VALUES (23, N'Car Repairs', 5)
INSERT [dbo].[BudgetCategories] ([Id], [Name], [BudgetGroupId]) VALUES (24, N'Home Repairs', 5)
INSERT [dbo].[BudgetCategories] ([Id], [Name], [BudgetGroupId]) VALUES (25, N'Registrations', 5)
INSERT [dbo].[BudgetCategories] ([Id], [Name], [BudgetGroupId]) VALUES (26, N'Renewals/Subscriptions', 4)
INSERT [dbo].[BudgetCategories] ([Id], [Name], [BudgetGroupId]) VALUES (28, N'Other', 5)
INSERT [dbo].[BudgetCategories] ([Id], [Name], [BudgetGroupId]) VALUES (57, N'Testing', 13)
INSERT [dbo].[BudgetCategories] ([Id], [Name], [BudgetGroupId]) VALUES (58, N'Lottery Winnings', 21)
SET IDENTITY_INSERT [dbo].[BudgetCategories] OFF
GO
SET IDENTITY_INSERT [dbo].[BudgetCycleItems] ON 

INSERT [dbo].[BudgetCycleItems] ([Id], [BudgetCycleId], [BudgetItemId], [Amount]) VALUES (1, 1, 4, 7200.0000)
SET IDENTITY_INSERT [dbo].[BudgetCycleItems] OFF
GO
SET IDENTITY_INSERT [dbo].[BudgetCycles] ON 

INSERT [dbo].[BudgetCycles] ([Id], [Name], [StartDate], [EndDate], [StartingBalance]) VALUES (1, N'Nov 2020                                                                                            ', CAST(N'2020-11-01' AS Date), CAST(N'2020-11-30' AS Date), 0.0000)
INSERT [dbo].[BudgetCycles] ([Id], [Name], [StartDate], [EndDate], [StartingBalance]) VALUES (2, N'Dec 2020                                                                                            ', CAST(N'2020-12-01' AS Date), CAST(N'2020-12-31' AS Date), 0.0000)
INSERT [dbo].[BudgetCycles] ([Id], [Name], [StartDate], [EndDate], [StartingBalance]) VALUES (4, N'Jan 2021                                                                                            ', CAST(N'0001-01-01' AS Date), CAST(N'0001-01-01' AS Date), 0.0000)
SET IDENTITY_INSERT [dbo].[BudgetCycles] OFF
GO
SET IDENTITY_INSERT [dbo].[BudgetGroups] ON 

INSERT [dbo].[BudgetGroups] ([Id], [Name]) VALUES (1, N'Income')
INSERT [dbo].[BudgetGroups] ([Id], [Name]) VALUES (2, N'Daily Expense')
INSERT [dbo].[BudgetGroups] ([Id], [Name]) VALUES (3, N'Monthly Expense')
INSERT [dbo].[BudgetGroups] ([Id], [Name]) VALUES (4, N'Yearly Expense')
INSERT [dbo].[BudgetGroups] ([Id], [Name]) VALUES (5, N'Occasional Expense')
INSERT [dbo].[BudgetGroups] ([Id], [Name]) VALUES (13, N'One-Time Expense')
INSERT [dbo].[BudgetGroups] ([Id], [Name]) VALUES (21, N'Once in a Liftetime')
SET IDENTITY_INSERT [dbo].[BudgetGroups] OFF
GO
SET IDENTITY_INSERT [dbo].[BudgetItems] ON 

INSERT [dbo].[BudgetItems] ([Id], [Name], [BudgetCategoryId], [IsIncome]) VALUES (2, N'Bentley', 2, 1)
INSERT [dbo].[BudgetItems] ([Id], [Name], [BudgetCategoryId], [IsIncome]) VALUES (3, N'Union Home Mortgage', 2, 1)
INSERT [dbo].[BudgetItems] ([Id], [Name], [BudgetCategoryId], [IsIncome]) VALUES (4, N'Transfer from Savings', 4, 1)
INSERT [dbo].[BudgetItems] ([Id], [Name], [BudgetCategoryId], [IsIncome]) VALUES (5, N'Transfer from Studen Loan', 4, 1)
INSERT [dbo].[BudgetItems] ([Id], [Name], [BudgetCategoryId], [IsIncome]) VALUES (6, N'Reimbursement', 3, 1)
INSERT [dbo].[BudgetItems] ([Id], [Name], [BudgetCategoryId], [IsIncome]) VALUES (7, N'Groceries', 5, 0)
INSERT [dbo].[BudgetItems] ([Id], [Name], [BudgetCategoryId], [IsIncome]) VALUES (8, N'Dining Out', 5, 0)
INSERT [dbo].[BudgetItems] ([Id], [Name], [BudgetCategoryId], [IsIncome]) VALUES (9, N'Snacks', 5, 0)
INSERT [dbo].[BudgetItems] ([Id], [Name], [BudgetCategoryId], [IsIncome]) VALUES (10, N'Fuel Pilot', 6, 0)
INSERT [dbo].[BudgetItems] ([Id], [Name], [BudgetCategoryId], [IsIncome]) VALUES (11, N'Fuel Accord', 6, 0)
INSERT [dbo].[BudgetItems] ([Id], [Name], [BudgetCategoryId], [IsIncome]) VALUES (12, N'Auto Maintenance', 6, 0)
SET IDENTITY_INSERT [dbo].[BudgetItems] OFF
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
ALTER TABLE [dbo].[BudgetCategories]  WITH CHECK ADD  CONSTRAINT [FK_BudgetCategory_BudgetGroup] FOREIGN KEY([BudgetGroupId])
REFERENCES [dbo].[BudgetGroups] ([Id])
GO
ALTER TABLE [dbo].[BudgetCategories] CHECK CONSTRAINT [FK_BudgetCategory_BudgetGroup]
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
ALTER TABLE [dbo].[BudgetItems]  WITH CHECK ADD  CONSTRAINT [FK_BudgetItem_BudgetCategory] FOREIGN KEY([BudgetCategoryId])
REFERENCES [dbo].[BudgetCategories] ([Id])
GO
ALTER TABLE [dbo].[BudgetItems] CHECK CONSTRAINT [FK_BudgetItem_BudgetCategory]
GO
ALTER TABLE [dbo].[MemorizedTransactions]  WITH CHECK ADD  CONSTRAINT [FK_MemorizedTransactions_BudgetItem] FOREIGN KEY([BudgetItemId])
REFERENCES [dbo].[BudgetItems] ([Id])
GO
ALTER TABLE [dbo].[MemorizedTransactions] CHECK CONSTRAINT [FK_MemorizedTransactions_BudgetItem]
GO
ALTER TABLE [dbo].[RegisterEntries]  WITH CHECK ADD  CONSTRAINT [FK_RegisterEntries_BudgetCycles] FOREIGN KEY([BudgetCycleId])
REFERENCES [dbo].[BudgetCycles] ([Id])
GO
ALTER TABLE [dbo].[RegisterEntries] CHECK CONSTRAINT [FK_RegisterEntries_BudgetCycles]
GO
ALTER TABLE [dbo].[RegisterEntries]  WITH CHECK ADD  CONSTRAINT [FK_RegisterEntries_BudgetItems] FOREIGN KEY([BudgetItemId])
REFERENCES [dbo].[BudgetItems] ([Id])
GO
ALTER TABLE [dbo].[RegisterEntries] CHECK CONSTRAINT [FK_RegisterEntries_BudgetItems]
GO
ALTER TABLE [dbo].[RegisterEntries]  WITH CHECK ADD  CONSTRAINT [FK_RegisterEntries_Registries] FOREIGN KEY([RegisterId])
REFERENCES [dbo].[Registers] ([Id])
GO
ALTER TABLE [dbo].[RegisterEntries] CHECK CONSTRAINT [FK_RegisterEntries_Registries]
GO
ALTER TABLE [dbo].[RegisterSplitEntries]  WITH CHECK ADD  CONSTRAINT [FK_RegisterSplitEntries_BudgetItems] FOREIGN KEY([BudgetItemId])
REFERENCES [dbo].[BudgetItems] ([Id])
GO
ALTER TABLE [dbo].[RegisterSplitEntries] CHECK CONSTRAINT [FK_RegisterSplitEntries_BudgetItems]
GO
ALTER TABLE [dbo].[RegisterSplitEntries]  WITH CHECK ADD  CONSTRAINT [FK_RegisterSplitEntries_Registers] FOREIGN KEY([RegisterEntryId])
REFERENCES [dbo].[Registers] ([Id])
GO
ALTER TABLE [dbo].[RegisterSplitEntries] CHECK CONSTRAINT [FK_RegisterSplitEntries_Registers]
GO
USE [master]
GO
ALTER DATABASE [BudgetPlanner] SET  READ_WRITE 
GO
