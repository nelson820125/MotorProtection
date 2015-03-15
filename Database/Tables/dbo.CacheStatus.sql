CREATE TABLE [dbo].[CacheStatus]
(
	[Application] NVARCHAR(300) NOT NULL , 
    [Group] NVARCHAR(50) NOT NULL, 
    [Timestamp] DATETIME NOT NULL, 
    PRIMARY KEY ([Application], [Group])
)
