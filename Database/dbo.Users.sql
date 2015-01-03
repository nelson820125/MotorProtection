CREATE TABLE [dbo].[Users] (
    [UserID]                 INT            IDENTITY (1, 1) NOT NULL,
    [UserName]               NVARCHAR (50)  NOT NULL,
    [Password]               NVARCHAR (300) NULL,
    [LastLoginAt]            DATETIME       NULL,
    [IsLocked]               BIT            NOT NULL,
    [LockedAt]               DATETIME       NULL,
    [FailedPasswordAttempts] VARCHAR (300)  NULL,
    [Enabled]                BIT            NOT NULL,
    [CreateTime]             DATETIME       NOT NULL,
    PRIMARY KEY CLUSTERED ([UserID] ASC)
);

