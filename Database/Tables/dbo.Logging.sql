CREATE TABLE [dbo].[Logging]
(
	[ID] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Level] INT NOT NULL, 
    [UserID] INT NULL, 
    [Attributs] XML NOT NULL, 
    [CreateTime] DATETIME NOT NULL
)
