
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 05/07/2013 00:41:00
-- Generated from EDMX file: F:\Projetos\TK1\Code\TK1.Bizz.Client\Data\ClientBizzModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [Broker]');
GO

IF SCHEMA_ID(N'Broker') IS NULL EXECUTE(N'CREATE SCHEMA [Broker]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[Broker].[PropertyAd]', 'U') IS NOT NULL
    DROP TABLE [Broker].[PropertyAd];
GO
IF OBJECT_ID(N'[Broker].[PropertyAdDetail]', 'U') IS NOT NULL
    DROP TABLE [Broker].[PropertyAdDetail];
GO
IF OBJECT_ID(N'[Broker].[PropertyAdPic]', 'U') IS NOT NULL
    DROP TABLE [Broker].[PropertyAdPic];
GO
IF OBJECT_ID(N'[Broker].[PropertyReleaseAd]', 'U') IS NOT NULL
    DROP TABLE [Broker].[PropertyReleaseAd];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'PropertyAd'
CREATE TABLE [Broker].[PropertyAd] (
    [CustomerCode] varchar(50)  NOT NULL,
    [PropertyAdType] varchar(20)  NOT NULL,
    [PropertyAdCode] int  NOT NULL,
    [PropertyAdStatus] varchar(50)  NOT NULL,
    [CategoryName] varchar(255)  NOT NULL,
    [PropertyTypeName] varchar(255)  NOT NULL,
    [CityName] varchar(255)  NOT NULL,
    [DistrictName] varchar(255)  NOT NULL,
    [TotalArea] float  NOT NULL,
    [TotalRooms] int  NOT NULL,
    [InternalArea] float  NOT NULL,
    [ExternalArea] float  NOT NULL,
    [Value] float  NOT NULL,
    [CityTaxes] float  NULL,
    [CondoTaxes] float  NULL,
    [Title] varchar(8000)  NULL,
    [ShortDescription] varchar(8000)  NULL,
    [Featured] bit  NOT NULL default(0),
    [Visible] bit  NOT NULL default(0),
    [FullDescription] varchar(8000)  NULL,
    [AreaDescription] varchar(8000)  NULL,
    [CondoDescription] varchar(8000)  NULL,
    [PicUrl] varchar(8000)  NULL
);
GO

-- Creating table 'PropertyAdDetail'
CREATE TABLE [Broker].[PropertyAdDetail] (
    [PropertyAdDetailID] int IDENTITY(1,1) NOT NULL,
    [CustomerCode] varchar(50)  NOT NULL,
    [PropertyAdType] varchar(20)  NOT NULL,
    [PropertyAdCode] int  NOT NULL,
    [Description] varchar(255)  NULL,
    [Value] varchar(255)  NULL,
    [Type] varchar(255)  NOT NULL
);
GO

-- Creating table 'PropertyAdPic'
CREATE TABLE [Broker].[PropertyAdPic] (
    [PropertyAdPicID] int IDENTITY(1,1) NOT NULL,
    [CustomerCode] varchar(50)  NOT NULL,
    [PropertyAdType] varchar(20)  NOT NULL,
    [PropertyAdCode] int  NOT NULL,
    [PicIndex] int  NOT NULL DEFAULT (0),
    [FileName] varchar(255)  NULL,
    [Description] varchar(255)  NULL,
    [PictureUrl] varchar(255)  NULL,
    [ThumbnailUrl] varchar(255)  NULL,
    [PictureFilePath] varchar(255)  NULL,
    [ThumbnailFilePath] varchar(255)  NULL
);
GO

