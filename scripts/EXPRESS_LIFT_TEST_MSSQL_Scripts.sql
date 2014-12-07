/****** Object:  Table [dbo].[user]    Script Date: 12/06/2014 23:26:18 ******/
USE [EXPRESSLIFE]
GO

/****** Object:  Table [dbo].[user]    Script Date: 12/06/2014 23:26:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[user]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[user](
	[user_id] [int] IDENTITY(1000,1) NOT NULL,
	[user_name] [nvarchar](50) NOT NULL,
	[user_nick_name] [nvarchar](50) NULL,
	[user_email] [nvarchar](200) NOT NULL,
	[user_password] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK_user] PRIMARY KEY CLUSTERED 
(
	[user_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO

/****** Object:  Table [dbo].[topic]    Script Date: 12/06/2014 23:26:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[topic]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[topic](
	[topic_id] [int] IDENTITY(1000,1) NOT NULL,
	[topic_name] [nvarchar](20) NOT NULL,
	[topic_desc] [nvarchar](500) NOT NULL,
 CONSTRAINT [PK_topic] PRIMARY KEY CLUSTERED 
(
	[topic_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO

/****** Object:  Table [dbo].[post]    Script Date: 12/06/2014 23:31:10 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[post]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[post](
	[post_id] [int] IDENTITY(1000,1) NOT NULL,
	[post_topic_id] [int] NOT NULL,
	[post_author_id] [int] NOT NULL,
	[post_content] [ntext] NOT NULL,
	[post_creation_datetime] [datetime] NOT NULL,
 CONSTRAINT [PK_post] PRIMARY KEY CLUSTERED 
(
	[post_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO

IF  NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_post_topic]') AND parent_object_id = OBJECT_ID(N'[dbo].[post]'))
BEGIN
ALTER TABLE [dbo].[post]  WITH CHECK ADD  CONSTRAINT [FK_post_topic] FOREIGN KEY([post_topic_id])
REFERENCES [dbo].[topic] ([topic_id])
END
GO

ALTER TABLE [dbo].[post] CHECK CONSTRAINT [FK_post_topic]
GO

IF  NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_post_user]') AND parent_object_id = OBJECT_ID(N'[dbo].[post]'))
BEGIN
ALTER TABLE [dbo].[post]  WITH CHECK ADD  CONSTRAINT [FK_post_user] FOREIGN KEY([post_author_id])
REFERENCES [dbo].[user] ([user_id])
END
GO

ALTER TABLE [dbo].[post] CHECK CONSTRAINT [FK_post_user]
GO

/****** Object:  Table [dbo].[comment]    Script Date: 12/06/2014 23:39:09 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[comment]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[comment](
	[comment_id] [int] IDENTITY(1000,1) NOT NULL,
	[comment_post_id] [int] NOT NULL,
	[comment_author_id] [int] NOT NULL,
	[comment_content] [ntext] NOT NULL,
	[comment_creation_datetime] [datetime] NOT NULL,
 CONSTRAINT [PK_comment] PRIMARY KEY CLUSTERED 
(
	[comment_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO

IF  NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_comment_post]') AND parent_object_id = OBJECT_ID(N'[dbo].[comment]'))
BEGIN
ALTER TABLE [dbo].[comment]  WITH CHECK ADD  CONSTRAINT [FK_comment_post] FOREIGN KEY([comment_post_id])
REFERENCES [dbo].[post] ([post_id])
END
GO

ALTER TABLE [dbo].[comment] CHECK CONSTRAINT [FK_comment_post]
GO

IF  NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_comment_user]') AND parent_object_id = OBJECT_ID(N'[dbo].[comment]'))
BEGIN
ALTER TABLE [dbo].[comment]  WITH CHECK ADD  CONSTRAINT [FK_comment_user] FOREIGN KEY([comment_author_id])
REFERENCES [dbo].[user] ([user_id])
END
GO

ALTER TABLE [dbo].[comment] CHECK CONSTRAINT [FK_comment_user]
GO