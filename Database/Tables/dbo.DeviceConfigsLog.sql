CREATE TABLE [dbo].[DeviceConfigsLog] (
    [ID]             INT            NOT NULL,
    [ProtectPower]   DECIMAL (8, 2) NULL,
    [ProtectMode]    INT            NULL,
    [MIRatio]        INT            NULL,
    [AlarmThreshold] INT            NULL,
    [StopThreshold]  INT            NULL,
    [FirstRMMode]    INT            NULL,
    [SecondRMMode]   INT            NULL,
    [UpdateTime]     DATETIME       NULL,
    [Status]         INT            NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);

