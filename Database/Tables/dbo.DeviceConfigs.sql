CREATE TABLE [dbo].[DeviceConfigs] (
    [DeviceConfigID] INT            IDENTITY (1, 1) NOT NULL,
    [DeviceID]       INT            NOT NULL,
    [ResetAt]        DATETIME       NULL,
    [ProtectPower]   DECIMAL (8, 2) NULL,
    [ProtectMode]    INT            NULL,
    [MIRatio]        INT            NULL,
    [AlarmThreshold] INT            NULL,
    [StopThreshold]  INT            NULL,
    [FirstRMMode]    INT            NULL,
    [SecondRMMode]   INT            NULL,
    [UpdateTime]     DATETIME       NULL,
    [Status]         INT            NOT NULL,
    PRIMARY KEY CLUSTERED ([DeviceConfigID] ASC),
    CONSTRAINT [FK_DeviceConfigs_Devices] FOREIGN KEY ([DeviceID]) REFERENCES [dbo].[Devices] ([DeviceID])
);

