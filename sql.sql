/*
 Navicat Premium Data Transfer

 Source Server         : sqlserver
 Source Server Type    : SQL Server
 Source Server Version : 13004001
 Source Host           : (localdb)\MSSQLLocalDB:1433
 Source Catalog        : JobHuntingPlatform
 Source Schema         : dbo

 Target Server Type    : SQL Server
 Target Server Version : 13004001
 File Encoding         : 65001

 Date: 02/06/2020 14:58:46
*/


-- ----------------------------
-- Table structure for admin
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[admin]') AND type IN ('U'))
	DROP TABLE [dbo].[admin]
GO

CREATE TABLE [dbo].[admin] (
  [id] int  IDENTITY(1,1) NOT NULL,
  [account] varchar(255) COLLATE Chinese_PRC_CI_AS  NULL,
  [password] varchar(255) COLLATE Chinese_PRC_CI_AS  NULL,
  [name] varchar(255) COLLATE Chinese_PRC_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[admin] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Records of [admin]
-- ----------------------------
BEGIN TRANSACTION
GO

SET IDENTITY_INSERT [dbo].[admin] ON
GO

INSERT INTO [dbo].[admin] ([id], [account], [password], [name]) VALUES (N'1', N'admin', N'123456', N'管理员')
GO

SET IDENTITY_INSERT [dbo].[admin] OFF
GO

COMMIT
GO


-- ----------------------------
-- Table structure for article
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[article]') AND type IN ('U'))
	DROP TABLE [dbo].[article]
GO

CREATE TABLE [dbo].[article] (
  [id] int  IDENTITY(1,1) NOT NULL,
  [adminId] int  NULL,
  [time] datetime  NULL,
  [articlePath] varchar(255) COLLATE Chinese_PRC_CI_AS  NULL,
  [title] varchar(255) COLLATE Chinese_PRC_CI_AS  NULL,
  [isTop] int  NULL
)
GO

ALTER TABLE [dbo].[article] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Records of [article]
-- ----------------------------
BEGIN TRANSACTION
GO

SET IDENTITY_INSERT [dbo].[article] ON
GO

INSERT INTO [dbo].[article] ([id], [adminId], [time], [articlePath], [title], [isTop]) VALUES (N'1', N'1', N'2020-04-27 20:17:15.000', N'/Source/resumes/2020-04-27-20-17-14.pdf', N'简历模版', N'0'), (N'2', N'1', N'2020-04-27 20:17:34.000', N'/Source/resumes/2020-04-27-20-17-33.pdf', N'测试1', N'1'), (N'3', N'1', N'2020-04-27 20:21:14.000', N'/Source/resumes/2020-04-27-20-21-13.pdf', N'讲座', N'0')
GO

SET IDENTITY_INSERT [dbo].[article] OFF
GO

COMMIT
GO


-- ----------------------------
-- Table structure for company
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[company]') AND type IN ('U'))
	DROP TABLE [dbo].[company]
GO

CREATE TABLE [dbo].[company] (
  [id] int  IDENTITY(1,1) NOT NULL,
  [account] varchar(255) COLLATE Chinese_PRC_CI_AS  NULL,
  [password] varchar(255) COLLATE Chinese_PRC_CI_AS  NULL,
  [name] varchar(255) COLLATE Chinese_PRC_CI_AS  NULL,
  [type] varchar(255) COLLATE Chinese_PRC_CI_AS  NULL,
  [address] varchar(255) COLLATE Chinese_PRC_CI_AS  NULL,
  [phone] varchar(255) COLLATE Chinese_PRC_CI_AS  NULL,
  [isPass] int  NULL
)
GO

ALTER TABLE [dbo].[company] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Records of [company]
-- ----------------------------
BEGIN TRANSACTION
GO

SET IDENTITY_INSERT [dbo].[company] ON
GO

INSERT INTO [dbo].[company] ([id], [account], [password], [name], [type], [address], [phone], [isPass]) VALUES (N'1', N'company', N'123456', N'阿里妈妈', N'互联网公司996', N'杭州', N'886', N'1'), (N'2', N'123456', N'123456', N'test', N'321', N'321', N'321', N'1')
GO

SET IDENTITY_INSERT [dbo].[company] OFF
GO

COMMIT
GO


-- ----------------------------
-- Table structure for notice
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[notice]') AND type IN ('U'))
	DROP TABLE [dbo].[notice]
GO

CREATE TABLE [dbo].[notice] (
  [id] int  IDENTITY(1,1) NOT NULL,
  [sourceId] int  NULL,
  [targetId] int  NULL,
  [time] datetime  NULL,
  [type] varchar(255) COLLATE Chinese_PRC_CI_AS  NULL,
  [content] varchar(255) COLLATE Chinese_PRC_CI_AS  NULL,
  [isReply] int  NULL
)
GO

ALTER TABLE [dbo].[notice] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Records of [notice]
-- ----------------------------
BEGIN TRANSACTION
GO

SET IDENTITY_INSERT [dbo].[notice] ON
GO

INSERT INTO [dbo].[notice] ([id], [sourceId], [targetId], [time], [type], [content], [isReply]) VALUES (N'3', N'1', N'1', N'2020-04-24 00:35:37.000', N'简历投递', N'小虾米先生-投递贵司-运维工程师岗位，详情内容请查看简历邮箱', N'1'), (N'4', N'1', N'1', N'2020-04-24 00:59:29.000', N'面试邀请', N'阿里妈妈向您发出面试邀请，请及时回复', N'1'), (N'5', N'1', N'1', N'2020-04-24 01:02:10.000', N'求职者回复', N'小虾米答应了您的面试邀请，请及时安排好面试时间和邮件通知', N'1'), (N'6', N'1002', N'1', N'2020-04-24 15:59:44.000', N'简历投递', N'测试先生-投递贵司-运维工程师岗位，详情内容请查看简历邮箱', N'1'), (N'7', N'1', N'1002', N'2020-04-24 16:00:02.000', N'面试邀请', N'阿里妈妈向您发出面试邀请，请及时回复', N'1'), (N'8', N'1002', N'1', N'2020-04-24 16:23:18.000', N'求职者回复', N'测试答应了您的面试邀请，请及时安排好面试时间和邮件通知', N'1')
GO

SET IDENTITY_INSERT [dbo].[notice] OFF
GO

COMMIT
GO


-- ----------------------------
-- Table structure for record
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[record]') AND type IN ('U'))
	DROP TABLE [dbo].[record]
GO

CREATE TABLE [dbo].[record] (
  [id] int  IDENTITY(1,1) NOT NULL,
  [seekerId] int  NULL,
  [recruitmentId] int  NULL,
  [time] datetime  NULL
)
GO

ALTER TABLE [dbo].[record] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Records of [record]
-- ----------------------------
BEGIN TRANSACTION
GO

SET IDENTITY_INSERT [dbo].[record] ON
GO

INSERT INTO [dbo].[record] ([id], [seekerId], [recruitmentId], [time]) VALUES (N'4', N'1', N'4', N'2020-04-24 00:35:37.000'), (N'5', N'1002', N'4', N'2020-04-24 15:59:44.000')
GO

SET IDENTITY_INSERT [dbo].[record] OFF
GO

COMMIT
GO


-- ----------------------------
-- Table structure for recruitment
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[recruitment]') AND type IN ('U'))
	DROP TABLE [dbo].[recruitment]
GO

CREATE TABLE [dbo].[recruitment] (
  [id] int  IDENTITY(1,1) NOT NULL,
  [companyId] int  NULL,
  [offer] varchar(255) COLLATE Chinese_PRC_CI_AS  NULL,
  [require] varchar(255) COLLATE Chinese_PRC_CI_AS  NULL,
  [number] int  NULL,
  [time] datetime  NULL
)
GO

ALTER TABLE [dbo].[recruitment] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Records of [recruitment]
-- ----------------------------
BEGIN TRANSACTION
GO

SET IDENTITY_INSERT [dbo].[recruitment] ON
GO

INSERT INTO [dbo].[recruitment] ([id], [companyId], [offer], [require], [number], [time]) VALUES (N'3', N'1', N'java工程师', N'会删库，会跑路', N'6', N'2020-04-20 02:09:38.000'), (N'4', N'1', N'运维工程师', N'从入门到跑路', N'3', N'2020-04-20 02:10:48.000'), (N'5', N'1', N'收银员', N'会算账', N'5', N'2020-04-24 15:58:18.000')
GO

SET IDENTITY_INSERT [dbo].[recruitment] OFF
GO

COMMIT
GO


-- ----------------------------
-- Table structure for seeker
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[seeker]') AND type IN ('U'))
	DROP TABLE [dbo].[seeker]
GO

CREATE TABLE [dbo].[seeker] (
  [id] int  IDENTITY(1,1) NOT NULL,
  [account] varchar(255) COLLATE Chinese_PRC_CI_AS  NULL,
  [password] varchar(255) COLLATE Chinese_PRC_CI_AS  NULL,
  [name] varchar(255) COLLATE Chinese_PRC_CI_AS  NULL,
  [sex] varchar(255) COLLATE Chinese_PRC_CI_AS  NULL,
  [age] int  NULL,
  [address] varchar(255) COLLATE Chinese_PRC_CI_AS  NULL,
  [offer] varchar(255) COLLATE Chinese_PRC_CI_AS  NULL,
  [resumePath] varchar(255) COLLATE Chinese_PRC_CI_AS  NULL,
  [isRelease] int  NULL,
  [phone] varchar(255) COLLATE Chinese_PRC_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[seeker] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Records of [seeker]
-- ----------------------------
BEGIN TRANSACTION
GO

SET IDENTITY_INSERT [dbo].[seeker] ON
GO

INSERT INTO [dbo].[seeker] ([id], [account], [password], [name], [sex], [age], [address], [offer], [resumePath], [isRelease], [phone]) VALUES (N'1', N'user', N'123456', N'小虾米', N'男', N'20', N'广州，深圳', N'Java开发', N'/Source/resumes/2020-04-24-15-56-38.pdf', N'1', N'110'), (N'1002', N'test', N'123456', N'测试', N'男', N'18', N'123', N'321', N'/Source/resumes/2020-04-24-15-57-24.pdf', N'1', N'888')
GO

SET IDENTITY_INSERT [dbo].[seeker] OFF
GO

COMMIT
GO


-- ----------------------------
-- Primary Key structure for table admin
-- ----------------------------
ALTER TABLE [dbo].[admin] ADD CONSTRAINT [PK__tmp_ms_x__3213E83F0158F6BB] PRIMARY KEY CLUSTERED ([id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table article
-- ----------------------------
ALTER TABLE [dbo].[article] ADD CONSTRAINT [PK__tmp_ms_x__3213E83F26D16D0A] PRIMARY KEY CLUSTERED ([id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table company
-- ----------------------------
ALTER TABLE [dbo].[company] ADD CONSTRAINT [PK__tmp_ms_x__3213E83F39FBE904] PRIMARY KEY CLUSTERED ([id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table notice
-- ----------------------------
ALTER TABLE [dbo].[notice] ADD CONSTRAINT [PK__tmp_ms_x__3213E83FB476A86B] PRIMARY KEY CLUSTERED ([id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table record
-- ----------------------------
ALTER TABLE [dbo].[record] ADD CONSTRAINT [PK__tmp_ms_x__3213E83FC1D889B3] PRIMARY KEY CLUSTERED ([id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table recruitment
-- ----------------------------
ALTER TABLE [dbo].[recruitment] ADD CONSTRAINT [PK__tmp_ms_x__3213E83FDF621102] PRIMARY KEY CLUSTERED ([id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table seeker
-- ----------------------------
ALTER TABLE [dbo].[seeker] ADD CONSTRAINT [PK__tmp_ms_x__3213E83F57E4C226] PRIMARY KEY CLUSTERED ([id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO

