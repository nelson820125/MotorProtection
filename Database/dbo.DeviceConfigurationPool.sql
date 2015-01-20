CREATE TABLE [dbo].[DeviceConfigurationPool] (
    [ID]          INT            IDENTITY (1, 1) NOT NULL,
    [Address]     INT            NOT NULL,
    [FunCode]     INT            NOT NULL,
    [Commands]    NVARCHAR (MAX) NOT NULL,
    [Description] NVARCHAR (MAX) NULL,
    [UserID]      INT            NOT NULL,
    [CreateTime]  DATETIME       NOT NULL,
    [Attempt]     INT            NOT NULL,
    [Status] INT NULL, 
    PRIMARY KEY CLUSTERED ([ID] ASC)
);

