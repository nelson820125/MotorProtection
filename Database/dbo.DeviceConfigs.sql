CREATE TABLE [dbo].[DeviceConfigs]
(
	[DeviceID] INT NOT NULL PRIMARY KEY, 
    [ResetAt] DATETIME NULL, 
    [ProtectPower] DECIMAL(8, 2) NULL, 
    [ProtectMode] INT NULL, 
    [MIRatio] INT NULL, 
    [AlarmThreshold] DECIMAL(3, 2) NULL, 
    [StopThreshold] DECIMAL(3, 2) NULL, 
    [FirstRMMode] INT NULL, 
    [SecondRMMode] INT NULL, 
    [UpdateTime] DATETIME NULL, 
    CONSTRAINT [FK_DeviceConfigs_Devices] FOREIGN KEY ([DeviceID]) REFERENCES [Devices]([DeviceID])
)
