alter table [dbo].[Products]
	add [ImageData] varbinary (max) null,
	[ImageMimeType] varchar(50) null;