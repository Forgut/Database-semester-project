
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 12/09/2018 23:38:35
-- Generated from EDMX file: C:\Users\nalkit\Documents\BD_P\Database semester project\Models\FactoryModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Factory];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK__Orders__Customer__4D94879B]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Orders] DROP CONSTRAINT [FK__Orders__Customer__4D94879B];
GO
IF OBJECT_ID(N'[dbo].[FK__Products___Ingre__571DF1D5]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Products_Ingredients] DROP CONSTRAINT [FK__Products___Ingre__571DF1D5];
GO
IF OBJECT_ID(N'[dbo].[FK__Orders__ProductI__4CA06362]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Orders] DROP CONSTRAINT [FK__Orders__ProductI__4CA06362];
GO
IF OBJECT_ID(N'[dbo].[FK__Products___Produ__5629CD9C]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Products_Ingredients] DROP CONSTRAINT [FK__Products___Produ__5629CD9C];
GO
IF OBJECT_ID(N'[dbo].[FK__Tasks__ProductID__5AEE82B9]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Tasks] DROP CONSTRAINT [FK__Tasks__ProductID__5AEE82B9];
GO
IF OBJECT_ID(N'[dbo].[FK_Employyes_Tasks_Employees]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Employyes_Tasks] DROP CONSTRAINT [FK_Employyes_Tasks_Employees];
GO
IF OBJECT_ID(N'[dbo].[FK_Employyes_Tasks_Tasks]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Employyes_Tasks] DROP CONSTRAINT [FK_Employyes_Tasks_Tasks];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Customers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Customers];
GO
IF OBJECT_ID(N'[dbo].[Employees]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Employees];
GO
IF OBJECT_ID(N'[dbo].[Ingredients]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Ingredients];
GO
IF OBJECT_ID(N'[dbo].[Orders]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Orders];
GO
IF OBJECT_ID(N'[dbo].[Products]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Products];
GO
IF OBJECT_ID(N'[dbo].[Products_Ingredients]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Products_Ingredients];
GO
IF OBJECT_ID(N'[dbo].[Tasks]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Tasks];
GO
IF OBJECT_ID(N'[dbo].[Employyes_Tasks]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Employyes_Tasks];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Customers'
CREATE TABLE [dbo].[Customers] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] varchar(50)  NOT NULL,
    [Address] varchar(50)  NULL,
    [Regular_customer] bit  NOT NULL
);
GO

-- Creating table 'Employees'
CREATE TABLE [dbo].[Employees] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [First_name] varchar(50)  NOT NULL,
    [Last_name] varchar(50)  NOT NULL,
    [PESEL] varchar(11)  NOT NULL
);
GO

-- Creating table 'Ingredients'
CREATE TABLE [dbo].[Ingredients] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] varchar(50)  NOT NULL,
    [Price] float  NOT NULL,
    [Stored_amount] int  NOT NULL
);
GO

-- Creating table 'Orders'
CREATE TABLE [dbo].[Orders] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ProductID] int  NOT NULL,
    [CustomerID] int  NOT NULL,
    [Price] float  NOT NULL,
    [Delivery_price] float  NOT NULL,
    [Product_quantity] int  NOT NULL,
    [Completed] bit  NOT NULL
);
GO

-- Creating table 'Products'
CREATE TABLE [dbo].[Products] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] varchar(50)  NOT NULL,
    [Production_price] int  NOT NULL,
    [Sell_price] int  NOT NULL,
    [Stored_amount] int  NOT NULL
);
GO

-- Creating table 'Products_Ingredients'
CREATE TABLE [dbo].[Products_Ingredients] (
    [ProductID] int  NOT NULL,
    [IngredientID] int  NOT NULL,
    [Required_ingredient_amount] int  NOT NULL
);
GO

-- Creating table 'Tasks'
CREATE TABLE [dbo].[Tasks] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ProductID] int  NOT NULL,
    [Predicted_work_time] int  NOT NULL,
    [Actual_work_time] int  NULL,
    [Finished] bit  NOT NULL,
    [Produced_quantity] int  NOT NULL
);
GO

-- Creating table 'Employyes_Tasks'
CREATE TABLE [dbo].[Employyes_Tasks] (
    [Employees_Id] int  NOT NULL,
    [Tasks_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Customers'
ALTER TABLE [dbo].[Customers]
ADD CONSTRAINT [PK_Customers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Employees'
ALTER TABLE [dbo].[Employees]
ADD CONSTRAINT [PK_Employees]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Ingredients'
ALTER TABLE [dbo].[Ingredients]
ADD CONSTRAINT [PK_Ingredients]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Orders'
ALTER TABLE [dbo].[Orders]
ADD CONSTRAINT [PK_Orders]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Products'
ALTER TABLE [dbo].[Products]
ADD CONSTRAINT [PK_Products]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [ProductID], [IngredientID] in table 'Products_Ingredients'
ALTER TABLE [dbo].[Products_Ingredients]
ADD CONSTRAINT [PK_Products_Ingredients]
    PRIMARY KEY CLUSTERED ([ProductID], [IngredientID] ASC);
GO

-- Creating primary key on [Id] in table 'Tasks'
ALTER TABLE [dbo].[Tasks]
ADD CONSTRAINT [PK_Tasks]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Employees_Id], [Tasks_Id] in table 'Employyes_Tasks'
ALTER TABLE [dbo].[Employyes_Tasks]
ADD CONSTRAINT [PK_Employyes_Tasks]
    PRIMARY KEY CLUSTERED ([Employees_Id], [Tasks_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [CustomerID] in table 'Orders'
ALTER TABLE [dbo].[Orders]
ADD CONSTRAINT [FK__Orders__Customer__4D94879B]
    FOREIGN KEY ([CustomerID])
    REFERENCES [dbo].[Customers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Orders__Customer__4D94879B'
CREATE INDEX [IX_FK__Orders__Customer__4D94879B]
ON [dbo].[Orders]
    ([CustomerID]);
GO

-- Creating foreign key on [IngredientID] in table 'Products_Ingredients'
ALTER TABLE [dbo].[Products_Ingredients]
ADD CONSTRAINT [FK__Products___Ingre__571DF1D5]
    FOREIGN KEY ([IngredientID])
    REFERENCES [dbo].[Ingredients]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Products___Ingre__571DF1D5'
CREATE INDEX [IX_FK__Products___Ingre__571DF1D5]
ON [dbo].[Products_Ingredients]
    ([IngredientID]);
GO

-- Creating foreign key on [ProductID] in table 'Orders'
ALTER TABLE [dbo].[Orders]
ADD CONSTRAINT [FK__Orders__ProductI__4CA06362]
    FOREIGN KEY ([ProductID])
    REFERENCES [dbo].[Products]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Orders__ProductI__4CA06362'
CREATE INDEX [IX_FK__Orders__ProductI__4CA06362]
ON [dbo].[Orders]
    ([ProductID]);
GO

-- Creating foreign key on [ProductID] in table 'Products_Ingredients'
ALTER TABLE [dbo].[Products_Ingredients]
ADD CONSTRAINT [FK__Products___Produ__5629CD9C]
    FOREIGN KEY ([ProductID])
    REFERENCES [dbo].[Products]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [ProductID] in table 'Tasks'
ALTER TABLE [dbo].[Tasks]
ADD CONSTRAINT [FK__Tasks__ProductID__5AEE82B9]
    FOREIGN KEY ([ProductID])
    REFERENCES [dbo].[Products]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Tasks__ProductID__5AEE82B9'
CREATE INDEX [IX_FK__Tasks__ProductID__5AEE82B9]
ON [dbo].[Tasks]
    ([ProductID]);
GO

-- Creating foreign key on [Employees_Id] in table 'Employyes_Tasks'
ALTER TABLE [dbo].[Employyes_Tasks]
ADD CONSTRAINT [FK_Employyes_Tasks_Employees]
    FOREIGN KEY ([Employees_Id])
    REFERENCES [dbo].[Employees]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Tasks_Id] in table 'Employyes_Tasks'
ALTER TABLE [dbo].[Employyes_Tasks]
ADD CONSTRAINT [FK_Employyes_Tasks_Tasks]
    FOREIGN KEY ([Tasks_Id])
    REFERENCES [dbo].[Tasks]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Employyes_Tasks_Tasks'
CREATE INDEX [IX_FK_Employyes_Tasks_Tasks]
ON [dbo].[Employyes_Tasks]
    ([Tasks_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------