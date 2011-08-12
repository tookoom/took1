CREATE TABLE [Staging.MdoFiles] ( 
	[FileName] varchar(100) NOT NULL,
	[FileType] varchar(50) NOT NULL,
	[LoadTimestamp] datetime NOT NULL,
	[ProcessTimestamp] datetime,
	[Data] varchar(1000)
)
;

ALTER TABLE [Staging.MdoFiles] ADD CONSTRAINT [PK_Staging.MdoFiles] 
	PRIMARY KEY CLUSTERED ([FileName])
;