-- Creating table 'PropertyReleaseAd'
CREATE TABLE [Broker].[PropertyReleaseAd] (
    [PropertyAdCode] int  NOT NULL,
    [PropertyAdType] varchar(20)  NOT NULL,
    [CustomerCode] varchar(50)  NOT NULL,
    [Name] varchar(255)  NULL,
    [ConstructorName] varchar(50)  NULL,
    [Address] varchar(64)  NOT NULL,
    [AddressComplement] varchar(64)  NOT NULL,
    [AddressNumber] int  NOT NULL,
    [MinTotalArea] float  NOT NULL,
    [MaxTotalArea] float  NOT NULL,
    [MinTotalRooms] int  NOT NULL,
    [MaxTotalRooms] int  NOT NULL,
    [MinSuites] int  NOT NULL,
    [MaxSuites] int  NOT NULL,
    [MinInternalArea] float  NOT NULL,
    [MaxInternalArea] float  NOT NULL,
    [MinExternalArea] float  NOT NULL,
    [MaxExternalArea] float  NOT NULL,
    [MinValue] float  NOT NULL,
    [MaxValue] float  NOT NULL,
    [TotalElevators] int  NOT NULL,
    [TotalFloorUnits] int  NOT NULL,
    [TotalUnits] int  NOT NULL,
    [TotalTowers] int  NOT NULL,
    [TotalTowerFloors] int  NOT NULL,
    [MinParkingLots] int  NOT NULL,
    [MaxParkingLots] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [CustomerCode], [PropertyAdType], [PropertyAdCode] in table 'PropertyAd'
ALTER TABLE [Broker].[PropertyAd]
ADD CONSTRAINT [PK_PropertyAd]
    PRIMARY KEY CLUSTERED ([CustomerCode], [PropertyAdType], [PropertyAdCode]  ASC);
GO

-- Creating primary key on [PropertyAdDetailID] in table 'PropertyAdDetail'
ALTER TABLE [Broker].[PropertyAdDetail]
ADD CONSTRAINT [PK_PropertyAdDetail]
    PRIMARY KEY CLUSTERED ([PropertyAdDetailID] ASC);
GO

-- Creating primary key on [PropertyAdPicID] in table 'PropertyAdPic'
ALTER TABLE [Broker].[PropertyAdPic]
ADD CONSTRAINT [PK_PropertyAdPic]
    PRIMARY KEY CLUSTERED ([PropertyAdPicID] ASC);
GO

-- Creating primary key on [CustomerCode], [PropertyAdType], [PropertyAdCode] in table 'PropertyReleaseAd'
ALTER TABLE [Broker].[PropertyReleaseAd]
ADD CONSTRAINT [PK_PropertyReleaseAd]
    PRIMARY KEY CLUSTERED ([CustomerCode], [PropertyAdType], [PropertyAdCode] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [CustomerCode], [PropertyAdType], [PropertyAdCode] in table 'PropertyAdDetail'
ALTER TABLE [Broker].[PropertyAdDetail]
ADD CONSTRAINT [FK_PropertyAdPropertyAdDetail]
    FOREIGN KEY ([CustomerCode], [PropertyAdType], [PropertyAdCode])
    REFERENCES [Broker].[PropertyAd]
        ([CustomerCode], [PropertyAdType], [PropertyAdCode])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PropertyAdPropertyAdDetail'
CREATE INDEX [IX_FK_PropertyAdPropertyAdDetail]
ON [Broker].[PropertyAdDetail]
    ([CustomerCode], [PropertyAdType], [PropertyAdCode]);
GO

-- Creating foreign key on [CustomerCode], [PropertyAdType], [PropertyAdCode] in table 'PropertyAdPic'
ALTER TABLE [Broker].[PropertyAdPic]
ADD CONSTRAINT [FK_PropertyAdPropertyAdPic]
    FOREIGN KEY ([CustomerCode], [PropertyAdType], [PropertyAdCode])
    REFERENCES [Broker].[PropertyAd]
        ([CustomerCode], [PropertyAdType], [PropertyAdCode])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PropertyAdPropertyAdPic'
CREATE INDEX [IX_FK_PropertyAdPropertyAdPic]
ON [Broker].[PropertyAdPic]
    ([CustomerCode], [PropertyAdType], [PropertyAdCode]);
GO

-- Creating foreign key on [CustomerCode], [PropertyAdType], [PropertyAdCode] in table 'PropertyAd'
ALTER TABLE [Broker].[PropertyAd]
ADD CONSTRAINT [FK_PropertyReleaseAdPropertyAd]
    FOREIGN KEY ([CustomerCode], [PropertyAdType], [PropertyAdCode])
    REFERENCES [Broker].[PropertyReleaseAd]
        ([CustomerCode], [PropertyAdType], [PropertyAdCode])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------


USE [TK1]
GO

/****** Object:  Table [Broker].[PropertyAdIDGenerator]    Script Date: 05/07/2013 00:50:35 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [Broker].[PropertyAdCodeGenerator](
    [CustomerCode] varchar(50)  NOT NULL,
    [PropertyAdType] varchar(20)  NOT NULL,
    [PropertyAdCode] int  NOT NULL DEFAULT(1),
 CONSTRAINT [PK_PropertyAdCodeGenerator] PRIMARY KEY CLUSTERED 
(
	[CustomerCode] ASC,
	[PropertyAdType] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO
