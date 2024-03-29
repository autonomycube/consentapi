USE [master]
GO
/****** Object:  Database [consent_api]    Script Date: 5/10/2021 7:04:52 PM ******/
CREATE DATABASE [consent_api]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'consent_api', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\consent_api.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'consent_api_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\consent_api_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [consent_api] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [consent_api].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [consent_api] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [consent_api] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [consent_api] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [consent_api] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [consent_api] SET ARITHABORT OFF 
GO
ALTER DATABASE [consent_api] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [consent_api] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [consent_api] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [consent_api] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [consent_api] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [consent_api] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [consent_api] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [consent_api] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [consent_api] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [consent_api] SET  DISABLE_BROKER 
GO
ALTER DATABASE [consent_api] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [consent_api] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [consent_api] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [consent_api] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [consent_api] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [consent_api] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [consent_api] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [consent_api] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [consent_api] SET  MULTI_USER 
GO
ALTER DATABASE [consent_api] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [consent_api] SET DB_CHAINING OFF 
GO
ALTER DATABASE [consent_api] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [consent_api] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [consent_api] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [consent_api] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'consent_api', N'ON'
GO
ALTER DATABASE [consent_api] SET QUERY_STORE = OFF
GO
USE [consent_api]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 5/10/2021 7:04:53 PM ******/
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
/****** Object:  Table [dbo].[tbl_auth_permissions]    Script Date: 5/10/2021 7:04:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_auth_permissions](
	[Id] [nvarchar](55) NOT NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
	[GroupName] [nvarchar](256) NULL,
	[IsActive] [bit] NULL,
	[CreatedBy] [nvarchar](55) NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedBy] [nvarchar](55) NULL,
	[UpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_Permissions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_auth_rolepermission]    Script Date: 5/10/2021 7:04:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_auth_rolepermission](
	[Id] [nvarchar](55) NOT NULL,
	[RoleId] [nvarchar](55) NOT NULL,
	[PermissionId] [nvarchar](55) NOT NULL,
	[IsActive] [bit] NULL,
	[CreatedBy] [nvarchar](55) NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedBy] [nvarchar](55) NULL,
	[UpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_RolePermission] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_auth_roles]    Script Date: 5/10/2021 7:04:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_auth_roles](
	[Id] [nvarchar](55) NOT NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[TenantId] [nvarchar](55) NULL,
	[IsActive] [bit] NULL,
	[CreatedBy] [nvarchar](55) NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedBy] [nvarchar](55) NULL,
	[UpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_auth_tenants]    Script Date: 5/10/2021 7:04:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_auth_tenants](
	[Id] [nvarchar](55) NOT NULL,
	[CIN] [nvarchar](55) NOT NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
	[ProfilePicture] [nvarchar](256) NULL,
	[Country] [nvarchar](256) NULL,
	[Address] [nvarchar](256) NULL,
	[TenantType] [int] NULL,
	[TenantStatus] [int] NULL,
	[Contact] [nvarchar](55) NULL,
	[Email] [nvarchar](55) NULL,
	[PhoneNumber] [nvarchar](20) NULL,
	[EmployeesCount] [int] NULL,
	[IsActive] [bit] NULL,
	[TenantId] [nvarchar](55) NULL,
	[CreatedBy] [nvarchar](55) NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedBy] [nvarchar](55) NULL,
	[UpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_Tenants] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_auth_tenants_onboard_status]    Script Date: 5/10/2021 7:04:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_auth_tenants_onboard_status](
	[Id] [nvarchar](55) NOT NULL,
	[Comment] [nvarchar](500) NOT NULL,
	[TenantId] [nvarchar](55) NOT NULL,
	[IsActive] [bit] NULL,
	[CreatedBy] [nvarchar](55) NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedBy] [nvarchar](55) NULL,
	[UpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_TenantsOnboardStatus] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_auth_userroles]    Script Date: 5/10/2021 7:04:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_auth_userroles](
	[UserId] [nvarchar](55) NOT NULL,
	[RoleId] [nvarchar](55) NOT NULL,
	[CreatedBy] [nvarchar](55) NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedBy] [nvarchar](55) NULL,
	[UpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_UserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_auth_users]    Script Date: 5/10/2021 7:04:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_auth_users](
	[Id] [nvarchar](55) NOT NULL,
	[UserName] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[Email] [nvarchar](256) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PhoneNumber] [nvarchar](20) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[FirstName] [nvarchar](256) NULL,
	[LastName] [nvarchar](256) NULL,
	[DateOfBirth] [datetimeoffset](7) NULL,
	[ProfilePicture] [nvarchar](256) NULL,
	[Country] [nvarchar](256) NULL,
	[Address] [nvarchar](256) NULL,
	[City] [nvarchar](256) NULL,
	[State] [nvarchar](256) NULL,
	[Street] [nvarchar](256) NULL,
	[Zip] [nvarchar](256) NULL,
	[IsActive] [bit] NULL,
	[Gender] [nvarchar](55) NULL,
	[TenantId] [nvarchar](55) NULL,
	[CreatedBy] [nvarchar](55) NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedBy] [nvarchar](55) NULL,
	[UpdatedDate] [datetime] NULL,
	[UserType] [int] NULL,
	[IsKYE] [bit] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_notify_email_template]    Script Date: 5/10/2021 7:04:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_notify_email_template](
	[Id] [nvarchar](55) NOT NULL,
	[Context] [nvarchar](55) NOT NULL,
	[SubContext] [nvarchar](55) NOT NULL,
	[MailSubject] [nvarchar](100) NOT NULL,
	[HtmlText] [nvarchar](max) NOT NULL,
	[ArbMailSubject] [nvarchar](100) NULL,
	[ArbHtmlText] [nvarchar](max) NULL,
	[HasPlaceholder] [bit] NULL,
	[IsActive] [bit] NULL,
	[CreatedBy] [nvarchar](55) NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedBy] [nvarchar](55) NULL,
	[UpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_NotifyEmailTemplate] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_notify_otp_tracker]    Script Date: 5/10/2021 7:04:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_notify_otp_tracker](
	[Id] [nvarchar](55) NOT NULL,
	[Context] [nvarchar](55) NOT NULL,
	[ContextId] [nvarchar](65) NOT NULL,
	[Otp] [nvarchar](10) NOT NULL,
	[OtpVerified] [bit] NULL,
	[Category] [int] NULL,
	[IsActive] [bit] NULL,
	[CreatedBy] [nvarchar](55) NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedBy] [nvarchar](55) NULL,
	[UpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_NotifyOtpTracker] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_notify_sms_template]    Script Date: 5/10/2021 7:04:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_notify_sms_template](
	[Id] [nvarchar](55) NOT NULL,
	[Context] [nvarchar](55) NOT NULL,
	[SubContext] [nvarchar](55) NOT NULL,
	[SmsContent] [nvarchar](500) NOT NULL,
	[ArabicContent] [nvarchar](500) NULL,
	[HasPlaceholder] [bit] NULL,
	[IsActive] [bit] NULL,
	[CreatedBy] [nvarchar](55) NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedBy] [nvarchar](55) NULL,
	[UpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_NotifySmsTemplate] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_notify_topic]    Script Date: 5/10/2021 7:04:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_notify_topic](
	[Id] [nvarchar](55) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Arn] [nvarchar](100) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [nvarchar](55) NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedBy] [nvarchar](55) NULL,
	[UpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_NotifyTopic] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_notify_user_subscription]    Script Date: 5/10/2021 7:04:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_notify_user_subscription](
	[Id] [nvarchar](55) NOT NULL,
	[TopicId] [nvarchar](55) NOT NULL,
	[UserEndpoint] [nvarchar](55) NOT NULL,
	[SubscriptionArn] [nvarchar](255) NOT NULL,
	[IsActive] [bit] NULL,
	[CreatedBy] [nvarchar](55) NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedBy] [nvarchar](55) NULL,
	[UpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_NotifyUserSubscription] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_payment_transactions]    Script Date: 5/10/2021 7:04:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_payment_transactions](
	[Id] [nvarchar](55) NOT NULL,
	[OrderId] [nvarchar](50) NULL,
	[CustomerId] [nvarchar](55) NULL,
	[CheckSum] [nvarchar](256) NULL,
	[MID] [nvarchar](20) NULL,
	[TransactionId] [nvarchar](64) NULL,
	[TransactionAmount] [nvarchar](10) NULL,
	[Currency] [nvarchar](3) NULL,
	[PaymentMode] [nvarchar](15) NULL,
	[TransactionDate] [datetime] NULL,
	[Status] [nvarchar](20) NULL,
	[ResponseCode] [nvarchar](10) NULL,
	[ResponseMessage] [nvarchar](500) NULL,
	[GatewayName] [nvarchar](15) NULL,
	[BankTransactionId] [nvarchar](256) NULL,
	[BankName] [nvarchar](500) NULL,
	[CreatedBy] [nvarchar](55) NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedBy] [nvarchar](55) NULL,
	[UpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_PaymentTransactions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_tenant_invitations]    Script Date: 5/10/2021 7:04:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_tenant_invitations](
	[Id] [nvarchar](55) NOT NULL,
	[Email] [nvarchar](55) NOT NULL,
	[Registered] [bit] NOT NULL,
	[TenantId] [nvarchar](55) NULL,
	[CreatedBy] [nvarchar](55) NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedBy] [nvarchar](55) NULL,
	[UpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_Tenant_Invitations] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[tbl_auth_tenants] ([Id], [CIN], [Name], [NormalizedName], [ProfilePicture], [Country], [Address], [TenantType], [TenantStatus], [Contact], [Email], [PhoneNumber], [EmployeesCount], [IsActive], [TenantId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (N'00000000-0000-0000-0000-000000000001', N'CIN00001', N'Consent', N'CONSENT', N'', N'London', N'London', 2, 2, N'+12354654654', N'admin@consent.com', N'+12354654654', 0, 1, N'', N'00000000-0000-0000-0000-000000000002', CAST(N'2021-02-23T11:44:26.817' AS DateTime), N'00000000-0000-0000-0000-000000000002', CAST(N'2021-02-23T11:44:26.817' AS DateTime))
INSERT [dbo].[tbl_auth_tenants] ([Id], [CIN], [Name], [NormalizedName], [ProfilePicture], [Country], [Address], [TenantType], [TenantStatus], [Contact], [Email], [PhoneNumber], [EmployeesCount], [IsActive], [TenantId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (N'b3352a07-6011-459f-9ed5-e3428eb4f0e6', N'CINTESTUSER', N'Lovaraju', NULL, NULL, NULL, N'Visakhapatnam', 0, 0, N'+919948879997', N'lovaraju.sappidi@gmail.com', N'+919948879997', 10, 1, N'00000000-0000-0000-0000-000000000001', N'00000000-0000-0000-0000-000000000002', CAST(N'2021-02-23T11:45:21.650' AS DateTime), N'00000000-0000-0000-0000-000000000002', CAST(N'2021-02-23T11:45:21.650' AS DateTime))
GO
INSERT [dbo].[tbl_auth_users] ([Id], [UserName], [NormalizedUserName], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [Email], [NormalizedEmail], [EmailConfirmed], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FirstName], [LastName], [DateOfBirth], [ProfilePicture], [Country], [Address], [City], [State], [Street], [Zip], [IsActive], [Gender], [TenantId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [UserType], [IsKYE]) VALUES (N'00000000-0000-0000-0000-000000000002', N'admin@consent.com', N'ADMIN@CONSENT.COM', NULL, N'U7LH7YTYTSANCN3UQ73P7RUTGP6VFDOB', N'400e3f9c-165a-4f06-b7c1-a0d57beebb16', N'admin@consent.com', N'ADMIN@CONSENT.COM', 1, N'+12354654654', 1, 0, NULL, 1, 0, N'Consent', N'', NULL, N'', N'London', N'London', N'London', N'London', N'', N'', 1, N'', N'00000000-0000-0000-0000-000000000001', N'00000000-0000-0000-0000-000000000002', CAST(N'2021-02-23T11:44:27.767' AS DateTime), N'00000000-0000-0000-0000-000000000002', CAST(N'2021-02-23T11:44:27.767' AS DateTime), 0, 0)
INSERT [dbo].[tbl_auth_users] ([Id], [UserName], [NormalizedUserName], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [Email], [NormalizedEmail], [EmailConfirmed], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FirstName], [LastName], [DateOfBirth], [ProfilePicture], [Country], [Address], [City], [State], [Street], [Zip], [IsActive], [Gender], [TenantId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [UserType], [IsKYE]) VALUES (N'06398e7d-a6f6-4269-a2a1-0b6b2e2d26a3', N'lsappidi@sweyainfotech.com', N'LSAPPIDI@SWEYAINFOTECH.COM', NULL, N'JSHUYCJ4LQV3M2NVWGADKJF5UP3WVSKK', N'10418bf4-f666-4ecd-a9e2-6b218ec4667f', N'lsappidi@sweyainfotech.com', N'LSAPPIDI@SWEYAINFOTECH.COM', 0, N'+919703379997', 0, 0, NULL, 1, 0, N'string', N'string', CAST(N'2021-03-04T06:08:08.8740000+00:00' AS DateTimeOffset), N'string', N'string', N'string', N'string', N'string', N'string', N'string', 0, N'string', N'00000000-0000-0000-0000-000000000001', N'00000000-0000-0000-0000-000000000002', CAST(N'2021-03-04T06:08:25.067' AS DateTime), N'00000000-0000-0000-0000-000000000002', CAST(N'2021-03-05T10:20:54.413' AS DateTime), 1, 1)
INSERT [dbo].[tbl_auth_users] ([Id], [UserName], [NormalizedUserName], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [Email], [NormalizedEmail], [EmailConfirmed], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FirstName], [LastName], [DateOfBirth], [ProfilePicture], [Country], [Address], [City], [State], [Street], [Zip], [IsActive], [Gender], [TenantId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [UserType], [IsKYE]) VALUES (N'b3352a07-6011-459f-9ed5-e3428eb4f0e6', N'lovaraju.sappidi@gmail.com', N'LOVARAJU.SAPPIDI@GMAIL.COM', NULL, N'BXDBOAEYJDJTYYMMRNWBF2TIL733Q3UK', N'4d6424d6-d809-4fdf-a264-1696b069eebe', N'lovaraju.sappidi@gmail.com', N'LOVARAJU.SAPPIDI@GMAIL.COM', 0, N'+919948879997', 1, 0, NULL, 1, 0, NULL, NULL, NULL, NULL, NULL, N'Visakhapatnam', NULL, NULL, NULL, NULL, 1, NULL, N'00000000-0000-0000-0000-000000000001', N'00000000-0000-0000-0000-000000000002', CAST(N'2021-02-23T11:45:21.650' AS DateTime), N'00000000-0000-0000-0000-000000000002', CAST(N'2021-02-23T11:46:28.290' AS DateTime), 0, 0)
GO
INSERT [dbo].[tbl_notify_email_template] ([Id], [Context], [SubContext], [MailSubject], [HtmlText], [ArbMailSubject], [ArbHtmlText], [HasPlaceholder], [IsActive], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (N'29a42343-2d7f-46e9-a33d-ecf96e03889e', N'TenantOnboard', N'Rejected', N'We need more Information!', N'<div style=''text-align: center;''><img src=''https://consent-assets.s3.amazonaws.com/logo_white_background.jpg'' alt=''Consent'' style=''width: 30%;'' /></div><hr /><div style=''font-family: Verdana;''><h1 style=''text-align: center;''>We need more Information!</h1><br /><p>Hello {{FirstName}},</p><p>We regret to inform that your verification request has been rejected. Pease find the reason for rejection below. Our team is always here to provide support in completing the verification process.</p><p><strong>Reason for rejection:</strong><br />&quot;<span style=''color: red;''>{{RejectionReason}}</span>&quot;</p><p><strong>Next Steps:</strong><br />A member of our support team should be in touch to see if we can help clarify and get you quickly on the Consent platform.</p><p><strong>Alternate:</strong><br />Please rectify the above by resubmitting the KYC details as per the above comment and we will pick this up immediate to get you onboard.</p><br /><p>Cheers, <br /><strong>Infoeaze Team</strong></p></div>', N'', N'', 1, 1, N'00000000-0000-0000-0000-000000000001', CAST(N'2021-03-02T05:40:44.153' AS DateTime), N'00000000-0000-0000-0000-000000000001', CAST(N'2021-03-02T05:40:44.153' AS DateTime))
INSERT [dbo].[tbl_notify_email_template] ([Id], [Context], [SubContext], [MailSubject], [HtmlText], [ArbMailSubject], [ArbHtmlText], [HasPlaceholder], [IsActive], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (N'724fbf5f-d4ea-4931-9d02-f07213276a76', N'TenantOnboard', N'Approved', N'Congrats you are Approved!', N'<div style=''text-align: center;''><img src=''https://consent-assets.s3.amazonaws.com/logo_white_background.jpg'' alt=''Consent'' style=''width: 30%;'' /></div><hr /><div style=''font-family: Verdana;''><h1 style=''text-align: center;''>Congrats you are Approved!</h1><br /><p>Hi {{FirstName}},</p><p>Good news! Your Consent account is verified! You can now onboard your employees, issue credentials and carry out request verification services on verifiable credentials instantly.</p><p>To get started, just login to the Consent Platform and follow the welcome screen.</p><p>We�re thrilled to welcome you on our Consent Platform!</p><br /><p>Cheers, <br /><strong>Infoeaze Team</strong></p></div>', N'', N'', 1, 1, N'00000000-0000-0000-0000-000000000001', CAST(N'2021-03-02T05:40:44.160' AS DateTime), N'00000000-0000-0000-0000-000000000001', CAST(N'2021-03-02T05:40:44.160' AS DateTime))
INSERT [dbo].[tbl_notify_email_template] ([Id], [Context], [SubContext], [MailSubject], [HtmlText], [ArbMailSubject], [ArbHtmlText], [HasPlaceholder], [IsActive], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (N'8d91529e-7a5a-4a1a-af2b-5e5c8cf88a07', N'Registration', N'ResetPassword', N'Reset Password Request', N'<div style=''text-align: center;''><img src=''https://consent-assets.s3.amazonaws.com/logo_white_background.jpg'' alt=''Consent'' style=''width: 30%;'' /></div><hr /><div style=''font-family: Verdana;''><p>Hi {{UserName}}</p><p>Looks like you asked to reset your password. Just hit the link below to get started.</p><p style=''text-align: center;''><a style=''padding: 10px;background-color: orange;color: white;'' href=''''>Reset My Password</a></p><p>Didn''t request a change? You can ignore this email and your password will stay the same.</p><br /><p>Cheers, <br /><strong>Infoeaze Team</strong></p></div>', N'', N'', 1, 1, N'00000000-0000-0000-0000-000000000001', CAST(N'2021-03-02T05:40:43.577' AS DateTime), N'00000000-0000-0000-0000-000000000001', CAST(N'2021-03-02T05:40:43.580' AS DateTime))
INSERT [dbo].[tbl_notify_email_template] ([Id], [Context], [SubContext], [MailSubject], [HtmlText], [ArbMailSubject], [ArbHtmlText], [HasPlaceholder], [IsActive], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (N'b7e90026-a617-450b-b0c4-2d7e991d7d0c', N'Registration', N'EmailVerification', N'Consent Email Verification', N'<div style=''text-align: center;''><img src=''https://consent-assets.s3.amazonaws.com/logo_white_background.jpg'' alt=''Consent'' style=''width: 30%;'' /></div><hr /><div style=''font-family: Verdana;''><p><strong>{{CompanyName}}</strong> has invited you to join the <strong>Consent Platform!</strong></p><p style=''text-align: center;''><a style=''padding: 10px;background-color: orange;color: white;'' href=''{{Link}}''>Verify Email</a></p><p><strong>Consent is a digital mobile wallet to store user credentials </strong></p><p>Consent is leading the digital credential verification Industry by helping individuals verify their background quickly in a transparent manner. You as an Consent user own your Credential data and have an opportunity to receive tokens in return for sharing it via our Consent platform � depending on who you wish to share, when and how long.</p><p>We�re thrilled to welcome you on our Consent Platform!</p><br /><p>Cheers, <br /><strong>Infoeaze Team</strong></p></div>', N'', N'', 1, 1, N'00000000-0000-0000-0000-000000000001', CAST(N'2021-03-02T05:40:44.107' AS DateTime), N'00000000-0000-0000-0000-000000000001', CAST(N'2021-03-02T05:40:44.107' AS DateTime))
INSERT [dbo].[tbl_notify_email_template] ([Id], [Context], [SubContext], [MailSubject], [HtmlText], [ArbMailSubject], [ArbHtmlText], [HasPlaceholder], [IsActive], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (N'ba0c5dd7-877b-4d65-a19b-b910176357a3', N'TenantOnboard', N'KYCProcessing', N'Keeping you informed at every step!', N'<div style=''text-align: center;''><img src=''https://consent-assets.s3.amazonaws.com/logo_white_background.jpg'' alt=''Consent'' style=''width: 30%;'' /></div><hr /><div style=''font-family: Verdana;''><h1 style=''text-align: center;''>Congrats you are Registered!</h1><br /><p>Welcome {{FirstName}},</p><p>We�re thrilled to welcome you on our Consent Platform! Your registration is now complete. To fully benefit, please complete your KYC to access all the features within the Consent Platform. This includes � onboarding all your existing employees, issuing credentials, request background checks and most importantly collect reward tokens every time your issued credential is verified.</p><p>To get started, just login to the Consent Platform and follow the welcome screen.</p><br /><p>Cheers, <br /><strong>Infoeaze Team</strong></p></div>', N'', N'', 1, 1, N'00000000-0000-0000-0000-000000000001', CAST(N'2021-03-02T05:40:44.150' AS DateTime), N'00000000-0000-0000-0000-000000000001', CAST(N'2021-03-02T05:40:44.150' AS DateTime))
INSERT [dbo].[tbl_notify_email_template] ([Id], [Context], [SubContext], [MailSubject], [HtmlText], [ArbMailSubject], [ArbHtmlText], [HasPlaceholder], [IsActive], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (N'cb4144b3-4e50-4b50-ae07-a8d08aa41ed2', N'Invitation', N'AcceptInvitation', N'Invitation', N'<div style=''text-align: center;''><img src=''https://consent-assets.s3.amazonaws.com/logo_white_background.jpg'' alt=''Consent'' style=''width: 30%;'' /></div><hr /><div style=''font-family: Verdana;''><p><strong>{{CompanyName}}</strong> has invited you to join the <strong>Consent Platform!</strong></p><p>Please download the mobile app from the apple or play store and register using this email to accept the Invitation.</p><p style=''text-align: center;''><a href=''https://play.google.com/''><img style=''width:150px;'' src=''https://lh3.googleusercontent.com/cjsqrWQKJQp9RFO7-hJ9AfpKzbUb_Y84vXfjlP0iRHBvladwAfXih984olktDhPnFqyZ0nu9A5jvFwOEQPXzv7hr3ce3QVsLN8kQ2Ao=s0'' /></a></p><p style=''text-align: center;''><a href=''https://www.apple.com/''><img style=''width: 150px;height: 50px;'' src=''https://developer.apple.com/app-store/marketing/guidelines/images/badge-example-preferred_2x.png'' /></a></p><p><strong>Consent is a digital mobile wallet to store user credentials </strong></p><p>Consent is leading the digital credential verification Industry by helping individuals verify their background quickly in a transparent manner. You as a Consent user own your Credential data and have an opportunity to receive tokens in return for sharing it via our Consent platform � depending on who you wish to share, when and how long.</p><p>We are thrilled to welcome you on our Consent Platform!</p><br /><p>Cheers, <br /><strong>Infoeaze Team</strong></p></div>', N'', N'', 1, 1, N'00000000-0000-0000-0000-000000000001', CAST(N'2021-03-02T05:40:44.127' AS DateTime), N'00000000-0000-0000-0000-000000000001', CAST(N'2021-03-02T05:40:44.127' AS DateTime))
INSERT [dbo].[tbl_notify_email_template] ([Id], [Context], [SubContext], [MailSubject], [HtmlText], [ArbMailSubject], [ArbHtmlText], [HasPlaceholder], [IsActive], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (N'd7e69450-9dda-43c6-96cc-d8f3b54e1b08', N'TenantOnboard', N'Registered', N'Congrats you are Registered!', N'<div style=''text-align: center;''><img src=''https://consent-assets.s3.amazonaws.com/logo_white_background.jpg'' alt=''Consent'' style=''width: 30%;'' /></div><hr /><div style=''font-family: Verdana;''><h1 style=''text-align: center;''>Congrats you are Registered!</h1><br /><p>Welcome {{FirstName}},</p><p>We�re thrilled to welcome you on our Consent Platform! Your registration is now complete. To fully benefit, please complete your KYC to access all the features within the Consent Platform. This includes � onboarding all your existing employees, issuing credentials, request background checks and most importantly collect reward tokens every time your issued credential is verified.</p><p>To get started, just login to the Consent Platform and follow the welcome screen.</p><br /><p>Cheers, <br /><strong>Infoeaze Team</strong></p></div>', N'', N'', 1, 1, N'00000000-0000-0000-0000-000000000001', CAST(N'2021-03-02T05:40:44.130' AS DateTime), N'00000000-0000-0000-0000-000000000001', CAST(N'2021-03-02T05:40:44.130' AS DateTime))
GO
INSERT [dbo].[tbl_notify_otp_tracker] ([Id], [Context], [ContextId], [Otp], [OtpVerified], [Category], [IsActive], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (N'9ded1bc1-768c-466a-aec9-9d4acf64ac40', N'Registration', N'b3352a07-6011-459f-9ed5-e3428eb4f0e6', N'8409', 1, 0, 1, N'b3352a07-6011-459f-9ed5-e3428eb4f0e6', CAST(N'2021-02-23T17:16:03.207' AS DateTime), N'b3352a07-6011-459f-9ed5-e3428eb4f0e6', CAST(N'2021-02-23T17:16:03.207' AS DateTime))
INSERT [dbo].[tbl_notify_otp_tracker] ([Id], [Context], [ContextId], [Otp], [OtpVerified], [Category], [IsActive], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (N'dc510427-0f83-422c-b307-b6354ba6242d', N'Registration', N'6d760960-943a-4b53-87b6-9fd2d1871f2a', N'7168', 1, 0, 1, N'6d760960-943a-4b53-87b6-9fd2d1871f2a', CAST(N'2021-02-22T11:52:30.813' AS DateTime), N'6d760960-943a-4b53-87b6-9fd2d1871f2a', CAST(N'2021-02-22T11:52:30.813' AS DateTime))
GO
INSERT [dbo].[tbl_notify_sms_template] ([Id], [Context], [SubContext], [SmsContent], [ArabicContent], [HasPlaceholder], [IsActive], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (N'a5793080-3237-41bb-b380-efa1a3860d78', N'Registration', N'Send OTP', N'{{Otp}} is the OTP for mobile number verification from Consent registration on {{Date}}.  Pls do not share it with anyone.', NULL, 1, 1, N'00000000-0000-0000-0000-000000000001', CAST(N'2021-02-22T06:21:36.253' AS DateTime), N'00000000-0000-0000-0000-000000000001', CAST(N'2021-02-22T06:21:36.253' AS DateTime))
INSERT [dbo].[tbl_notify_sms_template] ([Id], [Context], [SubContext], [SmsContent], [ArabicContent], [HasPlaceholder], [IsActive], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (N'feb6b47c-19ff-427f-bb1c-0b3c91bf9fec', N'Login', N'Send OTP', N'{{Otp}} is the OTP for mobile number verification from Consent login on {{Date}}.  Pls do not share it with anyone.', NULL, 1, 1, N'00000000-0000-0000-0000-000000000001', CAST(N'2021-02-22T06:21:35.923' AS DateTime), N'00000000-0000-0000-0000-000000000001', CAST(N'2021-02-22T06:21:35.923' AS DateTime))
GO
INSERT [dbo].[tbl_payment_transactions] ([Id], [OrderId], [CustomerId], [CheckSum], [MID], [TransactionId], [TransactionAmount], [Currency], [PaymentMode], [TransactionDate], [Status], [ResponseCode], [ResponseMessage], [GatewayName], [BankTransactionId], [BankName], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (N'422497f7-1e71-453e-8798-5bd0b2b98835', N'ruytutyuty', N'tuityuiyuiuy', N'GrrHcCQZkHjENDtWvcm39G3ShJDVosT2NUFBE0MagOYTC59LzVxuoYzb8UpW37Pna+scOxva5Pqg2APq4Ban+kj6S2kDuB1bC/QCDtm5Fpk=', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'00000000-0000-0000-0000-000000000002', CAST(N'2021-04-04T06:18:32.767' AS DateTime), N'00000000-0000-0000-0000-000000000002', CAST(N'2021-04-04T06:18:32.770' AS DateTime))
INSERT [dbo].[tbl_payment_transactions] ([Id], [OrderId], [CustomerId], [CheckSum], [MID], [TransactionId], [TransactionAmount], [Currency], [PaymentMode], [TransactionDate], [Status], [ResponseCode], [ResponseMessage], [GatewayName], [BankTransactionId], [BankName], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (N'5e23813c-4f74-49ea-996d-f46076398b06', N'ruytutyuty54564', N'tuityuiyuiuy456', N'g/cxJFDkH/cURdzOub3SOVcmMjsQT+uhSJnu/lMROzMX81pQCx9V2ZRkf8Ntb3E+mHy57oluDGNKou5a3A85EsXzvE0lmFgd3NnEQ9c5MqA=', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'00000000-0000-0000-0000-000000000002', CAST(N'2021-04-04T06:19:16.867' AS DateTime), N'00000000-0000-0000-0000-000000000002', CAST(N'2021-04-04T06:19:16.867' AS DateTime))
INSERT [dbo].[tbl_payment_transactions] ([Id], [OrderId], [CustomerId], [CheckSum], [MID], [TransactionId], [TransactionAmount], [Currency], [PaymentMode], [TransactionDate], [Status], [ResponseCode], [ResponseMessage], [GatewayName], [BankTransactionId], [BankName], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (N'6f4eeae4-2c2a-42fc-9f5a-565ea92ff38c', N'12312234534', N'567567565', N'z1IiuetlPhkrkgrVNTIqL1zhuFCEfAkhynNrS0TJ/4PZeDyX+U0l+lFWSeyU0eSfhm1UVh1+4HvfA92iVQ2TjHTQc2XhKPE6dgJ135WCwJs=', N'eZpxQY72390293941462', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', NULL, N'00000000-0000-0000-0000-000000000002', CAST(N'2021-04-03T10:48:19.697' AS DateTime), N'00000000-0000-0000-0000-000000000002', CAST(N'2021-04-03T10:48:19.697' AS DateTime))
INSERT [dbo].[tbl_payment_transactions] ([Id], [OrderId], [CustomerId], [CheckSum], [MID], [TransactionId], [TransactionAmount], [Currency], [PaymentMode], [TransactionDate], [Status], [ResponseCode], [ResponseMessage], [GatewayName], [BankTransactionId], [BankName], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (N'7f548317-c017-44c3-8c5d-196b8004a4aa', N'745689679786', N'789789', NULL, N'eZpxQY72390293941462', NULL, N'7879.00', N'INR', NULL, NULL, N'TXN_FAILURE', N'330', N'Invalid+checksum', NULL, N'', NULL, N'00000000-0000-0000-0000-000000000002', CAST(N'2021-04-04T06:22:12.063' AS DateTime), N'00000000-0000-0000-0000-000000000002', CAST(N'2021-04-04T06:22:12.063' AS DateTime))
INSERT [dbo].[tbl_payment_transactions] ([Id], [OrderId], [CustomerId], [CheckSum], [MID], [TransactionId], [TransactionAmount], [Currency], [PaymentMode], [TransactionDate], [Status], [ResponseCode], [ResponseMessage], [GatewayName], [BankTransactionId], [BankName], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (N'a467803a-7055-49d4-9971-eb247af29639', N'645756756867876', N'786786', NULL, N'eZpxQY72390293941462', NULL, N'6786786.00', N'INR', NULL, NULL, N'TXN_FAILURE', N'330', N'Invalid+checksum', NULL, N'', NULL, N'00000000-0000-0000-0000-000000000002', CAST(N'2021-04-04T06:20:44.590' AS DateTime), N'00000000-0000-0000-0000-000000000002', CAST(N'2021-04-04T06:20:44.590' AS DateTime))
INSERT [dbo].[tbl_payment_transactions] ([Id], [OrderId], [CustomerId], [CheckSum], [MID], [TransactionId], [TransactionAmount], [Currency], [PaymentMode], [TransactionDate], [Status], [ResponseCode], [ResponseMessage], [GatewayName], [BankTransactionId], [BankName], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (N'ff7be5f6-ac0f-437b-9e2d-c0c137a43778', N'ghdfyhrtyryrt4564', N'rtyrtyrt456', N'gxDKJ1YlF+uf4WpaJ/A0fzsUmQsNNGFEaVwPQjevpbTaddGYV42rbBTxL3kaJ0yPjmfUzwONw3U2bA5016XV/HumsKK3FZ9LT60RnjozVxA=', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'00000000-0000-0000-0000-000000000002', CAST(N'2021-04-04T06:20:12.720' AS DateTime), N'00000000-0000-0000-0000-000000000002', CAST(N'2021-04-04T06:20:12.720' AS DateTime))
GO
INSERT [dbo].[tbl_tenant_invitations] ([Id], [Email], [Registered], [TenantId], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate]) VALUES (N'68d365f3-92d3-4484-aa55-34f15059a95a', N'lsappidi@sweyainfotech.com', 1, N'00000000-0000-0000-0000-000000000001', N'00000000-0000-0000-0000-000000000002', CAST(N'2021-03-04T06:07:58.277' AS DateTime), N'00000000-0000-0000-0000-000000000002', CAST(N'2021-03-04T06:08:29.237' AS DateTime))
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__tbl_auth__C1F8DC560C7CB789]    Script Date: 5/10/2021 7:04:54 PM ******/
ALTER TABLE [dbo].[tbl_auth_tenants] ADD UNIQUE NONCLUSTERED 
(
	[CIN] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[tbl_auth_users] ADD  DEFAULT ((0)) FOR [UserType]
GO
ALTER TABLE [dbo].[tbl_auth_users] ADD  CONSTRAINT [DF_tbl_auth_users_IsKYE]  DEFAULT ((0)) FOR [IsKYE]
GO
ALTER TABLE [dbo].[tbl_notify_email_template] ADD  DEFAULT ((0)) FOR [HasPlaceholder]
GO
ALTER TABLE [dbo].[tbl_notify_otp_tracker] ADD  DEFAULT ((0)) FOR [OtpVerified]
GO
ALTER TABLE [dbo].[tbl_notify_sms_template] ADD  DEFAULT ((0)) FOR [HasPlaceholder]
GO
ALTER TABLE [dbo].[tbl_tenant_invitations] ADD  DEFAULT ((0)) FOR [Registered]
GO
ALTER TABLE [dbo].[tbl_auth_rolepermission]  WITH CHECK ADD  CONSTRAINT [FK_RolePermission_PermissionId] FOREIGN KEY([PermissionId])
REFERENCES [dbo].[tbl_auth_permissions] ([Id])
GO
ALTER TABLE [dbo].[tbl_auth_rolepermission] CHECK CONSTRAINT [FK_RolePermission_PermissionId]
GO
ALTER TABLE [dbo].[tbl_auth_tenants_onboard_status]  WITH CHECK ADD  CONSTRAINT [FK_TenantsOnboardStatus_TenantId] FOREIGN KEY([TenantId])
REFERENCES [dbo].[tbl_auth_tenants] ([Id])
GO
ALTER TABLE [dbo].[tbl_auth_tenants_onboard_status] CHECK CONSTRAINT [FK_TenantsOnboardStatus_TenantId]
GO
ALTER TABLE [dbo].[tbl_notify_user_subscription]  WITH CHECK ADD  CONSTRAINT [FK_NotifyUserSubscription_TopicId] FOREIGN KEY([TopicId])
REFERENCES [dbo].[tbl_notify_topic] ([Id])
GO
ALTER TABLE [dbo].[tbl_notify_user_subscription] CHECK CONSTRAINT [FK_NotifyUserSubscription_TopicId]
GO
USE [master]
GO
ALTER DATABASE [consent_api] SET  READ_WRITE 
GO
