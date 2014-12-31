CREATE TABLE [dbo].[Users] (
    [UserID]                 INT            NOT NULL IDENTITY,
    [UserName]               NVARCHAR (50)  NOT NULL,
    [Password]               NVARCHAR (300) NULL,
    [LastLoginAt]            DATETIME       NULL,
    [IsLocked]               BIT            NOT NULL,
    [LockedAt]               DATETIME       NULL,
    [FailedPasswordAttempts] VARCHAR (300)  NULL,
    [Enabled]                BIT            NOT NULL,
    [CreateTime]             DATETIME       NULL,
    PRIMARY KEY CLUSTERED ([UserID] ASC)
);

