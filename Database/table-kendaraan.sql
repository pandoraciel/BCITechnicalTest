USE [Belajar]
GO

/****** Object:  Table [dbo].[kendaraan]    Script Date: 31/10/2021 08:11:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[kendaraan](
	[kendaraan_id] [int] IDENTITY(1,1) NOT NULL,
	[kendaraan_nama] [varchar](50) NULL,
	[kendaraan_model] [varchar](50) NULL,
	[kendaraan_merek] [varchar](50) NULL,
	[kendaraan_transmisi] [varchar](50) NULL,
	[kendaraan_tahun] [varchar](4) NULL,
	[kendaraan_harga] [int] NULL,
	[created_dt] [datetime] NULL,
	[updated_dt] [datetime] NULL,
 CONSTRAINT [PK_kendaraan] PRIMARY KEY CLUSTERED 
(
	[kendaraan_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


