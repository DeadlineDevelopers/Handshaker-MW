
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 02/21/2016 23:07:30
-- Generated from EDMX file: C:\Users\Yunus Emre\Documents\Handshaker-MW\Data\Handshaker\Handshaker\DatabaseModel\HandshakerModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [HANDSHAKER];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[UserSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserSet];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'UserSet'
CREATE TABLE [dbo].[UserSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FirstName] nvarchar(max)  NOT NULL,
    [LastName] nvarchar(max)  NOT NULL,
    [Username] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'ContactSet'
CREATE TABLE [dbo].[ContactSet] (
    [UsersThatIamInContact_Id] int  NOT NULL,
    [MyContacts_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'UserSet'
ALTER TABLE [dbo].[UserSet]
ADD CONSTRAINT [PK_UserSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [UsersThatIamInContact_Id], [MyContacts_Id] in table 'ContactSet'
ALTER TABLE [dbo].[ContactSet]
ADD CONSTRAINT [PK_ContactSet]
    PRIMARY KEY NONCLUSTERED ([UsersThatIamInContact_Id], [MyContacts_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [UsersThatIamInContact_Id] in table 'ContactSet'
ALTER TABLE [dbo].[ContactSet]
ADD CONSTRAINT [FK_ContactSet_User]
    FOREIGN KEY ([UsersThatIamInContact_Id])
    REFERENCES [dbo].[UserSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [MyContacts_Id] in table 'ContactSet'
ALTER TABLE [dbo].[ContactSet]
ADD CONSTRAINT [FK_ContactSet_User1]
    FOREIGN KEY ([MyContacts_Id])
    REFERENCES [dbo].[UserSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ContactSet_User1'
CREATE INDEX [IX_FK_ContactSet_User1]
ON [dbo].[ContactSet]
    ([MyContacts_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------