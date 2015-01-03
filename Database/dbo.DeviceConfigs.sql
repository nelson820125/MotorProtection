CREATE TABLE [dbo].[DeviceConfigs] (
    [DeviceID]       INT            NOT NULL,
    [ResetAt]        DATETIME       NULL,
    [ProtectPower]   DECIMAL (8, 2) NULL,
    [ProtectMode]    INT            NULL,
    [MIRatio]        INT            NULL,
    [AlarmThreshold] DECIMAL (3, 2) NULL,
    [StopThreshold]  DECIMAL (3, 2) NULL,
    [FirstRMMode]    INT            NULL,
    [SecondRMMode]   INT            NULL,
    [UpdateTime]     DATETIME       NOT NULL,
    PRIMARY KEY CLUSTERED ([DeviceID] ASC),
    CONSTRAINT [FK_DeviceConfigs_Devices] FOREIGN KEY ([DeviceID]) REFERENCES [dbo].[Devices] ([DeviceID])
);

