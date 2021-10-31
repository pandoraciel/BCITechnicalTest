USE [Belajar]
GO

/****** Object:  UserDefinedTableType [dbo].[AddKendaraans]    Script Date: 31/10/2021 08:14:18 ******/
CREATE TYPE [dbo].[AddKendaraans] AS TABLE(
	[nama] [varchar](50) NULL,
	[model] [varchar](50) NULL,
	[merek] [varchar](50) NULL,
	[transmisi] [varchar](50) NULL,
	[tahun] [varchar](4) NULL,
	[harga] [int] NULL
)
GO


