USE [Belajar]
GO
/****** Object:  StoredProcedure [dbo].[AddMultipleKendaraan]    Script Date: 31/10/2021 08:13:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create PROCEDURE [dbo].[AddMultipleKendaraan]
	@Kendaraans AddKendaraans READONLY,
	@Result INT OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO kendaraan(
		kendaraan_nama, 
		kendaraan_model, 
		kendaraan_merek,
		kendaraan_transmisi,
		kendaraan_tahun,
		kendaraan_harga,
		created_dt
	)
	SELECT 
		nama,
		model,
		merek,
		transmisi,
		tahun,
		harga,
		GETDATE()
	FROM @Kendaraans 

	SELECT @Result = COUNT(*) FROM @Kendaraans
	SELECT @Result
END
