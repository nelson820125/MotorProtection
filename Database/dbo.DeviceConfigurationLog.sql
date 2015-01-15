CREATE TABLE [dbo].[DeviceConfigurationLog]
(
	[ID] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Address] INT NOT NULL, 
    [FunCode] INT NOT NULL, 
    [Commands] NVARCHAR(MAX) NOT NULL, 
    [Description] NVARCHAR(MAX) NULL, 
    [UserID] INT NOT NULL, 
    [CreateTime] DATETIME NOT NULL
)