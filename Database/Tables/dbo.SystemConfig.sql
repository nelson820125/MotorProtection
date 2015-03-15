CREATE TABLE [dbo].[SystemConfig] (
    [Name]   NVARCHAR (50)  NOT NULL,
    [Value] NVARCHAR (MAX) NULL, 
    CONSTRAINT [PK_SystemConfig] PRIMARY KEY ([Name])
);

